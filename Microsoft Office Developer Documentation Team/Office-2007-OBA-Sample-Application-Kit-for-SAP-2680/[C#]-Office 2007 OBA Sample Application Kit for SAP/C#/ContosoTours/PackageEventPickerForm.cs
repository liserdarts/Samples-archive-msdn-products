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
    public partial class PackageEventPickerForm : Form
    {
        private List<AssociatedEventList> _sourceEventList =
            new List<AssociatedEventList>();

        public AssociatedEventList _associatedEvent =
            new AssociatedEventList();

        public PackageEventPickerForm()
        {
            InitializeComponent();
        }

        private void PackagePickerForm_Load(object sender, EventArgs e)
        {
            GetEventList();
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

        private void comboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxName.SelectedIndex > -1 &&
              (_sourceEventList.Count - 1) >= comboBoxName.SelectedIndex)
            {
                AssociatedEventList item = _sourceEventList[comboBoxName.SelectedIndex];
                textBoxDate.Text = item.EventDate.ToString();
                textBoxVenue.Text = item.EventVenue;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            _associatedEvent = null;
            Close();
        }

        private void buttonAssociate_Click(object sender, EventArgs e)
        {
            _associatedEvent = new AssociatedEventList();
            if(_sourceEventList[comboBoxName.SelectedIndex]!=null)
            {
                _associatedEvent = 
                    _sourceEventList[comboBoxName.SelectedIndex];
            }
            if (_associatedEvent != null)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                _associatedEvent = null;
            }
            Close();
        }
    }
}
