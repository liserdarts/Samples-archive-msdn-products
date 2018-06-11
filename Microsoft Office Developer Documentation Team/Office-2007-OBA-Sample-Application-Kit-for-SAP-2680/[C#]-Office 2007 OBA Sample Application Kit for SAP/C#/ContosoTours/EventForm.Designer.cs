namespace Microsoft.SAPSK.ContosoTours
{
    partial class EventForm
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
            components = new System.ComponentModel.Container();
            tabControl = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            comboBoxEventType = new System.Windows.Forms.ComboBox();
            labelEventType = new System.Windows.Forms.Label();
            dateTimePickerSchedule = new System.Windows.Forms.DateTimePicker();
            monthCalendarSchedule = new System.Windows.Forms.MonthCalendar();
            textBoxName = new System.Windows.Forms.TextBox();
            dataGridViewPackage = new System.Windows.Forms.DataGridView();
            label5 = new System.Windows.Forms.Label();
            comboBoxVenue = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            textBoxDescription = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            tabPage2 = new System.Windows.Forms.TabPage();
            checkBoxStretch = new System.Windows.Forms.CheckBox();
            panelPoster = new System.Windows.Forms.Panel();
            pictureBoxPoster = new System.Windows.Forms.PictureBox();
            linkLabelUploadPoster = new System.Windows.Forms.LinkLabel();
            tabPage3 = new System.Windows.Forms.TabPage();
            comboBoxActor = new System.Windows.Forms.ComboBox();
            dataGridViewActor = new System.Windows.Forms.DataGridView();
            ColumnActorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnActorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnDelete = new System.Windows.Forms.DataGridViewImageColumn();
            buttonAddToList = new System.Windows.Forms.Button();
            label7 = new System.Windows.Forms.Label();
            buttonCancel = new System.Windows.Forms.Button();
            buttonSave = new System.Windows.Forms.Button();
            openFileDialog = new System.Windows.Forms.OpenFileDialog();
            errorProvider = new System.Windows.Forms.ErrorProvider(components);
            pictureBoxNewVenue = new System.Windows.Forms.PictureBox();
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewPackage)).BeginInit();
            tabPage2.SuspendLayout();
            panelPoster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxPoster)).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewActor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxNewVenue)).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Controls.Add(tabPage3);
            tabControl.Location = new System.Drawing.Point(3, 4);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(494, 400);
            tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(pictureBoxNewVenue);
            tabPage1.Controls.Add(comboBoxEventType);
            tabPage1.Controls.Add(labelEventType);
            tabPage1.Controls.Add(dateTimePickerSchedule);
            tabPage1.Controls.Add(monthCalendarSchedule);
            tabPage1.Controls.Add(textBoxName);
            tabPage1.Controls.Add(dataGridViewPackage);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(comboBoxVenue);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(textBoxDescription);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(label4);
            tabPage1.Location = new System.Drawing.Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(486, 374);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Event";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBoxEventType
            // 
            comboBoxEventType.FormattingEnabled = true;
            comboBoxEventType.Location = new System.Drawing.Point(11, 127);
            comboBoxEventType.Name = "comboBoxEventType";
            comboBoxEventType.Size = new System.Drawing.Size(189, 21);
            comboBoxEventType.TabIndex = 27;
            // 
            // labelEventType
            // 
            labelEventType.AutoSize = true;
            labelEventType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelEventType.Location = new System.Drawing.Point(11, 107);
            labelEventType.Name = "labelEventType";
            labelEventType.Size = new System.Drawing.Size(62, 13);
            labelEventType.TabIndex = 26;
            labelEventType.Text = "Event Type";
            // 
            // dateTimePickerSchedule
            // 
            dateTimePickerSchedule.CustomFormat = "";
            dateTimePickerSchedule.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            dateTimePickerSchedule.Location = new System.Drawing.Point(11, 337);
            dateTimePickerSchedule.Name = "dateTimePickerSchedule";
            dateTimePickerSchedule.ShowUpDown = true;
            dateTimePickerSchedule.Size = new System.Drawing.Size(178, 20);
            dateTimePickerSchedule.TabIndex = 4;
            // 
            // monthCalendarSchedule
            // 
            monthCalendarSchedule.Location = new System.Drawing.Point(11, 175);
            monthCalendarSchedule.Name = "monthCalendarSchedule";
            monthCalendarSchedule.ShowToday = false;
            monthCalendarSchedule.TabIndex = 3;
            monthCalendarSchedule.TodayDate = new System.DateTime(2006, 12, 29, 0, 0, 0, 0);
            // 
            // textBoxName
            // 
            textBoxName.Location = new System.Drawing.Point(11, 32);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(189, 20);
            textBoxName.TabIndex = 1;
            // 
            // dataGridViewPackage
            // 
            dataGridViewPackage.AllowUserToAddRows = false;
            dataGridViewPackage.AllowUserToDeleteRows = false;
            dataGridViewPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPackage.Location = new System.Drawing.Point(221, 30);
            dataGridViewPackage.Name = "dataGridViewPackage";
            dataGridViewPackage.RowHeadersVisible = false;
            dataGridViewPackage.Size = new System.Drawing.Size(246, 127);
            dataGridViewPackage.TabIndex = 5;
            dataGridViewPackage.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(dataGridViewPackage_CellValueNeeded);
            dataGridViewPackage.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(dataGridViewPackage_CellValuePushed);
            // 
            // label5
            // 
            label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(218, 12);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(104, 13);
            label5.TabIndex = 25;
            label5.Text = "Packages";
            // 
            // comboBoxVenue
            // 
            comboBoxVenue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxVenue.FormattingEnabled = true;
            comboBoxVenue.Location = new System.Drawing.Point(11, 79);
            comboBoxVenue.Name = "comboBoxVenue";
            comboBoxVenue.Size = new System.Drawing.Size(162, 21);
            comboBoxVenue.TabIndex = 2;
            // 
            // label2
            // 
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(218, 170);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(104, 13);
            label2.TabIndex = 18;
            label2.Text = "Description";
            // 
            // label3
            // 
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(11, 155);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(98, 13);
            label3.TabIndex = 20;
            label3.Text = "Schedule";
            // 
            // textBoxDescription
            // 
            textBoxDescription.Location = new System.Drawing.Point(221, 187);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBoxDescription.Size = new System.Drawing.Size(246, 170);
            textBoxDescription.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(11, 12);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(35, 13);
            label1.TabIndex = 17;
            label1.Text = "Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(11, 59);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(38, 13);
            label4.TabIndex = 22;
            label4.Text = "Venue";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(checkBoxStretch);
            tabPage2.Controls.Add(panelPoster);
            tabPage2.Controls.Add(linkLabelUploadPoster);
            tabPage2.Location = new System.Drawing.Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(486, 374);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Poster";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxStretch
            // 
            checkBoxStretch.AutoSize = true;
            checkBoxStretch.Location = new System.Drawing.Point(3, 7);
            checkBoxStretch.Name = "checkBoxStretch";
            checkBoxStretch.Size = new System.Drawing.Size(92, 17);
            checkBoxStretch.TabIndex = 3;
            checkBoxStretch.Text = "Stretch Image";
            checkBoxStretch.UseVisualStyleBackColor = true;
            checkBoxStretch.CheckedChanged += new System.EventHandler(checkBoxStretch_CheckedChanged);
            // 
            // panelPoster
            // 
            panelPoster.AutoScroll = true;
            panelPoster.BackColor = System.Drawing.Color.Black;
            panelPoster.Controls.Add(pictureBoxPoster);
            panelPoster.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelPoster.Location = new System.Drawing.Point(3, 29);
            panelPoster.Name = "panelPoster";
            panelPoster.Size = new System.Drawing.Size(480, 342);
            panelPoster.TabIndex = 2;
            // 
            // pictureBoxPoster
            // 
            pictureBoxPoster.BackColor = System.Drawing.Color.Black;
            pictureBoxPoster.Location = new System.Drawing.Point(0, 0);
            pictureBoxPoster.Name = "pictureBoxPoster";
            pictureBoxPoster.Size = new System.Drawing.Size(10, 10);
            pictureBoxPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBoxPoster.TabIndex = 0;
            pictureBoxPoster.TabStop = false;
            // 
            // linkLabelUploadPoster
            // 
            linkLabelUploadPoster.AutoSize = true;
            linkLabelUploadPoster.Location = new System.Drawing.Point(390, 6);
            linkLabelUploadPoster.Name = "linkLabelUploadPoster";
            linkLabelUploadPoster.Size = new System.Drawing.Size(82, 13);
            linkLabelUploadPoster.TabIndex = 1;
            linkLabelUploadPoster.TabStop = true;
            linkLabelUploadPoster.Text = "Upload poster...";
            linkLabelUploadPoster.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabelUploadPoster_LinkClicked);
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(comboBoxActor);
            tabPage3.Controls.Add(dataGridViewActor);
            tabPage3.Controls.Add(buttonAddToList);
            tabPage3.Controls.Add(label7);
            tabPage3.Location = new System.Drawing.Point(4, 22);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new System.Drawing.Size(486, 374);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Actor";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // comboBoxActor
            // 
            comboBoxActor.FormattingEnabled = true;
            comboBoxActor.Location = new System.Drawing.Point(12, 29);
            comboBoxActor.Name = "comboBoxActor";
            comboBoxActor.Size = new System.Drawing.Size(360, 21);
            comboBoxActor.TabIndex = 23;
            // 
            // dataGridViewActor
            // 
            dataGridViewActor.AllowUserToAddRows = false;
            dataGridViewActor.AllowUserToDeleteRows = false;
            dataGridViewActor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewActor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            ColumnActorName,
            ColumnActorID,
            ColumnDelete});
            dataGridViewActor.Location = new System.Drawing.Point(12, 66);
            dataGridViewActor.Name = "dataGridViewActor";
            dataGridViewActor.ReadOnly = true;
            dataGridViewActor.RowHeadersVisible = false;
            dataGridViewActor.RowHeadersWidth = 10;
            dataGridViewActor.Size = new System.Drawing.Size(460, 291);
            dataGridViewActor.TabIndex = 22;
            dataGridViewActor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridViewActor_CellClick);
            // 
            // ColumnActorName
            // 
            ColumnActorName.DataPropertyName = "ActorName";
            ColumnActorName.HeaderText = "Actor";
            ColumnActorName.Name = "ColumnActorName";
            ColumnActorName.ReadOnly = true;
            ColumnActorName.Width = 435;
            // 
            // ColumnActorID
            // 
            ColumnActorID.DataPropertyName = "ActorID";
            ColumnActorID.HeaderText = "ActorID";
            ColumnActorID.Name = "ColumnActorID";
            ColumnActorID.ReadOnly = true;
            ColumnActorID.Visible = false;
            // 
            // ColumnDelete
            // 
            ColumnDelete.HeaderText = "";
            ColumnDelete.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icodelete;
            ColumnDelete.Name = "ColumnDelete";
            ColumnDelete.ReadOnly = true;
            ColumnDelete.Width = 25;
            // 
            // buttonAddToList
            // 
            buttonAddToList.Location = new System.Drawing.Point(378, 27);
            buttonAddToList.Name = "buttonAddToList";
            buttonAddToList.Size = new System.Drawing.Size(94, 23);
            buttonAddToList.TabIndex = 21;
            buttonAddToList.Text = "Add To List";
            buttonAddToList.UseVisualStyleBackColor = true;
            buttonAddToList.Click += new System.EventHandler(buttonAddToList_Click);
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.ForeColor = System.Drawing.Color.Gray;
            label7.Location = new System.Drawing.Point(9, 11);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(43, 13);
            label7.TabIndex = 18;
            label7.Text = "Actors";
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new System.Drawing.Point(399, 410);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(75, 23);
            buttonCancel.TabIndex = 8;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
            // 
            // buttonSave
            // 
            buttonSave.Location = new System.Drawing.Point(318, 410);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new System.Drawing.Size(75, 23);
            buttonSave.TabIndex = 7;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += new System.EventHandler(buttonSave_Click);
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = "ImageFiles(*.gif,*.jpg,*.jpeg,*.bmp,*.wmf,*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*" +
                ".png|All Files (*.*)|*.*";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // pictureBoxNewVenue
            // 
            pictureBoxNewVenue.Location = new System.Drawing.Point(180, 79);
            pictureBoxNewVenue.Name = "pictureBoxNewVenue";
            pictureBoxNewVenue.Size = new System.Drawing.Size(20, 20);
            pictureBoxNewVenue.TabIndex = 28;
            pictureBoxNewVenue.TabStop = false;
            pictureBoxNewVenue.MouseLeave += new System.EventHandler(pictureBoxNewVenue_MouseLeave);
            pictureBoxNewVenue.Click += new System.EventHandler(pictureBoxNewVenue_Click);
            pictureBoxNewVenue.MouseHover += new System.EventHandler(pictureBoxNewVenue_MouseHover);
            // 
            // EventForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(499, 445);
            ControlBox = false;
            Controls.Add(buttonSave);
            Controls.Add(buttonCancel);
            Controls.Add(tabControl);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "EventForm";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = " Event";
            Load += new System.EventHandler(frmEvent_Load);
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewPackage)).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            panelPoster.ResumeLayout(false);
            panelPoster.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxPoster)).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewActor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxNewVenue)).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DateTimePicker dateTimePickerSchedule;
        private System.Windows.Forms.MonthCalendar monthCalendarSchedule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.DataGridView dataGridViewPackage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxVenue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.PictureBox pictureBoxPoster;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.LinkLabel linkLabelUploadPoster;
        private System.Windows.Forms.Panel panelPoster;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ComboBox comboBoxEventType;
        private System.Windows.Forms.Label labelEventType;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button buttonAddToList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridViewActor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnActorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnActorID;
        private System.Windows.Forms.DataGridViewImageColumn ColumnDelete;
        private System.Windows.Forms.ComboBox comboBoxActor;
        private System.Windows.Forms.CheckBox checkBoxStretch;
        private System.Windows.Forms.PictureBox pictureBoxNewVenue;
    }
}