using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace PTCEventLogger
{
    public class Common
    {
        /// <summary>
        /// Log the event to the specified list
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listName"></param>
        /// <param name="eventType"></param>
        /// <param name="details"></param>
        public static void LogEvent(SPWeb web, string listName, SPEventReceiverType eventType, string details)
        {
            SPList logList = Common.EnsureLogList(web, listName);
            SPListItem logItem = logList.Items.Add();
            logItem["Title"] = string.Format("{0} triggered at {1}", eventType, DateTime.Now);
            logItem["Event"] = eventType.ToString();
            logItem["Before"] = Common.IsBeforeEvent(eventType);
            logItem["Date"] = DateTime.Now;
            logItem["Details"] = details;
            logItem.Update();
        }

        /// <summary>
        /// Tells whether an event is a before or after event.
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private static bool IsBeforeEvent(SPEventReceiverType eventType)
        {
            return (eventType < SPEventReceiverType.ItemAdded) ? true : false;
        }

        /// <summary>
        /// Ensures that the Logs list with the specified name is created.
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listName"></param>
        /// <returns></returns>
        private static SPList EnsureLogList(SPWeb web, string listName)
        {
            SPList list = null;
            try
            {
                list = web.Lists[listName];
            }
            catch
            {
                //  Create list
                Guid listGuid = web.Lists.Add(listName, listName, SPListTemplateType.GenericList);
                list = web.Lists[listGuid];
                list.OnQuickLaunch = true;

                //  Add the fields to the list
                //  No need to add "Title" since it's already added by default. We use it to set the event name
                list.Fields.Add("Event", SPFieldType.Text, true);
                list.Fields.Add("Before", SPFieldType.Boolean, true);
                list.Fields.Add("Date", SPFieldType.DateTime, true);
                list.Fields.Add("Details", SPFieldType.Note, false);

                //  Specify what fields to view
                SPView view = list.DefaultView;
                view.ViewFields.Add("Event");
                view.ViewFields.Add("Before");
                view.ViewFields.Add("Date");
                view.ViewFields.Add("Details");
                view.Update();

                list.Update();
            }

            return list;
        }
    }
}
