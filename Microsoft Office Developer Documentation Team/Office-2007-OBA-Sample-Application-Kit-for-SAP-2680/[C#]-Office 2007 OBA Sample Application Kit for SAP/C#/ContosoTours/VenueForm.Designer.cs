namespace Microsoft.SAPSK.ContosoTours
{
    partial class VenueForm
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
            this.dataGridViewVenue = new System.Windows.Forms.DataGridView();
            this.tabControlDetail = new System.Windows.Forms.TabControl();
            this.tabPageDetail = new System.Windows.Forms.TabPage();
            this.textBoxPostalCode = new System.Windows.Forms.TextBox();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.textBoxStreet = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelVenueName = new System.Windows.Forms.Label();
            this.tabVenueImage = new System.Windows.Forms.TabPage();
            this.checkBoxVenueImage = new System.Windows.Forms.CheckBox();
            this.panelVenueImage = new System.Windows.Forms.Panel();
            this.pictureBoxVenueImage = new System.Windows.Forms.PictureBox();
            this.linkLabelVenueImage = new System.Windows.Forms.LinkLabel();
            this.tabVenueLocationMap = new System.Windows.Forms.TabPage();
            this.checkBoxLocationMap = new System.Windows.Forms.CheckBox();
            this.panelLocationMap = new System.Windows.Forms.Panel();
            this.pictureBoxLocationMap = new System.Windows.Forms.PictureBox();
            this.linkLabelLocationMap = new System.Windows.Forms.LinkLabel();
            this.tabPageVenueFacitlity = new System.Windows.Forms.TabPage();
            this.checkBoxFacilityImage = new System.Windows.Forms.CheckBox();
            this.panelFacilityImage = new System.Windows.Forms.Panel();
            this.pictureBoxFacilityImage = new System.Windows.Forms.PictureBox();
            this.linkLabelFacilityImage = new System.Windows.Forms.LinkLabel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAddSave = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVenue)).BeginInit();
            this.tabControlDetail.SuspendLayout();
            this.tabPageDetail.SuspendLayout();
            this.tabVenueImage.SuspendLayout();
            this.panelVenueImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVenueImage)).BeginInit();
            this.tabVenueLocationMap.SuspendLayout();
            this.panelLocationMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLocationMap)).BeginInit();
            this.tabPageVenueFacitlity.SuspendLayout();
            this.panelFacilityImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFacilityImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewVenue
            // 
            this.dataGridViewVenue.AllowUserToAddRows = false;
            this.dataGridViewVenue.AllowUserToDeleteRows = false;
            this.dataGridViewVenue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVenue.Location = new System.Drawing.Point(12, 25);
            this.dataGridViewVenue.Name = "dataGridViewVenue";
            this.dataGridViewVenue.ReadOnly = true;
            this.dataGridViewVenue.RowHeadersVisible = false;
            this.dataGridViewVenue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewVenue.Size = new System.Drawing.Size(552, 119);
            this.dataGridViewVenue.TabIndex = 0;
            this.dataGridViewVenue.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewVenue_RowEnter);
            this.dataGridViewVenue.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewVenue_CellClick);
            // 
            // tabControlDetail
            // 
            this.tabControlDetail.Controls.Add(this.tabPageDetail);
            this.tabControlDetail.Controls.Add(this.tabVenueImage);
            this.tabControlDetail.Controls.Add(this.tabVenueLocationMap);
            this.tabControlDetail.Controls.Add(this.tabPageVenueFacitlity);
            this.tabControlDetail.Location = new System.Drawing.Point(13, 150);
            this.tabControlDetail.Name = "tabControlDetail";
            this.tabControlDetail.SelectedIndex = 0;
            this.tabControlDetail.Size = new System.Drawing.Size(552, 290);
            this.tabControlDetail.TabIndex = 1;
            // 
            // tabPageDetail
            // 
            this.tabPageDetail.Controls.Add(this.textBoxPostalCode);
            this.tabPageDetail.Controls.Add(this.textBoxCity);
            this.tabPageDetail.Controls.Add(this.textBoxState);
            this.tabPageDetail.Controls.Add(this.textBoxStreet);
            this.tabPageDetail.Controls.Add(this.label5);
            this.tabPageDetail.Controls.Add(this.label4);
            this.tabPageDetail.Controls.Add(this.label3);
            this.tabPageDetail.Controls.Add(this.label2);
            this.tabPageDetail.Controls.Add(this.label1);
            this.tabPageDetail.Controls.Add(this.textBoxDescription);
            this.tabPageDetail.Controls.Add(this.textBoxName);
            this.tabPageDetail.Controls.Add(this.labelVenueName);
            this.tabPageDetail.Location = new System.Drawing.Point(4, 22);
            this.tabPageDetail.Name = "tabPageDetail";
            this.tabPageDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDetail.Size = new System.Drawing.Size(544, 264);
            this.tabPageDetail.TabIndex = 0;
            this.tabPageDetail.Text = "Venue";
            this.tabPageDetail.UseVisualStyleBackColor = true;
            // 
            // textBoxPostalCode
            // 
            this.textBoxPostalCode.Location = new System.Drawing.Point(85, 228);
            this.textBoxPostalCode.MaxLength = 255;
            this.textBoxPostalCode.Name = "textBoxPostalCode";
            this.textBoxPostalCode.Size = new System.Drawing.Size(169, 20);
            this.textBoxPostalCode.TabIndex = 5;
            // 
            // textBoxCity
            // 
            this.textBoxCity.Location = new System.Drawing.Point(85, 176);
            this.textBoxCity.MaxLength = 255;
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(169, 20);
            this.textBoxCity.TabIndex = 3;
            // 
            // textBoxState
            // 
            this.textBoxState.Location = new System.Drawing.Point(85, 202);
            this.textBoxState.MaxLength = 255;
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.Size = new System.Drawing.Size(169, 20);
            this.textBoxState.TabIndex = 4;
            // 
            // textBoxStreet
            // 
            this.textBoxStreet.Location = new System.Drawing.Point(85, 130);
            this.textBoxStreet.MaxLength = 255;
            this.textBoxStreet.Multiline = true;
            this.textBoxStreet.Name = "textBoxStreet";
            this.textBoxStreet.Size = new System.Drawing.Size(436, 40);
            this.textBoxStreet.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Postal Code:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "State/Country:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "City:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Street:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(85, 43);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(436, 81);
            this.textBoxDescription.TabIndex = 1;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(85, 17);
            this.textBoxName.MaxLength = 255;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(436, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // labelVenueName
            // 
            this.labelVenueName.AutoSize = true;
            this.labelVenueName.Location = new System.Drawing.Point(9, 20);
            this.labelVenueName.Name = "labelVenueName";
            this.labelVenueName.Size = new System.Drawing.Size(38, 13);
            this.labelVenueName.TabIndex = 0;
            this.labelVenueName.Text = "Name:";
            // 
            // tabVenueImage
            // 
            this.tabVenueImage.Controls.Add(this.checkBoxVenueImage);
            this.tabVenueImage.Controls.Add(this.panelVenueImage);
            this.tabVenueImage.Controls.Add(this.linkLabelVenueImage);
            this.tabVenueImage.Location = new System.Drawing.Point(4, 22);
            this.tabVenueImage.Name = "tabVenueImage";
            this.tabVenueImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabVenueImage.Size = new System.Drawing.Size(544, 264);
            this.tabVenueImage.TabIndex = 4;
            this.tabVenueImage.Text = "Venue Image";
            this.tabVenueImage.UseVisualStyleBackColor = true;
            // 
            // checkBoxVenueImage
            // 
            this.checkBoxVenueImage.AutoSize = true;
            this.checkBoxVenueImage.Location = new System.Drawing.Point(3, 3);
            this.checkBoxVenueImage.Name = "checkBoxVenueImage";
            this.checkBoxVenueImage.Size = new System.Drawing.Size(92, 17);
            this.checkBoxVenueImage.TabIndex = 14;
            this.checkBoxVenueImage.Text = "Stretch Image";
            this.checkBoxVenueImage.UseVisualStyleBackColor = true;
            this.checkBoxVenueImage.CheckedChanged += new System.EventHandler(this.checkBoxVenueImage_CheckedChanged);
            // 
            // panelVenueImage
            // 
            this.panelVenueImage.AutoScroll = true;
            this.panelVenueImage.BackColor = System.Drawing.Color.Black;
            this.panelVenueImage.Controls.Add(this.pictureBoxVenueImage);
            this.panelVenueImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelVenueImage.Location = new System.Drawing.Point(3, 26);
            this.panelVenueImage.Name = "panelVenueImage";
            this.panelVenueImage.Size = new System.Drawing.Size(538, 235);
            this.panelVenueImage.TabIndex = 13;
            // 
            // pictureBoxVenueImage
            // 
            this.pictureBoxVenueImage.BackColor = System.Drawing.Color.Black;
            this.pictureBoxVenueImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxVenueImage.Name = "pictureBoxVenueImage";
            this.pictureBoxVenueImage.Size = new System.Drawing.Size(10, 10);
            this.pictureBoxVenueImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxVenueImage.TabIndex = 0;
            this.pictureBoxVenueImage.TabStop = false;
            // 
            // linkLabelVenueImage
            // 
            this.linkLabelVenueImage.AutoSize = true;
            this.linkLabelVenueImage.Location = new System.Drawing.Point(448, 3);
            this.linkLabelVenueImage.Name = "linkLabelVenueImage";
            this.linkLabelVenueImage.Size = new System.Drawing.Size(82, 13);
            this.linkLabelVenueImage.TabIndex = 12;
            this.linkLabelVenueImage.TabStop = true;
            this.linkLabelVenueImage.Text = "Upload poster...";
            this.linkLabelVenueImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelVenueImage_LinkClicked);
            // 
            // tabVenueLocationMap
            // 
            this.tabVenueLocationMap.Controls.Add(this.checkBoxLocationMap);
            this.tabVenueLocationMap.Controls.Add(this.panelLocationMap);
            this.tabVenueLocationMap.Controls.Add(this.linkLabelLocationMap);
            this.tabVenueLocationMap.Location = new System.Drawing.Point(4, 22);
            this.tabVenueLocationMap.Name = "tabVenueLocationMap";
            this.tabVenueLocationMap.Size = new System.Drawing.Size(544, 264);
            this.tabVenueLocationMap.TabIndex = 2;
            this.tabVenueLocationMap.Text = "Venue Location Map";
            this.tabVenueLocationMap.UseVisualStyleBackColor = true;
            // 
            // checkBoxLocationMap
            // 
            this.checkBoxLocationMap.AutoSize = true;
            this.checkBoxLocationMap.Location = new System.Drawing.Point(3, 3);
            this.checkBoxLocationMap.Name = "checkBoxLocationMap";
            this.checkBoxLocationMap.Size = new System.Drawing.Size(92, 17);
            this.checkBoxLocationMap.TabIndex = 8;
            this.checkBoxLocationMap.Text = "Stretch Image";
            this.checkBoxLocationMap.UseVisualStyleBackColor = true;
            this.checkBoxLocationMap.CheckedChanged += new System.EventHandler(this.checkBoxLocationMap_CheckedChanged);
            // 
            // panelLocationMap
            // 
            this.panelLocationMap.AutoScroll = true;
            this.panelLocationMap.BackColor = System.Drawing.Color.Black;
            this.panelLocationMap.Controls.Add(this.pictureBoxLocationMap);
            this.panelLocationMap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLocationMap.Location = new System.Drawing.Point(0, 26);
            this.panelLocationMap.Name = "panelLocationMap";
            this.panelLocationMap.Size = new System.Drawing.Size(544, 238);
            this.panelLocationMap.TabIndex = 7;
            // 
            // pictureBoxLocationMap
            // 
            this.pictureBoxLocationMap.BackColor = System.Drawing.Color.Black;
            this.pictureBoxLocationMap.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLocationMap.Name = "pictureBoxLocationMap";
            this.pictureBoxLocationMap.Size = new System.Drawing.Size(10, 10);
            this.pictureBoxLocationMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxLocationMap.TabIndex = 0;
            this.pictureBoxLocationMap.TabStop = false;
            // 
            // linkLabelLocationMap
            // 
            this.linkLabelLocationMap.AutoSize = true;
            this.linkLabelLocationMap.Location = new System.Drawing.Point(448, 3);
            this.linkLabelLocationMap.Name = "linkLabelLocationMap";
            this.linkLabelLocationMap.Size = new System.Drawing.Size(82, 13);
            this.linkLabelLocationMap.TabIndex = 6;
            this.linkLabelLocationMap.TabStop = true;
            this.linkLabelLocationMap.Text = "Upload poster...";
            this.linkLabelLocationMap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLocationMap_LinkClicked);
            // 
            // tabPageVenueFacitlity
            // 
            this.tabPageVenueFacitlity.Controls.Add(this.checkBoxFacilityImage);
            this.tabPageVenueFacitlity.Controls.Add(this.panelFacilityImage);
            this.tabPageVenueFacitlity.Controls.Add(this.linkLabelFacilityImage);
            this.tabPageVenueFacitlity.Location = new System.Drawing.Point(4, 22);
            this.tabPageVenueFacitlity.Name = "tabPageVenueFacitlity";
            this.tabPageVenueFacitlity.Size = new System.Drawing.Size(544, 264);
            this.tabPageVenueFacitlity.TabIndex = 3;
            this.tabPageVenueFacitlity.Text = "Venue Facility Image";
            this.tabPageVenueFacitlity.UseVisualStyleBackColor = true;
            // 
            // checkBoxFacilityImage
            // 
            this.checkBoxFacilityImage.AutoSize = true;
            this.checkBoxFacilityImage.Location = new System.Drawing.Point(3, 3);
            this.checkBoxFacilityImage.Name = "checkBoxFacilityImage";
            this.checkBoxFacilityImage.Size = new System.Drawing.Size(92, 17);
            this.checkBoxFacilityImage.TabIndex = 11;
            this.checkBoxFacilityImage.Text = "Stretch Image";
            this.checkBoxFacilityImage.UseVisualStyleBackColor = true;
            this.checkBoxFacilityImage.CheckedChanged += new System.EventHandler(this.checkBoxFacilityImage_CheckedChanged);
            // 
            // panelFacilityImage
            // 
            this.panelFacilityImage.AutoScroll = true;
            this.panelFacilityImage.BackColor = System.Drawing.Color.Black;
            this.panelFacilityImage.Controls.Add(this.pictureBoxFacilityImage);
            this.panelFacilityImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFacilityImage.Location = new System.Drawing.Point(0, 26);
            this.panelFacilityImage.Name = "panelFacilityImage";
            this.panelFacilityImage.Size = new System.Drawing.Size(544, 238);
            this.panelFacilityImage.TabIndex = 10;
            // 
            // pictureBoxFacilityImage
            // 
            this.pictureBoxFacilityImage.BackColor = System.Drawing.Color.Black;
            this.pictureBoxFacilityImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFacilityImage.Name = "pictureBoxFacilityImage";
            this.pictureBoxFacilityImage.Size = new System.Drawing.Size(10, 10);
            this.pictureBoxFacilityImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxFacilityImage.TabIndex = 0;
            this.pictureBoxFacilityImage.TabStop = false;
            // 
            // linkLabelFacilityImage
            // 
            this.linkLabelFacilityImage.AutoSize = true;
            this.linkLabelFacilityImage.Location = new System.Drawing.Point(448, 3);
            this.linkLabelFacilityImage.Name = "linkLabelFacilityImage";
            this.linkLabelFacilityImage.Size = new System.Drawing.Size(82, 13);
            this.linkLabelFacilityImage.TabIndex = 9;
            this.linkLabelFacilityImage.TabStop = true;
            this.linkLabelFacilityImage.Text = "Upload poster...";
            this.linkLabelFacilityImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFacilityImage_LinkClicked);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(486, 449);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Close";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAddSave
            // 
            this.buttonAddSave.Location = new System.Drawing.Point(288, 449);
            this.buttonAddSave.Name = "buttonAddSave";
            this.buttonAddSave.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSave.TabIndex = 2;
            this.buttonAddSave.Text = "Add";
            this.buttonAddSave.UseVisualStyleBackColor = true;
            this.buttonAddSave.Visible = false;
            this.buttonAddSave.Click += new System.EventHandler(this.buttonAddSave_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "ImageFiles(*.gif,*.jpg,*.jpeg,*.bmp,*.wmf,*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*" +
                ".png|All Files (*.*)|*.*";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Venue List";
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(14, 449);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(75, 23);
            this.buttonNew.TabIndex = 4;
            this.buttonNew.Text = "New";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(95, 449);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 5;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(176, 449);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 6;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(405, 449);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Visible = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // VenueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 479);
            this.ControlBox = false;
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridViewVenue);
            this.Controls.Add(this.buttonAddSave);
            this.Controls.Add(this.tabControlDetail);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "VenueForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Venues";
            this.Load += new System.EventHandler(this.VenueForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVenue)).EndInit();
            this.tabControlDetail.ResumeLayout(false);
            this.tabPageDetail.ResumeLayout(false);
            this.tabPageDetail.PerformLayout();
            this.tabVenueImage.ResumeLayout(false);
            this.tabVenueImage.PerformLayout();
            this.panelVenueImage.ResumeLayout(false);
            this.panelVenueImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVenueImage)).EndInit();
            this.tabVenueLocationMap.ResumeLayout(false);
            this.tabVenueLocationMap.PerformLayout();
            this.panelLocationMap.ResumeLayout(false);
            this.panelLocationMap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLocationMap)).EndInit();
            this.tabPageVenueFacitlity.ResumeLayout(false);
            this.tabPageVenueFacitlity.PerformLayout();
            this.panelFacilityImage.ResumeLayout(false);
            this.panelFacilityImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFacilityImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewVenue;
        private System.Windows.Forms.TabControl tabControlDetail;
        private System.Windows.Forms.TabPage tabPageDetail;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAddSave;
        private System.Windows.Forms.TabPage tabVenueLocationMap;
        private System.Windows.Forms.TabPage tabPageVenueFacitlity;
        private System.Windows.Forms.Panel panelLocationMap;
        private System.Windows.Forms.PictureBox pictureBoxLocationMap;
        private System.Windows.Forms.LinkLabel linkLabelLocationMap;
        private System.Windows.Forms.CheckBox checkBoxLocationMap;
        private System.Windows.Forms.CheckBox checkBoxFacilityImage;
        private System.Windows.Forms.Panel panelFacilityImage;
        private System.Windows.Forms.PictureBox pictureBoxFacilityImage;
        private System.Windows.Forms.LinkLabel linkLabelFacilityImage;
        private System.Windows.Forms.TabPage tabVenueImage;
        private System.Windows.Forms.CheckBox checkBoxVenueImage;
        private System.Windows.Forms.Panel panelVenueImage;
        private System.Windows.Forms.PictureBox pictureBoxVenueImage;
        private System.Windows.Forms.LinkLabel linkLabelVenueImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label labelVenueName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxPostalCode;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.TextBox textBoxState;
        private System.Windows.Forms.TextBox textBoxStreet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonSave;
    }
}