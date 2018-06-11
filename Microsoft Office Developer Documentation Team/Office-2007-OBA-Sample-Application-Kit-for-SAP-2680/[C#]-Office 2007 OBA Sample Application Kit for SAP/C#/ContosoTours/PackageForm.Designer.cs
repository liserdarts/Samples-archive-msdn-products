namespace Microsoft.SAPSK.ContosoTours
{
    partial class PackageForm
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.groupBoxAssociatedEvent = new System.Windows.Forms.GroupBox();
            this.dataGridViewEvents = new System.Windows.Forms.DataGridView();
            this.buttonDeleteAssociatedEvent = new System.Windows.Forms.Button();
            this.buttonNewAssociatedEvent = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxVenue = new System.Windows.Forms.TextBox();
            this.labelVenue = new System.Windows.Forms.Label();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelEventName = new System.Windows.Forms.Label();
            this.comboBoxName = new System.Windows.Forms.ComboBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.tabPagePicture = new System.Windows.Forms.TabPage();
            this.checkBoxStretch = new System.Windows.Forms.CheckBox();
            this.panelImage = new System.Windows.Forms.Panel();
            this.pictureBoxPoster = new System.Windows.Forms.PictureBox();
            this.linkLabelUploadPoster = new System.Windows.Forms.LinkLabel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.groupBoxAssociatedEvent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvents)).BeginInit();
            this.tabPagePicture.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(362, 430);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(443, 430);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Close";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "ImageFiles(*.gif,*.jpg,*.jpeg,*.bmp,*.wmf,*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*" +
                ".png|All Files (*.*)|*.*";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageMain);
            this.tabControl.Controls.Add(this.tabPagePicture);
            this.tabControl.Location = new System.Drawing.Point(5, 8);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(518, 416);
            this.tabControl.TabIndex = 10;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.textBoxDescription);
            this.tabPageMain.Controls.Add(this.textBoxName);
            this.tabPageMain.Controls.Add(this.groupBoxAssociatedEvent);
            this.tabPageMain.Controls.Add(this.labelDescription);
            this.tabPageMain.Controls.Add(this.labelName);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(510, 390);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Package Item";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(90, 41);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(395, 74);
            this.textBoxDescription.TabIndex = 2;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(90, 15);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(395, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // groupBoxAssociatedEvent
            // 
            this.groupBoxAssociatedEvent.Controls.Add(this.dataGridViewEvents);
            this.groupBoxAssociatedEvent.Controls.Add(this.buttonDeleteAssociatedEvent);
            this.groupBoxAssociatedEvent.Controls.Add(this.buttonNewAssociatedEvent);
            this.groupBoxAssociatedEvent.Controls.Add(this.buttonAdd);
            this.groupBoxAssociatedEvent.Controls.Add(this.textBoxVenue);
            this.groupBoxAssociatedEvent.Controls.Add(this.labelVenue);
            this.groupBoxAssociatedEvent.Controls.Add(this.textBoxDate);
            this.groupBoxAssociatedEvent.Controls.Add(this.labelDate);
            this.groupBoxAssociatedEvent.Controls.Add(this.labelEventName);
            this.groupBoxAssociatedEvent.Controls.Add(this.comboBoxName);
            this.groupBoxAssociatedEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAssociatedEvent.Location = new System.Drawing.Point(6, 121);
            this.groupBoxAssociatedEvent.Name = "groupBoxAssociatedEvent";
            this.groupBoxAssociatedEvent.Size = new System.Drawing.Size(498, 259);
            this.groupBoxAssociatedEvent.TabIndex = 2;
            this.groupBoxAssociatedEvent.TabStop = false;
            this.groupBoxAssociatedEvent.Text = "Associated Event";
            // 
            // dataGridViewEvents
            // 
            this.dataGridViewEvents.AllowUserToAddRows = false;
            this.dataGridViewEvents.AllowUserToDeleteRows = false;
            this.dataGridViewEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEvents.Location = new System.Drawing.Point(13, 19);
            this.dataGridViewEvents.Name = "dataGridViewEvents";
            this.dataGridViewEvents.ReadOnly = true;
            this.dataGridViewEvents.RowHeadersVisible = false;
            this.dataGridViewEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEvents.Size = new System.Drawing.Size(475, 209);
            this.dataGridViewEvents.TabIndex = 7;
            this.dataGridViewEvents.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEvents_RowEnter);
            // 
            // buttonDeleteAssociatedEvent
            // 
            this.buttonDeleteAssociatedEvent.Location = new System.Drawing.Point(91, 230);
            this.buttonDeleteAssociatedEvent.Name = "buttonDeleteAssociatedEvent";
            this.buttonDeleteAssociatedEvent.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteAssociatedEvent.TabIndex = 11;
            this.buttonDeleteAssociatedEvent.Text = "Delete";
            this.buttonDeleteAssociatedEvent.UseVisualStyleBackColor = true;
            this.buttonDeleteAssociatedEvent.Click += new System.EventHandler(this.buttonDeleteAssociatedEvent_Click);
            // 
            // buttonNewAssociatedEvent
            // 
            this.buttonNewAssociatedEvent.Location = new System.Drawing.Point(13, 230);
            this.buttonNewAssociatedEvent.Name = "buttonNewAssociatedEvent";
            this.buttonNewAssociatedEvent.Size = new System.Drawing.Size(75, 23);
            this.buttonNewAssociatedEvent.TabIndex = 10;
            this.buttonNewAssociatedEvent.Text = "New ...";
            this.buttonNewAssociatedEvent.UseVisualStyleBackColor = true;
            this.buttonNewAssociatedEvent.Click += new System.EventHandler(this.buttonNewAssociatedEvent_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(404, 22);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "Associate";
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // textBoxVenue
            // 
            this.textBoxVenue.Location = new System.Drawing.Point(85, 51);
            this.textBoxVenue.Name = "textBoxVenue";
            this.textBoxVenue.ReadOnly = true;
            this.textBoxVenue.Size = new System.Drawing.Size(313, 20);
            this.textBoxVenue.TabIndex = 4;
            // 
            // labelVenue
            // 
            this.labelVenue.AutoSize = true;
            this.labelVenue.Location = new System.Drawing.Point(10, 54);
            this.labelVenue.Name = "labelVenue";
            this.labelVenue.Size = new System.Drawing.Size(41, 13);
            this.labelVenue.TabIndex = 5;
            this.labelVenue.Text = "Venue:";
            // 
            // textBoxDate
            // 
            this.textBoxDate.Location = new System.Drawing.Point(84, 77);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.ReadOnly = true;
            this.textBoxDate.Size = new System.Drawing.Size(314, 20);
            this.textBoxDate.TabIndex = 5;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(10, 80);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(33, 13);
            this.labelDate.TabIndex = 3;
            this.labelDate.Text = "Date:";
            // 
            // labelEventName
            // 
            this.labelEventName.AutoSize = true;
            this.labelEventName.Location = new System.Drawing.Point(10, 27);
            this.labelEventName.Name = "labelEventName";
            this.labelEventName.Size = new System.Drawing.Size(38, 13);
            this.labelEventName.TabIndex = 2;
            this.labelEventName.Text = "Name:";
            // 
            // comboBoxName
            // 
            this.comboBoxName.FormattingEnabled = true;
            this.comboBoxName.Location = new System.Drawing.Point(85, 24);
            this.comboBoxName.Name = "comboBoxName";
            this.comboBoxName.Size = new System.Drawing.Size(313, 21);
            this.comboBoxName.TabIndex = 3;
            this.comboBoxName.SelectedIndexChanged += new System.EventHandler(this.comboBoxName_SelectedIndexChanged);
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(14, 41);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(63, 13);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "Description:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(14, 15);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";
            // 
            // tabPagePicture
            // 
            this.tabPagePicture.Controls.Add(this.checkBoxStretch);
            this.tabPagePicture.Controls.Add(this.panelImage);
            this.tabPagePicture.Controls.Add(this.linkLabelUploadPoster);
            this.tabPagePicture.Location = new System.Drawing.Point(4, 22);
            this.tabPagePicture.Name = "tabPagePicture";
            this.tabPagePicture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePicture.Size = new System.Drawing.Size(510, 390);
            this.tabPagePicture.TabIndex = 1;
            this.tabPagePicture.Text = "Poster";
            this.tabPagePicture.UseVisualStyleBackColor = true;
            // 
            // checkBoxStretch
            // 
            this.checkBoxStretch.AutoSize = true;
            this.checkBoxStretch.Location = new System.Drawing.Point(3, 7);
            this.checkBoxStretch.Name = "checkBoxStretch";
            this.checkBoxStretch.Size = new System.Drawing.Size(92, 17);
            this.checkBoxStretch.TabIndex = 4;
            this.checkBoxStretch.Text = "Stretch Image";
            this.checkBoxStretch.UseVisualStyleBackColor = true;
            this.checkBoxStretch.CheckedChanged += new System.EventHandler(this.checkBoxStretch_CheckedChanged);
            // 
            // panelImage
            // 
            this.panelImage.AutoScroll = true;
            this.panelImage.BackColor = System.Drawing.Color.Black;
            this.panelImage.Controls.Add(this.pictureBoxPoster);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelImage.Location = new System.Drawing.Point(3, 26);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(504, 361);
            this.panelImage.TabIndex = 3;
            // 
            // pictureBoxPoster
            // 
            this.pictureBoxPoster.BackColor = System.Drawing.Color.Black;
            this.pictureBoxPoster.Location = new System.Drawing.Point(3, 0);
            this.pictureBoxPoster.Name = "pictureBoxPoster";
            this.pictureBoxPoster.Size = new System.Drawing.Size(10, 10);
            this.pictureBoxPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxPoster.TabIndex = 0;
            this.pictureBoxPoster.TabStop = false;
            // 
            // linkLabelUploadPoster
            // 
            this.linkLabelUploadPoster.AutoSize = true;
            this.linkLabelUploadPoster.Location = new System.Drawing.Point(422, 7);
            this.linkLabelUploadPoster.Name = "linkLabelUploadPoster";
            this.linkLabelUploadPoster.Size = new System.Drawing.Size(82, 13);
            this.linkLabelUploadPoster.TabIndex = 2;
            this.linkLabelUploadPoster.TabStop = true;
            this.linkLabelUploadPoster.Text = "Upload poster...";
            this.linkLabelUploadPoster.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUploadPoster_LinkClicked);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // PackageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 458);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PackageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Package Editor";
            this.Load += new System.EventHandler(this.NewPackage_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tabPageMain.PerformLayout();
            this.groupBoxAssociatedEvent.ResumeLayout(false);
            this.groupBoxAssociatedEvent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvents)).EndInit();
            this.tabPagePicture.ResumeLayout(false);
            this.tabPagePicture.PerformLayout();
            this.panelImage.ResumeLayout(false);
            this.panelImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPagePicture;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.GroupBox groupBoxAssociatedEvent;
        private System.Windows.Forms.TextBox textBoxVenue;
        private System.Windows.Forms.Label labelVenue;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelEventName;
        private System.Windows.Forms.ComboBox comboBoxName;
        private System.Windows.Forms.DataGridView dataGridViewEvents;
        private System.Windows.Forms.LinkLabel linkLabelUploadPoster;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.PictureBox pictureBoxPoster;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.CheckBox checkBoxStretch;
        private System.Windows.Forms.Button buttonDeleteAssociatedEvent;
        private System.Windows.Forms.Button buttonNewAssociatedEvent;
    }
}