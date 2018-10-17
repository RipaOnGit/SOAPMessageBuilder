using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using XMLGenerator;

namespace BusinessLogic
{
    public class LogicModule
    {
        /// <summary>
        /// This application is based on this WSDL documentation https://msdn.microsoft.com/en-us/library/ms996486.aspx
        /// And target functionality is to build SOAP request and response messages, based on WSDL that SOAP service publishes
        /// So that those messages can be used to call service action as raw web request style.
        /// Application also contsructs basic http headers required for that raw request flow.
        /// 
        /// How to generate XML Schema the XML instances dynamically https://msdn.microsoft.com/en-us/library/aa302296.aspx
        /// 
        /// Test with this service the inline schema wsdl handling.
        /// http://www.dneonline.com/calculator.asmx?WSDL
        /// </summary>
        /// 

        /// <summary>
        /// In memory store for fetched WSDL.
        /// </summary>
        private static XmlDocument WSDLContent = new XmlDocument();

        private static XmlDocument rootSchemaContent = new XmlDocument();

        /// <summary>
        /// Info about binding that affect the style how concrete SOAP message will be formatted.
        /// </summary>
        private static BindingData BindingInfo = new BindingData();

        /// <summary>
        /// Info about Port type message references and operation input and output parameter names.
        /// </summary>
        private static PortTypeData PortTypeInfo = new PortTypeData();

        /// <summary>
        /// XML generator to create SOAP message body XML instance.
        /// </summary>
        private static GeneratorCreator SOAPBodyXMLGenerator = null;


        /// <summary>
        /// External version of method to fetch XML from URI
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="needLogin"></param>
        /// <returns></returns>
        public string GetXMLfromURI(string uri, bool needLogin = false)
        {
            return getXMLfromURI(WSDLContent, uri, needLogin).OuterXml;
        }

        /// <summary>
        /// Generic document fetcher method that is used to fetch wsdl and message type schemas from URI.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private XmlDocument getXMLfromURI(XmlDocument document, string uri, bool needLogin = false)
        {
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(uri);

            if (needLogin)
            {
                // If required by the server, set the credentials.  
                request.Credentials = CredentialCache.DefaultCredentials;
            }

            // Get the response.  
            using (WebResponse response = request.GetResponse())
            {
                // Get the stream containing content returned by the server.  
                using (Stream wsdlInStream = response.GetResponseStream())
                {
                    // Get a WSDL file describing a service.
                    StreamReader reader = new StreamReader(wsdlInStream, Encoding.UTF8);
                    string documentXML = reader.ReadToEnd();
                    if (document.OuterXml.Length > 0)
                    {
                        document.OuterXml.Remove(0);
                    }

                    //XML document loadin into memory.
                    document.LoadXml(documentXML);

                    return document;
                }
            }
        }


        public string ProduceSOAPMessage(string bindingName, string operationName)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(WSDLContent.NameTable);

            //Collected namespaces in WSDL document to use in element search.
            Dictionary<string, string> prefixesAndNamespaces = new Dictionary<string, string>();

            prefixesAndNamespaces.Add("wsdl", "http://schemas.xmlsoap.org/wsdl/");
            //SOAP 1.1 is always referenced in wsdl.
            prefixesAndNamespaces.Add("soap", "http://schemas.xmlsoap.org/wsdl/soap/");
            //Check if binding uses SOAP 1.2 vesion instead of 1.1 this affect the namespace setup for WSDL element search.
            if (bindingName.EndsWith("12"))
            {
                prefixesAndNamespaces.Add("soap12", "http://schemas.xmlsoap.org/wsdl/soap12/");
            }
            //To find schema related elements.
            prefixesAndNamespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");

            //Add namespaces into search related namespace manager.
            foreach (var pair in prefixesAndNamespaces)
            {
                nsmgr.AddNamespace(pair.Key, pair.Value);
            }

            //Collect binding data.
            checkBinding(bindingName, operationName, nsmgr);

            //Collect Operation specific port type data.
            getTypeOfPortAndOperationMessageReferences(BindingInfo.PortTypeName, operationName, nsmgr);

            //Find wsdl message types root schema from import URI.(non single schema scenarios)
            var schema = getXMLfromURI(rootSchemaContent, PortTypeInfo.MessageSchemaURI);

            //Target namespace URI defined in root schema for the generator (generator do not work without it)
            string targetNamespace = "";

            //Remove element definitions from root schema, that are not related to single operation specific message types.
            //This is due to limitations of XMLGenerator implementation that is based on Microsoft example code.
            //Referenced Microsoft implementation can only take first element definition from schema xml and generate xml sample out of it.
            //TODO: handle multiple input message part types!
            var trimmedSchema = trimOutSchemaXML(schema, operationName, out targetNamespace);

            //XML generator initialization 
            SOAPBodyXMLGenerator = new GeneratorCreator(trimmedSchema, new XmlQualifiedName(), targetNamespace);

            //XML instance generation for SOAP message body (style=document and use=literal)
            var soapBodyXML = SOAPBodyXMLGenerator.GenerateXMLInstanceFromSchema();

            //Show generated SOAP envelope XML
            return buildSOAPEnvelope(soapBodyXML);
        }


        private XmlNode findNode(string nodeName, string xpathQuery, XmlNode startNode, XmlNamespaceManager nsmgr)
        {
            XmlNode root = startNode;

            XmlNode nodeList = root.SelectSingleNode(xpathQuery, nsmgr);

            return nodeList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemaXML"></param>
        /// <param name="nodeNameToKeep"></param>
        /// <param name="schemaTargetNamespaceUri"></param>
        /// <returns></returns>
        private XmlDocument trimOutSchemaXML(XmlDocument schemaXML, string nodeNameToKeep, out string schemaTargetNamespaceUri)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(schemaXML.NameTable);
            nsmgr.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");

            var nodesToRemove = schemaXML.SelectNodes("/xs:schema/xs:element[not(@name='" + nodeNameToKeep + "')]", nsmgr);

            schemaTargetNamespaceUri = schemaXML.SelectSingleNode("/xs:schema/@targetNamespace", nsmgr).Value;

            foreach (XmlNode nodeToRemove in nodesToRemove)
            {
                nodeToRemove.ParentNode.RemoveChild(nodeToRemove);
            }

            return schemaXML;
        }

        private string buildSOAPEnvelope(string soapBodyXml)
        {
            StringBuilder soapMessage = new StringBuilder();

            soapMessage.Append("<soapenv:Envelope xmlns:soapenv = \"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\">" + Environment.NewLine);
            soapMessage.Append("<soapenv:Header />" + Environment.NewLine);
            soapMessage.Append("<soapenv:Body>" + Environment.NewLine);
            //Body content will be here.
            soapMessage.Append(soapBodyXml);
            soapMessage.Append("</soapenv:Body>" + Environment.NewLine);
            soapMessage.Append("</soapenv:Envelope>");

            return soapMessage.ToString();
        }

        /// <summary>
        /// Checks the WSDL binding part to determine SOAP message body structure.
        /// 
        /// Binding "style" -attribute value options:
        /// - document
        /// - rpc
        /// 
        /// Binding operation input & output "use" -attribute value options:
        /// - literal
        /// - encoded
        /// 
        /// Bindins style/use attribute value combinations in SOAP are:
        /// - document/literal
        /// - rpc/encoded
        /// - rpc/literal
        /// 
        /// This method search binding element by name and operation element by name within binding element from WSDL.
        /// Then examine the binding "style" -attribute value and operation "use" -attribute value.
        /// </summary>
        private void checkBinding(string bindingName, string operationName, XmlNamespaceManager nsmgr)
        {
            //Find selected binding and operation information for SOAP message formating and http header building.

            if (true)
            {

            }

            //Get bindig Porttype for finding operation message types and their schemas.
            var portTypeValue = findNode("bindingPortType", "/wsdl:definitions/wsdl:binding[@name='" + bindingName + "']/@type", WSDLContent.DocumentElement, nsmgr).Value;
            BindingInfo.PortTypeName = portTypeValue.Substring(portTypeValue.IndexOf(':') + 1);
            //Binding's transport type value. TODO: check if SOAP 1.1 or 1.2, requires diffrent namespace references.
            BindingInfo.SOAPBindingTransport = findNode("bindingPortType", "/wsdl:definitions/wsdl:binding[@name='" + bindingName + "']/soap:binding/@transport", WSDLContent.DocumentElement, nsmgr).Value;

            //Binding operation node for finding style and use attributes
            var nodes = findNode("binding", "/wsdl:definitions/wsdl:binding[@name='" + bindingName + "']/wsdl:operation[@name = '" + operationName + "']", WSDLContent.DocumentElement, nsmgr);

            //Style value for the operation, needed to select formating of SOAP envelope and envelope body.
            BindingInfo.SOAPOperationStyle = findNode("styleAttribute", "soap:operation/@style", nodes, nsmgr).Value;

            //Operation input body "use" -attribute value for formating of SOAP envelope body.
            BindingInfo.SOAPOperationInputBodyUse = findNode("inputBodyUse", "wsdl:input/soap:body/@use", nodes, nsmgr).Value;

            //Operation output body "use" -attribute value for formating of SOAP envelope body.
            BindingInfo.SOAPOperationOutputBodyUse = findNode("outputBodyUse", "wsdl:output/soap:body/@use", nodes, nsmgr).Value;

        }

        /// <summary>
        /// Find the Binding related portType and operation messages, for SOAP envelope body structure generation.
        /// </summary>
        private void getTypeOfPortAndOperationMessageReferences(string portTypeName, string operationName, XmlNamespaceManager nsmgr)
        {
            var portNode = findNode("porttype", "/wsdl:definitions/wsdl:portType[@name='" + portTypeName + "']", WSDLContent.DocumentElement, nsmgr);
            var messageTypeName = findNode("input", "wsdl:operation[@name='" + operationName + "']/wsdl:input/@message", portNode, nsmgr).Value;
            PortTypeInfo.InputMessageTypeName = messageTypeName.Substring(messageTypeName.IndexOf(':') + 1);
            PortTypeInfo.InputMessagePartName = findNode("input", "/wsdl:definitions/wsdl:message[@name='" + PortTypeInfo.InputMessageTypeName + "']/wsdl:part/@name", portNode, nsmgr).Value;
            PortTypeInfo.InputMessagePartElementTypeName = findNode("input", "/wsdl:definitions/wsdl:message[@name='" + PortTypeInfo.InputMessageTypeName + "']/wsdl:part/@element", portNode, nsmgr).Value;
            PortTypeInfo.MessageSchemaURI = findNode("input", "/wsdl:definitions/wsdl:types/xsd:schema/xsd:import/@schemaLocation", WSDLContent.DocumentElement, nsmgr).Value;
        }
    }
}
