namespace Microsoft.SAPSK.ContosoTours.Controls
{
    partial class PackageListControl
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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("No Data");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("No Data", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageListControl));
            treeViewPackages = new System.Windows.Forms.TreeView();
            imageList = new System.Windows.Forms.ImageList(components);
            label1 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            panel2 = new System.Windows.Forms.Panel();
            comboBoxAgency = new System.Windows.Forms.ComboBox();
            buttonRefresh = new System.Windows.Forms.Button();
            buttonDetails = new System.Windows.Forms.Button();
            toolTip = new System.Windows.Forms.ToolTip(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // treeViewPackages
            // 
            treeViewPackages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            treeViewPackages.ImageIndex = 0;
            treeViewPackages.ImageList = imageList;
            treeViewPackages.Location = new System.Drawing.Point(12, 41);
            treeViewPackages.Name = "treeViewPackages";
            treeNode3.Name = "Node1";
            treeNode3.Text = "No Data";
            treeNode4.Name = "Node0";
            treeNode4.Text = "No Data";
            treeViewPackages.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            treeViewPackages.SelectedImageIndex = 0;
            treeViewPackages.ShowNodeToolTips = true;
            treeViewPackages.Size = new System.Drawing.Size(228, 321);
            treeViewPackages.TabIndex = 0;
            treeViewPackages.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeViewPackages_AfterSelect);
            // 
            // imageList
            // 
            imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            imageList.TransparentColor = System.Drawing.Color.Transparent;
            imageList.Images.SetKeyName(0, "icospacer.gif");
            imageList.Images.SetKeyName(1, "");
            imageList.Images.SetKeyName(2, "");
            imageList.Images.SetKeyName(3, "");
            imageList.Images.SetKeyName(4, "");
            imageList.Images.SetKeyName(5, "");
            imageList.Images.SetKeyName(6, "flight.gif");
            imageList.Images.SetKeyName(7, "tower.gif");
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            label1.Location = new System.Drawing.Point(34, 3);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(206, 44);
            label1.TabIndex = 1;
            label1.Text = "Select an agency to populate the Package Outline with flights available on that a" +
                "gency.";
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(0, 371);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(253, 59);
            panel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icoquestion;
            pictureBox1.Location = new System.Drawing.Point(12, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(16, 16);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(comboBoxAgency);
            panel2.Controls.Add(buttonRefresh);
            panel2.Controls.Add(treeViewPackages);
            panel2.Controls.Add(buttonDetails);
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(253, 371);
            panel2.TabIndex = 8;
            // 
            // comboBoxAgency
            // 
            comboBoxAgency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxAgency.DropDownWidth = 171;
            comboBoxAgency.FormattingEnabled = true;
            comboBoxAgency.Location = new System.Drawing.Point(12, 14);
            comboBoxAgency.Name = "comboBoxAgency";
            comboBoxAgency.Size = new System.Drawing.Size(171, 21);
            comboBoxAgency.TabIndex = 7;
            toolTip.SetToolTip(comboBoxAgency, "Select an agency to update the view below \r\nwith flights available on that agency" +
                    ".\r\n\r\nNOTE: Only the Bavarian Castle agency has\r\nflight connection available in t" +
                    "he SAP seed\r\ndata.");
            comboBoxAgency.SelectionChangeCommitted += new System.EventHandler(comboBoxAgency_SelectionChangeCommitted);
            // 
            // buttonRefresh
            // 
            buttonRefresh.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icoreload;
            buttonRefresh.Location = new System.Drawing.Point(217, 12);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new System.Drawing.Size(23, 23);
            buttonRefresh.TabIndex = 5;
            buttonRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            toolTip.SetToolTip(buttonRefresh, "Refresh/reloads the data from the database.");
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += new System.EventHandler(buttonRefresh_Click);
            // 
            // buttonDetails
            // 
            buttonDetails.Enabled = false;
            buttonDetails.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icodetails;
            buttonDetails.Location = new System.Drawing.Point(189, 12);
            buttonDetails.Name = "buttonDetails";
            buttonDetails.Size = new System.Drawing.Size(23, 23);
            buttonDetails.TabIndex = 6;
            buttonDetails.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            toolTip.SetToolTip(buttonDetails, "Show details of particular package or event.");
            buttonDetails.UseVisualStyleBackColor = true;
            buttonDetails.Click += new System.EventHandler(buttonDetails_Click);
            // 
            // toolTip
            // 
            toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            toolTip.ToolTipTitle = "For your information";
            // 
            // PackageListControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "PackageListControl";
            Size = new System.Drawing.Size(253, 430);
            Load += new System.EventHandler(PackageListControl_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewPackages;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonDetails;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox comboBoxAgency;
    }
}
