namespace OutlookAddIn2
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
            this.addCustomColumnsButton = new System.Windows.Forms.Button();
            this.filterCustomColumnsButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.reportPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // addCustomColumnsButton
            // 
            this.addCustomColumnsButton.Location = new System.Drawing.Point(174, 12);
            this.addCustomColumnsButton.Name = "addCustomColumnsButton";
            this.addCustomColumnsButton.Size = new System.Drawing.Size(123, 23);
            this.addCustomColumnsButton.TabIndex = 1;
            this.addCustomColumnsButton.Text = "Customize Columns";
            this.addCustomColumnsButton.UseVisualStyleBackColor = true;
            this.addCustomColumnsButton.Click += new System.EventHandler(this.customizeColumnsButton_Click);
            // 
            // filterCustomColumnsButton
            // 
            this.filterCustomColumnsButton.Location = new System.Drawing.Point(303, 12);
            this.filterCustomColumnsButton.Name = "filterCustomColumnsButton";
            this.filterCustomColumnsButton.Size = new System.Drawing.Size(123, 23);
            this.filterCustomColumnsButton.TabIndex = 1;
            this.filterCustomColumnsButton.Text = "Filter Custom Columns";
            this.filterCustomColumnsButton.UseVisualStyleBackColor = true;
            this.filterCustomColumnsButton.Click += new System.EventHandler(this.filterCustomColumnsButton_Click);
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(93, 12);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(75, 23);
            this.filterButton.TabIndex = 1;
            this.filterButton.Text = "Filter";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(12, 12);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 2;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // reportPanel
            // 
            this.reportPanel.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress;
            this.reportPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.reportPanel.AutoScroll = true;
            this.reportPanel.Location = new System.Drawing.Point(13, 42);
            this.reportPanel.Name = "reportPanel";
            this.reportPanel.Size = new System.Drawing.Size(1185, 536);
            this.reportPanel.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 590);
            this.Controls.Add(this.reportPanel);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.filterCustomColumnsButton);
            this.Controls.Add(this.addCustomColumnsButton);
            this.Controls.Add(this.filterButton);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Sales Opportunities Summary";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addCustomColumnsButton;
        private System.Windows.Forms.Button filterCustomColumnsButton;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Panel reportPanel;
    }
}