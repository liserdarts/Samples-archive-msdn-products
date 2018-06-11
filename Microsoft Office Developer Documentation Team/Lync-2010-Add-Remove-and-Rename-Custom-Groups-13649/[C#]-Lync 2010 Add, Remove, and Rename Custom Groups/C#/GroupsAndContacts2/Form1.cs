using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Group;
using Microsoft.Lync.Model.Extensibility;

namespace GroupsAndContacts2
{
    public partial class Form1 : Form
    {
        ContactManager contactManager;
        LyncClient client;
        
        public Form1()
        {
            InitializeComponent();
            client = LyncClient.GetClient();
            contactManager = client.ContactManager;
        }

        #region Click event handlers
        //Click handler for Add button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string groupName = txtBoxAdd.Text;
            contactManager.GroupAdded += new EventHandler<GroupCollectionChangedEventArgs>(contactManager_GroupAdded);
            AsyncCallback callback = new AsyncCallback(BeginAddGroupComplete);
            contactManager.BeginAddGroup(groupName, BeginAddGroupComplete, null);
        }

        //Click handler for Rename button
        private void btnRename_Click(object sender, EventArgs e)
        {
            //Get the group name
            string oldName = txtBoxOldname.Text;
            string newName = txtBoxNewName.Text;
            
            //Verify the group is a GroupType.CustomGroup
            Group groupToRename = null;
            
            if (contactManager.Groups.TryGetGroup(oldName, out groupToRename))
            {
                groupToRename.NameChanged += new EventHandler<GroupNameChangedEventArgs>(groupToRename_NameChanged);
                if (groupToRename.Type == GroupType.CustomGroup)
                {
                    //Cast it to Group.CustomGroup                
                    CustomGroup customGroup = groupToRename as CustomGroup;
                    if (customGroup != null)
                    {
                        //Call BeginRename
                        IAsyncResult ar = customGroup.BeginRename(newName, null, null);

                        if (ar != null) //Call EndRename
                            customGroup.EndRename(ar);
                    }
                }   
            }
            

        }

        //Click handler for Remove button
        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Get the group name
            string removeName = txtBoxRemove.Text;

            //Create an event handler for GroupRemoved
            contactManager.GroupRemoved += new EventHandler<GroupCollectionChangedEventArgs>(contactManager_GroupRemoved);

            //call BeginRemoveGroup
            Group groupToTremove = null;
            if (contactManager.Groups.TryGetGroup(removeName, out groupToTremove))
            {
                if (groupToTremove.Type == GroupType.CustomGroup)
                {
                    contactManager.BeginRemoveGroup(groupToTremove, RemoveCustomGroupCallback, null);
                }
            }

            System.Windows.Forms.MessageBox.Show("Group " + groupToTremove.Name + " is removed");
        }

        //Click handler for the Exit button
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region ContactManager event handlers
        //Handler for the ContactManager.NameChanged event
        void groupToRename_NameChanged(object sender, GroupNameChangedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Group is renamed to " + e.NewName);
        }
        
        //Handler for the ContactManager.GroupRemoved event
        void contactManager_GroupRemoved(object sender, GroupCollectionChangedEventArgs e)
        {
            //Remove any existing event registrations on the group
            e.Group.NameChanged -= groupToRename_NameChanged; 
        }


        //ContactManager.GroupAdded event handler
        void contactManager_GroupAdded(object sender, GroupCollectionChangedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("This group is added: " + e.Group.Name);
        }
        #endregion

        #region callback methods
        //Callback method for the ContactManager.BeginAddGroup event
        public void BeginAddGroupComplete(IAsyncResult res)
        {
            contactManager.EndAddGroup(res);
        }

        //Callback method for the ContactManager.BeginRemoveGroup event
        private void RemoveCustomGroupCallback(IAsyncResult ar)
        {
            if (ar != null)
            {
                client = LyncClient.GetClient();
                contactManager = client.ContactManager;
                contactManager.EndRemoveGroup(ar);            
            }
        }
        #endregion
    }
}
