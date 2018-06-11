namespace Microsoft.SAPSK.ContosoTours
{
    partial class BuyPackageControl
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
            panelSelectPackage = new System.Windows.Forms.Panel();
            panelView = new System.Windows.Forms.Panel();
            buttonViewPresentation = new System.Windows.Forms.Button();
            buttonView = new System.Windows.Forms.Button();
            comboBoxPackage = new System.Windows.Forms.ComboBox();
            labelPackage = new System.Windows.Forms.Label();
            panelGrid = new System.Windows.Forms.Panel();
            dataGridViewEvents = new System.Windows.Forms.DataGridView();
            panel6 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            panelBottom = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            panelBuyPackage = new System.Windows.Forms.Panel();
            labelTotalCost = new System.Windows.Forms.Label();
            buttonBuyPackage = new System.Windows.Forms.Button();
            labelTotalPrice = new System.Windows.Forms.Label();
            panelSelectPackage.SuspendLayout();
            panelView.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewEvents)).BeginInit();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            panelBuyPackage.SuspendLayout();
            SuspendLayout();
            // 
            // panelSelectPackage
            // 
            panelSelectPackage.Controls.Add(panelView);
            panelSelectPackage.Controls.Add(comboBoxPackage);
            panelSelectPackage.Controls.Add(labelPackage);
            panelSelectPackage.Dock = System.Windows.Forms.DockStyle.Top;
            panelSelectPackage.Location = new System.Drawing.Point(0, 0);
            panelSelectPackage.Name = "panelSelectPackage";
            panelSelectPackage.Size = new System.Drawing.Size(286, 104);
            panelSelectPackage.TabIndex = 5;
            // 
            // panelView
            // 
            panelView.Controls.Add(buttonViewPresentation);
            panelView.Controls.Add(buttonView);
            panelView.Location = new System.Drawing.Point(11, 57);
            panelView.Name = "panelView";
            panelView.Size = new System.Drawing.Size(265, 38);
            panelView.TabIndex = 12;
            // 
            // buttonViewPresentation
            // 
            buttonViewPresentation.Location = new System.Drawing.Point(140, 8);
            buttonViewPresentation.Name = "buttonViewPresentation";
            buttonViewPresentation.Size = new System.Drawing.Size(103, 23);
            buttonViewPresentation.TabIndex = 13;
            buttonViewPresentation.Text = "View Presentation";
            buttonViewPresentation.UseVisualStyleBackColor = true;
            buttonViewPresentation.Click += new System.EventHandler(buttonViewPresentation_Click);
            // 
            // buttonView
            // 
            buttonView.Location = new System.Drawing.Point(15, 8);
            buttonView.Name = "buttonView";
            buttonView.Size = new System.Drawing.Size(103, 23);
            buttonView.TabIndex = 12;
            buttonView.Text = "View Package";
            buttonView.UseVisualStyleBackColor = true;
            buttonView.Click += new System.EventHandler(buttonView_Click);
            // 
            // comboBoxPackage
            // 
            comboBoxPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            comboBoxPackage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxPackage.FormattingEnabled = true;
            comboBoxPackage.Location = new System.Drawing.Point(11, 29);
            comboBoxPackage.Name = "comboBoxPackage";
            comboBoxPackage.Size = new System.Drawing.Size(265, 21);
            comboBoxPackage.TabIndex = 10;
            comboBoxPackage.SelectedIndexChanged += new System.EventHandler(comboBoxPackage_SelectedIndexChanged);
            // 
            // labelPackage
            // 
            labelPackage.AutoSize = true;
            labelPackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelPackage.Location = new System.Drawing.Point(8, 13);
            labelPackage.Name = "labelPackage";
            labelPackage.Size = new System.Drawing.Size(86, 13);
            labelPackage.TabIndex = 10;
            labelPackage.Text = "Select Package:";
            // 
            // panelGrid
            // 
            panelGrid.Controls.Add(dataGridViewEvents);
            panelGrid.Controls.Add(panel6);
            panelGrid.Controls.Add(panel4);
            panelGrid.Dock = System.Windows.Forms.DockStyle.Top;
            panelGrid.Location = new System.Drawing.Point(0, 104);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new System.Drawing.Size(286, 209);
            panelGrid.TabIndex = 11;
            // 
            // dataGridViewEvents
            // 
            dataGridViewEvents.AllowUserToAddRows = false;
            dataGridViewEvents.AllowUserToDeleteRows = false;
            dataGridViewEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewEvents.Location = new System.Drawing.Point(11, 0);
            dataGridViewEvents.Name = "dataGridViewEvents";
            dataGridViewEvents.RowHeadersVisible = false;
            dataGridViewEvents.Size = new System.Drawing.Size(265, 209);
            dataGridViewEvents.TabIndex = 9;
            dataGridViewEvents.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dataGridViewEvents_EditingControlShowing);
            // 
            // panel6
            // 
            panel6.Dock = System.Windows.Forms.DockStyle.Right;
            panel6.Location = new System.Drawing.Point(276, 0);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(10, 209);
            panel6.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.Dock = System.Windows.Forms.DockStyle.Left;
            panel4.Location = new System.Drawing.Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(11, 209);
            panel4.TabIndex = 0;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(label1);
            panelBottom.Controls.Add(pictureBox1);
            panelBottom.Controls.Add(panelBuyPackage);
            panelBottom.Dock = System.Windows.Forms.DockStyle.Top;
            panelBottom.Location = new System.Drawing.Point(0, 313);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new System.Drawing.Size(286, 125);
            panelBottom.TabIndex = 12;
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            label1.Location = new System.Drawing.Point(34, 77);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(229, 48);
            label1.TabIndex = 10;
            label1.Text = "After selecting a package and selecting the promo type of your choice, click on" +
                " Buy Package to purchase.";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icoquestion;
            pictureBox1.Location = new System.Drawing.Point(11, 77);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(16, 16);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // panelBuyPackage
            // 
            panelBuyPackage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            panelBuyPackage.Controls.Add(labelTotalCost);
            panelBuyPackage.Controls.Add(buttonBuyPackage);
            panelBuyPackage.Controls.Add(labelTotalPrice);
            panelBuyPackage.Location = new System.Drawing.Point(11, 14);
            panelBuyPackage.Name = "panelBuyPackage";
            panelBuyPackage.Size = new System.Drawing.Size(252, 56);
            panelBuyPackage.TabIndex = 8;
            // 
            // labelTotalCost
            // 
            labelTotalCost.AutoSize = true;
            labelTotalCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelTotalCost.Location = new System.Drawing.Point(7, 9);
            labelTotalCost.Name = "labelTotalCost";
            labelTotalCost.Size = new System.Drawing.Size(55, 13);
            labelTotalCost.TabIndex = 5;
            labelTotalCost.Text = "Total Cost";
            // 
            // buttonBuyPackage
            // 
            buttonBuyPackage.Location = new System.Drawing.Point(169, 9);
            buttonBuyPackage.Name = "buttonBuyPackage";
            buttonBuyPackage.Size = new System.Drawing.Size(71, 37);
            buttonBuyPackage.TabIndex = 6;
            buttonBuyPackage.Text = "Buy Package";
            buttonBuyPackage.UseVisualStyleBackColor = true;
            buttonBuyPackage.Click += new System.EventHandler(buttonBuyPackage_Click);
            // 
            // labelTotalPrice
            // 
            labelTotalPrice.BackColor = System.Drawing.Color.Black;
            labelTotalPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            labelTotalPrice.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelTotalPrice.ForeColor = System.Drawing.Color.Gold;
            labelTotalPrice.Location = new System.Drawing.Point(10, 26);
            labelTotalPrice.Name = "labelTotalPrice";
            labelTotalPrice.Size = new System.Drawing.Size(151, 20);
            labelTotalPrice.TabIndex = 7;
            labelTotalPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BuyPackageControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelBottom);
            Controls.Add(panelGrid);
            Controls.Add(panelSelectPackage);
            Name = "BuyPackageControl";
            Size = new System.Drawing.Size(286, 448);
            Load += new System.EventHandler(BuyPackage_Load);
            Resize += new System.EventHandler(BuyPackageControl_Resize);
            panelSelectPackage.ResumeLayout(false);
            panelSelectPackage.PerformLayout();
            panelView.ResumeLayout(false);
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(dataGridViewEvents)).EndInit();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            panelBuyPackage.ResumeLayout(false);
            panelBuyPackage.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSelectPackage;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dataGridViewEvents;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button buttonBuyPackage;
        private System.Windows.Forms.Label labelTotalPrice;
        private System.Windows.Forms.Label labelTotalCost;
        private System.Windows.Forms.Label labelPackage;
        private System.Windows.Forms.ComboBox comboBoxPackage;
        private System.Windows.Forms.Panel panelBuyPackage;
        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.Button buttonViewPresentation;
        private System.Windows.Forms.Button buttonView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
