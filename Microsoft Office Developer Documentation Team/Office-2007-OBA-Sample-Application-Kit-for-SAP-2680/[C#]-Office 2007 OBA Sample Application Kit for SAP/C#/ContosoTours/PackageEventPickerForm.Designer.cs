namespace Microsoft.SAPSK.ContosoTours
{
    partial class PackageEventPickerForm
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
            this.textBoxVenue = new System.Windows.Forms.TextBox();
            this.labelVenue = new System.Windows.Forms.Label();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelEventName = new System.Windows.Forms.Label();
            this.comboBoxName = new System.Windows.Forms.ComboBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonAssociate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxVenue
            // 
            this.textBoxVenue.Location = new System.Drawing.Point(56, 31);
            this.textBoxVenue.Name = "textBoxVenue";
            this.textBoxVenue.ReadOnly = true;
            this.textBoxVenue.Size = new System.Drawing.Size(313, 20);
            this.textBoxVenue.TabIndex = 9;
            // 
            // labelVenue
            // 
            this.labelVenue.AutoSize = true;
            this.labelVenue.Location = new System.Drawing.Point(7, 34);
            this.labelVenue.Name = "labelVenue";
            this.labelVenue.Size = new System.Drawing.Size(41, 13);
            this.labelVenue.TabIndex = 11;
            this.labelVenue.Text = "Venue:";
            // 
            // textBoxDate
            // 
            this.textBoxDate.Location = new System.Drawing.Point(55, 57);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.ReadOnly = true;
            this.textBoxDate.Size = new System.Drawing.Size(314, 20);
            this.textBoxDate.TabIndex = 10;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(7, 60);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(33, 13);
            this.labelDate.TabIndex = 7;
            this.labelDate.Text = "Date:";
            // 
            // labelEventName
            // 
            this.labelEventName.AutoSize = true;
            this.labelEventName.Location = new System.Drawing.Point(7, 7);
            this.labelEventName.Name = "labelEventName";
            this.labelEventName.Size = new System.Drawing.Size(38, 13);
            this.labelEventName.TabIndex = 6;
            this.labelEventName.Text = "Name:";
            // 
            // comboBoxName
            // 
            this.comboBoxName.FormattingEnabled = true;
            this.comboBoxName.Location = new System.Drawing.Point(56, 4);
            this.comboBoxName.Name = "comboBoxName";
            this.comboBoxName.Size = new System.Drawing.Size(313, 21);
            this.comboBoxName.TabIndex = 8;
            this.comboBoxName.SelectedIndexChanged += new System.EventHandler(this.comboBoxName_SelectedIndexChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(296, 83);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 12;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonAssociate
            // 
            this.buttonAssociate.Location = new System.Drawing.Point(215, 83);
            this.buttonAssociate.Name = "buttonAssociate";
            this.buttonAssociate.Size = new System.Drawing.Size(75, 23);
            this.buttonAssociate.TabIndex = 13;
            this.buttonAssociate.Text = "Ok";
            this.buttonAssociate.UseVisualStyleBackColor = true;
            this.buttonAssociate.Click += new System.EventHandler(this.buttonAssociate_Click);
            // 
            // PackagePickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 110);
            this.Controls.Add(this.buttonAssociate);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxVenue);
            this.Controls.Add(this.labelVenue);
            this.Controls.Add(this.textBoxDate);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelEventName);
            this.Controls.Add(this.comboBoxName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PackagePickerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Package Event Picker";
            this.Load += new System.EventHandler(this.PackagePickerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxVenue;
        private System.Windows.Forms.Label labelVenue;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelEventName;
        private System.Windows.Forms.ComboBox comboBoxName;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonAssociate;
    }
}