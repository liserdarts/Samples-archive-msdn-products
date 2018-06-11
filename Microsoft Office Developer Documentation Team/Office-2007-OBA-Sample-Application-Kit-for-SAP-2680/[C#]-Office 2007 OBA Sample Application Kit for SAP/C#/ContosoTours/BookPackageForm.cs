using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.Helper;
using Microsoft.SAPSK.ContosoTours.SAPServices;
using Microsoft.SAPSK.ContosoTours.SAPServices.SAP_FLIGHTCUSTOMERLIST;
using BAPIRET2=Microsoft.SAPSK.ContosoTours.SAPServices.SAP_FLIGHTTRIPCREATE.BAPIRET2;
using BAPIRET2X = Microsoft.SAPSK.ContosoTours.SAPServices.SAP_FLIGHTTRIPCREATE;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class BookPackageForm : Form
    {
        #region declaration
        public int _packageID;

        //private BAPISCUDAT[] customerList; 

        private SortedList<string, BAPISCUDAT> customerList;

        private List<FlightConnectionList> _itineraryList = null;

        private string _country;
        
        private string _language;

        private string _customerName = string.Empty;

        private string _customerNumber = string.Empty;
        
        private DataTable dtEventFlight;
        
        private bool _hasChanges = false;

        private SAPServices.SAP_FLIGHTCUSTOMERCREATE.BAPIRET2[] _errorCustomer = null;

        private BAPIRET2[] _errorFlight = null;

        public Dictionary<int, string> dictPackageType = null;

        private delegate void AddCustomerItem(string custName);
        
        private AddCustomerItem addCustomerItem;

        private DataTable dtCustomer = null;

        #endregion

        #region methods
        public BookPackageForm()
        {
            InitializeComponent();
        }

        private void CheckCustomer()
        {
            if (comboBoxCustomername.SelectedIndex > -1
                && comboBoxCustomername.SelectedIndex <
                customerList.Count)
            {
                if (comboBoxCustomername.SelectedValue != null)
                {
                    BAPISCUDAT itemSelected = customerList[(string)comboBoxCustomername.SelectedValue];
                    textboxAddress.Text = itemSelected.STREET;
                    textBoxCity.Text = itemSelected.CITY;
                    textboxPhone.Text = itemSelected.PHONE;
                    textBoxPostalCode.Text = itemSelected.POSTCODE;
                    comboBoxCountry.SelectedValue = itemSelected.COUNTR;
                }
            }
            else if (comboBoxCustomername.Items.Contains(comboBoxCustomername.Text))
            {
                comboBoxCustomername.SelectedIndex = comboBoxCustomername.Items.IndexOf(comboBoxCustomername.Text);
                if (comboBoxCustomername.SelectedValue != null)
                {
                    BAPISCUDAT itemSelected =
                       customerList[(string)comboBoxCustomername.SelectedValue];
                    textboxAddress.Text = itemSelected.STREET;
                    textBoxCity.Text = itemSelected.CITY;
                    textboxPhone.Text = itemSelected.PHONE;
                    textBoxPostalCode.Text = itemSelected.POSTCODE;
                    comboBoxCountry.SelectedValue = itemSelected.COUNTR;
                }
            }
        }
        #endregion

        #region events
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public void AddCustomer(string custName)
        {
            comboBoxCustomername.Items.Add(custName);
        }

        private void BookEvent_Load(object sender, EventArgs e)
        {
            #region populate combobox country
            SAPRefCountryReadWrite country =
                new SAPRefCountryReadWrite(Config._dbConnectionName);
            comboBoxCountry.DataSource = country.SelectAll().RefCountry;
            comboBoxCountry.DisplayMember = SAPRefCountryReadWrite._countryNameColumnName;
            comboBoxCountry.ValueMember = SAPRefCountryReadWrite._countryCodeColumnName;
            #endregion

            #region populate combobox language
            SAPRefLanguageReadWrite language =
                new SAPRefLanguageReadWrite(Config._dbConnectionName);
            comboBoxLanguage.DataSource = language.SelectAll().RefLanguage;
            comboBoxLanguage.DisplayMember = SAPRefLanguageReadWrite._languageColumnName;
            comboBoxLanguage.ValueMember = SAPRefLanguageReadWrite._languageCodeColumnName;
            #endregion

            #region create grid
            SAPPackageEventReadOnly packageEvent =
                new SAPPackageEventReadOnly(Config._dbConnectionName);
            dtEventFlight = new DataTable();
            dtEventFlight.Columns.Add("EventName", typeof(string));
            dtEventFlight.Columns.Add("EventVenue", typeof(string));
            dtEventFlight.Columns.Add("EventDate", typeof(DateTime));
            dtEventFlight.Columns.Add("FlightDate", typeof(string));
            dtEventFlight.Columns.Add("FlightTime", typeof(string));
            dtEventFlight.Columns.Add("FlightAirport", typeof(string));
            dtEventFlight.Columns.Add("FlightConnection", typeof(string));
            dtEventFlight.Columns.Add("FlightAgency", typeof(string));
            dtEventFlight.Columns.Add("VenueCity", typeof(string));
            dtEventFlight.Columns.Add("EventID", typeof(int));

            using (SAPDataReaderPackageEvent rdrEvent =
                packageEvent.ReaderSelectByPackageID(_packageID))
            {
                if (rdrEvent.DataReader != null &&
                    rdrEvent.DataReader.HasRows)
                {
                    while (rdrEvent.DataReader.Read())
                    {
                        dtEventFlight.Rows.Add(
                            rdrEvent.EventName,
                            rdrEvent.VenueName,
                            rdrEvent.EventDate,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            rdrEvent.VenueCity,
                            rdrEvent.EventID);
                    } //while (rdrEvent.DataReader.Read());
                }
            }
            dataGridViewEvent.DataSource = dtEventFlight;
            GridHelper.HideColumns(
                dataGridViewEvent, 
                "FlightConnection", 
                "FlightAgency", 
                "VenueCity",
                "EventID");

            #endregion

            #region get customer from SAP
            dtCustomer = new DataTable();
            dtCustomer.Columns.Add("customername", typeof(string));
            dtCustomer.Columns.Add("customerid", typeof(string));

            PseudoProgressForm progress = new PseudoProgressForm();
            progress.ProgressLabel = "Querying SAP...";
            BackgroundWorker bgWorker = new BackgroundWorker();

            addCustomerItem = new AddCustomerItem(AddCustomer);

            bgWorker.DoWork +=
                delegate(object senderWorker, DoWorkEventArgs eWorker)
                {
                    SAPCustomer sapCustomer = new SAPCustomer(Config.SAPUserName, Config.SAPPassword);
                    if (sapCustomer.GetList() && sapCustomer._customerList.Length > 0)
                    {
                        //customerList = sapCustomer._customerList;
                        customerList = new SortedList<string,BAPISCUDAT>();
                        foreach (BAPISCUDAT item in
                            sapCustomer._customerList)
                        {
                            if (!customerList.ContainsKey(item.CUSTOMERID))
                            {
                                customerList.Add(item.CUSTOMERID, item);
                                dtCustomer.Rows.Add(item.CUSTNAME, item.CUSTOMERID);
                            }
                        }
                    }
                };

            bgWorker.RunWorkerCompleted +=
                delegate(object senderWorker, RunWorkerCompletedEventArgs eWorker)
                {
                    progress.Close();
                    progress.Dispose();
                };

            bgWorker.RunWorkerAsync();
            progress.ShowDialog();
            dtCustomer.DefaultView.Sort = "customername";
            comboBoxCustomername.DataSource = dtCustomer.DefaultView;
            comboBoxCustomername.DisplayMember = "customername";
            comboBoxCustomername.ValueMember = "customerid";

            comboBoxCustomername.Text = string.Empty;
            #endregion
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            bool hasNoError = true;
            errorProvider.Clear();

            if (comboBoxCustomername.Text.Trim().Length == 0)
            {
                errorProvider.SetError(comboBoxCustomername, "Customer Name is required");
                hasNoError = false;
            }
            if (textBoxCity.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBoxCity, "City is required");
                hasNoError = false;
            }

            if (!_hasChanges)
            {
                errorProvider.SetError(dataGridViewEvent, "Not all event has flight information");
                hasNoError = false;
            }
            if (hasNoError)
            {
                #region verify item
                _itineraryList = new List<FlightConnectionList>();
                foreach (DataRow dr in dtEventFlight.Rows)
                {
                    FlightConnectionList itemList = new FlightConnectionList();
                    itemList.AgencyNumber = dr["FlightAgency"].ToString();
                    itemList.FlightConnectionNumber = dr["FlightConnection"].ToString();
                    itemList.FlightDate = dr["FlightDate"].ToString();
                    if (itemList.FlightDate.Length == 0)
                    {
                        errorProvider.SetError(dataGridViewEvent, "Not all event has flight information");
                        return;
                    }
                    itemList.EventID = Convert.ToInt32(dr["EventID"]);
                    _itineraryList.Add(itemList);
                }
                _country = (string)comboBoxCountry.SelectedValue;
                _language = (string)comboBoxLanguage.SelectedValue;
                _customerName = comboBoxCustomername.Text.Trim();
                _customerNumber = string.Empty;

                if (comboBoxCustomername.SelectedIndex > -1
                    && comboBoxCustomername.SelectedIndex <
                    customerList.Count)
                {
                    if (comboBoxCustomername.SelectedValue != null)
                    {
                        _customerNumber = customerList[(string)comboBoxCustomername.SelectedValue].CUSTOMERID;
                    }

                }
                else if (comboBoxCustomername.Items.Contains(_customerName))
                {
                    comboBoxCustomername.SelectedIndex = comboBoxCustomername.Items.IndexOf(_customerName);
                    if (comboBoxCustomername.SelectedValue != null)
                    {
                        BAPISCUDAT itemSelected =
                           customerList[(string)comboBoxCustomername.SelectedValue];
                        _customerNumber = itemSelected.CUSTOMERID;
                    }
                }
                #endregion

                PseudoProgressForm progress = new PseudoProgressForm();
                progress.ProgressLabel = "Saving to SAP...";
                BackgroundWorker bgWorker = new BackgroundWorker();
                
                string dob =
                    string.Format("{0:yyyy-MM-dd}", dateTimePicker.Value);
                string city = textBoxCity.Text.Trim();
                string address = textboxAddress.Text.Trim();
                string phone = textboxPhone.Text.Trim();
                string postalCode = textBoxPostalCode.Text.Trim();

                bgWorker.DoWork +=
                    delegate(object senderWorker, DoWorkEventArgs eWorker)
                    {
                        #region savecustomer
                        SAPCustomer customer = new SAPCustomer(Config.SAPUserName, Config.SAPPassword);
                        if (_customerNumber.Length == 0)
                        {
                            if (customer.CreateFromData(
                               city,
                               _country,
                               _customerName,
                               "P",
                               "none",
                               _language,
                               address,
                               phone,
                               postalCode))
                            {
                                //get customer number of inserted customer
                                bool found = false;
                                while (!found)
                                {
                                    customer.GetList();
                                    if (customer._customerList.Length > 0)
                                    {
                                        //search last 4
                                        for (int i = customer._customerList.Length - 1;
                                             i > (customer._customerList.Length - 5);
                                             i--)
                                        {
                                            if (customer._customerList[i].CUSTNAME.Trim() == _customerName.Trim())
                                            {
                                                _customerNumber = customer._customerList[i].CUSTOMERID;
                                                found = true;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                _errorCustomer = customer._bapiReturn;
                                eWorker.Cancel = true;
                            }
                        }
                        #endregion

                        #region create flight trip
                        if (_customerNumber.Length > 0)
                        {
                            SAPFlightTrip flightTrip = 
                                new SAPFlightTrip(Config.SAPUserName, Config.SAPPassword);
                            for (int index = 0; index < _itineraryList.Count; index++)
                            {
                                FlightConnectionList itemList = _itineraryList[index];
                                string classType = "Y";
                                if (dictPackageType.ContainsKey(itemList.EventID))
                                {
                                    classType = dictPackageType[itemList.EventID];
                                }
                                string tripNumber;
                                string travelAgencyNumber;
                                if (flightTrip.CreateTrip(
                                    itemList.AgencyNumber,
                                    classType,
                                    _customerNumber,
                                    itemList.FlightConnectionNumber,
                                    "",
                                    itemList.FlightDate,
                                    "",
                                    "none",
                                    dob,
                                    _customerName,
                                    out travelAgencyNumber,
                                    out tripNumber))
                                {
                                    itemList.TripNumber = tripNumber;
                                    _itineraryList[index] = itemList;
                                }
                                else
                                {
                                    _errorFlight = flightTrip._bapiFlTripReturn;
                                    eWorker.Cancel = true;
                                }
                            }
                        }
                        #endregion
                    };

                bgWorker.RunWorkerCompleted +=
                    delegate(object senderWorker, RunWorkerCompletedEventArgs eWorker)
                    {
                        progress.Close();

                        int eventAttendeeID = 0;
                        SAPEventAttendeeReadWrite eventAttendee =
                            new SAPEventAttendeeReadWrite(Config._dbConnectionName);
                        DateTime dateCreate;
                        eventAttendee.Insert(
                            _packageID,
                            _customerNumber,
                            dateTimePicker.Value,
                            out dateCreate,
                            out eventAttendeeID);

                        foreach (FlightConnectionList item in _itineraryList)
                        {
                            int eventAttendMapID = 0;
                            SAPEventAttendeeAgencyMapReadWrite eventMap =
                                new SAPEventAttendeeAgencyMapReadWrite(Config._dbConnectionName);
                            eventMap.Insert(
                                eventAttendeeID,
                                item.EventID,
                                item.AgencyNumber,
                                item.TripNumber,
                                out eventAttendMapID);
                        }
                        Message.DisplayInfo("Thanks for purchasing the package."); // Total Ticket Price: " + e.Result.ToString());
                        Close();
                    };

                bgWorker.RunWorkerAsync();
                progress.ShowDialog();

            }
        }

        private void dataGridViewEvent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (textBoxCity.Text.Trim().Length == 0)
                {
                    errorProvider.SetError(textBoxCity, "City is required");
                    return;
                }
                
                FlightConnectionForm form = new FlightConnectionForm();
                form._eventDate = (DateTime)dtEventFlight.Rows[e.RowIndex]["EventDate"];
                form._flightCityTo = dtEventFlight.Rows[e.RowIndex]["VenueCity"].ToString();
                form._flightCityFrom = textBoxCity.Text;
                form._prevEventDate = DateTime.MinValue;
                if (e.RowIndex > 0)
                {
                    form._prevEventDate = (DateTime)dtEventFlight.Rows[e.RowIndex - 1]["EventDate"];
                    form._flightCityFrom = dtEventFlight.Rows[e.RowIndex -1]["VenueCity"].ToString();
                }
                string classType = "Y";
                int eventID = (int)dtEventFlight.Rows[e.RowIndex]["EventID"];
                if (dictPackageType.ContainsKey(eventID))
                {
                    classType = dictPackageType[eventID];
                }
                form._packageType = classType;
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (form._addedFlight != null)
                    {
                        dtEventFlight.Rows[e.RowIndex]["FlightDate"] = form._addedFlight.FlightDate;
                        dtEventFlight.Rows[e.RowIndex]["FlightTime"] = form._addedFlight.DepartureTime;
                        dtEventFlight.Rows[e.RowIndex]["FlightAirport"] = form._addedFlight.AirportFrom;
                        dtEventFlight.Rows[e.RowIndex]["FlightConnection"] = form._addedFlight.FlightConnectionNumber;
                        dtEventFlight.Rows[e.RowIndex]["FlightAgency"] = form._addedFlight.AgencyNumber;
                        _hasChanges = true;
                    }
                }
                form.Dispose();
            }
        }

        private void comboBoxCustomername_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCustomername.SelectedValue.GetType() != typeof(string))
            {
                return;
            }
            CheckCustomer();
        }

        private void comboBoxCustomername_Validated(object sender, EventArgs e)
        {
            CheckCustomer();
        }

        private void dataGridViewEvent_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch (dataGridViewEvent.Columns[e.ColumnIndex].Name)
            {
                case "ColumnAddFlightImage":
                    dataGridViewEvent.Cursor = Cursors.Hand;
                    break;
            }
        }

        private void dataGridViewEvent_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            switch (dataGridViewEvent.Columns[e.ColumnIndex].Name)
            {
                case "ColumnAddFlightImage":
                    dataGridViewEvent.Cursor = Cursors.Default;
                    break;
            }
        }
        #endregion

    }
}
