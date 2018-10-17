using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BindingData
    {
        public string SOAPAction { get; set; }
        public string SOAPOperationStyle { get; set; }
        public string SOAPOperationInputBodyUse { get; set; }
        public string SOAPOperationOutputBodyUse { get; set; }
        public string SOAPBindingTransport { get; set; }
        public string PortTypeName { get; set; }
    }

    public class PortTypeData
    {
        public string InputMessageTypeName { get; set; }
        public string InputMessagePartName { get; set; }
        public string InputMessagePartElementTypeName { get; set; }
        public string MessageSchemaURI { get; set; }
    }
}
