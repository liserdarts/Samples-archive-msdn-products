namespace Microsoft.SAPSK.ContosoTours
{
    partial class SeedDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeedDataForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabelSeed = new System.Windows.Forms.LinkLabel();
            this.linkLabelClose = new System.Windows.Forms.LinkLabel();
            this.checkBoxNoShow = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icoquestion;
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "For your information";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(268, 73);
            this.label2.TabIndex = 2;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Thank you.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Congratulation!";
            // 
            // linkLabelSeed
            // 
            this.linkLabelSeed.AutoSize = true;
            this.linkLabelSeed.Location = new System.Drawing.Point(12, 171);
            this.linkLabelSeed.Name = "linkLabelSeed";
            this.linkLabelSeed.Size = new System.Drawing.Size(97, 13);
            this.linkLabelSeed.TabIndex = 6;
            this.linkLabelSeed.TabStop = true;
            this.linkLabelSeed.Text = "Seed the database";
            this.linkLabelSeed.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSeed_LinkClicked);
            // 
            // linkLabelClose
            // 
            this.linkLabelClose.AutoSize = true;
            this.linkLabelClose.Location = new System.Drawing.Point(12, 186);
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
            this.checkBoxNoShow.Location = new System.Drawing.Point(169, 186);
            this.checkBoxNoShow.Name = "checkBoxNoShow";
            this.checkBoxNoShow.Size = new System.Drawing.Size(108, 17);
            this.checkBoxNoShow.TabIndex = 8;
            this.checkBoxNoShow.Text = "Don\'t show again";
            this.checkBoxNoShow.UseVisualStyleBackColor = true;
            // 
            // SeedDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.CancelButton = this.linkLabelClose;
            this.ClientSize = new System.Drawing.Size(292, 208);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxNoShow);
            this.Controls.Add(this.linkLabelClose);
            this.Controls.Add(this.linkLabelSeed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SeedDataForm";
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
        private System.Windows.Forms.LinkLabel linkLabelSeed;
        private System.Windows.Forms.LinkLabel linkLabelClose;
        private System.Windows.Forms.CheckBox checkBoxNoShow;
    }
}