using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Model.Extensibility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace O365_LyncPeopleSearch
{
    public partial class Search : Form
    {
        #region Variables
        LyncClient lyncClient;
        Automation automation;
        ContactManager contactMgr;
        Conversation conversation;
        List<SearchProviders> activeSearchProviders;
        ContactSubscription searchResultSubscription;
        SearchResults results = null;
        DataTable dt;
        string contactDetails;
        #endregion

        public Search()
        {
            InitializeComponent();
            try
            {
                // Get instances of Lync Client and Contact Manager.
                lyncClient = LyncClient.GetClient();
                automation = LyncClient.GetAutomation();
                contactMgr = lyncClient.ContactManager;

                // Create a DataTable for search results.
                dt = new DataTable();
                dt.Columns.Add("Contact Uri");
                dt.Columns.Add("Contact Details");

                // Create list to cache a list of SearchProviders instances
                // that are synchronized and can accept user search requests. 
                activeSearchProviders = new List<SearchProviders>();
                searchResultSubscription = contactMgr.CreateSubscription();

                // Loads Expert search provider if it is configured and enables the checkbox.
                if (contactMgr.GetSearchProviderStatus(SearchProviders.Expert)
                              == SearchProviderStatusType.SyncSucceeded || contactMgr.GetSearchProviderStatus(SearchProviders.Expert)
                              == SearchProviderStatusType.SyncSucceededForExternalOnly || contactMgr.GetSearchProviderStatus(SearchProviders.Expert)
                              == SearchProviderStatusType.SyncSucceededForInternalOnly)
                {
                    activeSearchProviders.Add(SearchProviders.Expert);
                    ExpertSearch.Enabled = true;
                }

                // Register for the SearchProviderStatusChanged event raised
                // by ContactManager.
                contactMgr.SearchProviderStateChanged += contactMgr_SearchProviderStateChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:    " + ex.Message);
            }
        }


        /// <summary>
        /// Removes the SearchProviders enumerator whose sync is not succeeded and 
        /// Adds the Expert SearchProvider if its sync is succeeded. If the sync for
        /// Expert search provider is not succeeded then disables the ExpertSearch control
        /// and chcekbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void contactMgr_SearchProviderStateChanged(object sender,
            SearchProviderStateChangedEventArgs e)
        {
            if (e.NewStatus != SearchProviderStatusType.SyncSucceeded)
            {
                //  Remove the SearchProviders enumerator to the
                // local application cache declared previously.
                activeSearchProviders.Remove(e.Provider);

            }
            if (e.Provider == SearchProviders.Expert && (e.NewStatus == SearchProviderStatusType.SyncSucceeded ||
                e.NewStatus == SearchProviderStatusType.SyncSucceededForExternalOnly ||
                e.NewStatus == SearchProviderStatusType.SyncSucceededForInternalOnly))
            {
                activeSearchProviders.Add(SearchProviders.Expert);

                //Invoke delegate to update a property of control owned by UI thread from this Lync platform thread
                this.Invoke(new ControlEnablerDelegate(ControlEnabler), new object[] { ExpertSearch, true });
            }

            //If the provider status is Expert and ALL of the enumerators in the if test return false then
            //disable the Expert search provider radio check box
            if (e.Provider == SearchProviders.Expert && (e.NewStatus != SearchProviderStatusType.SyncSucceeded &&
              e.NewStatus != SearchProviderStatusType.SyncSucceededForExternalOnly &&
              e.NewStatus != SearchProviderStatusType.SyncSucceededForInternalOnly))
            {
                activeSearchProviders.Remove(SearchProviders.Expert);

                //Invoke delegates to update a property of controls owned by UI thread from this Lync platform thread
                //to avoid cross-thread exception raised on UI thread when it complains that properties are being updated
                //from a thread that does not “own” the control.
                this.Invoke(new ControlEnablerDelegate(ControlEnabler), new object[] { ExpertSearch, false });
                this.Invoke(new RadioEnablerDelegate(RadioEnabler), new object[] { ExpertSearch, false });
            }


        }

        /// <summary>
        /// Create a delegate to enable the form control.
        /// </summary>
        private delegate void ControlEnablerDelegate(System.Windows.Forms.Control controlToEnable, Boolean newEnableState);


        /// <summary>
        /// Sets the Enabled property of the form control to the assigned value.
        /// </summary>
        private void ControlEnabler(System.Windows.Forms.Control controlToEnable, Boolean newEnableState)
        {
            controlToEnable.Enabled = newEnableState;
        }


        /// <summary>
        /// Create a delegate to enable the checkbox.
        /// </summary>
        private delegate void RadioEnablerDelegate(System.Windows.Forms.RadioButton radioToEnable, Boolean newCheckState);

        /// <summary>
        /// Sets the Checked property of the checkbox to the assigned value.
        /// </summary>
        private void RadioEnabler(System.Windows.Forms.RadioButton radioToEnable, Boolean newCheckState)
        {
            radioToEnable.Checked = newCheckState;
        }


        /// <summary>
        /// Click event handler for SignIn button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignIn_Click(object sender, EventArgs e)
        {
            // Display the Login form.
            Login formLogin = new Login();
            formLogin.Show();
            formLogin.Activate();
            formLogin.BringToFront();
        }

        /// <summary>
        /// Click event handler for SearchContact button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchContact_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(Contact.Text))
                {
                    // Check if the user is signed in then search for the contact.
                    if (lyncClient.State == ClientState.SignedIn)
                    {
                        // Search for the group or contact.
                        SearchForGroupOrContact(Contact.Text, 5);

                    }

                    else
                        MessageBox.Show("Sign in to Lync first");
                }

                else
                    MessageBox.Show("Enter the text to search for.");

            }
            catch (Exception ex) { MessageBox.Show("Error:    " + ex.Message); }
        }

        /// <summary>
        /// Search for a contact or group
        /// </summary>
        /// <param name="searchName">string. Name of contact or group to search for</param>
        /// <param name="numResults">uint. Number of results to return.</param>
        void SearchForGroupOrContact(string searchName, uint numResults)
        {
            try
            {

                // Initiate search for entity based on name.
                if (lyncClient.State == ClientState.SignedIn)
                {
                    // Get the search fields from the contact manager.
                    SearchFields searchFields = lyncClient.ContactManager.GetSearchFields();
                    object[] asyncState = { lyncClient.ContactManager, searchName };

                    dt.Clear();

                    if (ExpertSearch.Checked)
                    {
                        // Get the Sharepoint expert search URL with the user's search string query parameter.
                        string sharePointSearchQueryString = lyncClient.ContactManager.GetExpertSearchQueryString(searchName);

                        lyncClient.ContactManager.BeginSearch(sharePointSearchQueryString
                            , SearchProviders.Expert
                            , searchFields
                            , SearchOptions.Default
                            , numResults
                            , SearchResultsCallback
                            , asyncState);

                    }
                    else
                    {

                        // Perform the search which uses any of the search providers that a Lync
                        // server admin has configured to accept a name search.  
                        lyncClient.ContactManager.BeginSearch(searchName, SearchResultsCallback, asyncState);
                       
                    }

                }
            }

            catch (Exception ex) { MessageBox.Show("Error:    " + ex.Message); }
        }

        /// <summary>
        /// Handles callback containing results of a search.
        /// </summary>
        private void SearchResultsCallback(IAsyncResult ar)
        {
            // Check the state of search operation.
            if (ar.IsCompleted == true)
            {

                object[] asyncState = (object[])ar.AsyncState;
                try
                {
                    results = ((ContactManager)asyncState[0]).EndSearch(ar);
                    if (results.AllResults.Count != 0)
                    {
                        // Subscribe to the search results.
                        SubscribeToSearchResults(results.Contacts.ToList());


                        // Display the results in data grid view.
                        foreach (var contact in results.Contacts)
                        {
                            string contactUri = contact.Uri;
                            contactDetails = GetContactInfo(contact);
                            DataRow contactRow = dt.NewRow();
                            contactRow["Contact Uri"] = contactUri;
                            contactRow["Contact Details"] = contactDetails;
                            dt.Rows.Add(contactRow);
                            dt.AcceptChanges();

                        }
                        SetDataSource(dt);

                    }
                    else
                    {
                       
                            MessageBox.Show("0 instances found for " + Contact.Text);
                    }

                }
                catch (SearchException se)
                {
                    MessageBox.Show("Search failed: " + se.Reason.ToString());
                }
            }
        }


        /// <summary>
        /// Gets the contact details.
        /// </summary>
        /// <param name="contact">The contact whose details needs to be retrieved.</param>
        public string GetContactInfo(Contact contact)
        {
            string returnValue = string.Empty;
            string activityDescription = null;

            List<object> collaborationPhones = null;

            // Get the Availability information for the contact.
            ContactAvailability availEnum = (ContactAvailability)(contact)
                .GetContactInformation(ContactInformationType.Availability);

            // Get the Activity information for the contact.
            activityDescription = (string)contact
                .GetContactInformation(ContactInformationType.Activity);

            // Get the ContactEndpoints information for the contact.
            collaborationPhones = (List<object>)contact
                .GetContactInformation(ContactInformationType.ContactEndpoints);

            // Generate a string for the information retrieved and return it.
            if (collaborationPhones != null)
            {
                returnValue += "\r\nPhones:";
                foreach (object phone in collaborationPhones)
                {
                    ContactEndpoint anEndpoint = (ContactEndpoint)phone;
                    returnValue += "\r\nType: "
                        + anEndpoint.Type.ToString()
                        + "\r\n DisplayName: "
                        + anEndpoint.DisplayName
                        + "\r\n Uri: "
                        + anEndpoint.Uri;

                }
            }

            returnValue += "\r\n Activity:" + activityDescription;
            returnValue += "\r\nPersonal note: " + contact
                .GetContactInformation(ContactInformationType.PersonalNote);
            returnValue += "\r\nOOF note: " + contact
                .GetContactInformation(ContactInformationType.OutOfficeNote);
            returnValue += "\r\nAvailable:" + availEnum.ToString();

            return returnValue;
        }

        /// <summary>
        /// Create a delegate to set the data source of data grid view.
        /// This delegate is created because the DataGridView 
        /// element is used in an another thread than the thread 
        /// on which it has been created.
        /// </summary>
        private delegate void dtDelegate(DataTable dt);

        /// <summary>
        /// Sets the data source of data frid view to the data table assigned.
        /// </summary>
        private void SetDataSource(DataTable dt)
        {
            // Create a data view to remove duplicate values from the data table.
            DataView dtView = new DataView(dt);
            DataTable dtNew = dtView.ToTable(true, "Contact Uri", "Contact Details");

            if (SearchResults.InvokeRequired)
            {
                dtDelegate sd = new dtDelegate(SetDataSource);
                this.Invoke(sd, new object[] { dtNew });
            }
            else
            {
                SearchResults.DataSource = dtNew;
            }
        }

        /// <summary>
        /// Adds contacts found through a search to a ContactSubscription
        /// and raises the ContactAddedEvent.
        /// </summary>
        /// <param name="sContact">List[Contact]. The list of contacts found in a search.</param>
        public void SubscribeToSearchResults(List<Contact> sContactList)
        {
            try
            {
                if (searchResultSubscription == null)
                {
                    // Create subscription for the contact manager
                    // if the contact manager is not subscribed.
                    searchResultSubscription = contactMgr.CreateSubscription();
                }
                else
                {
                    // Remove all existing search results.
                    searchResultSubscription.Unsubscribe();
                    foreach (Contact c in searchResultSubscription.Contacts)
                    {
                        searchResultSubscription.RemoveContact(c);
                    }
                }

                // Add the Contact to a ContactSubscription.
                searchResultSubscription.AddContacts(sContactList);

                // Specify the Contact Information Types to be
                // returned in ContactInformationChanged events.
                ContactInformationType[] ContactInformationTypes = { ContactInformationType.Availability, ContactInformationType.ActivityId };

                // Activate the subscription.
                searchResultSubscription.Subscribe(ContactSubscriptionRefreshRate.High, ContactInformationTypes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:    " + ex.Message);
            }
        }

        /// <summary>
        /// Click event handler for StartIM button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartIM_Click(object sender, EventArgs e)
        {
            try
            {
                // Check the entire row is selected or not in the grid view.
                if (SearchResults.SelectedRows.Count > 0)
                {
                    // Check the selected contacts Uri is not null or empty.
                    if (!string.IsNullOrEmpty(SearchResults.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        string selectedPerson = (string)SearchResults.SelectedRows[0].Cells[0].Value;

                        // Get the selected contact by Uri.
                        Contact selectedContact = contactMgr.GetContactByUri(selectedPerson);

                        // Create a list of the contact to start conversation.
                        List<string> lstInvitee = new List<string>();
                        lstInvitee.Add(selectedPerson);

                        // Create text for the first IM message.
                        string firstIMMessageText = Message.Text;

                        // Create a generic Dictionary object to contain
                        // conversation setting objects.
                        Dictionary<AutomationModalitySettings, object> modalitySettings = new
                            Dictionary<AutomationModalitySettings, object>();
                        AutomationModalities chosenMode = AutomationModalities.InstantMessage;

                        modalitySettings.Add(AutomationModalitySettings.FirstInstantMessage,
                            firstIMMessageText);
                        modalitySettings.Add(AutomationModalitySettings.SendFirstInstantMessageImmediately,
                            true);

                        // Start the conversation.
                        IAsyncResult arStartConversation = automation.BeginStartConversation(
                            chosenMode
                            , lstInvitee
                            , modalitySettings
                            , StartConversationCallback
                            , null);
                    }

                    else
                        MessageBox.Show("Contact Uri is null or empty.");
                }

                else
                    MessageBox.Show("Select the entire row to start conversation.");


            }
            catch (Exception ex) { MessageBox.Show("Error:    " + ex.Message); }
        }

        /// <summary>
        /// Handles callback for starting the IM conversation.
        /// </summary>
        private void StartConversationCallback(IAsyncResult ar)
        {
            try
            {
                conversation = automation.EndStartConversation(ar).Conversation;
            }
            catch (Exception ex) { MessageBox.Show("Error:    " + ex.Message); }
        }
    }
}
