namespace Microsoft.SAPSK.ContosoTours
{
    partial class ManagePackageForm
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
            this.components = new System.ComponentModel.Container();
            this.labelNewPackage = new System.Windows.Forms.Label();
            this.dataGridViewPackage = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBoxNewPackage = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelTipEdit = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPoster = new System.Windows.Forms.PictureBox();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNewPackage)).BeginInit();
            this.panelTipEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNewPackage
            // 
            this.labelNewPackage.AutoSize = true;
            this.labelNewPackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNewPackage.Location = new System.Drawing.Point(44, 238);
            this.labelNewPackage.Name = "labelNewPackage";
            this.labelNewPackage.Size = new System.Drawing.Size(75, 13);
            this.labelNewPackage.TabIndex = 5;
            this.labelNewPackage.Text = "New Package";
            // 
            // dataGridViewPackage
            // 
            this.dataGridViewPackage.AllowUserToAddRows = false;
            this.dataGridViewPackage.AllowUserToDeleteRows = false;
            this.dataGridViewPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPackage.Location = new System.Drawing.Point(5, 4);
            this.dataGridViewPackage.Name = "dataGridViewPackage";
            this.dataGridViewPackage.ReadOnly = true;
            this.dataGridViewPackage.RowHeadersVisible = false;
            this.dataGridViewPackage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPackage.Size = new System.Drawing.Size(426, 195);
            this.dataGridViewPackage.TabIndex = 1;
            this.dataGridViewPackage.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPackage_CellMouseLeave);
            this.dataGridViewPackage.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPackage_RowEnter);
            this.dataGridViewPackage.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridViewPackage_CellValueNeeded);
            this.dataGridViewPackage.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPackage_CellMouseEnter);
            this.dataGridViewPackage.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPackage_CellClick);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(436, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Poster Preview";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "For your information";
            // 
            // pictureBoxNewPackage
            // 
            this.pictureBoxNewPackage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxNewPackage.Location = new System.Drawing.Point(125, 235);
            this.pictureBoxNewPackage.Name = "pictureBoxNewPackage";
            this.pictureBoxNewPackage.Size = new System.Drawing.Size(19, 17);
            this.pictureBoxNewPackage.TabIndex = 4;
            this.pictureBoxNewPackage.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBoxNewPackage, "Creates a new package.");
            this.pictureBoxNewPackage.Click += new System.EventHandler(this.pictureBoxNewPackage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Package can be deleted.";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(244, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 28);
            this.label4.TabIndex = 5;
            this.label4.Text = "Sold Package.  Deletion is not allowed.";
            // 
            // panelTipEdit
            // 
            this.panelTipEdit.BackColor = System.Drawing.SystemColors.Info;
            this.panelTipEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTipEdit.Controls.Add(this.label4);
            this.panelTipEdit.Controls.Add(this.label5);
            this.panelTipEdit.Controls.Add(this.pictureBox3);
            this.panelTipEdit.Controls.Add(this.pictureBox4);
            this.panelTipEdit.Controls.Add(this.label3);
            this.panelTipEdit.Controls.Add(this.label6);
            this.panelTipEdit.Controls.Add(this.pictureBox2);
            this.panelTipEdit.Controls.Add(this.pictureBox5);
            this.panelTipEdit.Controls.Add(this.label7);
            this.panelTipEdit.Controls.Add(this.pictureBox6);
            this.panelTipEdit.ForeColor = System.Drawing.SystemColors.InfoText;
            this.panelTipEdit.Location = new System.Drawing.Point(5, 202);
            this.panelTipEdit.Name = "panelTipEdit";
            this.panelTipEdit.Size = new System.Drawing.Size(426, 80);
            this.panelTipEdit.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(41, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 28);
            this.label5.TabIndex = 5;
            this.label5.Text = "Sold Package.  Changes not allowed.  View only.";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icodelete_disabled;
            this.pictureBox3.Location = new System.Drawing.Point(224, 48);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icoedit_disabled;
            this.pictureBox4.Location = new System.Drawing.Point(19, 48);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(16, 16);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 4;
            this.pictureBox4.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Package can be edited.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icodelete;
            this.pictureBox2.Location = new System.Drawing.Point(224, 26);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icoedit;
            this.pictureBox5.Location = new System.Drawing.Point(19, 26);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(16, 16);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox5.TabIndex = 2;
            this.pictureBox5.TabStop = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(25, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "For your information";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icoquestion;
            this.pictureBox6.Location = new System.Drawing.Point(3, 3);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(16, 16);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox6.TabIndex = 0;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBoxPoster
            // 
            this.pictureBoxPoster.BackColor = System.Drawing.Color.Black;
            this.pictureBoxPoster.Location = new System.Drawing.Point(436, 4);
            this.pictureBoxPoster.Name = "pictureBoxPoster";
            this.pictureBoxPoster.Size = new System.Drawing.Size(195, 195);
            this.pictureBoxPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPoster.TabIndex = 0;
            this.pictureBoxPoster.TabStop = false;
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(9, 300);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(75, 23);
            this.buttonNew.TabIndex = 9;
            this.buttonNew.Text = "New...";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(86, 300);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 10;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(163, 300);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 11;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(551, 300);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 12;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ManagePackageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(636, 328);
            this.ControlBox = false;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.panelTipEdit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxPoster);
            this.Controls.Add(this.labelNewPackage);
            this.Controls.Add(this.pictureBoxNewPackage);
            this.Controls.Add(this.dataGridViewPackage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManagePackageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Package Manager";
            this.Load += new System.EventHandler(this.ManagePackage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNewPackage)).EndInit();
            this.panelTipEdit.ResumeLayout(false);
            this.panelTipEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPackage;
        private System.Windows.Forms.PictureBox pictureBoxNewPackage;
        private System.Windows.Forms.Label labelNewPackage;
        private System.Windows.Forms.PictureBox pictureBoxPoster;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panelTipEdit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonClose;

    }
}