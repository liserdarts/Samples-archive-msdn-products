namespace Microsoft.SAPSK.ContosoTours
{
    partial class ReseedDataForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabelSeedDatabase = new System.Windows.Forms.LinkLabel();
            this.linkLabelClose = new System.Windows.Forms.LinkLabel();
            this.checkBoxNoShow = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icoquestion;
            this.pictureBox1.Location = new System.Drawing.Point(8, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "For your information";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 56);
            this.label2.TabIndex = 3;
            this.label2.Text = "The system detected that your SAP Starter Kit data is already outdated . For bett" +
                "er user experience, we  recommend that you let us seed the  database again with " +
                "random but pertinent  information.\r\n";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(268, 69);
            this.label3.TabIndex = 4;
            this.label3.Text = "Moreover , we recommend that you run  SAPBC_DATA_GENERATOR in the SAP  GUI before" +
                " proceeding with the seeding process to  insure that  the sample data is relativ" +
                "e  to  the current date.";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(268, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Thank You.";
            // 
            // linkLabelSeedDatabase
            // 
            this.linkLabelSeedDatabase.AutoSize = true;
            this.linkLabelSeedDatabase.Location = new System.Drawing.Point(12, 179);
            this.linkLabelSeedDatabase.Name = "linkLabelSeedDatabase";
            this.linkLabelSeedDatabase.Size = new System.Drawing.Size(97, 13);
            this.linkLabelSeedDatabase.TabIndex = 6;
            this.linkLabelSeedDatabase.TabStop = true;
            this.linkLabelSeedDatabase.Text = "Seed the database";
            this.linkLabelSeedDatabase.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSeedDatabase_LinkClicked);
            // 
            // linkLabelClose
            // 
            this.linkLabelClose.AutoSize = true;
            this.linkLabelClose.Location = new System.Drawing.Point(12, 195);
            this.linkLabelClose.Name = "linkLabelClose";
            this.linkLabelClose.Size = new System.Drawing.Size(91, 13);
            this.linkLabelClose.TabIndex = 7;
            this.linkLabelClose.TabStop = true;
            this.linkLabelClose.Text = "Close this window";
            this.linkLabelClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClose_LinkClicked);
            // 
            // checkBoxNoShow
            // 
            this.checkBoxNoShow.AutoSize = true;
            this.checkBoxNoShow.Location = new System.Drawing.Point(174, 193);
            this.checkBoxNoShow.Name = "checkBoxNoShow";
            this.checkBoxNoShow.Size = new System.Drawing.Size(108, 17);
            this.checkBoxNoShow.TabIndex = 8;
            this.checkBoxNoShow.Text = "Don\'t show again";
            this.checkBoxNoShow.UseVisualStyleBackColor = true;
            // 
            // ReseedDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(292, 221);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxNoShow);
            this.Controls.Add(this.linkLabelClose);
            this.Controls.Add(this.linkLabelSeedDatabase);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ReseedDataForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabelSeedDatabase;
        private System.Windows.Forms.LinkLabel linkLabelClose;
        private System.Windows.Forms.CheckBox checkBoxNoShow;
    }
}