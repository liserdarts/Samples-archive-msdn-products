using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.Helper;
using Microsoft.SAPSK.ContosoTours.Properties;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class ManageEventForm : Form
    {
        private List<EventList> _sourceEvents;

        //private bool _isCellClick = false;

        private int _eventCurrentIndex = -1;

        public ManageEventForm()
        {
            InitializeComponent();
        }

        private void GetSource()
        {
            List<EventList> unUsedEvent =
                new List<EventList>();
            List<EventList> usedEvent =
                new List<EventList>();
            _sourceEvents =
                new List<EventList>();

            SAPEventReadWrite eventRW =
                new SAPEventReadWrite(Config._dbConnectionName);

            SAPEventAttendeeAgencyMapReadWrite eventAttendyMapRW =
                new SAPEventAttendeeAgencyMapReadWrite(Config._dbConnectionName);

            SAPPackageEventMapReadWrite packageEventMapRW =
                new SAPPackageEventMapReadWrite(Config._dbConnectionName);

            using (SAPDataReaderEvent readerEvent =
                eventRW.ReaderSelectAll())
            {
                if (readerEvent.DataReader != null &&
                    readerEvent.DataReader.HasRows)
                {
                    SAPDataSetEventAttendeeAgencyMap.EventAttendeeAgencyMapDataTable dtEventAttendee =
                        eventAttendyMapRW.SelectAll().EventAttendeeAgencyMap;
                    SAPDataSetPackageEventMap.PackageEventMapDataTable dtPackageEvent =
                        packageEventMapRW.SelectAll().PackageEventMap;

                    while (readerEvent.DataReader.Read())
                    {
                        EventList item = new EventList();
                        item.EventName = readerEvent.EventName;
                        item.EventDescription = readerEvent.EventDescription;
                        item.EventPhoto = readerEvent.EventPhoto;
                        item.EventID = readerEvent.EventID;
                        
                        DataRow[] rows =
                            dtEventAttendee.Select("EventID = " + item.EventID.ToString());

                        if (rows != null && rows.Length > 0)
                        {
                            item.EventEditTag = true;
                            item.EventDeleteTag = true;
                            usedEvent.Add(item);
                        }
                        else
                        {
                            item.EventEditTag = false;

                            rows =
                                dtPackageEvent.Select("EventId = " + item.EventID.ToString());

                            item.EventDeleteTag =
                                (rows != null && rows.Length > 0);

                            unUsedEvent.Add(item);
                        }
                    }// while (readerEvent.DataReader.Read());
                    _sourceEvents.AddRange(unUsedEvent);
                    _sourceEvents.AddRange(usedEvent);
                }
            }

            if (_sourceEvents.Count > 0)
            {
                pictureBoxPoster.Image =
                    UtilityHelper.ByteToImage(_sourceEvents[0].EventPhoto);
            }
        }

        private void pictureBoxNewEvent_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridViewEvent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //_isCellClick = true;
            _eventCurrentIndex= dataGridViewEventCommand(e);   
        }

        private void dataGridViewEvent_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (_sourceEvents.Count == 0)
            {
                return;
            }

            EventList currentEvent =
                _sourceEvents[e.RowIndex];

            switch (dataGridViewEvent.Columns[e.ColumnIndex].Name)
            {
                case "ColumnName":
                    e.Value = currentEvent.EventName;
                    break;

                case "ColumnDescription":
                    e.Value = currentEvent.EventDescription;
                    break;

                case "ColumnPoster":
                    e.Value = currentEvent.EventPhoto;
                    break;

                case "ColumnEdit":
                    if (currentEvent.EventEditTag)
                    {
                        e.Value = Resources.icoedit_disabled;
                    }
                    else
                    {
                        e.Value = Resources.icoedit;
                    }
                    break;
                case "ColumnDelete":
                    if (currentEvent.EventDeleteTag)
                    {
                        e.Value = Resources.icodelete_disabled;
                    }
                    else
                    {
                        e.Value = Resources.icodelete;
                    }
                    break;
            }
        }

        private void ManageEventForm_Load(object sender, EventArgs e)
        {
            #region Create Data Grid
            dataGridViewEvent.VirtualMode = true;

            DataGridViewTextBoxColumn columnName =
                new DataGridViewTextBoxColumn();
            columnName.HeaderText = "Name";
            columnName.Name = "ColumnName";
            columnName.Width = 90;
            dataGridViewEvent.Columns.Add(columnName);

            DataGridViewTextBoxColumn columnDesc =
                new DataGridViewTextBoxColumn();
            columnDesc.HeaderText = "Description";
            columnDesc.Name = "ColumnDescription";
            columnDesc.Width = 170;
            columnDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewEvent.Columns.Add(columnDesc);

            //create icon edit column
            DataGridViewImageColumn editColumn =
                new DataGridViewImageColumn();
            editColumn.Image = Resources.icoedit;
            editColumn.Width = 20;
            editColumn.Name = "ColumnEdit";
            editColumn.HeaderText = string.Empty;
            editColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //create icond delete column
            DataGridViewImageColumn deleteColumn = new DataGridViewImageColumn();
            deleteColumn.Image = Resources.icodelete;
            deleteColumn.Width = 20;
            deleteColumn.Name = "ColumnDelete";
            deleteColumn.HeaderText = string.Empty;
            deleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dataGridViewEvent.Columns.Insert(2, editColumn);
            dataGridViewEvent.Columns.Insert(3, deleteColumn);


            
           
            #endregion

            #region Populate datagrid

            GetSource();

            dataGridViewEvent.RowCount = _sourceEvents.Count;

            pictureBoxNewEvent.Image = Resources.iconew;

            #endregion
        }

        private void dataGridViewEvent_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            /*
            switch (dataGridViewEvent.Columns[e.ColumnIndex].Name)
            {
                case "ColumnEdit":
                    dataGridViewEvent.Cursor = Cursors.Hand;
                    break;
                case "ColumnDelete":
                    dataGridViewEvent.Cursor = Cursors.Hand;
                    break;
            }*/
        }

        private void dataGridViewEvent_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            /*
            switch (dataGridViewEvent.Columns[e.ColumnIndex].Name)
            {
                case "ColumnEdit":
                    dataGridViewEvent.Cursor = Cursors.Default;
                    break;
                case "ColumnDelete":
                    dataGridViewEvent.Cursor = Cursors.Default;
                    break;
            }
             * */
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            base.OnClosing(e);
        }

        private void dataGridViewEvent_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //_isCellClick = false;
            _eventCurrentIndex= dataGridViewEventCommand(e);

        }

        private int dataGridViewEventCommand(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                ShowPoster(e.RowIndex);
                return e.RowIndex;
            }

            return -1;
            /*
            switch (e.ColumnIndex)
            {

                case 0: //edit column

                    if (!_isCellClick)
                    {
                        ShowPoster(e.RowIndex);
                        return;
                    }

                    EventList currentEvent =
                        _sourceEvents[e.RowIndex];

                    EventForm newEventForm = new EventForm();

                    if (currentEvent.EventEditTag)
                    {
                        newEventForm._isReadOnly = true;
                        //return;
                    }                    

                    newEventForm.Text = string.Format(newEventForm.Text, "Edit");
                    newEventForm._eventID = currentEvent.EventID;
                    newEventForm.ShowDialog(this);
                    if (newEventForm._eventList != null)
                    {
                        bool deltag = _sourceEvents[e.RowIndex].EventDeleteTag;
                        _sourceEvents[e.RowIndex] = newEventForm._eventList;
                        _sourceEvents[e.RowIndex].EventDeleteTag = deltag;
                    }
                    newEventForm.Dispose();
                    break;
                case 1: //delete column
                    if (!_isCellClick)
                    {
                        ShowPoster(e.RowIndex);
                        return;
                    }

                    if (_sourceEvents.Count > 0)
                    {
                        EventList deletedEvent =
                            _sourceEvents[e.RowIndex];
                        if (deletedEvent.EventDeleteTag)
                        {
                            return;
                        }

                        SAPPackageEventMapReadWrite eventMapRW =
                            new SAPPackageEventMapReadWrite(Config._dbConnectionName);

                        if (eventMapRW.SelectByEventID(
                            deletedEvent.EventID).PackageEventMap.Rows.Count > 0)
                        {
                            Message.DisplayMessage("Unable to delete. " +
                                "Event is currently associated with a package.");
                            return;
                        }

                        if (Message.DeleteMessage(
                            deletedEvent.EventName) == DialogResult.Yes)
                        {
                            SAPEventReadWrite eventRW =
                                new SAPEventReadWrite(Config._dbConnectionName);
                            eventRW.Delete(deletedEvent.EventID);
                            _sourceEvents.RemoveAt(e.RowIndex);
                            dataGridViewEvent.RowCount = _sourceEvents.Count;
                        }
                    }
                    break;
                default:
                    if (_sourceEvents.Count > 0 && e.RowIndex > -1)
                    {
                        pictureBoxPoster.Image =
                            UtilityHelper.ByteToImage(_sourceEvents[e.RowIndex].EventPhoto);
                    }
                    break;
            }*/
        }

        private void ShowPoster(int rowIndex)
        {
            if (_sourceEvents.Count > 0 && rowIndex > -1)
            {
                pictureBoxPoster.Image =
                    UtilityHelper.ByteToImage(_sourceEvents[rowIndex].EventPhoto);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            EventForm newEventForm = new EventForm();

            newEventForm.Text = string.Format(newEventForm.Text, "Add");
            if (newEventForm.ShowDialog(this) == DialogResult.OK)
            {
                if (newEventForm._eventList != null)
                {
                    _sourceEvents.Add(newEventForm._eventList);
                    dataGridViewEvent.RowCount = _sourceEvents.Count;
                }
            }
            newEventForm.Dispose();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (_eventCurrentIndex >= 0)
            {
                EventList currentEvent =
                       _sourceEvents[_eventCurrentIndex];

                EventForm newEventForm = new EventForm();

                if (currentEvent.EventEditTag)
                {
                    newEventForm._isReadOnly = true;
                    //return;
                }

                newEventForm.Text = string.Format(newEventForm.Text, "Edit");
                newEventForm._eventID = currentEvent.EventID;
                newEventForm.ShowDialog(this);
                if (newEventForm._eventList != null)
                {
                    bool deltag = _sourceEvents[_eventCurrentIndex].EventDeleteTag;
                    _sourceEvents[_eventCurrentIndex] = newEventForm._eventList;
                    _sourceEvents[_eventCurrentIndex].EventDeleteTag = deltag;
                }
                newEventForm.Dispose();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_eventCurrentIndex >= 0)
            {
                if (_sourceEvents.Count > 0)
                {
                    EventList deletedEvent =
                        _sourceEvents[_eventCurrentIndex];
                    if (deletedEvent.EventDeleteTag)
                    {
                        return;
                    }

                    SAPPackageEventMapReadWrite eventMapRW =
                        new SAPPackageEventMapReadWrite(Config._dbConnectionName);

                    if (eventMapRW.SelectByEventID(
                        deletedEvent.EventID).PackageEventMap.Rows.Count > 0)
                    {
                        Message.DisplayMessage("Unable to delete. " +
                            "Event is currently associated with a package.");
                        return;
                    }

                    if (Message.DeleteMessage(
                        deletedEvent.EventName) == DialogResult.Yes)
                    {
                        SAPEventReadWrite eventRW =
                            new SAPEventReadWrite(Config._dbConnectionName);
                        eventRW.Delete(deletedEvent.EventID);
                        _sourceEvents.RemoveAt(_eventCurrentIndex);
                        dataGridViewEvent.RowCount = _sourceEvents.Count;
                    }
                }
            }
        }
    }
}