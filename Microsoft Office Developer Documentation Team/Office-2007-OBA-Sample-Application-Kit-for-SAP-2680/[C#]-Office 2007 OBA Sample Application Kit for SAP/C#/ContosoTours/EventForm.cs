using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.Helper;
using Microsoft.SAPSK.ContosoTours.Properties;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class EventForm : Form
    {
        private List<PackageTypeList> _packageTypeList;
        private DataSet dsEventActor;
        public int _eventID = 0;
        public EventList _eventList;
        public bool _isReadOnly;
        public byte[] _previousImage;
        
        public EventForm()
        {
            InitializeComponent();
        }

        private void frmEvent_Load(object sender, EventArgs e)
        {
            #region Initialize Venue Combobox
            SAPVenueReadWrite venue =
                new SAPVenueReadWrite(Config._dbConnectionName);
            comboBoxVenue.DataSource = venue.SelectAll().Venue;
            comboBoxVenue.DisplayMember = 
                SAPVenueReadWrite._venueNameColumnName;
            comboBoxVenue.ValueMember = 
                SAPVenueReadWrite._venueIDColumnName;

            #endregion
            
            #region Get Event Types
            SAPEventTypeReadWrite eventTypeRW =
                new SAPEventTypeReadWrite(Config._dbConnectionName);
            SAPDataSetEventType.EventTypeDataTable dtEventType =
                eventTypeRW.SelectAll().EventType;

            comboBoxEventType.DataSource = dtEventType;
            comboBoxEventType.DisplayMember = SAPEventTypeReadWrite._eventTypeNameColumnName;
            comboBoxEventType.ValueMember = SAPEventTypeReadWrite._eventTypeIDColumnName;

            SAPEventActorReadWrite actors =
                new SAPEventActorReadWrite(Config._dbConnectionName);
            comboBoxActor.DataSource = actors.SelectAll().EventActor;
            comboBoxActor.DisplayMember =
                SAPEventActorReadWrite._eventActorNameColumnName;
            comboBoxActor.ValueMember =
                SAPEventActorReadWrite._eventActorIDColumnName;
            #endregion

            #region Initialize Event Package Grid

            dataGridViewPackage.VirtualMode = true;

            DataGridViewTextBoxColumn columnPackageType =
                new DataGridViewTextBoxColumn();
            columnPackageType.HeaderText = "Name";
            columnPackageType.Name = "ColumnName";
            columnPackageType.Width = 70;
            columnPackageType.ReadOnly = true;
            columnPackageType.Frozen = true;
            columnPackageType.DefaultCellStyle.BackColor =
                Color.FromKnownColor(KnownColor.Control);
            columnPackageType.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            dataGridViewPackage.Columns.Add(columnPackageType);

            DataGridViewTextBoxColumn columnPrice =
                new DataGridViewTextBoxColumn();
            columnPrice.HeaderText = "Price";
            columnPrice.Name = "ColumnPrice";
            columnPrice.Width = 85;
            columnPrice.DefaultCellStyle.Format = "c";
            columnPrice.DefaultCellStyle.Alignment = 
                DataGridViewContentAlignment.BottomRight;
            columnPrice.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            dataGridViewPackage.Columns.Add(columnPrice);

            DataGridViewTextBoxColumn columnCost =
                new DataGridViewTextBoxColumn();
            columnCost.HeaderText = "Cost";
            columnCost.Name = "ColumnCost";
            columnCost.Width = 85;
            columnCost.DefaultCellStyle.Format = "c";
            columnCost.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.BottomRight;
            columnCost.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            dataGridViewPackage.Columns.Add(columnCost);

            SAPActorEventReadOnly actorEvent =
                new SAPActorEventReadOnly(Config._dbConnectionName);
            DataTable dtEventActor = new DataTable();
            dtEventActor.Columns.Add("ActorName", typeof(string));
            dtEventActor.Columns.Add("ActorID", typeof(string));
            if (_eventID > 0)
            {
                using (SAPDataReaderActorEvent rdrActorEvent =
                    actorEvent.ReaderSelectByEventID(_eventID))
                {
                    if (rdrActorEvent.DataReader != null &&
                        rdrActorEvent.DataReader.HasRows)
                    {
                        while (rdrActorEvent.DataReader.Read())
                        {
                            dtEventActor.Rows.Add(
                                rdrActorEvent.EventActorName,
                                rdrActorEvent.EventActorID);
                        } //while (rdrActorEvent.DataReader.Read());
                    }
                }
            }
            dsEventActor = new DataSet();
            dsEventActor.Tables.Add(dtEventActor);
            dataGridViewActor.DataSource = dsEventActor.Tables[0];

            #endregion

            #region Populate Values
            openFileDialog.FileName = string.Empty;
            _packageTypeList = new List<PackageTypeList>();
            if (_eventID > 0)
            {
                SAPEventReadWrite eventRW =
                    new SAPEventReadWrite(Config._dbConnectionName);

                using (SAPDataReaderEvent rdrEvent =
                    eventRW.ReaderSelectByEventID(_eventID))
                {
                    rdrEvent.DataReader.Read();
                    textBoxName.Text = rdrEvent.EventName;
                    textBoxDescription.Text = rdrEvent.EventDescription;
                    dateTimePickerSchedule.Value = rdrEvent.EventDate;
                    comboBoxEventType.Text = rdrEvent.EventTypeName;
                    comboBoxVenue.Text = rdrEvent.VenueName;
                    monthCalendarSchedule.SetDate(rdrEvent.EventDate);
                    _packageTypeList.Add(
                        new PackageTypeList(
                            "Gold",
                            rdrEvent.GoldPackagePrice,
                            rdrEvent.GoldPackageTrueCost));
                    _packageTypeList.Add(
                        new PackageTypeList(
                            "Silver",
                            rdrEvent.SilverPackagePrice,
                            rdrEvent.SilverPackageTrueCost));
                    _packageTypeList.Add(
                        new PackageTypeList(
                            "Bronze",
                            rdrEvent.BronzePackagePrice,
                            rdrEvent.BronzePackageTrueCost));
                    _previousImage = rdrEvent.EventPhoto;
                    using (MemoryStream ms =
                        new MemoryStream(rdrEvent.EventPhoto, 0, rdrEvent.EventPhoto.Length))
                    {
                        ms.Write(rdrEvent.EventPhoto, 0, rdrEvent.EventPhoto.Length);
                        pictureBoxPoster.Image = Image.FromStream(ms, true);
                    }
                }
            }
            else
            {
                _packageTypeList.Add(new PackageTypeList("Gold", 0, 0));
                _packageTypeList.Add(new PackageTypeList("Silver", 0, 0));
                _packageTypeList.Add(new PackageTypeList("Bronze", 0, 0));
            }
            if (_isReadOnly)
            {
                UtilityHelper.SetToReadOnly(Controls);
                buttonSave.Enabled = false;
                dataGridViewPackage.ReadOnly = true;
                GridHelper.HideColumns(
                    dataGridViewActor, 
                    "ColumnActorID", 
                    "ColumnDelete");
                buttonAddToList.Enabled = false;
                pictureBoxNewVenue.Enabled = false;
            }

            pictureBoxNewVenue.Image = Resources.iconew;

            dataGridViewPackage.RowCount = _packageTypeList.Count;
            #endregion
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string eventName = comboBoxEventType.Text.Trim();
            int eventTypeID = 0;
            int eventID = _eventID;
            bool hasNoError = true;
            bool isSuccess = false;
            errorProvider.Clear();


            #region get price/cost

            DateTime eventSchedule =
                new DateTime(
                    monthCalendarSchedule.SelectionEnd.Year,
                    monthCalendarSchedule.SelectionEnd.Month,
                    monthCalendarSchedule.SelectionEnd.Day,
                    dateTimePickerSchedule.Value.Hour,
                    dateTimePickerSchedule.Value.Minute,
                    dateTimePickerSchedule.Value.Second);

            decimal goldPackageCost =
                Convert.ToDecimal(_packageTypeList[0].PackageCost);
            decimal silverPackageCost =
                Convert.ToDecimal(_packageTypeList[1].PackageCost);
            decimal bronzePackageCost =
                Convert.ToDecimal(_packageTypeList[2].PackageCost);

            decimal totalPackageCost =
                goldPackageCost + silverPackageCost + bronzePackageCost;
            #endregion

            _eventList = new EventList();

            #region check image file
            string fileName = openFileDialog.FileName;
            byte[] byteImage = null;
            
            if (File.Exists(fileName))
            {
                FileInfo imageFile = new FileInfo(fileName);

                using (FileStream fs = new FileStream(
                    fileName,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read))
                {
                    int fileSize = Convert.ToInt32(imageFile.Length);
                    byteImage = new byte[fileSize];
                    int bytesRead = fs.Read(
                        byteImage,
                        0,
                        fileSize);
                    fs.Close();
                }
            }
            else if (_eventID > 0)
            {
                byteImage = _previousImage;
            }
            else
            {
                byteImage = UtilityHelper.BitmapToByte(Resources.blank);
            }

            #endregion

            if (byteImage == null)
            {
                errorProvider.SetError(pictureBoxPoster, "Poster is required.");
                hasNoError = false;
            }

            if (eventName.Length == 0)
            {
                errorProvider.SetError(comboBoxEventType, "Event Type is required.");
                hasNoError = false;
            }

            if (textBoxName.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBoxName, "Event name is required.");
                hasNoError = false;
            }

            if (textBoxDescription.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBoxDescription, "Event description is required.");
                hasNoError = false;
            }

            if (!hasNoError)
            {
                return;
            }

            try
            {
                SAPEventTypeReadWrite eventType =
                    new SAPEventTypeReadWrite(Config._dbConnectionName);
                SAPDataSetEventType.EventTypeDataTable dtEventType
                    = eventType.SelectByEventTypeName(eventName).EventType;
                if (dtEventType.Rows.Count > 0)
                {
                    eventTypeID =
                        ((SAPDataSetEventType.EventTypeRow)dtEventType.Rows[0]).EventTypeID;
                }

                if (eventTypeID == 0)
                {
                    eventType.Insert(
                        eventName,
                        eventName,
                        out eventTypeID);
                }

                using (SWPTransactionDBConnection dbTransaction =
                    new SWPTransactionDBConnection(Config._dbConnectionName))
                {
                    try
                    {
                        SAPEventReadWrite mEvent =
                            new SAPEventReadWrite(dbTransaction);
                        SAPEventActorReadWrite eventActor =
                            new SAPEventActorReadWrite(dbTransaction);
                        SAPEventActorMapReadWrite eventActorMap =
                            new SAPEventActorMapReadWrite(dbTransaction);
                        if (_eventID == 0)
                        {
                            mEvent.Insert(
                                Convert.ToInt32(comboBoxVenue.SelectedValue),
                                eventTypeID,
                                textBoxName.Text,
                                textBoxDescription.Text,
                                byteImage,
                                eventSchedule,
                                _packageTypeList[0].PackagePrice,
                                _packageTypeList[1].PackagePrice,
                                _packageTypeList[2].PackagePrice,
                                goldPackageCost,
                                silverPackageCost,
                                bronzePackageCost,
                                totalPackageCost,
                                out eventID);
                        }
                        else
                        {
                            mEvent.Update(
                                _eventID,
                                Convert.ToInt32(comboBoxVenue.SelectedValue),
                                eventTypeID,
                                textBoxName.Text,
                                textBoxDescription.Text,
                                byteImage,
                                eventSchedule,
                                _packageTypeList[0].PackagePrice,
                                _packageTypeList[1].PackagePrice,
                                _packageTypeList[2].PackagePrice,
                                goldPackageCost,
                                silverPackageCost,
                                bronzePackageCost,
                                totalPackageCost);
                        }
                        if (dsEventActor.Tables[0].Rows.Count == 0)
                        {
                            eventActorMap.DeleteByEventID(eventID);
                        }

                        DataTable dtAdded = dsEventActor.Tables[0].GetChanges(DataRowState.Added);
                        DataTable dtDeleted = dsEventActor.Tables[0].GetChanges(DataRowState.Deleted);
                        int eventActorMapID = 0;
                        if (dtAdded != null)
                        {
                            foreach (DataRow dr in dtAdded.Rows)
                            {
                                int eventActorID = Convert.ToInt32(dr[1]);
                                if (eventActorID == 0)
                                {
                                    eventActor.Insert(
                                        (string)dr[0],
                                        out eventActorID);
                                }
                                eventActorMap.Insert(
                                    eventID,
                                    eventActorID,
                                    out eventActorMapID);
                            }
                        }
                        if (dtDeleted != null)
                        {
                            foreach (DataRow dr in dtDeleted.Rows)
                            {
                                int eventActorID = (int)dr[1];
                                if (eventActorID != 0)
                                {
                                    eventActorMap.DeleteByEventActorID(eventActorID);
                                }
                            }
                        }
                        dbTransaction.Transaction.Commit();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Transaction.Rollback();

                        ErrorForm errorForm =
                            new ErrorForm(ex.Message, ex.ToString());
                        errorForm.ShowDialog(this);
                        errorForm.Close();
                    }
                    finally
                    {
                        dbTransaction.Transaction.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorForm errorForm =
                    new ErrorForm(ex.Message, ex.ToString());
                errorForm.ShowDialog(this);
                errorForm.Close();
            }
            if (isSuccess)
            {
                _eventList.EventName = textBoxName.Text;
                _eventList.EventDescription = textBoxDescription.Text;
                _eventList.EventPhoto = byteImage;
                _eventList.EventID = eventID;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void linkLabelUploadPoster_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog.ShowDialog(this);
            pictureBoxPoster.ImageLocation = openFileDialog.FileName;
        }

        private void dataGridViewPackage_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (_packageTypeList.Count == 0)
            {
                return;
            }

            PackageTypeList currentPackageType =
                _packageTypeList[e.RowIndex];

            switch (dataGridViewPackage.Columns[e.ColumnIndex].Name)
            {
                case "ColumnName":
                    e.Value = currentPackageType.PackageType;
                    break;

                case "ColumnPrice":
                    e.Value = currentPackageType.PackagePrice;
                    break;

                case "ColumnCost":
                    e.Value = currentPackageType.PackageCost;
                    break;
            }
        }

        private void dataGridViewPackage_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            if (_packageTypeList.Count == 0)
            {
                return;
            }

            PackageTypeList currentPackageType =
                _packageTypeList[e.RowIndex];

            switch (dataGridViewPackage.Columns[e.ColumnIndex].Name)
            {
                case "ColumnName":
                    currentPackageType.PackageType = e.Value.ToString();
                    _packageTypeList[e.RowIndex] = currentPackageType;
                    break;

                case "ColumnPrice":
                    
                    currentPackageType.PackagePrice = 
                        Decimal.Parse(e.Value.ToString(), NumberStyles.Currency);
                    _packageTypeList[e.RowIndex] = currentPackageType;
                    break;

                case "ColumnCost":
                    currentPackageType.PackageCost =
                        Decimal.Parse(e.Value.ToString(), NumberStyles.Currency);
                    _packageTypeList[e.RowIndex] = currentPackageType;
                    break;
            }
        }

        private void buttonAddToList_Click(object sender, EventArgs e)
        {
            string actorID = "0";
            if (comboBoxActor.SelectedValue != null)
            {
                actorID = comboBoxActor.SelectedValue.ToString();
            }
            dsEventActor.Tables[0].Rows.Add(comboBoxActor.Text.Trim(), actorID);
            dataGridViewActor.DataSource = dsEventActor.Tables[0];
        }

        private void dataGridViewActor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewActor.Columns["ColumnDelete"].Index == e.ColumnIndex)
            {
                DataRow row = dsEventActor.Tables[0].Rows[e.RowIndex];
                row.Delete();
                dataGridViewActor.DataSource = dsEventActor.Tables[0];
            }
        }

        private void checkBoxStretch_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxStretch.Checked)
            {
                pictureBoxPoster.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxPoster.Size = new Size(panelPoster.Width, panelPoster.Height);
            }
            else
            {
                pictureBoxPoster.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private void pictureBoxNewVenue_Click(object sender, EventArgs e)
        {
            VenueForm venueForm = new VenueForm();
            venueForm._isChildForm = true;
            if (venueForm.ShowDialog(this) == DialogResult.OK)
            {
                SAPVenueReadWrite venue =
                    new SAPVenueReadWrite(Config._dbConnectionName);
                comboBoxVenue.DataSource = venue.SelectAll().Venue;
                comboBoxVenue.DisplayMember =
                    SAPVenueReadWrite._venueNameColumnName;
                comboBoxVenue.ValueMember =
                    SAPVenueReadWrite._venueIDColumnName;
                comboBoxVenue.Text = venueForm._venueName;
            }
            venueForm.Dispose();
        }

        private void pictureBoxNewVenue_MouseHover(object sender, EventArgs e)
        {
            pictureBoxNewVenue.Cursor = Cursors.Hand;
        }

        private void pictureBoxNewVenue_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}