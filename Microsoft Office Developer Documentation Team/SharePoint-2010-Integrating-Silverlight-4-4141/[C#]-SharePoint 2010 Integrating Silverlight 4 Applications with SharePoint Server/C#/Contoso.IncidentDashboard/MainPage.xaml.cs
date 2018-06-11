using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SharePoint.Client;
using System.Windows.Printing;

namespace Contoso.IncidentDashboard
{
    /// <summary>
    /// The MainPage user control acts as the main logic point for the 
    /// dashboard. It handles pulling data from SharePoint via the
    /// Client SharePoint Object Model and then displaying a dashboard
    /// showing a list of open issues.
    /// </summary>
    public partial class MainPage : UserControl
    {
        /// <summary>
        /// The ClientContext property is used to house the instance of the 
        /// ClientContext object that is used throughout the application.
        /// </summary>
        private ClientContext _context = null;
        public ClientContext Context
        {
            get
            {
                return _context;
            }
        }

        /// <summary>
        /// The Site property holds a reference to the Site we'll be working 
        /// with.
        /// </summary>
        private Web _site = null;
        public Web Site
        {
            get
            {
                return _site;
            }
        }

        // Private member variables for holding the list of Incident objects,
        // the ListItemCollection that the CSOM will populate, and a reference 
        // to the selected header.
        private List<Incident> _incidents = new List<Incident>();
        private ListItemCollection _incidentListItems = null;
        private StatusHeader _selectedHeader = null;

        /// <summary>
        /// The MainPage constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        /// <summary>
        /// MainPage's Loaded event handler. Upon the child controls being laid
        /// out and ready to work with we set the default selected header, set
        /// up our Context and Site properties. And then get a list of 
        /// Incidents from the SharePoint site.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this._selectedHeader = this.hdrTotal;

            // Set up the Context and Site propeties
            _context = new ClientContext("http://data.contoso.com/");
            _site = _context.Web;

            // Get the items in the Incidents list. We follow the pattern
            // identified in the associated white paper. 
            // Specifically:
            // 1. Get a reference to the logical object to work with
            // 2. Set up the action to perform
            // 3. Execute the action, providing callback methods for 
            //    success and failure.

            // 1. Get a reference to the logical object to work with, in this
            //    case our "Incidents" list
            List list = _site.Lists.GetByTitle("Incidents");

            // 2. Set up the action to perform. In this case set up our query,
            CamlQuery query = new CamlQuery();

            // 3. Execute the action, providing callback methods
            _incidentListItems = list.GetItems(query);
            _context.Load(_incidentListItems);
            _context.ExecuteQueryAsync(
                onGetIncidentsSucceeded, 
                onGetIncidentsFailed
                );
        }

        /// <summary>
        /// The onGetIncidentsSucceeded method is called when retreiving data
        /// from SharePoint succeeded. Just calls out to the method used to
        /// display the results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void onGetIncidentsSucceeded(
            object sender, 
            ClientRequestSucceededEventArgs args
            )
        {
            UpdateIncidentsSummary();
        }

        /// <summary>
        /// The UpdateIncidentsSummaryDelegate is used to bring the background
        /// thread's operation to the UI thread as necessary.
        /// </summary>
        private delegate void UpdateIncidentsSummaryDelegate();

        /// <summary>
        /// UpdateIncidentsSummary is used to parse the results we received
        /// from SharePoint and display them.
        /// </summary>
        private void UpdateIncidentsSummary()
        {
            // First we check for access. If we don't have UI access we use a
            // new instance of the UpdateIncidentSummaryDelegate delegate to
            // execute this method on the UI thread.
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(
                    new UpdateIncidentsSummaryDelegate(UpdateIncidentsSummary)
                    );
                return;
            }

            // Once we know we're on the UI thread we loop through the ListItems
            // received, storing the incidents in our Incidents List and counting 
            // instances of each Status.
            Dictionary<string, int> counts = new Dictionary<string, int>();
            counts.Add("Total", 0);

            foreach (ListItem listItem in _incidentListItems)
            {
                // Create a new Incident object, setting the properties based
                // on the values we received from SharePoint and store it in
                // our Incident List.
                Incident incident = new Incident() { 
                    ID=(int)listItem.Id, 
                    Customer= (string)listItem["Customer"], 
                    Agent = (string)listItem["Agent"], 
                    Status = (string)listItem["Status"], 
                    CreationDate = (DateTime)listItem["Created"] 
                };
                _incidents.Add(incident);

                // Check to see if we already have an entry in our counts
                // Dictionary keyed to the incident status. If not add one 
                // otherwise increment the value contained at that key.
                if (counts.ContainsKey(incident.Status))
                {
                    counts[incident.Status]++;
                }
                else
                {
                    counts.Add(incident.Status, 1);
                }

                // If the status isn't "Closed" we want to count it in the
                // Total key of the Dictionary.
                if (incident.Status.Equals("Closed")) continue;
                
                counts["Total"]++;
            }

            // Update the StatusHeaders with the correct counts.
            hdrTotal.Count = counts["Total"];
            if (counts.ContainsKey("Agent Assigned"))
            {
                hdrAgentAssigned.Count = counts["Agent Assigned"];
            }

            if (counts.ContainsKey("New"))
            {
                hdrNewClaims.Count = counts["New"];
            }

            if (counts.ContainsKey("Claim Approved"))
            {
                hdrClaimApproved.Count = counts["Claim Approved"];
            }

            if (counts.ContainsKey("Bid Received"))
            {
                hdrBidReceived.Count = counts["Bid Received"];
            }

            if (counts.ContainsKey("Repair Shop Assigned"))
            {
                hdrShopAssigned.Count = counts["Repair Shop Assigned"];
            }

            if (counts.ContainsKey("Repair Complete"))
            {
                hdrRepairComplete.Count = counts["Repair Complete"];
            }

            grdIncidents.ItemsSource = _incidents;
        }

        /// <summary>
        /// In the event that we received an error pop up a MessageBox. Note
        /// that we're using a separate method with a delegate to make sure we
        /// show the MessageBox on the UI thread otherwise we'll get an 
        /// exception.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void onGetIncidentsFailed(
            object sender, 
            ClientRequestFailedEventArgs args
            )
        {
            Alert("An Error Occurred Retreiving Incidents. Please refresh the page and try again:\n\n" + args.Exception);
        }

        /// <summary>
        /// The Alert method and the AlertDelegate delegate are used to ensure
        /// that the MessageBox displaying the message are displayed via the UI
        /// thread so they don't cause problems.
        /// </summary>
        /// <param name="message">The message to display to the user</param>
        private delegate void AlertDelegate(string message);
        private void Alert(string message)
        {
            // Check that we have UI access otherwise call this method back 
            // via the delegate.
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(new AlertDelegate(Alert), message);
                return;
            }

            // Display the Message
            MessageBox.Show(message);
        }

        /// <summary>
        /// Event handler for the Print button being clicked. Binds the list 
        /// of Incidents to a DataGrid and prints it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += (s, args) =>
            {
                DataGrid dataGrid = new DataGrid();
                dataGrid.ItemsSource = _incidents;
                dataGrid.AutoGenerateColumns = true;
                
                args.PageVisual = dataGrid;
            };

            printDoc.Print("Incidents");
        }

        /// <summary>
        /// Copies the first incident in the list to the Clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Incident incident = _incidents[0];
            Clipboard.SetText(incident.Customer + " - " +
                incident.Agent + " - " + 
                incident.Status);
        }

        /// <summary>
        /// Event handler for MouseRightButtonDown. This is required to set the
        /// Handled property on the Event Args in order for us to get the 
        /// MouseRightButtonUp event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutRoot_MouseRightButtonDown(
            object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Handler for the MouseRightButtonUp. Displays a right-click menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutRoot_MouseRightButtonUp(
            object sender, MouseButtonEventArgs e)
        {

            e.Handled = true;
            this.pnlPop.Visibility = System.Windows.Visibility.Visible;

            Point mousePosition = e.GetPosition(this.pnlPop);
            Canvas.SetLeft(popMenu, mousePosition.X);
            Canvas.SetTop(popMenu, mousePosition.Y);

            this.popMenu.IsOpen = true;
        }

        /// <summary>
        /// MouseLeftButtonDown handler on the LayoutRoot. Hides the right-
        /// click menu if it is visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutRoot_MouseLeftButtonDown(
            object sender, MouseButtonEventArgs e)
        {
            this.pnlPop.Visibility = System.Windows.Visibility.Collapsed;
            this.popMenu.IsOpen = false;
        }

        /// <summary>
        /// Click handler for one of the Status items being clicked on. Updates
        /// the list of incidents to show only the ones with that particular 
        /// status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Status_Click(object sender, MouseButtonEventArgs e)
        {
            // Get a typed reference to the sender and verify that the user 
            // didn't click on the already selected status to ensure we don't
            // take an unnecessary performance hit.
            StatusHeader header = (StatusHeader)sender;

            if (header.Status.Equals(this._selectedHeader.Status)) return;
            

            // Set the previously selected header's Selected property to false,
            // set the _selectedHeader to the one just clicked, and then set
            // the Selected property on the newly selected StatusHeader to true
            this._selectedHeader.Selected = false;
            this._selectedHeader = header;
            this._selectedHeader.Selected = true;

            // Reset the ItemsSource for the DataGrid that is displaying the 
            // incidents. If the user clicked on the "Total" StatusHeader then
            // display all incidents that don't have a status of closed 
            // otherwise find the incidents whose status matches that of the
            // clicked one.
            if (header.Status.Equals("Total"))
            {
                grdIncidents.ItemsSource = from incident in _incidents
                                           where !incident.Status.Equals("Closed")
                                           select incident;
            }
            else
            {
                grdIncidents.ItemsSource = from incident in _incidents
                                           where incident.Status.Equals(header.Status)
                                           select incident;
            }
        }
    }
}
