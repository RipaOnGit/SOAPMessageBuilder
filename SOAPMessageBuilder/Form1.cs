using System;
using System.Windows.Forms;
using BusinessLogic;

namespace SOAPMessageBuilder
{
    public partial class Form1 : Form
    {

        private static LogicModule generator = new LogicModule();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnFetchWSDL_Click(object sender, EventArgs e)
        {
            //WSDL loading for display.
            tbWSDL.Text = generator.GetXMLfromURI(tbWSDLUrl.Text, chbNeedLogin.Checked);
        }

        private void btnGenerateSoapMessage_Click(object sender, EventArgs e)
        {
            //Show generated SOAP envelope XML
            tbSOAPMessage.Text = generator.ProduceSOAPMessage(tbBindingName.Text, tbOperatioName.Text);
        }

    }
}
