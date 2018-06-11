using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Collections;

using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Core;
using CustomTaskPane = Microsoft.Office.Tools.CustomTaskPane;

using Microsoft.SAPSK.ContosoTours.Controls;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.Helper;
using Microsoft.SAPSK.ContosoTours.Properties;
using Microsoft.SAPSK.ContosoTours.SAPServices;
using Microsoft.SAPSK.ContosoTours.Schema;


namespace Microsoft.SAPSK.ContosoTours
{
    public partial class RibbonExcel : OfficeRibbon
    {
        private CustomTaskPane customTaskPaneBuy,
            customTaskPaneOutline, customTaskPaneEvents;

        #region Properties

        private bool analysisMode = false;

        private bool AnalysisMode
        {
            set
            {
                analysisMode = value;
                groupEvents.Visible = !analysisMode;
                groupListings.Visible = !analysisMode;
                groupPackages.Visible = !analysisMode;
                groupAnalysis.Visible = analysisMode;
                if (analysisMode)
                {
                    toggleButtonSwitchMode.Label = "Switch to General";
                }
                else
                {
                    toggleButtonSwitchMode.Label = "Switch to Analysis";
                }
            }
            get
            {
                return analysisMode;
            }
        }

        private bool beginTransaction = false;

        private bool BeginTransaction
        {
            set
            {
                beginTransaction = value;
                buttonLogin.Enabled = !beginTransaction;
                toggleButtonSwitchMode.Enabled = beginTransaction;
                toggleButtonBuyPackage.Enabled = beginTransaction;
                toggleButtonPackageOutline.Enabled = beginTransaction;
                toggleButtonSearchEvents.Enabled = beginTransaction;
                buttonEventManagement.Enabled = beginTransaction;
                buttonFlightList.Enabled = beginTransaction;
                buttonListofCustomers.Enabled = beginTransaction;
                buttonPackageManagement.Enabled = beginTransaction;
                buttonVenues.Enabled = beginTransaction;
            }
            get
            {
                return beginTransaction;
            }
        }

        #endregion

        public RibbonExcel()
        {
            InitializeComponent();
        }

        private void RibbonExcel_Load(object sender, RibbonUIEventArgs e)
        {
            BeginTransaction = false;
            AnalysisMode = false;
        }

        #region SAP Group Event Handler

        private void buttonLogin_Click(object sender, RibbonControlEventArgs e)
        {
            Login loginForm = new Login();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                BeginTransaction = !BeginTransaction;

                if (!Config.SeedData || Config.ShowSeedDataForm)
                {
                    SeedDataForm seedForm = new SeedDataForm();
                    seedForm.ShowDialog();
                }
                else if (UtilityHelper.GetMonthDifference(Config.DateLastSeed,
                    DateTime.Now) > Config.SeedDataExpiration || Config.ShowReseedDataForm)
                {
                    ReseedDataForm reseedForm = new ReseedDataForm();
                    reseedForm.ShowDialog();
                }
                else if (DataHelper.AreEventsOutdated())
                {
                    ReseedDataForm reseedForm = new ReseedDataForm();
                    reseedForm.ShowDialog();
                }

            }
        }

        #endregion

        #region Mode Group Event Handler

        private void toggleButtonSwitchMode_Click(object sender, RibbonControlEventArgs e)
        {
            AnalysisMode = !AnalysisMode;
        }

        #endregion

        #region Package Group Event Handler

        private void toggleButtonBuyPackage_Click(object sender, RibbonControlEventArgs e)
        {
            if (toggleButtonBuyPackage.Checked)
            {
                customTaskPaneBuy = 
                    Globals.ThisAddIn.CustomTaskPanes.Add(new BuyPackageControl(), "Buy Packages");
                customTaskPaneBuy.VisibleChanged += 
                    new EventHandler(customTaskPaneBuy_VisibleChanged);
                customTaskPaneBuy.Width = 280;
                customTaskPaneBuy.Visible = true;
            }
            else
            {
                customTaskPaneBuy.Visible = false;
            }
        }

        private void customTaskPaneBuy_VisibleChanged(object sender, EventArgs e)
        {
            if (!customTaskPaneBuy.Visible)
            {
                toggleButtonBuyPackage.Checked =
                    customTaskPaneBuy.Visible;
                customTaskPaneBuy.Dispose();
            }
        }

        private void toggleButtonPackageOutline_Click(object sender, RibbonControlEventArgs e)
        {
            if (toggleButtonPackageOutline.Checked)
            {
                customTaskPaneOutline =
                    Globals.ThisAddIn.CustomTaskPanes.Add(new Controls.PackageListControl(), "Package Outline");
                customTaskPaneOutline.VisibleChanged += new EventHandler(customTaskPaneOutline_VisibleChanged);
                customTaskPaneOutline.Width = 270;
                customTaskPaneOutline.Visible = true;
            }
            else
            {
                customTaskPaneOutline.Visible = false;
            }
        }

        private void customTaskPaneOutline_VisibleChanged(object sender, EventArgs e)
        {
            if (!customTaskPaneOutline.Visible)
            {
                toggleButtonPackageOutline.Checked =
                    customTaskPaneOutline.Visible;
                customTaskPaneOutline.Dispose();
            }
        }

        private void buttonPackageManagement_Click(object sender, RibbonControlEventArgs e)
        {
            ManagePackageForm managePackageForm = new ManagePackageForm();
            managePackageForm.ShowDialog();
            managePackageForm.Dispose();
        }

        #endregion 

        #region Events Group Event Handler

        private void toggleButtonSearchEvents_Click(object sender, RibbonControlEventArgs e)
        {
            if (toggleButtonSearchEvents.Checked)
            {
                customTaskPaneEvents =
                    Globals.ThisAddIn.CustomTaskPanes.Add(new EventListControl(), "Event Management");
                customTaskPaneEvents.VisibleChanged += new EventHandler(customTaskPaneEvents_VisibleChanged);
                customTaskPaneEvents.Width = 270;
                customTaskPaneEvents.Visible = true;
            }
            else
            {
                customTaskPaneEvents.Visible = false;
            }
        }

        private void customTaskPaneEvents_VisibleChanged(object sender, EventArgs e)
        {
            if (!customTaskPaneEvents.Visible)
            {
                toggleButtonSearchEvents.Checked =
                    customTaskPaneEvents.Visible;
                customTaskPaneEvents.Dispose();
            }
        }

        private void buttonEventManagement_Click(object sender, RibbonControlEventArgs e)
        {
            ManageEventForm manageEventForm = new ManageEventForm();
            manageEventForm.ShowDialog();
            manageEventForm.Dispose();
        }

        private void buttonVenues_Click(object sender, RibbonControlEventArgs e)
        {
            VenueForm venueForm = new VenueForm();
            venueForm.ShowDialog();
            venueForm.Dispose();
        }

        #endregion

        #region Listing Group Event Handlers

        private void buttonListofCustomers_Click(object sender, RibbonControlEventArgs e)
        {
            PseudoProgressForm progress = new PseudoProgressForm();
            progress.ProgressLabel = "Querying SAP...";
            BackgroundWorker backgroundWorker = new BackgroundWorker();

            Customers customerDataset = new Customers();

            backgroundWorker.DoWork +=
                delegate(object workSender, DoWorkEventArgs eventArg)
                {
                    SAPEventAttendeeReadWrite attendee = new SAPEventAttendeeReadWrite(Config._dbConnectionName);
                    SAPDataSetEventAttendee attendeeData = attendee.SelectAll();

                    SAPCustomer sapCustomer =
                        new SAPCustomer(Config.SAPUserName, Config.SAPPassword);

                    sapCustomer.GetList();

                    Customers.CustomerDataTable customerData = customerDataset.Customer;

                    foreach (SAPServices.SAP_FLIGHTCUSTOMERLIST.BAPISCUDAT customer in sapCustomer._customerList)
                    {
                        DataRow[] dr =
                            attendeeData.EventAttendee.Select("CustomerNumber = " + customer.CUSTOMERID.Trim());

                        int IsContosoCustomer = dr.Length > 0 ? 1 : 0;

                        customerData.AddCustomerRow(
                            customer.CUSTNAME,
                            customer.STREET,
                            customer.POBOX,
                            customer.POSTCODE,
                            customer.CITY,
                            customer.COUNTR,
                            customer.PHONE,
                            IsContosoCustomer);
                    }
                };

            backgroundWorker.RunWorkerCompleted +=
                delegate(object workSender, RunWorkerCompletedEventArgs eventArg)
                {
                    progress.Close();
                    ExcelHelper.LoadExcelSheet("ListOfCustomer", Resources.ListOfCustomers, customerDataset);
                };

            backgroundWorker.RunWorkerAsync();
            progress.ShowDialog();

        }

        private void buttonFlightList_Click(object sender, RibbonControlEventArgs e)
        {
            PseudoProgressForm progress = new PseudoProgressForm();
            progress.ProgressLabel = "Querying SAP...";
            BackgroundWorker backgroundWorker = new BackgroundWorker();

            Flights flightDataset = new Flights();

            backgroundWorker.DoWork +=
                delegate(object workSender, DoWorkEventArgs eventArg)
                {
                    SAPFlight flightHelper =
                        new SAPFlight(Config.SAPUserName, Config.SAPPassword);

                    flightHelper.GetList();

                    Flights.FlightDataTable flightTable = flightDataset.Flight;

                    foreach (SAPServices.SAP_FLIGHTLIST.BAPISFLDAT flight in flightHelper._flightList)
                    {
                        DateTime flDate = Convert.ToDateTime(flight.FLIGHTDATE);
                        if (flDate > DateTime.Today)
                        {
                            flightHelper.CheckAvailability(flight.AIRLINEID, flight.CONNECTID, flight.FLIGHTDATE);
                            flight.FLIGHTDATE = flDate.ToShortDateString();

                            flightTable.AddFlightRow(
                                flight.AIRLINE,
                                flight.CITYFROM,
                                flight.CITYTO,
                                flight.AIRPORTFR,
                                flight.AIRPORTTO,
                                flight.FLIGHTDATE,
                                flight.DEPTIME,
                                flight.ARRDATE,
                                flight.ARRTIME,
                                flightHelper._availability.FIRSTFREE,
                                flightHelper._availability.BUSINFREE,
                                flightHelper._availability.ECONOFREE);
                        }
                    }
                };

            backgroundWorker.RunWorkerCompleted +=
                delegate(object workSender, RunWorkerCompletedEventArgs eventArg)
                {
                    progress.Close();
                    ExcelHelper.LoadExcelSheet("ListOfFlights", Resources.ListOfFlights, flightDataset);
                };

            backgroundWorker.RunWorkerAsync();
            progress.ShowDialog();

        }
        #endregion

        #region Analysis Group Event Handlers

        private void buttonRevenueForecast_Click(object sender, RibbonControlEventArgs e)
        {
            SelectYearForm selectYearForm = new SelectYearForm();
            selectYearForm.ShowDialog();
            int year = selectYearForm.SelectedYear;
            selectYearForm.Dispose();

            if (year == 0)
            {
                return;
            }

            PseudoProgressForm progress = new PseudoProgressForm();
            progress.ProgressLabel = "Querying SAP...";
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            PackagesRevenue revenueDataset = new PackagesRevenue();

            backgroundWorker.DoWork +=
                delegate(object workSender, DoWorkEventArgs eventArg)
                {
                    PackagesRevenue.PackageRevenueDataTable revenueTable =
                        revenueDataset.PackageRevenue;

                    Dictionary<string, ArrayList> allSales =
                        new Dictionary<string, ArrayList>();

                    allSales.Add("Gold", new ArrayList());
                    allSales.Add("Silver", new ArrayList());
                    allSales.Add("Bronze", new ArrayList());

                    allSales["Gold"].Add("Gold");
                    allSales["Silver"].Add("Silver");
                    allSales["Bronze"].Add("Bronze");

                    Sales[] aSales = new Sales[12];
                    for (int month = 0; month < aSales.Length; month++)
                    {
                        aSales[month] = DataHelper.GetSalesData(month + 1, year);

                        allSales["Gold"].Add(0);
                        allSales["Silver"].Add(0);
                        allSales["Bronze"].Add(0);

                        foreach (Sales.EventSaleRow eventSaleRow in aSales[month].EventSale.Rows)
                        {
                            allSales[eventSaleRow.PackageType][month + 1] =
                                Convert.ToDecimal(allSales[eventSaleRow.PackageType][month + 1]) +
                                (eventSaleRow.Price - eventSaleRow.Cost);
                        }
                    }

                    allSales["Gold"].Add(year);
                    allSales["Silver"].Add(year);
                    allSales["Bronze"].Add(year);

                    revenueTable.LoadDataRow(allSales["Gold"].ToArray(), true);
                    revenueTable.LoadDataRow(allSales["Silver"].ToArray(), true);
                    revenueTable.LoadDataRow(allSales["Bronze"].ToArray(), true);

                };

            backgroundWorker.RunWorkerCompleted +=
                delegate(object workSender, RunWorkerCompletedEventArgs eventArg)
                {
                    progress.Close();
                    progress.Dispose();

                    ExcelHelper.LoadExcelSheet(
                        "RevenueForecast",
                        Resources.RevenueForecast,
                        revenueDataset);
                };

            backgroundWorker.RunWorkerAsync();
            progress.ShowDialog();

        }

        private void buttonPackageSales_Click(object sender, RibbonControlEventArgs e)
        {
            PseudoProgressForm progress = new PseudoProgressForm();
            progress.ProgressLabel = "Querying SAP...";
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            PackageSales packageSalesDataset = new PackageSales();

            backgroundWorker.DoWork +=
                delegate(object workSender, DoWorkEventArgs eventArg)
                {
                    PackageSales.PackageSaleDataTable packageSalesTable =
                        packageSalesDataset.PackageSale;

                    Sales sales = DataHelper.GetSalesData();

                    foreach (Sales.PackageRow packageRow in sales.Package.Rows)
                    {
                        PackageSales.PackageSaleRow packageSaleRow = packageSalesTable.NewPackageSaleRow();
                        packageSaleRow.PackageName = packageRow.PackageName;
                        packageSaleRow.NumberOfSales = 0;
                        packageSaleRow.PriceOfSales = 0;
                        foreach (DataRow row in packageRow.GetChildRows("Package_PackageSale"))
                        {
                            Sales.PackageSaleRow salesPackageSaleRow = row as Sales.PackageSaleRow;
                            packageSaleRow.NumberOfSales += 1;
                            packageSaleRow.PriceOfSales += salesPackageSaleRow.Price;
                        }
                        packageSalesTable.AddPackageSaleRow(packageSaleRow);
                    }

                };

            backgroundWorker.RunWorkerCompleted +=
                delegate(object workSender, RunWorkerCompletedEventArgs eventArg)
                {
                    progress.Close();
                    progress.Dispose();

                    ExcelHelper.LoadExcelSheet(
                        "PackageSalesDistribution",
                        Resources.PackageSalesDistribution,
                        packageSalesDataset);
                };

            backgroundWorker.RunWorkerAsync();
            progress.ShowDialog();

        }

        private void buttonPackageType_Click(object sender, RibbonControlEventArgs e)
        {
            PseudoProgressForm progress = new PseudoProgressForm();
            progress.ProgressLabel = "Querying SAP...";
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            PackagePerPromoType packageSalesPerTypeDataset =
                new PackagePerPromoType();

            backgroundWorker.DoWork +=
                delegate(object workSender, DoWorkEventArgs eventArg)
                {
                    PackagePerPromoType.PackageDataTable packageSalesTable =
                        packageSalesPerTypeDataset.Package;

                    Sales sales = DataHelper.GetSalesData();

                    foreach (Sales.PackageRow packageRow in sales.Package.Rows)
                    {
                        PackagePerPromoType.PackageRow packageSaleRow = packageSalesTable.NewPackageRow();
                        packageSaleRow.PackageID = packageRow.PackageID;
                        packageSaleRow.PackageName = packageRow.PackageName;
                        packageSaleRow.GoldPackageCount = 0;
                        packageSaleRow.SilverPackageCount = 0;
                        packageSaleRow.BronzePackageCount = 0;
                        packageSaleRow.GoldPackageTotalSales = 0;
                        packageSaleRow.SilverPackageTotalSales = 0;
                        packageSaleRow.BronzePackageTotalSales = 0;
                        foreach (DataRow saleRow in packageRow.GetChildRows("Package_PackageSale"))
                        {
                            Sales.PackageSaleRow salesPackageSaleRow = saleRow as Sales.PackageSaleRow;
                            foreach (DataRow eventRow in salesPackageSaleRow.GetChildRows("PackageSale_EventSale"))
                            {
                                Sales.EventSaleRow salesEventSaleRow = eventRow as Sales.EventSaleRow;

                                switch (salesEventSaleRow.PackageType)
                                {
                                    case "Gold":
                                        packageSaleRow.GoldPackageCount += 1;
                                        packageSaleRow.GoldPackageTotalSales += salesEventSaleRow.Price;
                                        break;
                                    case "Silver":
                                        packageSaleRow.SilverPackageCount += 1;
                                        packageSaleRow.SilverPackageTotalSales += salesEventSaleRow.Price;
                                        break;
                                    case "Bronze":
                                        packageSaleRow.BronzePackageCount += 1;
                                        packageSaleRow.BronzePackageTotalSales += salesEventSaleRow.Price;
                                        break;
                                }
                            }
                        }
                        packageSalesTable.AddPackageRow(packageSaleRow);
                    }

                };

            backgroundWorker.RunWorkerCompleted +=
                delegate(object workSender, RunWorkerCompletedEventArgs eventArg)
                {
                    progress.Close();
                    progress.Dispose();

                    ExcelHelper.LoadExcelSheet(
                        "PackageSalesPerPromoType",
                        Resources.PackageSalesPerPromoType,
                        packageSalesPerTypeDataset);
                };

            backgroundWorker.RunWorkerAsync();
            progress.ShowDialog();

        }

        private void buttonTicket_Click(object sender, RibbonControlEventArgs e)
        {
            PseudoProgressForm progress = new PseudoProgressForm();
            progress.ProgressLabel = "Querying SAP...";
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            TicketSales ticketDataset = new TicketSales();

            backgroundWorker.DoWork +=
                delegate(object workSender, DoWorkEventArgs eventArg)
                {
                    TicketSales.TicketSaleDataTable ticketSaleDataTable =
                        ticketDataset.TicketSale;

                    SAPEventAttendeeAgencyMapReadWrite attendeeAgency =
                        new SAPEventAttendeeAgencyMapReadWrite(Config._dbConnectionName);

                    SAPRefTravelAgencyReadWrite travelAgency =
                        new SAPRefTravelAgencyReadWrite(Config._dbConnectionName);

                    //get all agencies where client booked flights to
                    Dictionary<string, string> agencyList = new Dictionary<string, string>();
                    using (SAPDataReaderEventAttendeeAgencyMap reader =
                        attendeeAgency.ReaderSelectAll())
                    {
                        if (reader.DataReader.HasRows)
                        {
                            while (reader.DataReader.Read())
                            {
                                if (!agencyList.ContainsKey(reader.AgencyNumber))
                                {
                                    string agencyName=string.Empty;
                                    using(SAPDataReaderRefTravelAgency travelReader=
                                        travelAgency.ReaderSelectByAgencyNumber(reader.AgencyNumber))
                                    {
                                        travelReader.DataReader.Read();
                                        agencyName=travelReader.AgencyName;
                                    }
                                    
                                    agencyList.Add(
                                        reader.AgencyNumber,
                                        agencyName);
                                }
                            }                         }
                    }

                    SAPFlightTrip flightTripHelper =
                        new SAPFlightTrip(Config.SAPUserName, Config.SAPPassword);

                    SAPFlightConnection flightConnectionHelper =
                        new SAPFlightConnection(Config.SAPUserName, Config.SAPPassword);

                    foreach (KeyValuePair<string, string> agency in agencyList)
                    {
                        TicketSales.TicketSaleRow ticketSaleRow =
                            ticketSaleDataTable.NewTicketSaleRow();

                        ticketSaleRow.AgencyName = agency.Value;
                        ticketSaleRow.TicketsSold = 0;
                        ticketSaleRow.TotalTicketPrice = 0;

                        flightTripHelper.GetList(string.Empty, agency.Key);

                        foreach (SAPServices.SAP_FLIGHTTRIPLIST.BAPISTRDAT flightTrip in flightTripHelper._bapiFlightTripList)
                        {
                            flightConnectionHelper.GetDetail(
                                flightTrip.FLCONN1,
                                flightTrip.FLDATE1,
                                string.Empty,
                                flightTrip.AGENCYNUM);

                            ticketSaleRow.TicketsSold += 1;
                            ticketSaleRow.TotalTicketPrice += flightConnectionHelper._bapiPrice.PRICE_BUS1;
                        }

                        ticketSaleDataTable.AddTicketSaleRow(ticketSaleRow);
                    }

                };

            backgroundWorker.RunWorkerCompleted +=
                delegate(object workSender, RunWorkerCompletedEventArgs eventArg)
                {
                    progress.Close();
                    progress.Dispose();

                    ExcelHelper.LoadExcelSheet(
                        "TicketSalesDistribution",
                        Resources.TicketSalesDistribution,
                        ticketDataset);
                };

            backgroundWorker.RunWorkerAsync();
            progress.ShowDialog();

        }

        #endregion



    }
}
