namespace Microsoft.SAPSK.ContosoTours
{
    partial class EventListControl
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
            this.components = new System.ComponentModel.Container();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelShowEventDetails = new System.Windows.Forms.Panel();
            this.linkLabelShowEventDetails = new System.Windows.Forms.LinkLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.listViewResult = new System.Windows.Forms.ListView();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnDescription = new System.Windows.Forms.ColumnHeader();
            this.columnVenue = new System.Windows.Forms.ColumnHeader();
            this.columnSchedule = new System.Windows.Forms.ColumnHeader();
            this.panelMoreOptions = new System.Windows.Forms.Panel();
            this.comboBoxVenue = new System.Windows.Forms.ComboBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonGo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBoxExpand = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelMonthCalendar = new System.Windows.Forms.Panel();
            this.monthCalendarSchedules = new System.Windows.Forms.MonthCalendar();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelMain.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelShowEventDetails.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelMoreOptions.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExpand)).BeginInit();
            this.panelMonthCalendar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.Controls.Add(this.panel2);
            this.panelMain.Controls.Add(this.panelShowEventDetails);
            this.panelMain.Controls.Add(this.panel4);
            this.panelMain.Controls.Add(this.panelMoreOptions);
            this.panelMain.Controls.Add(this.panel3);
            this.panelMain.Controls.Add(this.panelMonthCalendar);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(272, 517);
            this.panelMain.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 451);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(272, 63);
            this.panel2.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(33, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 53);
            this.label4.TabIndex = 23;
            this.label4.Text = "See events for a particular date via selecting the calendar dates in BOLD.  Refin" +
                "e your search by either specifying part of event name or where it is held using " +
                "More Options.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.icoquestion;
            this.pictureBox1.Location = new System.Drawing.Point(11, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // panelShowEventDetails
            // 
            this.panelShowEventDetails.Controls.Add(this.linkLabelShowEventDetails);
            this.panelShowEventDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelShowEventDetails.Location = new System.Drawing.Point(0, 430);
            this.panelShowEventDetails.Name = "panelShowEventDetails";
            this.panelShowEventDetails.Size = new System.Drawing.Size(272, 21);
            this.panelShowEventDetails.TabIndex = 19;
            this.panelShowEventDetails.Visible = false;
            // 
            // linkLabelShowEventDetails
            // 
            this.linkLabelShowEventDetails.AutoSize = true;
            this.linkLabelShowEventDetails.Location = new System.Drawing.Point(8, 3);
            this.linkLabelShowEventDetails.Name = "linkLabelShowEventDetails";
            this.linkLabelShowEventDetails.Size = new System.Drawing.Size(100, 13);
            this.linkLabelShowEventDetails.TabIndex = 6;
            this.linkLabelShowEventDetails.TabStop = true;
            this.linkLabelShowEventDetails.Text = "Show Event Details";
            this.linkLabelShowEventDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelShowEventDetails_LinkClicked);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.listViewResult);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 279);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(272, 151);
            this.panel4.TabIndex = 18;
            // 
            // listViewResult
            // 
            this.listViewResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewResult.CheckBoxes = true;
            this.listViewResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnDescription,
            this.columnVenue,
            this.columnSchedule});
            this.listViewResult.FullRowSelect = true;
            this.listViewResult.Location = new System.Drawing.Point(10, 6);
            this.listViewResult.Name = "listViewResult";
            this.listViewResult.ShowItemToolTips = true;
            this.listViewResult.Size = new System.Drawing.Size(251, 139);
            this.listViewResult.TabIndex = 5;
            this.listViewResult.UseCompatibleStateImageBehavior = false;
            this.listViewResult.View = System.Windows.Forms.View.Details;
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            // 
            // columnDescription
            // 
            this.columnDescription.Text = "Description";
            // 
            // columnVenue
            // 
            this.columnVenue.Text = "Venue";
            // 
            // columnSchedule
            // 
            this.columnSchedule.Text = "Schedule";
            // 
            // panelMoreOptions
            // 
            this.panelMoreOptions.Controls.Add(this.comboBoxVenue);
            this.panelMoreOptions.Controls.Add(this.textBoxSearch);
            this.panelMoreOptions.Controls.Add(this.label2);
            this.panelMoreOptions.Controls.Add(this.buttonGo);
            this.panelMoreOptions.Controls.Add(this.label1);
            this.panelMoreOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMoreOptions.Location = new System.Drawing.Point(0, 187);
            this.panelMoreOptions.Name = "panelMoreOptions";
            this.panelMoreOptions.Size = new System.Drawing.Size(272, 92);
            this.panelMoreOptions.TabIndex = 15;
            this.toolTip.SetToolTip(this.panelMoreOptions, "Specify the event name or part of the \r\nevent name as criteria and/or select a \r\n" +
                    "particular venue to see the event/s \r\ngoing to be held there, or to see all\r\neve" +
                    "nts just keep all blanks and click Go.");
            this.panelMoreOptions.Visible = false;
            // 
            // comboBoxVenue
            // 
            this.comboBoxVenue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxVenue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxVenue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxVenue.FormattingEnabled = true;
            this.comboBoxVenue.Location = new System.Drawing.Point(11, 63);
            this.comboBoxVenue.Name = "comboBoxVenue";
            this.comboBoxVenue.Size = new System.Drawing.Size(250, 21);
            this.comboBoxVenue.TabIndex = 2;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(11, 20);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(211, 20);
            this.textBoxSearch.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Held in:";
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Location = new System.Drawing.Point(228, 18);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(33, 23);
            this.buttonGo.TabIndex = 1;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Search for:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBoxExpand);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 167);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(272, 20);
            this.panel3.TabIndex = 17;
            this.toolTip.SetToolTip(this.panel3, "Refine your search");
            // 
            // pictureBoxExpand
            // 
            this.pictureBoxExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxExpand.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.downarrows_white;
            this.pictureBoxExpand.Location = new System.Drawing.Point(253, 2);
            this.pictureBoxExpand.Name = "pictureBoxExpand";
            this.pictureBoxExpand.Size = new System.Drawing.Size(17, 17);
            this.pictureBoxExpand.TabIndex = 11;
            this.pictureBoxExpand.TabStop = false;
            this.pictureBoxExpand.Tag = "true";
            this.pictureBoxExpand.Click += new System.EventHandler(this.pictureBoxExpand_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "More Options";
            // 
            // panelMonthCalendar
            // 
            this.panelMonthCalendar.Controls.Add(this.monthCalendarSchedules);
            this.panelMonthCalendar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMonthCalendar.Location = new System.Drawing.Point(0, 0);
            this.panelMonthCalendar.Name = "panelMonthCalendar";
            this.panelMonthCalendar.Size = new System.Drawing.Size(272, 167);
            this.panelMonthCalendar.TabIndex = 16;
            this.panelMonthCalendar.Resize += new System.EventHandler(this.panelMonthCalendar_Resize);
            // 
            // monthCalendarSchedules
            // 
            this.monthCalendarSchedules.Location = new System.Drawing.Point(48, 11);
            this.monthCalendarSchedules.Name = "monthCalendarSchedules";
            this.monthCalendarSchedules.ShowToday = false;
            this.monthCalendarSchedules.TabIndex = 8;
            this.toolTip.SetToolTip(this.monthCalendarSchedules, "Dates in BOLD indicates an event or \r\nevents are scheduled on that date");
            this.monthCalendarSchedules.MouseLeave += new System.EventHandler(this.monthCalendarSchedules_MouseLeave);
            this.monthCalendarSchedules.MouseHover += new System.EventHandler(this.monthCalendarSchedules_MouseHover);
            this.monthCalendarSchedules.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarSchedules_DateChanged);
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "For your information";
            // 
            // EventListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Name = "EventListControl";
            this.Size = new System.Drawing.Size(272, 517);
            this.Load += new System.EventHandler(this.EventList_Load);
            this.panelMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelShowEventDetails.ResumeLayout(false);
            this.panelShowEventDetails.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panelMoreOptions.ResumeLayout(false);
            this.panelMoreOptions.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExpand)).EndInit();
            this.panelMonthCalendar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelMoreOptions;
        private System.Windows.Forms.ComboBox comboBoxVenue;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBoxExpand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelMonthCalendar;
        private System.Windows.Forms.MonthCalendar monthCalendarSchedules;
        private System.Windows.Forms.Panel panelShowEventDetails;
        private System.Windows.Forms.LinkLabel linkLabelShowEventDetails;
        private System.Windows.Forms.ListView listViewResult;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnDescription;
        private System.Windows.Forms.ColumnHeader columnVenue;
        private System.Windows.Forms.ColumnHeader columnSchedule;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
