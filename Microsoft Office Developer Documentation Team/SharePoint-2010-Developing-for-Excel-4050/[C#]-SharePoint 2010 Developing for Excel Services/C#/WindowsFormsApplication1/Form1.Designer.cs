namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.btnLoadRanges = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtSite = new System.Windows.Forms.TextBox();
            this.txtSpreadsheet = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLoadRanges
            // 
            this.btnLoadRanges.Location = new System.Drawing.Point(12, 77);
            this.btnLoadRanges.Name = "btnLoadRanges";
            this.btnLoadRanges.Size = new System.Drawing.Size(117, 23);
            this.btnLoadRanges.TabIndex = 0;
            this.btnLoadRanges.Text = "Load Named Ranges";
            this.btnLoadRanges.UseVisualStyleBackColor = true;
            this.btnLoadRanges.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 106);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // txtSite
            // 
            this.txtSite.Location = new System.Drawing.Point(82, 2);
            this.txtSite.Name = "txtSite";
            this.txtSite.Size = new System.Drawing.Size(268, 20);
            this.txtSite.TabIndex = 2;
            this.txtSite.Text = "intranet.contoso.com";
            // 
            // txtSpreadsheet
            // 
            this.txtSpreadsheet.Location = new System.Drawing.Point(82, 29);
            this.txtSpreadsheet.Name = "txtSpreadsheet";
            this.txtSpreadsheet.Size = new System.Drawing.Size(268, 20);
            this.txtSpreadsheet.TabIndex = 3;
            this.txtSpreadsheet.Text = "Shared Documents/Book1.xlsx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Site";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Spreadsheet";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(9, 55);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(32, 13);
            this.lblUrl.TabIndex = 7;
            this.lblUrl.Text = "URL:";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(138, 106);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(81, 13);
            this.lblValue.TabIndex = 8;
            this.lblValue.Text = "<Range Value>";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 221);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSpreadsheet);
            this.Controls.Add(this.txtSite);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnLoadRanges);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadRanges;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txtSite;
        private System.Windows.Forms.TextBox txtSpreadsheet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Label lblValue;
    }
}

