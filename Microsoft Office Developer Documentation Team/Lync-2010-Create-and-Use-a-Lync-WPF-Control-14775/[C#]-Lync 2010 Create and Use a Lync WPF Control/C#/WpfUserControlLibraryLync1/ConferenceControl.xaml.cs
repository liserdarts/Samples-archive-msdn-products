using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Group;
using Microsoft.Lync.Model.Extensibility;
using Microsoft.Lync.Model.Conversation;

namespace WpfUserControlLibraryLync1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        ContactManager contactManager;
        LyncClient client;

        public UserControl1()
        {
            InitializeComponent();
            client = LyncClient.GetClient();
            contactManager = client.ContactManager;
            listBox1.SelectionMode = SelectionMode.Multiple;
        }

        #region Click event handlers

        //event handler adds a "Sample" group
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string groupName = "Sample";
            contactManager.GroupAdded += new EventHandler<GroupCollectionChangedEventArgs>(contactManager_GroupAdded);
            AsyncCallback callback = new AsyncCallback(BeginAddGroupComplete);
            contactManager.BeginAddGroup(groupName, BeginAddGroupComplete, null);
        }

        //event handler fills a contact list
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            foreach (Group _Group in contactManager.Groups)
            {
                if (_Group.Type == GroupType.CustomGroup)
                    GetGroupContacts(_Group);
            }
        }

        //event handler sends conference invitations
        private void button3_Click(object sender, RoutedEventArgs e)
        {
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
                , null
                , null);

            //get the conversation object
            ConversationWindow window = automation.EndStartConversation(ar);
            Conversation conference = window.Conversation;

            //display the conference URI
            textBox1.Text = "conference URI: " + conference.Properties[ConversationProperty.ConferencingUri] + "?" + conference.Properties[ConversationProperty.Id];
        }

        //event handler simulates sending email containing the conference URI
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("sending email with conference URI");
        }

        #endregion

        #region other event handlers

        //event handler for the GroupAdded event
        void contactManager_GroupAdded(object sender, GroupCollectionChangedEventArgs e)
        {
            MessageBox.Show("GroupAdded event occurred");
        }

        //event handler for the ContactAdded event
        void group_ContactAdded(object sender, GroupMemberChangedEventArgs e)
        {
            MessageBox.Show("ContactAdded event occurred");
        }

        #endregion

        //method adds contacts to the listbox
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

        //callback method for the BeginAddGroup method
        public void BeginAddGroupComplete(IAsyncResult res)
        {
            contactManager.EndAddGroup(res);
        }
    }
}
