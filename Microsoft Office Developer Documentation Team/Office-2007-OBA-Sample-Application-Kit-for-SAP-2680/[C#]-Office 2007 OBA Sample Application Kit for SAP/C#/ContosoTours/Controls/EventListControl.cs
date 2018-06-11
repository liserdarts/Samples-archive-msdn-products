using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.Helper;
using Microsoft.SAPSK.ContosoTours.Properties;
using Microsoft.SAPSK.ContosoTours.Schema;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class EventListControl : UserControl
    {
        private SAPDataSetEvent _eventData;
      
        public EventListControl()
        {
            InitializeComponent();
        }

        private void EventList_Load(object sender, EventArgs e)
        {
            #region Initialize Venue Combobox
            
            SAPVenueReadWrite venue =
                new SAPVenueReadWrite(Config._dbConnectionName);

            comboBoxVenue.DataSource = venue.SelectAll().Venue;
            comboBoxVenue.DisplayMember =
                SAPVenueReadWrite._venueNameColumnName;
            comboBoxVenue.ValueMember =
                SAPVenueReadWrite._venueIDColumnName;

            comboBoxVenue.SelectedIndex = -1;

            #endregion

            PopulateEvents();
            
            monthCalendarSchedules.BoldedDates = GetEventSchedules();
        }

        private void PopulateEvents()
        {
           SAPEventReadWrite mEvent =
           new SAPEventReadWrite(Config._dbConnectionName);

           _eventData = mEvent.SelectAll();
        }

        private void pictureBoxExpand_Click(object sender, EventArgs e)
        {
            panelMoreOptions.Visible = Convert.ToBoolean(pictureBoxExpand.Tag);
            if(panelMoreOptions.Visible)
            {
                pictureBoxExpand.Image = Resources.uparrows_white;
                pictureBoxExpand.Tag = false;
            }
            else
            {
                pictureBoxExpand.Image = Resources.downarrows_white;
                pictureBoxExpand.Tag = true;
            }
        }

        private DateTime[] GetEventSchedules()
        {
            SAPEventReadWrite mEvent =
                new SAPEventReadWrite(Config._dbConnectionName);
      
            DateTime[] schedules = 
                new DateTime[_eventData.Event.Rows.Count];

            int i = 0;
            foreach (SAPDataSetEvent.EventRow row in _eventData.Event.Rows)
            {
                schedules[i] = row.EventDate;
                i++;
            }

            return schedules;
        }

        private void panelMonthCalendar_Resize(object sender, EventArgs e)
        {
            monthCalendarSchedules.Location = new Point(
                (panelMonthCalendar.Width - monthCalendarSchedules.Width)/2,
                monthCalendarSchedules.Location.Y);
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
    
            string rowFilter = string.Empty;
            if(textBoxSearch.Text.Trim().Length > 0)
            {
                rowFilter += string.Format("EventName LIKE '%{0}%'", textBoxSearch.Text);
            }

            if(comboBoxVenue.SelectedIndex > -1)
            {
                if(rowFilter != string.Empty)
                {
                    rowFilter += " AND ";
                }
                rowFilter += string.Format("VenueID = {0}", comboBoxVenue.SelectedValue);
            }

            _eventData.Event.DefaultView.RowFilter = rowFilter;

            PopulateResult(_eventData.Event.DefaultView);

        }

        private void PopulateResult(DataView view)
        {
            listViewResult.Items.Clear();
            if (view.Count > 0)
            {
                //linkLabelShowEventDetails.Visible = true;
                panelShowEventDetails.Visible = true;
                foreach (DataRowView rowView in view)
                {
                    SAPDataSetEvent.EventRow row = 
                        (SAPDataSetEvent.EventRow)rowView.Row;

                    ListViewItem item = listViewResult.Items.Add(row.EventName);
                    item.Tag = row;
                    item.SubItems.AddRange(new string[]
                                     {
                                         row.EventDescription,
                                         row.VenueName,
                                         row.EventDate.ToString()
                                     });
                }
            }
            else
            {
                //linkLabelShowEventDetails.Visible = false;
                panelShowEventDetails.Visible = false;
            }
        }

        private void linkLabelShowEventDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Events eventData = new Events();

            foreach (ListViewItem item in listViewResult.Items)
            {
                if(item.Checked)
                {
                    Events.EventRow schemaRow = 
                        eventData.Event.NewEventRow();

                    SAPDataSetEvent.EventRow row = (SAPDataSetEvent.EventRow) item.Tag;

                    schemaRow.Name = row.EventName;
                    schemaRow.Date = row.EventDate.ToString();
                    schemaRow.Description = row.EventDescription;
                    schemaRow.Venue = row.VenueName;
                    schemaRow.GoldPackagePrice = row.GoldPackagePrice;
                    schemaRow.SilverPackagePrice = row.SilverPackagePrice;
                    schemaRow.BronzePackagePrice = row.BronzePackagePrice;
                    schemaRow.GoldPackageTrueCost = row.GoldPackageTrueCost;
                    schemaRow.SilverPackageTrueCost = row.SilverPackageTrueCost;
                    schemaRow.BronzePackageTrueCost = row.BronzePackageTrueCost;

                    eventData.Event.Rows.Add(schemaRow);
                }
            }

            if(eventData.Event.Rows.Count > 0)
            {
                ExcelHelper.LoadExcelSheet("Event List", Resources.EventList, eventData);
            }
        }

        private void monthCalendarSchedules_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (e.Start != e.End)
            {
                _eventData.Event.DefaultView.RowFilter =
                    string.Format("EventDate >= '{0}' AND EventDate <= '{1}'", e.Start, e.End.AddDays(1));
            }
            else
            {
                _eventData.Event.DefaultView.RowFilter = string.Format("EventDate >= '{0}' AND EventDate < '{1}'", e.End, e.End.AddDays(1));
            }

            PopulateResult(_eventData.Event.DefaultView);
        }

        private void monthCalendarSchedules_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void monthCalendarSchedules_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}