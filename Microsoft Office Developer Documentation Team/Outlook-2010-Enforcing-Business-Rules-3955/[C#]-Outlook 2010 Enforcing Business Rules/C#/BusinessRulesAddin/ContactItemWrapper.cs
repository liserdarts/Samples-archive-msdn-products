using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace BusinessRulesAddin {

    internal class ContactItemWrapper : InspectorWrapper {

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="inspector">The Outlook Inspector instance that should be handled</param>
        public ContactItemWrapper(Outlook.Inspector inspector)
            : base(inspector) {
        }

        /// <summary>
        /// The Object instance behind the Inspector (CurrentItem)
        /// </summary>
        public Outlook.ContactItem Item { get; private set; }

        /// <summary>
        /// Method is called when the Wrapper has been initialized
        /// </summary>
        protected override void Initialize() {
            // Get the Item of the current Inspector
            Item = (Outlook.ContactItem)Inspector.CurrentItem;

            // Register for the Item events
            Item.Open += new Outlook.ItemEvents_10_OpenEventHandler(Item_Open);
            Item.Write += new Outlook.ItemEvents_10_WriteEventHandler(Item_Write);
            ((Outlook.ItemEvents_10_Event)Item).Close += new Microsoft.Office.Interop.Outlook.ItemEvents_10_CloseEventHandler(Item_Close);


            SetupBusinessRules();
        }

        /// <summary>
        /// This Method is called when the Item is going to be closed
        /// </summary>
        /// <param name="Cancel"></param>
        void Item_Close(ref bool Cancel) {
            if (!Item.Saved) {
                Cancel = !Validate();
            }
        }

        /// <summary>
        /// This Method is called when the Item is saved.
        /// </summary>
        /// <param name="Cancel">When set to true, the save operation is cancelled</param>
        void Item_Write(ref bool Cancel) {
            if (!Item.Saved) {
                Cancel = !Validate();
            }
        }

        /// <summary>
        /// This Method is called when the Item is visible and the UI is initialized.
        /// </summary>
        /// <param name="Cancel">When you set this property to true, the Inspector is closed.</param>
        void Item_Open(ref bool Cancel) {
            //TODO: Implement something 
        }


        /// <summary>
        /// The Close Method is called when the Inspector has been closed.
        /// Do your cleanup tasks here.
        /// The UI is gone, can't access it here.
        /// </summary>
        protected override void Close() {
            // unregister events
            Item.Write -= new Outlook.ItemEvents_10_WriteEventHandler(Item_Write);
            Item.Open -= new Outlook.ItemEvents_10_OpenEventHandler(Item_Open);
            ((Outlook.ItemEvents_10_Event)Item).Close -= new Microsoft.Office.Interop.Outlook.ItemEvents_10_CloseEventHandler(Item_Close);

            _BusinessRules.Clear();
            _BusinessRules = null; 

            // required, just stting to NULL may keep a reference in memory of the Garbage Collector.
            Item = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        #region business rule check methods

        List<X4UBusinessRule> _BusinessRules = new List<X4UBusinessRule>();

        private void SetupBusinessRules() {
            _BusinessRules.Add(new X4UBusinessRule("OfficeLocationRequiredAndUppercase", "The Office Location is required and must be uppercase.", this.BusinessRuleCheck_OfficeLocationRequiredAndUppercase));
            _BusinessRules.Add(new X4UBusinessRule("CompanyName3Letters", "The Company Name must have a minimum length of 3.", this.BusinessRuleCheck_CompanyNameMinimum3Letters));
        }

        public bool Validate() {
            // if every business rule is valid we return true
            if (_BusinessRules.TrueForAll(X4UBusinessRule.IsRuleValid)) return true;

            // no ?
            // show a warning to the user and 
            StringBuilder message = new StringBuilder(500);
            message.AppendLine("You can't save this Item, because the following requirements are not met:");
            foreach (X4UBusinessRule rule in _BusinessRules) {
                if (!rule.IsValid()) message.AppendLine(rule.Description);
            }

            // display a message to the user what's wrong.
            MessageBox.Show (new OutlookWin32Window(Inspector), message.ToString());

            return false;
        }

        public bool BusinessRuleCheck_OfficeLocationRequiredAndUppercase() {
            return (!string.IsNullOrEmpty (Item.OfficeLocation) && Item.OfficeLocation == Item.OfficeLocation.ToUpper());
        }

        public bool BusinessRuleCheck_CompanyNameMinimum3Letters() {
            return (Item.CompanyName != null && Item.CompanyName.Length > 2);
        }

        #endregion

    }
}
