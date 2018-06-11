using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.Helper;
using Microsoft.SAPSK.ContosoTours.Properties;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class PackageForm : Form
    {
        #region > declarations

        private byte[] _imageSelected = null;
     
        private List<AssociatedEventList> _sourceEvent;
        
        private List<AssociatedEventList> _removedEvent;
        
        private const string EventNameColumn = "EventNameColumn";
        
        private const string EventDateColumn = "EventDateColumn";
        
        private const string EventVenueColumn = "EventVenueColumn";
        
        private const string EventGoldPriceColumn = "EventGoldPriceColumn";
        
        private const string EventSilverPriceColumn = "EventSilverPriceColumn";
        
        private const string EventBronzePriceColumn = "EventBronzePriceColumn";
        
        private const string EventDeleteButton = "EventDeleteButton";

        public int _packageID = 0;

        public PackageList _packageItem = null;
        
        private List<AssociatedEventList> _sourceEventList;

        public bool _isReadOnly = false;

        private int _currentEventIndex = -1;

        #endregion

        public PackageForm()
        {
            InitializeComponent();
        }

        private DataGridViewColumn CreateColumn(
            string columnHeaderText,
            string columnName,
            int columnWidth,
            string formatStyle)
        {
            DataGridViewTextBoxColumn textBoxColumn =
                new DataGridViewTextBoxColumn();
            textBoxColumn.HeaderText = columnHeaderText;
            textBoxColumn.Name = columnName;
            textBoxColumn.Width = columnWidth;
            textBoxColumn.DefaultCellStyle.Format = formatStyle;
            return textBoxColumn;
        }

        private void GetEventList()
        {
            _sourceEventList = new List<AssociatedEventList>();
            SAPEventReadWrite eventRW =
                new SAPEventReadWrite(Config._dbConnectionName);
            using (SAPDataReaderEvent readerEvent =
                eventRW.ReaderSelectAll())
            {
                if (readerEvent.DataReader != null &&
                   readerEvent.DataReader.HasRows)
                {
                    while (readerEvent.DataReader.Read()) 
                    {
                        AssociatedEventList item = new AssociatedEventList();
                        item.EventID = readerEvent.EventID;
                        item.EventName = readerEvent.EventName;
                        item.EventDate = readerEvent.EventDate;
                        item.EventGoldPrice = readerEvent.GoldPackagePrice;
                        item.EventSilverPrice = readerEvent.SilverPackagePrice;
                        item.EventBronzePrice = readerEvent.BronzePackagePrice;
                        item.EventVenue = readerEvent.VenueName;
                        _sourceEventList.Add(item);
                    } //while (readerEvent.DataReader.Read());
                }
            }

            comboBoxName.DataSource = _sourceEventList;
            comboBoxName.DisplayMember = "EventName";
            comboBoxName.ValueMember = "EventID";
        }

        private void PopulateItems()
        {
            _sourceEvent = new List<AssociatedEventList>();
            if (_packageID > 0)
            {
                //get associated events 
                SAPPackageEventMapReadWrite eventMap =
                    new SAPPackageEventMapReadWrite(Config._dbConnectionName);

                using (SAPDataReaderPackageEventMap readerEventMap =
                    eventMap.ReaderSelectByPackageID(_packageID))
                {
                    if (readerEventMap.DataReader != null &&
                        readerEventMap.DataReader.HasRows)
                    {
                        while (readerEventMap.DataReader.Read()) 
                        {
                            AssociatedEventList item =
                            GetItem(readerEventMap.EventID);
                            item.EventID = readerEventMap.EventID;
                            item.IsNewID = false;
                            item.PackageMapID = readerEventMap.PackageEventMapID;
                            _sourceEvent.Add(item);
                        }// while (readerEventMap.DataReader.Read());
                    }
                }
                //get pacakage info
                SAPPackageReadWrite package =
                    new SAPPackageReadWrite(Config._dbConnectionName);
                using (SAPDataReaderPackage readerPackage =
                    package.ReaderSelectByPackageID(_packageID))
                {
                    if (readerPackage.DataReader != null &&
                        readerPackage.DataReader.HasRows)
                    {
                        readerPackage.DataReader.Read();
                        textBoxName.Text = readerPackage.PackageName;
                        textBoxDescription.Text = readerPackage.PackageDescription;
                        pictureBoxPoster.Image = 
                            UtilityHelper.ByteToImage(readerPackage.PackageImage);
                        _imageSelected = readerPackage.PackageImage;
                    }
                }
            }

            #region Create Datagrid
            dataGridViewEvents.VirtualMode = true;
            dataGridViewEvents.Columns.Add(
                CreateColumn("Name", EventNameColumn, 100, ""));
            dataGridViewEvents.Columns.Add(
                CreateColumn("Date", EventDateColumn, 100, ""));
            dataGridViewEvents.Columns.Add(
                CreateColumn("Venue", EventVenueColumn, 100, ""));
            dataGridViewEvents.Columns.Add(
                CreateColumn("Gold Price", EventGoldPriceColumn, 75, "c"));
            dataGridViewEvents.Columns.Add(
                CreateColumn("Silver", EventSilverPriceColumn, 75, "c"));
            dataGridViewEvents.Columns.Add(
                CreateColumn("Bronze", EventBronzePriceColumn, 75, "c"));

     
            #endregion

            _removedEvent = new List<AssociatedEventList>();
            dataGridViewEvents.RowCount = _sourceEvent.Count;
        }

        private AssociatedEventList GetItem(int eventID)
        {
            foreach (AssociatedEventList item in _sourceEventList)
            {
                if (item.EventID == eventID)
                {
                    return item;
                }
            }
            return null;
        }

        private bool IsNewID(int eventID)
        {
            foreach (AssociatedEventList item in _sourceEvent)
            {
                if (item.EventID == eventID)
                {
                    return false;
                }
            }
            return true;
        }

        private int IndexFromDeleted(int eventID)
        {
            for (int index = 0; index < _removedEvent.Count; index++)
            {
                AssociatedEventList item =
                    _removedEvent[index];
                if (item.EventID == eventID && !item.IsNewID)
                {
                    return index;
                }
            }
            return -1;
        }

        private bool IsNotExist(string eventName)
        {
            foreach (AssociatedEventList item in _sourceEvent)
            {
                if (item.EventName == eventName)
                {
                    return false;
                }
            }
            return true;
        }

        private int dataGridViewEventsCommand(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                return e.RowIndex;
            }
            return -1;
        }


        #region > events

        private void dataGridViewEvents_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (_sourceEvent.Count == 0)
            {
                return;
            }
            
            AssociatedEventList currentEvent =
                _sourceEvent[e.RowIndex];
            
            switch (dataGridViewEvents.Columns[e.ColumnIndex].Name)
            {
                case EventNameColumn:
                    e.Value = currentEvent.EventName;
                    break;

                case EventVenueColumn:
                    e.Value = currentEvent.EventVenue;
                    break;

                case EventDateColumn:
                    e.Value = currentEvent.EventDate;
                    break;

                case EventGoldPriceColumn:
                    e.Value = currentEvent.EventGoldPrice.ToString();
                    break;
                
                case EventSilverPriceColumn:
                    e.Value = currentEvent.EventSilverPrice.ToString();
                    break;
                
                case EventBronzePriceColumn:
                    e.Value = currentEvent.EventBronzePrice.ToString();
                    break;
            }
        }

        private void dataGridViewEvents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 6)
            {
                AssociatedEventList item = _sourceEvent[e.RowIndex];
                EventForm eventForm = new EventForm();
                eventForm._isReadOnly = true;
                eventForm._eventID = item.EventID;
                eventForm.ShowDialog(this);
                eventForm.Dispose();
            }
        }
        
        private void linkLabelUploadPoster_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog.ShowDialog(this);
            pictureBoxPoster.ImageLocation = openFileDialog.FileName;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            bool hasNoError = true;

            _packageItem = new PackageList();
            int packageID = _packageID;
            int packageEventMapID;
            bool isSuccess = false;

            #region Check Image file
            string fileName = openFileDialog.FileName;
            byte[] byteImage = null;

            if (File.Exists(fileName))
            {
                byteImage = UtilityHelper.FileToByte(fileName);
            }
            else if (_packageID > 0)
            {
                byteImage = _imageSelected;
            }
            else
            {
                byteImage = UtilityHelper.BitmapToByte(Resources.blank);
            }
            #endregion

            if (textBoxName.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBoxName, "Package name is required.");
                hasNoError = false;
            }
            if (textBoxDescription.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBoxDescription, "Package description is required.");
                hasNoError = false;
            }
            if (dataGridViewEvents.RowCount == 0)
            {
                errorProvider.SetError(dataGridViewEvents, "At least one event is required.");
                hasNoError = false;
            }
            if (byteImage == null)
            {
                errorProvider.SetError(pictureBoxPoster, "Poster is required.");
                hasNoError = false;
            }

            if (!hasNoError)
            {
                return;
            }

            using (SWPTransactionDBConnection dbTransaction =
                new SWPTransactionDBConnection(Config._dbConnectionName))
            {
                try
                {
                    SAPPackageReadWrite packageRW =
                        new SAPPackageReadWrite(dbTransaction);
                    SAPPackageEventMapReadWrite packageEventRW =
                        new SAPPackageEventMapReadWrite(dbTransaction);
                    if (_packageID  == 0)
                    {
                        packageRW.Insert(
                            textBoxName.Text,
                            textBoxDescription.Text,
                            byteImage,
                            out packageID);
                        foreach (AssociatedEventList item in _sourceEvent)
                        {
                            packageEventRW.Insert(
                                packageID,
                                item.EventID,
                                out packageEventMapID);
                        }
                    }
                    else
                    {
                        packageRW.Update(
                            _packageID,
                            textBoxName.Text,
                            textBoxDescription.Text,
                            byteImage);

                        foreach (AssociatedEventList item in _removedEvent)
                        {
                            packageEventRW.Delete(item.PackageMapID);
                        }

                        foreach (AssociatedEventList item in _sourceEvent)
                        {
                            if (item.IsNewID)
                            {
                                packageEventRW.Insert(
                                   _packageID,
                                   item.EventID,
                                   out packageEventMapID);
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
            if (isSuccess)
            {
                _packageItem.PackageName = textBoxName.Text;
                _packageItem.PackageDescription = textBoxDescription.Text;
                _packageItem.PackagePhoto = byteImage;
                _packageItem.PackageID = packageID;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
       
        private void NewPackage_Load(object sender, EventArgs e)
        {
            dataGridViewEvents.CellValueNeeded +=
                new DataGridViewCellValueEventHandler(dataGridViewEvents_CellValueNeeded);
            dataGridViewEvents.CellDoubleClick +=
                new DataGridViewCellEventHandler(dataGridViewEvents_CellDoubleClick);

            GetEventList();

            PopulateItems();

            if (_isReadOnly)
            {
                UtilityHelper.SetToReadOnly(Controls);
                buttonSave.Visible = false;
                buttonDeleteAssociatedEvent.Visible = false;
                buttonNewAssociatedEvent.Visible = false;
                this.Text = "Package View";

            }
        }

        private void comboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxName.SelectedIndex > -1 && 
                (_sourceEventList.Count -1) >= comboBoxName.SelectedIndex)
            {
                AssociatedEventList item = _sourceEventList[comboBoxName.SelectedIndex];
                textBoxDate.Text = item.EventDate.ToString();
                textBoxVenue.Text = item.EventVenue;
            }
        }

        private void checkBoxStretch_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxStretch.Checked)
            {
                pictureBoxPoster.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxPoster.Size = new Size(panelImage.Width, panelImage.Height);
            }
            else
            {
                pictureBoxPoster.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private void buttonNewAssociatedEvent_Click(object sender, EventArgs e)
        {
            PackageEventPickerForm pickEvent = new PackageEventPickerForm();
            if (pickEvent.ShowDialog(this) == DialogResult.OK)
            {

                if (pickEvent._associatedEvent!=null)
                {
                    AssociatedEventList item = pickEvent._associatedEvent;
                    int indexDeleted = IndexFromDeleted(item.EventID);
                    if (indexDeleted != -1)
                    {
                        item.IsNewID = false;
                        _removedEvent.RemoveAt(indexDeleted);
                        _sourceEvent.Add(item);
                    }
                    else if (IsNewID(item.EventID))
                    {
                        item.IsNewID = true;
                        _sourceEvent.Add(item);
                    }
                    dataGridViewEvents.RowCount = _sourceEvent.Count;
                }
            }
        }
        
        #endregion

        private void dataGridViewEvents_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _currentEventIndex = dataGridViewEventsCommand(e);
        }

   
        private void buttonDeleteAssociatedEvent_Click(object sender, EventArgs e)
        {
            if (_currentEventIndex >= 0)
            {
                AssociatedEventList assocList = _sourceEvent[_currentEventIndex];
                if (Message.DeleteMessage(assocList.EventName) == DialogResult.Yes)
                {
                    _removedEvent.Add(_sourceEvent[_currentEventIndex]);
                    _sourceEvent.RemoveAt(_currentEventIndex);
                    dataGridViewEvents.RowCount = _sourceEvent.Count;
                }

            }
        }

       

     

       
    }
}