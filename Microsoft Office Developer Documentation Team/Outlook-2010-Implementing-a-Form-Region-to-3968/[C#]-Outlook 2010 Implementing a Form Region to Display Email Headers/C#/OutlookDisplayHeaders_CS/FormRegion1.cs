using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace OutlookDisplayHeaders_CS
{
    partial class FormRegion1
    {
        #region Form Region Factory

        [Microsoft.Office.Tools.Outlook.FormRegionMessageClass(Microsoft.Office.Tools.Outlook.FormRegionMessageClassAttribute.Note)]
        [Microsoft.Office.Tools.Outlook.FormRegionName("OutlookDisplayHeaders_CS.FormRegion1")]
        public partial class FormRegion1Factory
        {
            // Occurs before the form region is initialized.
            // To prevent the form region from appearing, set e.Cancel to true.
            // Use e.OutlookItem to get a reference to the current Outlook item.
            private void FormRegion1Factory_FormRegionInitializing(object sender, Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs e)
            {
            }
        }

        #endregion

        // Occurs before the form region is displayed.
        // Use this.OutlookItem to get a reference to the current Outlook item.
        // Use this.OutlookFormRegion to get a reference to the form region.
        private void FormRegion1_FormRegionShowing(object sender, System.EventArgs e)
        {
        }

        // Occurs when the form region is closed.
        // Use this.OutlookItem to get a reference to the current Outlook item.
        // Use this.OutlookFormRegion to get a reference to the form region.
        private void FormRegion1_FormRegionClosed(object sender, System.EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Use a constant to define the PidTagTransportMessageHeaders property.
            string PidTagTransportMessageHeaders =
                @"http://schemas.microsoft.com/mapi/proptag/0x007D001E";

            // Get a reference to the mail message displayed in the
            // active inspector.
            Outlook.MailItem message =
                (Outlook.MailItem)this.OutlookItem;

            // Use the PropertyAccessor of the mail message to get
            // the value of the MAPI property that contains the
            // message headers. 
            string headers = string.Empty;
            try
            {
                headers = (string)message.PropertyAccessor.GetProperty
                    (PidTagTransportMessageHeaders);
            }
            catch
            {
            }

            // If getting the Internet headers is successful, display them
            // in the text box.
            textBox1.Text = headers;
        }
    }
}
