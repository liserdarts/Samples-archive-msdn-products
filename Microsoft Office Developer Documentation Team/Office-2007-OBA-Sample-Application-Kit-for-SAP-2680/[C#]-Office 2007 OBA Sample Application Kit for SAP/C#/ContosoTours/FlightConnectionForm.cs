using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.SAPServices;
using Microsoft.SAPSK.ContosoTours.SAPServices.SAP_FLIGHTCONNLIST;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class FlightConnectionForm : Form
    {
        private List<FlightConnectionList> _connectionList;
        private BAPISCODAT[] _flConn = null;
        private delegate void AddListItem(BAPISCODAT item);
        private AddListItem addListItem;
       
        public FlightConnectionList _addedFlight = null;
        public string _flightCityTo;
        public string _flightCityFrom;
        public DateTime _eventDate;
        public DateTime _prevEventDate;

        public string _packageType;
        
        public FlightConnectionForm()
        {
            InitializeComponent();
        }

        private DateTime FlightDate(string flightDate)
        {
            return new DateTime(Convert.ToInt32(flightDate.Substring(0, 4)),
                Convert.ToInt32(flightDate.Substring(5, 2)),
                Convert.ToInt32(flightDate.Substring(8, 2)));
        }
        
        public void AddListItemMethod(BAPISCODAT item)
        {
            DateTime flightDate = FlightDate(item.FLIGHTDATE);
            
            if (flightDate.AddMonths(3) < _eventDate)
            {
                return;
            }
            else if (flightDate > _eventDate)
            {
                return;
            }
            else if (flightDate < DateTime.Now)
            {
                return;
            }
            else if (flightDate < _prevEventDate)
            {
                return;
            }

            //check availability first
            SAPFlightConnection flightConn = new SAPFlightConnection(Config.SAPUserName, Config.SAPPassword);
            if (!flightConn.GetDetail(
                item.FLIGHTCONN,
                item.FLIGHTDATE,
                "",
                item.AGENCYNUM))
            {
                return;
            }
            if (flightConn._bapiAvailability.Length > 0)
            {
                for (int counter = 0; counter < flightConn._bapiAvailability.Length; counter++)
                {
                    switch (_packageType)
                    {
                        case "Y":
                            if (flightConn._bapiAvailability[counter].ECONOFREE == 0)
                            {
                                return;
                            }
                            break;
                        case "C":
                            if (flightConn._bapiAvailability[counter].BUSINFREE == 0)
                            {
                                return;
                            }
                            break;
                        case "F":
                            if (flightConn._bapiAvailability[counter].FIRSTFREE == 0)
                            {
                                return;
                            }
                            break;
                    }
                }
            }
            else
            {
                return;
            }

            ListViewItem itemCreate = listViewFlight.Items.Add(item.FLIGHTDATE);
            itemCreate.SubItems.Add(item.DEPTIME);
            itemCreate.SubItems.Add(item.AIRPORTFR);
            itemCreate.SubItems.Add(item.ARRDATE);
            itemCreate.SubItems.Add(item.AIRPORTTO);
            itemCreate.SubItems.Add(item.ARRTIME);
            itemCreate.SubItems.Add(item.CITYFROM);
            itemCreate.SubItems.Add(item.CITYTO);

            FlightConnectionList itemList = new FlightConnectionList();
            itemList.AgencyNumber = item.AGENCYNUM;
            itemList.AirportFrom = item.AIRPORTFR;
            itemList.AirportTo = item.AIRPORTTO;
            itemList.ArrivalDate = item.ARRDATE;
            itemList.ArrivalTime = item.ARRTIME;
            itemList.CityFrom = item.CITYFROM;
            itemList.CityTo = item.CITYTO;
            itemList.DepartureTime = item.DEPTIME;
            itemList.FlightConnectionNumber = item.FLIGHTCONN;
            itemList.FlightDate = item.FLIGHTDATE;
            itemList.FlightTime = item.FLIGHTTIME;
            itemList.NumberHops = item.NUMHOPS;
            _connectionList.Add(itemList);

            labelNumRecords.Text = string.Format("{0} records", listViewFlight.Items.Count);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                listViewFlight.Items.Clear();
                addListItem = new AddListItem(AddListItemMethod);

                SAPFlightConnection _flightConnection = new SAPFlightConnection(Config.SAPUserName, Config.SAPPassword);

                string airlineID = string.Empty;
                string travelAgency = string.Empty;

                if (comboBoxAirline.SelectedIndex > -1)
                {
                    airlineID = (string)comboBoxAirline.SelectedValue;
                }
                if (comboBoxTravelAgency.SelectedIndex > -1)
                {
                    travelAgency = (string)comboBoxTravelAgency.SelectedValue;
                }

                PseudoProgressForm progress = new PseudoProgressForm();
                progress.ProgressLabel = "Querying SAP...";
                BackgroundWorker bgWorker = new BackgroundWorker();
                string destinationTo = comboBoxTo.Text;
                string destinationFrom = textBoxCity.Text;

                bgWorker.DoWork +=
                    delegate(object senderWorker, DoWorkEventArgs eWorker)
                    {
                        if (_flightConnection.GetList(
                                            airlineID,
                                            travelAgency,
                                            destinationFrom,
                                            destinationTo))
                        {
                            _flConn = _flightConnection._bapiConnectionList;

                            _connectionList = new List<FlightConnectionList>();

                            foreach (BAPISCODAT item
                                in _flConn)
                            {
                                Invoke(addListItem, item);
                            }
                        }
                    };
                bgWorker.RunWorkerCompleted +=
                    delegate(object senderWorker, RunWorkerCompletedEventArgs eWorker)
                    {
                        progress.Close();
                        progress.Dispose();

                        if (listViewFlight.Items.Count == 0)
                        {
                            Message.DisplayMessage("No records found.");
                        }
                    };

                bgWorker.RunWorkerAsync();
                progress.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorForm errorForm =
                    new ErrorForm(ex.Message, ex.ToString());
                errorForm.ShowDialog(this);
                errorForm.Close();                
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            _addedFlight = new FlightConnectionList();

            foreach (ListViewItem itemSelected in listViewFlight.SelectedItems)
            {
                _addedFlight = _connectionList[itemSelected.Index];
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FlightConnectionForm_Load(object sender, EventArgs e)
        {
            #region populate combobox travel agency
            SAPRefTravelAgencyReadWrite travelAgency =
                new SAPRefTravelAgencyReadWrite(Config._dbConnectionName);
            SAPDataSetRefTravelAgency.RefTravelAgencyDataTable dt =
                travelAgency.SelectAll().RefTravelAgency;
            comboBoxTravelAgency.DataSource = dt;
            comboBoxTravelAgency.DisplayMember = SAPRefTravelAgencyReadWrite._agencyNameColumnName;
            comboBoxTravelAgency.ValueMember = SAPRefTravelAgencyReadWrite._agencyNumberColumnName;
            comboBoxTravelAgency.SelectedValue = "00000110"; //Bavarian Castle
            #endregion

            #region populate combobox carrier
            SAPRefCarrierReadWrite carrier =
                new SAPRefCarrierReadWrite(Config._dbConnectionName);
            SAPDataSetRefCarrier.RefCarrierDataTable dtCarrier =
                carrier.SelectAll().RefCarrier;
            SAPDataSetRefCarrier.RefCarrierRow rowCarrier = dtCarrier.NewRefCarrierRow();
            rowCarrier.CarrID = string.Empty;
            rowCarrier.CarrName = string.Empty;
            rowCarrier.CurrCode = string.Empty;
            dtCarrier.Rows.InsertAt(rowCarrier, 0);
            comboBoxAirline.DataSource = dtCarrier;
            comboBoxAirline.DisplayMember = SAPRefCarrierReadWrite._carrNameColumnName;
            comboBoxAirline.ValueMember = SAPRefCarrierReadWrite._carrIDColumnName;
            #endregion

            #region populate city
            SAPRefFlightCityReadWrite flightCity =
                new SAPRefFlightCityReadWrite(Config._dbConnectionName);
            //SAPDataSetRefFlightCity.RefFlightCityDataTable dtFlightCityFrom =
            //    flightCity.SelectAll().RefFlightCity;
            SAPDataSetRefFlightCity.RefFlightCityDataTable dtFlightCityTo =
                flightCity.SelectAll().RefFlightCity;
                //(SAPDataSetRefFlightCity.RefFlightCityDataTable)dtFlightCityFrom.Copy();
            //comboBoxFrom.DataSource = dtFlightCityFrom;
            //comboBoxFrom.DisplayMember = SAPRefFlightCityReadWrite._cityNameColumnName;
            comboBoxTo.DataSource = dtFlightCityTo;
            comboBoxTo.DisplayMember = SAPRefFlightCityReadWrite._cityNameColumnName;
            comboBoxTo.Text = _flightCityTo;
            //comboBoxFrom.Text = _flightCityFrom;
            textBoxCity.Text = _flightCityFrom;
            #endregion
        }
    }
}