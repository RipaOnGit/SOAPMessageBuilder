using System.Xml;
using System.Xml.Schema;
using System.IO;
using Microsoft.Xml.XMLGen;
using System.Text;

namespace XMLGenerator
{
    public class GeneratorCreator
    {
        private static XmlSchemaSet schemas = null;
        private static XmlQualifiedName qname = null;

        public GeneratorCreator(XmlDocument schemasIn, XmlQualifiedName qualifiedName, string targetNamespace)
        {
            schemas = new XmlSchemaSet();
            schemas.Add(targetNamespace, XmlReader.Create(new StringReader(schemasIn.OuterXml)));
            if (qualifiedName == null)
            {
                qname = XmlQualifiedName.Empty;
            }
            else
            {
                qname = qualifiedName;
            }

        }

        public string GenerateXMLInstanceFromSchema()
        {
            StringBuilder xmlOutString = new StringBuilder();
            using (StringWriter xmlString = new StringWriter(xmlOutString))
            {
                using (XmlTextWriter textWriter = new XmlTextWriter(xmlString))
                {
                    textWriter.Formatting = Formatting.Indented;
                    XmlSampleGenerator genr = new XmlSampleGenerator(schemas, qname);
                    //genr.MaxThreshold = 0;
                    //genr.ListLength = 1;
                    genr.WriteXml(textWriter);
                }
            }

            return xmlOutString.ToString();

        }

    }
}
