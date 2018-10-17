namespace SOAPMessageBuilder
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFetchWSDL = new System.Windows.Forms.Button();
            this.tbWSDL = new System.Windows.Forms.TextBox();
            this.btnGenerateSoapMessage = new System.Windows.Forms.Button();
            this.tbSOAPMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbWSDLUrl = new System.Windows.Forms.TextBox();
            this.chbNeedLogin = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPw = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbBindingName = new System.Windows.Forms.TextBox();
            this.tbOperatioName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFetchWSDL
            // 
            this.btnFetchWSDL.Location = new System.Drawing.Point(45, 12);
            this.btnFetchWSDL.Name = "btnFetchWSDL";
            this.btnFetchWSDL.Size = new System.Drawing.Size(468, 84);
            this.btnFetchWSDL.TabIndex = 0;
            this.btnFetchWSDL.Text = "Fetch WSDL from URL";
            this.btnFetchWSDL.UseVisualStyleBackColor = true;
            this.btnFetchWSDL.Click += new System.EventHandler(this.btnFetchWSDL_Click);
            // 
            // tbWSDL
            // 
            this.tbWSDL.Location = new System.Drawing.Point(968, 229);
            this.tbWSDL.Multiline = true;
            this.tbWSDL.Name = "tbWSDL";
            this.tbWSDL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbWSDL.Size = new System.Drawing.Size(888, 979);
            this.tbWSDL.TabIndex = 1;
            // 
            // btnGenerateSoapMessage
            // 
            this.btnGenerateSoapMessage.Location = new System.Drawing.Point(519, 13);
            this.btnGenerateSoapMessage.Name = "btnGenerateSoapMessage";
            this.btnGenerateSoapMessage.Size = new System.Drawing.Size(431, 83);
            this.btnGenerateSoapMessage.TabIndex = 2;
            this.btnGenerateSoapMessage.Text = "Generate SOAP Message";
            this.btnGenerateSoapMessage.UseVisualStyleBackColor = true;
            this.btnGenerateSoapMessage.Click += new System.EventHandler(this.btnGenerateSoapMessage_Click);
            // 
            // tbSOAPMessage
            // 
            this.tbSOAPMessage.Location = new System.Drawing.Point(60, 229);
            this.tbSOAPMessage.Multiline = true;
            this.tbSOAPMessage.Name = "tbSOAPMessage";
            this.tbSOAPMessage.Size = new System.Drawing.Size(890, 979);
            this.tbSOAPMessage.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "WSDL Ulr: ";
            // 
            // tbWSDLUrl
            // 
            this.tbWSDLUrl.Location = new System.Drawing.Point(162, 123);
            this.tbWSDLUrl.Name = "tbWSDLUrl";
            this.tbWSDLUrl.Size = new System.Drawing.Size(1694, 29);
            this.tbWSDLUrl.TabIndex = 5;
            this.tbWSDLUrl.Text = "http://localhost:15005/Service1.svc?wsdl";
            // 
            // chbNeedLogin
            // 
            this.chbNeedLogin.AutoSize = true;
            this.chbNeedLogin.Location = new System.Drawing.Point(968, 13);
            this.chbNeedLogin.Name = "chbNeedLogin";
            this.chbNeedLogin.Size = new System.Drawing.Size(141, 29);
            this.chbNeedLogin.TabIndex = 6;
            this.chbNeedLogin.Text = "Needs login";
            this.chbNeedLogin.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(968, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "User name: ";
            // 
            // tbUID
            // 
            this.tbUID.Location = new System.Drawing.Point(1092, 67);
            this.tbUID.Name = "tbUID";
            this.tbUID.Size = new System.Drawing.Size(330, 29);
            this.tbUID.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1449, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Password: ";
            // 
            // tbPw
            // 
            this.tbPw.Location = new System.Drawing.Point(1556, 65);
            this.tbPw.Name = "tbPw";
            this.tbPw.Size = new System.Drawing.Size(300, 29);
            this.tbPw.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(202, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "SOAP binding name: ";
            // 
            // tbBindingName
            // 
            this.tbBindingName.Location = new System.Drawing.Point(262, 173);
            this.tbBindingName.Name = "tbBindingName";
            this.tbBindingName.Size = new System.Drawing.Size(687, 29);
            this.tbBindingName.TabIndex = 12;
            this.tbBindingName.Text = "BasicHttpBinding_IService1";
            // 
            // tbOperatioName
            // 
            this.tbOperatioName.Location = new System.Drawing.Point(1194, 176);
            this.tbOperatioName.Name = "tbOperatioName";
            this.tbOperatioName.Size = new System.Drawing.Size(649, 29);
            this.tbOperatioName.TabIndex = 13;
            this.tbOperatioName.Text = "GetData";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(968, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(220, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "SOAP Operation name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1868, 1220);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbOperatioName);
            this.Controls.Add(this.tbBindingName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbPw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbUID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chbNeedLogin);
            this.Controls.Add(this.tbWSDLUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSOAPMessage);
            this.Controls.Add(this.btnGenerateSoapMessage);
            this.Controls.Add(this.tbWSDL);
            this.Controls.Add(this.btnFetchWSDL);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFetchWSDL;
        private System.Windows.Forms.TextBox tbWSDL;
        private System.Windows.Forms.Button btnGenerateSoapMessage;
        private System.Windows.Forms.TextBox tbSOAPMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbWSDLUrl;
        private System.Windows.Forms.CheckBox chbNeedLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPw;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbBindingName;
        private System.Windows.Forms.TextBox tbOperatioName;
        private System.Windows.Forms.Label label5;
    }
}

