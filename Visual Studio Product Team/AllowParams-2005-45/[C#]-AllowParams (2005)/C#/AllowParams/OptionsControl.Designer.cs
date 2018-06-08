using System.Text;

namespace Vsip.AllowParams
{
    partial class OptionsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.labelParamDesc = new System.Windows.Forms.Label();
            this.tbParamDesc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(11, 12);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(350, 207);
            this.richTextBox.TabIndex = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("Valid parameter types: (see Readme.txt for details)\r\n");
            sb.Append("\r\n"); 
            sb.Append("   '~' - No autocompletion for this parameter\r\n");
            sb.Append("   '$' - This parameter is the rest of the input line\r\n");
            sb.Append("   '$' - This parameter is the rest of the input line\r\n");
            sb.Append("         (no autocompletion).\r\n");
            sb.Append("   'a' – An alias.\r\n");
            sb.Append("   'c' – The canonical name of a command.\r\n");
            sb.Append("   'd' – A filename from the file system.\r\n");
            sb.Append("   'p' – The filename from a project in the current solution.\r\n");
            sb.Append("   'u' – A URL.\r\n");
            sb.Append("   '|' – Combines two parameter types for the same parameter.\r\n"); 
            sb.Append("   '*' – Indicates zero or more occurrences of the previous \r\n");
            sb.Append("         parameter.\r\n");
            sb.Append("\r\n");
            sb.Append("Some example ParameterDescription strings:\r\n");
            sb.Append("\r\n");
            sb.Append("   \"p p\" – Command accepts two filenames \r\n");
            sb.Append("   \"u d\" – Command accepts one URL and one filename argument.\r\n");
            sb.Append("   \"u *\" – Command accepts zero or more URL arguments.\r\n");

            this.richTextBox.Text = sb.ToString();
            // 
            // labelParamDesc
            // 
            this.labelParamDesc.AutoSize = true;
            this.labelParamDesc.Location = new System.Drawing.Point(8, 236);
            this.labelParamDesc.Name = "labelParamDesc";
            this.labelParamDesc.Size = new System.Drawing.Size(293, 13);
            this.labelParamDesc.TabIndex = 1;
            this.labelParamDesc.Text = "Parameter Description String for AllowParams.TestCommand:";
            // 
            // tbParamDesc
            // 
            this.tbParamDesc.Location = new System.Drawing.Point(11, 252);
            this.tbParamDesc.Name = "tbParamDesc";
            this.tbParamDesc.Size = new System.Drawing.Size(347, 20);
            this.tbParamDesc.TabIndex = 2;
            // 
            // OptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbParamDesc);
            this.Controls.Add(this.labelParamDesc);
            this.Controls.Add(this.richTextBox);
            this.Name = "OptionsControl";
            this.Size = new System.Drawing.Size(377, 288);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Label labelParamDesc;
        public System.Windows.Forms.TextBox tbParamDesc;

    }
}
