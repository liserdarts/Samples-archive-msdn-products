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
using System.Windows.Navigation;

//added
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Model.Extensibility;
using Microsoft.Lync.Model.Group;

namespace SilverlightClassLibrary1
{
    public partial class LyncCustomControl : Page
    {
        ContactManager contactManager;
        LyncClient client;

        //constructor
        public LyncCustomControl()
        {
            InitializeComponent();

            //client = LyncClient.GetClient();
            //contactManager = client.ContactManager;
            //listBox1.SelectionMode = SelectionMode.Multiple;
        }

        //adds custom group contacts to the listbox control
        private void GetGroupContacts(Group group)
        {
            ContactSubscription newSubscription = contactManager.CreateSubscription();
            Dictionary<ContactInformationType, object> _ContactInformation = new Dictionary<ContactInformationType, object>();

            group.ContactAdded += new EventHandler<GroupMemberChangedEventArgs>(group_ContactAdded);

            // Iterate on the contacts in the group.
            foreach (Contact _Contact in group)
            {
                // Test if contact already exists in subscription.
                if (newSubscription.Contacts.Contains(_Contact) == false)
                {
                    // Add contact to contact subscription.
                    newSubscription.AddContact(_Contact);

                    // Get contact information from the contact.
                    string uri = _Contact.Uri;
                    listBox1.Items.Add(uri);
                }
            }
        }

        #region event handlers
        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        //button 1 click event handler
        //create a custom group called 'sample'
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Adding custom group..");
            try
            {
                client = LyncClient.GetClient();
                contactManager = client.ContactManager;
                listBox1.SelectionMode = SelectionMode.Multiple;

                string groupName = "Sample";
                contactManager.GroupAdded += new EventHandler<GroupCollectionChangedEventArgs>(contactManager_GroupAdded);
                AsyncCallback callback = new AsyncCallback(BeginAddGroupComplete);
                contactManager.BeginAddGroup(groupName, BeginAddGroupComplete, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //button 2 click event handler
        //find the contacts in the custom group, calls the GetGroupContacts method
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Listing contacts...");
            foreach (Group _Group in contactManager.Groups)
            {
                if (_Group.Type == GroupType.CustomGroup)
                    GetGroupContacts(_Group);
            }
        }

        //button 3 click event handler
        //sends IM to selected contacts
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sending IM...");
            Automation automation = LyncClient.GetAutomation();

            //add contact URIs to a List object
            List<string> inviteeList = new List<string>();
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            { inviteeList.Add(listBox1.SelectedItems[i].ToString()); }

            //create settings object
            Dictionary<AutomationModalitySettings, object> settings = new Dictionary<AutomationModalitySettings, object>();

            //specify message modality
            AutomationModalities mode = AutomationModalities.InstantMessage;

            //add settings to the settings object
            settings.Add(AutomationModalitySettings.FirstInstantMessage, "Weekly project status conference is starting...");
            settings.Add(AutomationModalitySettings.SendFirstInstantMessageImmediately, true);

            //launch the conference invite
            IAsyncResult ar = automation.BeginStartConversation(
                mode
                , inviteeList
                , settings
                , EndStartConversation
                , null);

        }

        //event handler for the Group.ContactAdded event
        void group_ContactAdded(object sender, GroupMemberChangedEventArgs e)
        {
            MessageBox.Show("ContactAdded event occurred");
        }

        //event handler for the ContactManager.GroupAdded event
        void contactManager_GroupAdded(object sender, GroupCollectionChangedEventArgs e)
        {
            MessageBox.Show("GroupAdded event occurred");
        }

        #endregion event handlers


        #region callback methods
        //callback method for the ContactManager.BeginAddGroup method
        public void BeginAddGroupComplete(IAsyncResult res)
        {
            contactManager.EndAddGroup(res);
        }

        //callback method for the Automation.BeginStartConversation
        public void EndStartConversation(IAsyncResult res)
        {
            Automation automation = LyncClient.GetAutomation();

            //get the conversation object
            ConversationWindow window = automation.EndStartConversation(res);
            Conversation conference = window.Conversation;

            //display the conference URI
            textBox1.Text = "conference URI: " + conference.Properties[ConversationProperty.ConferencingUri] + "?" + conference.Properties[ConversationProperty.Id];
        }

        #endregion callback methods
    }
}
