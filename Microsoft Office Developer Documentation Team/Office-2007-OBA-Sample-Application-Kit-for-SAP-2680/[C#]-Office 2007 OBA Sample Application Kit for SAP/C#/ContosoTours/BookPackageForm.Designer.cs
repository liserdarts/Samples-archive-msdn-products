namespace Microsoft.SAPSK.ContosoTours
{
    partial class BookPackageForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookPackageForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBoxCustomer = new System.Windows.Forms.GroupBox();
            this.comboBoxCustomername = new System.Windows.Forms.ComboBox();
            this.labelPostalCode = new System.Windows.Forms.Label();
            this.textBoxPostalCode = new System.Windows.Forms.TextBox();
            this.labelPhone = new System.Windows.Forms.Label();
            this.textboxPhone = new System.Windows.Forms.TextBox();
            this.textboxAddress = new System.Windows.Forms.TextBox();
            this.labelAddress = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.comboBoxCountry = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCountry = new System.Windows.Forms.Label();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.labelCity = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.groupBoxFlight = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewEvent = new System.Windows.Forms.DataGridView();
            this.ColumnAddFlightImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnEventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEventVenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEventDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFlightDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDepartureTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAirport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBoxCustomer.SuspendLayout();
            this.groupBoxFlight.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxCustomer
            // 
            this.groupBoxCustomer.Controls.Add(this.comboBoxCustomername);
            this.groupBoxCustomer.Controls.Add(this.labelPostalCode);
            this.groupBoxCustomer.Controls.Add(this.textBoxPostalCode);
            this.groupBoxCustomer.Controls.Add(this.labelPhone);
            this.groupBoxCustomer.Controls.Add(this.textboxPhone);
            this.groupBoxCustomer.Controls.Add(this.textboxAddress);
            this.groupBoxCustomer.Controls.Add(this.labelAddress);
            this.groupBoxCustomer.Controls.Add(this.dateTimePicker);
            this.groupBoxCustomer.Controls.Add(this.label1);
            this.groupBoxCustomer.Controls.Add(this.comboBoxLanguage);
            this.groupBoxCustomer.Controls.Add(this.comboBoxCountry);
            this.groupBoxCustomer.Controls.Add(this.label2);
            this.groupBoxCustomer.Controls.Add(this.labelCountry);
            this.groupBoxCustomer.Controls.Add(this.textBoxCity);
            this.groupBoxCustomer.Controls.Add(this.labelCity);
            this.groupBoxCustomer.Controls.Add(this.labelName);
            this.groupBoxCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxCustomer.Location = new System.Drawing.Point(9, 6);
            this.groupBoxCustomer.Name = "groupBoxCustomer";
            this.groupBoxCustomer.Size = new System.Drawing.Size(563, 192);
            this.groupBoxCustomer.TabIndex = 1;
            this.groupBoxCustomer.TabStop = false;
            this.groupBoxCustomer.Text = "Customer";
            // 
            // comboBoxCustomername
            // 
            this.comboBoxCustomername.FormattingEnabled = true;
            this.comboBoxCustomername.Location = new System.Drawing.Point(94, 16);
            this.comboBoxCustomername.Name = "comboBoxCustomername";
            this.comboBoxCustomername.Size = new System.Drawing.Size(459, 21);
            this.comboBoxCustomername.TabIndex = 0;
            this.comboBoxCustomername.SelectedIndexChanged += new System.EventHandler(this.comboBoxCustomername_SelectedIndexChanged);
            this.comboBoxCustomername.Validated += new System.EventHandler(this.comboBoxCustomername_Validated);
            // 
            // labelPostalCode
            // 
            this.labelPostalCode.AutoSize = true;
            this.labelPostalCode.Location = new System.Drawing.Point(294, 168);
            this.labelPostalCode.Name = "labelPostalCode";
            this.labelPostalCode.Size = new System.Drawing.Size(67, 13);
            this.labelPostalCode.TabIndex = 15;
            this.labelPostalCode.Text = "Postal Code:";
            // 
            // textBoxPostalCode
            // 
            this.textBoxPostalCode.Location = new System.Drawing.Point(372, 163);
            this.textBoxPostalCode.Name = "textBoxPostalCode";
            this.textBoxPostalCode.Size = new System.Drawing.Size(181, 20);
            this.textBoxPostalCode.TabIndex = 7;
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Location = new System.Drawing.Point(12, 168);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(71, 13);
            this.labelPhone.TabIndex = 13;
            this.labelPhone.Text = "Telephone# :";
            // 
            // textboxPhone
            // 
            this.textboxPhone.Location = new System.Drawing.Point(94, 165);
            this.textboxPhone.Name = "textboxPhone";
            this.textboxPhone.Size = new System.Drawing.Size(181, 20);
            this.textboxPhone.TabIndex = 6;
            // 
            // textboxAddress
            // 
            this.textboxAddress.Location = new System.Drawing.Point(94, 45);
            this.textboxAddress.Multiline = true;
            this.textboxAddress.Name = "textboxAddress";
            this.textboxAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxAddress.Size = new System.Drawing.Size(459, 55);
            this.textboxAddress.TabIndex = 1;
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(12, 53);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(48, 13);
            this.labelAddress.TabIndex = 10;
            this.labelAddress.Text = "Address:";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker.Location = new System.Drawing.Point(94, 135);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(88, 20);
            this.dateTimePicker.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Date of Birth:";
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(372, 136);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(181, 21);
            this.comboBoxLanguage.TabIndex = 5;
            // 
            // comboBoxCountry
            // 
            this.comboBoxCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCountry.FormattingEnabled = true;
            this.comboBoxCountry.Location = new System.Drawing.Point(372, 108);
            this.comboBoxCountry.Name = "comboBoxCountry";
            this.comboBoxCountry.Size = new System.Drawing.Size(181, 21);
            this.comboBoxCountry.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(294, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Language:";
            // 
            // labelCountry
            // 
            this.labelCountry.AutoSize = true;
            this.labelCountry.Location = new System.Drawing.Point(294, 111);
            this.labelCountry.Name = "labelCountry";
            this.labelCountry.Size = new System.Drawing.Size(46, 13);
            this.labelCountry.TabIndex = 4;
            this.labelCountry.Text = "Country:";
            // 
            // textBoxCity
            // 
            this.textBoxCity.Location = new System.Drawing.Point(94, 109);
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(181, 20);
            this.textBoxCity.TabIndex = 2;
            // 
            // labelCity
            // 
            this.labelCity.AutoSize = true;
            this.labelCity.Location = new System.Drawing.Point(12, 112);
            this.labelCity.Name = "labelCity";
            this.labelCity.Size = new System.Drawing.Size(27, 13);
            this.labelCity.TabIndex = 1;
            this.labelCity.Text = "City:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 22);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";
            // 
            // groupBoxFlight
            // 
            this.groupBoxFlight.Controls.Add(this.panel1);
            this.groupBoxFlight.Controls.Add(this.dataGridViewEvent);
            this.groupBoxFlight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxFlight.Location = new System.Drawing.Point(9, 203);
            this.groupBoxFlight.Name = "groupBoxFlight";
            this.groupBoxFlight.Size = new System.Drawing.Size(563, 192);
            this.groupBoxFlight.TabIndex = 2;
            this.groupBoxFlight.TabStop = false;
            this.groupBoxFlight.Text = "Itinerary";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(10, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 23);
            this.panel1.TabIndex = 11;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.airplane;
            this.pictureBox2.Location = new System.Drawing.Point(70, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(19, 19);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icoquestion;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Click on         to book a flight for the event.";
            // 
            // dataGridViewEvent
            // 
            this.dataGridViewEvent.AllowUserToAddRows = false;
            this.dataGridViewEvent.AllowUserToDeleteRows = false;
            this.dataGridViewEvent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEvent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAddFlightImage,
            this.ColumnEventName,
            this.ColumnEventVenue,
            this.ColumnEventDate,
            this.ColumnFlightDate,
            this.ColumnDepartureTime,
            this.ColumnAirport});
            this.dataGridViewEvent.Location = new System.Drawing.Point(10, 19);
            this.dataGridViewEvent.Name = "dataGridViewEvent";
            this.dataGridViewEvent.RowHeadersVisible = false;
            this.dataGridViewEvent.Size = new System.Drawing.Size(543, 138);
            this.dataGridViewEvent.TabIndex = 8;
            this.dataGridViewEvent.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEvent_CellMouseLeave);
            this.dataGridViewEvent.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEvent_CellMouseEnter);
            this.dataGridViewEvent.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEvent_CellClick);
            // 
            // ColumnAddFlightImage
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle1.NullValue")));
            this.ColumnAddFlightImage.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnAddFlightImage.Frozen = true;
            this.ColumnAddFlightImage.HeaderText = "";
            this.ColumnAddFlightImage.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.airplane;
            this.ColumnAddFlightImage.Name = "ColumnAddFlightImage";
            this.ColumnAddFlightImage.ReadOnly = true;
            this.ColumnAddFlightImage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnAddFlightImage.ToolTipText = "Add Flight";
            this.ColumnAddFlightImage.Width = 22;
            // 
            // ColumnEventName
            // 
            this.ColumnEventName.DataPropertyName = "EventName";
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            this.ColumnEventName.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnEventName.HeaderText = "Event";
            this.ColumnEventName.Name = "ColumnEventName";
            this.ColumnEventName.ReadOnly = true;
            this.ColumnEventName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnEventVenue
            // 
            this.ColumnEventVenue.DataPropertyName = "EventVenue";
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            this.ColumnEventVenue.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnEventVenue.HeaderText = "Venue";
            this.ColumnEventVenue.Name = "ColumnEventVenue";
            this.ColumnEventVenue.ReadOnly = true;
            this.ColumnEventVenue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnEventDate
            // 
            this.ColumnEventDate.DataPropertyName = "EventDate";
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            this.ColumnEventDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnEventDate.HeaderText = "Event Date";
            this.ColumnEventDate.Name = "ColumnEventDate";
            this.ColumnEventDate.ReadOnly = true;
            this.ColumnEventDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnFlightDate
            // 
            this.ColumnFlightDate.DataPropertyName = "FlightDate";
            this.ColumnFlightDate.HeaderText = "Flight Date";
            this.ColumnFlightDate.Name = "ColumnFlightDate";
            this.ColumnFlightDate.ReadOnly = true;
            this.ColumnFlightDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnDepartureTime
            // 
            this.ColumnDepartureTime.DataPropertyName = "FlightTime";
            this.ColumnDepartureTime.HeaderText = "Departure Time";
            this.ColumnDepartureTime.Name = "ColumnDepartureTime";
            this.ColumnDepartureTime.ReadOnly = true;
            this.ColumnDepartureTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnAirport
            // 
            this.ColumnAirport.DataPropertyName = "FlightAirport";
            this.ColumnAirport.HeaderText = "Airport";
            this.ColumnAirport.Name = "ColumnAirport";
            this.ColumnAirport.ReadOnly = true;
            this.ColumnAirport.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSave.Location = new System.Drawing.Point(416, 408);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(497, 408);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // BookPackageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(584, 439);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxFlight);
            this.Controls.Add(this.groupBoxCustomer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BookPackageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buy Package";
            this.Load += new System.EventHandler(this.BookEvent_Load);
            this.groupBoxCustomer.ResumeLayout(false);
            this.groupBoxCustomer.PerformLayout();
            this.groupBoxFlight.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCustomer;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelCity;
        private System.Windows.Forms.GroupBox groupBoxFlight;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelCountry;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.ComboBox comboBoxCountry;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridView dataGridViewEvent;
        private System.Windows.Forms.TextBox textboxAddress;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label labelPostalCode;
        private System.Windows.Forms.TextBox textBoxPostalCode;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.TextBox textboxPhone;
        private System.Windows.Forms.ComboBox comboBoxCustomername;
        private System.Windows.Forms.DataGridViewImageColumn ColumnAddFlightImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEventName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEventVenue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEventDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFlightDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDepartureTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAirport;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
    }
}