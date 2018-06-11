using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.SAPSK.ContosoTours.DAL;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class BuyPackageControl : UserControl
    {
        #region > Properties
        private List<AssociatedEventList> _sourceEvent;
        private Dictionary<int,AssociatedEventList> _eventList;

        private int _packageID = 0;

        private ArrayList _packageTypes = null;

        private List<AssociatedEventList> SourceEvent
        {
            get
            {
                return _sourceEvent;
            }
            set
            {
                _sourceEvent = value;
            }
        }

        private ArrayList PackageTypes
        {
            get
            {
                if (_packageTypes == null)
                {
                    _packageTypes = new ArrayList();
                    _packageTypes.Add("Gold");
                    _packageTypes.Add("Silver");
                    _packageTypes.Add("Bronze");
                }
                return _packageTypes;
            }
        }

        public int PackageID
        {
            set
            {
                _packageID = value;
            }
        }

        #endregion

        public BuyPackageControl()
        {
            InitializeComponent();
        }

        public void RefreshList()
        {
            #region populate package combobox

            SAPPackageReadWrite packageRW =
                new SAPPackageReadWrite(Config._dbConnectionName);
            SAPDataSetPackage.PackageDataTable dt =
                packageRW.SelectAll().Package;
            SAPDataSetPackage.PackageRow row = dt.NewPackageRow();
            row.PackageName = string.Empty;
            row.PackageID = 0;
            row.PackageDescription = "";
            row.PackageImage = new byte[0];
            dt.Rows.InsertAt(row, 0);

            int index = 0;
            if(comboBoxPackage.SelectedIndex > 0)
            {
                index = comboBoxPackage.SelectedIndex;
            }

            comboBoxPackage.DataSource = dt;
            comboBoxPackage.DisplayMember =
                SAPPackageReadWrite._packageNameColumnName;
            comboBoxPackage.ValueMember =
                SAPPackageReadWrite._packageIDColumnName;
            if (comboBoxPackage.Items.Count > index)
            {
                comboBoxPackage.SelectedIndex = index;
            }
            #endregion

            CreateEvents();

            if (comboBoxPackage.SelectedIndex > 0)
            {
                //default to index 0
                _packageID = (int)comboBoxPackage.SelectedValue;
                GetAssociatedEvents();
            } 
        }

        private void GetAssociatedEvents()
        {
            dataGridViewEvents.RowCount = 0;
            SourceEvent = new List<AssociatedEventList>();
            decimal price = 0;
            
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
                        if (item != null)
                        {
                            price += item.EventGoldPrice;
                            item.PackageType = "Gold";
                            SourceEvent.Add(item);
                        }
                    } //while (readerEventMap.DataReader.Read());
                }
            }
            labelTotalPrice.Text = price.ToString("c");
            dataGridViewEvents.RowCount = SourceEvent.Count;
        }

        private AssociatedEventList GetItem(int eventID)
        {
            if (_eventList == null)
            {
                _eventList = new Dictionary<int, AssociatedEventList>();
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
                            item.EventGoldPrice = readerEvent.GoldPackagePrice;
                            item.EventSilverPrice = readerEvent.SilverPackagePrice;
                            item.EventBronzePrice = readerEvent.BronzePackagePrice;
                            _eventList.Add(item.EventID, item);
                        } //while (readerEvent.DataReader.Read());
                    }
                }
            }
            if (_eventList.ContainsKey(eventID))
            {
                return _eventList[eventID];
            }
            return null;
        }

        public void CreateEvents()
        {
            dataGridViewEvents.Columns.Clear();

            dataGridViewEvents.VirtualMode = true;
            dataGridViewEvents.CellValueNeeded += 
                new DataGridViewCellValueEventHandler(dataGridViewEvents_CellValueNeeded);
            dataGridViewEvents.CellValuePushed += 
                new DataGridViewCellValueEventHandler(dataGridViewEvents_CellValuePushed);

            DataGridViewTextBoxColumn textBoxCol =
                new DataGridViewTextBoxColumn();
            textBoxCol.HeaderText = "Event Name";
            textBoxCol.Name = "EventName";
            textBoxCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            textBoxCol.ReadOnly = true;

            DataGridViewComboBoxColumn comboBoxCol =
                new DataGridViewComboBoxColumn();
            comboBoxCol.HeaderText = "Promo Type";
            comboBoxCol.Name = "PackageType";
            comboBoxCol.DataSource = PackageTypes;

            int colIndex = dataGridViewEvents.Columns.Add(comboBoxCol);
            dataGridViewEvents.Columns[colIndex].Frozen = true;
            dataGridViewEvents.Columns.Add(textBoxCol);
        }

        private void SetTotalPrice()
        {
            decimal price = 0;
            foreach (AssociatedEventList item in SourceEvent)
            {
                switch (item.PackageType)
                {
                    case "Gold":
                        price += item.EventGoldPrice;
                        break;
                    case "Silver":
                        price += item.EventSilverPrice;
                        break;
                    case "Bronze":
                        price += item.EventBronzePrice;
                        break;
                    default:
                        price += item.EventGoldPrice;
                        break;
                }
            }
            labelTotalPrice.Text = price.ToString("c");
        }

        private void dataGridViewEvents_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            if (SourceEvent.Count == 0)
            {
                return;
            }

            AssociatedEventList currentEvent =
                SourceEvent[e.RowIndex];
            
            switch (dataGridViewEvents.Columns[e.ColumnIndex].Name)
            {
                case "PackageType":
                    currentEvent.PackageType = e.Value.ToString();
                    SourceEvent[e.RowIndex] = currentEvent;
                    SetTotalPrice();
                    break;
            }
        }

        private void dataGridViewEvents_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (SourceEvent.Count == 0)
            {
                return;
            }

            AssociatedEventList currentEvent =
                SourceEvent[e.RowIndex];
            
            switch (dataGridViewEvents.Columns[e.ColumnIndex].Name)
            {
                case "EventName":
                    e.Value = currentEvent.EventName;
                    break;

                case "PackageType":
                    e.Value = currentEvent.PackageType;
                    break;
            }
        }

        private void BuyPackage_Load(object sender, EventArgs e)
        {
            RefreshList();
            dataGridViewEvents.Enabled = false;
            buttonBuyPackage.Enabled = false;
        }

        private void buttonBuyPackage_Click(object sender, EventArgs e)
        {
            BookPackageForm bookEvent = new BookPackageForm();
            bookEvent._packageID = _packageID;
            bookEvent.dictPackageType = new Dictionary<int, string>();
            foreach (AssociatedEventList item in SourceEvent)
            {
                switch (item.PackageType)
                {
                    case "Gold":
                        bookEvent.dictPackageType.Add(item.EventID, "F");
                        break;
                    case "Silver":
                        bookEvent.dictPackageType.Add(item.EventID, "C");
                        break;
                    case "Bronze":
                        bookEvent.dictPackageType.Add(item.EventID, "Y");
                        break;
                    default:
                        bookEvent.dictPackageType.Add(item.EventID, "Y");
                        break;
                }
            }
            bookEvent.ShowDialog(this);
            bookEvent.Dispose();
        }

        private void BuyPackageControl_Resize(object sender, EventArgs e)
        {
            panelBuyPackage.Location = new Point(
                (panelBottom.Width - panelBuyPackage.Width)/2,
                panelBuyPackage.Location.Y);
            panelView.Location = new Point(
                (panelSelectPackage.Width - panelView.Width) / 2,
                panelView.Location.Y);
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            if (comboBoxPackage.SelectedIndex > 0)
            {
                PackageForm packageForm = new PackageForm();
                packageForm._isReadOnly = true;
                packageForm._packageID = (int)comboBoxPackage.SelectedValue;
                packageForm.ShowDialog(this);
                packageForm.Dispose();
            }
        }

        private void comboBoxPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPackage.SelectedIndex > 0)
            {
                _packageID = (int)comboBoxPackage.SelectedValue;
                GetAssociatedEvents();
                dataGridViewEvents.Enabled = true;
                buttonBuyPackage.Enabled = true;
                buttonView.Enabled = true;
                buttonViewPresentation.Enabled = true;
            }
            else
            {
                dataGridViewEvents.Enabled = false;
                buttonBuyPackage.Enabled = false;
                buttonView.Enabled = false;
                buttonViewPresentation.Enabled = false;
                dataGridViewEvents.RowCount = 0;
                labelTotalPrice.Text = string.Empty;
            }
        }

        private void dataGridViewEvents_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox comboBoxPackageType = e.Control as ComboBox;
            if (comboBoxPackageType != null)
            {
                comboBoxPackageType.SelectedIndexChanged -= new EventHandler(comboBoxPackageType_SelectedIndexChanged);
                comboBoxPackageType.SelectedIndexChanged += new EventHandler(comboBoxPackageType_SelectedIndexChanged);
            }
        }

        void comboBoxPackageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedItem != null)
            {
                SourceEvent[dataGridViewEvents.CurrentRow.Index].PackageType = cb.SelectedItem.ToString();
                SetTotalPrice();
            }
        }

        private void buttonViewPresentation_Click(object sender, EventArgs e)
        {
            if (comboBoxPackage.SelectedIndex > 0)
            {
                Helper.PowerPointHelper.OpenPowerPoint(_packageID);
            }
        }
    }
}
