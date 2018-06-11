namespace Microsoft.SAPSK.ContosoTours
{
    partial class FlightConnectionForm
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
            this.listViewFlight = new System.Windows.Forms.ListView();
            this.ColumnFlightDate = new System.Windows.Forms.ColumnHeader();
            this.ColumnDepartTime = new System.Windows.Forms.ColumnHeader();
            this.columnAirportFrom = new System.Windows.Forms.ColumnHeader();
            this.ColumnArrivalDate = new System.Windows.Forms.ColumnHeader();
            this.ColumnAirportTo = new System.Windows.Forms.ColumnHeader();
            this.ColumnArrivalTime = new System.Windows.Forms.ColumnHeader();
            this.ColumnCityFrom = new System.Windows.Forms.ColumnHeader();
            this.ColumnCityTo = new System.Windows.Forms.ColumnHeader();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxAirline = new System.Windows.Forms.ComboBox();
            this.comboBoxTravelAgency = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxTo = new System.Windows.Forms.ComboBox();
            this.labelFrom = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.labelNumRecords = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewFlight
            // 
            this.listViewFlight.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnFlightDate,
            this.ColumnDepartTime,
            this.columnAirportFrom,
            this.ColumnArrivalDate,
            this.ColumnAirportTo,
            this.ColumnArrivalTime,
            this.ColumnCityFrom,
            this.ColumnCityTo});
            this.listViewFlight.FullRowSelect = true;
            this.listViewFlight.Location = new System.Drawing.Point(12, 106);
            this.listViewFlight.MultiSelect = false;
            this.listViewFlight.Name = "listViewFlight";
            this.listViewFlight.Size = new System.Drawing.Size(664, 182);
            this.listViewFlight.TabIndex = 6;
            this.listViewFlight.UseCompatibleStateImageBehavior = false;
            this.listViewFlight.View = System.Windows.Forms.View.Details;
            // 
            // ColumnFlightDate
            // 
            this.ColumnFlightDate.Text = "Flight Date";
            this.ColumnFlightDate.Width = 100;
            // 
            // ColumnDepartTime
            // 
            this.ColumnDepartTime.Text = "Departure Time";
            this.ColumnDepartTime.Width = 100;
            // 
            // columnAirportFrom
            // 
            this.columnAirportFrom.Text = "Airport From";
            this.columnAirportFrom.Width = 80;
            // 
            // ColumnArrivalDate
            // 
            this.ColumnArrivalDate.Text = "Arrival Date";
            this.ColumnArrivalDate.Width = 90;
            // 
            // ColumnAirportTo
            // 
            this.ColumnAirportTo.Text = "Airport To";
            this.ColumnAirportTo.Width = 70;
            // 
            // ColumnArrivalTime
            // 
            this.ColumnArrivalTime.Text = "Arrival Time";
            this.ColumnArrivalTime.Width = 80;
            // 
            // ColumnCityFrom
            // 
            this.ColumnCityFrom.Text = "City From";
            this.ColumnCityFrom.Width = 70;
            // 
            // ColumnCityTo
            // 
            this.ColumnCityTo.Text = "City To";
            this.ColumnCityTo.Width = 70;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(601, 299);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(520, 299);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Carrier:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(343, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Travel Agency:";
            // 
            // comboBoxAirline
            // 
            this.comboBoxAirline.FormattingEnabled = true;
            this.comboBoxAirline.Location = new System.Drawing.Point(98, 15);
            this.comboBoxAirline.Name = "comboBoxAirline";
            this.comboBoxAirline.Size = new System.Drawing.Size(223, 21);
            this.comboBoxAirline.TabIndex = 1;
            // 
            // comboBoxTravelAgency
            // 
            this.comboBoxTravelAgency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTravelAgency.FormattingEnabled = true;
            this.comboBoxTravelAgency.Location = new System.Drawing.Point(428, 15);
            this.comboBoxTravelAgency.Name = "comboBoxTravelAgency";
            this.comboBoxTravelAgency.Size = new System.Drawing.Size(223, 21);
            this.comboBoxTravelAgency.TabIndex = 3;
            this.toolTip.SetToolTip(this.comboBoxTravelAgency, "Use BAVARIAN CASTLE, this is the only agency in \r\nSAP seed data that have flight " +
                    "connections");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "City To:";
            // 
            // comboBoxTo
            // 
            this.comboBoxTo.FormattingEnabled = true;
            this.comboBoxTo.Location = new System.Drawing.Point(428, 42);
            this.comboBoxTo.Name = "comboBoxTo";
            this.comboBoxTo.Size = new System.Drawing.Size(223, 21);
            this.comboBoxTo.TabIndex = 4;
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(29, 45);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(53, 13);
            this.labelFrom.TabIndex = 8;
            this.labelFrom.Text = "City From:";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(576, 72);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxCity
            // 
            this.textBoxCity.Location = new System.Drawing.Point(98, 42);
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.ReadOnly = true;
            this.textBoxCity.Size = new System.Drawing.Size(223, 20);
            this.textBoxCity.TabIndex = 16;
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "For your information";
            // 
            // labelNumRecords
            // 
            this.labelNumRecords.AutoSize = true;
            this.labelNumRecords.Location = new System.Drawing.Point(15, 292);
            this.labelNumRecords.Name = "labelNumRecords";
            this.labelNumRecords.Size = new System.Drawing.Size(51, 13);
            this.labelNumRecords.TabIndex = 17;
            this.labelNumRecords.Text = "0 records";
            // 
            // FlightConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 332);
            this.ControlBox = false;
            this.Controls.Add(this.labelNumRecords);
            this.Controls.Add(this.textBoxCity);
            this.Controls.Add(this.listViewFlight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxTravelAgency);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxTo);
            this.Controls.Add(this.comboBoxAirline);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FlightConnectionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Flight List";
            this.Load += new System.EventHandler(this.FlightConnectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewFlight;
        private System.Windows.Forms.ColumnHeader ColumnFlightDate;
        private System.Windows.Forms.ColumnHeader ColumnDepartTime;
        private System.Windows.Forms.ColumnHeader columnAirportFrom;
        private System.Windows.Forms.ColumnHeader ColumnArrivalDate;
        private System.Windows.Forms.ColumnHeader ColumnArrivalTime;
        private System.Windows.Forms.ColumnHeader ColumnAirportTo;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ColumnHeader ColumnCityFrom;
        private System.Windows.Forms.ColumnHeader ColumnCityTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAirline;
        private System.Windows.Forms.ComboBox comboBoxTravelAgency;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelNumRecords;
    }
}