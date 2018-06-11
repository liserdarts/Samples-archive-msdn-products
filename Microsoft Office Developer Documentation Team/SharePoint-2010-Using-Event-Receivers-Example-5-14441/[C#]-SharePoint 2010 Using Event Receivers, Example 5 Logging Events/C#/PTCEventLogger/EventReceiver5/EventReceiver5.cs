using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Text;

namespace PTCEventLogger.EventReceiver5
{
    /// <summary>
    /// List Workflow Events
    /// </summary>
    public class EventReceiver5 : SPWorkflowEventReceiver
    {
       /// <summary>
       /// A workflow is starting.
       /// </summary>
       public override void WorkflowStarting(SPWorkflowEventProperties properties)
       {
           this.LogWorkflowEventProperties(properties);
       }

       /// <summary>
       /// A workflow was started.
       /// </summary>
       public override void WorkflowStarted(SPWorkflowEventProperties properties)
       {
           this.LogWorkflowEventProperties(properties);
       }

       /// <summary>
       /// A workflow was postponed.
       /// </summary>
       public override void WorkflowPostponed(SPWorkflowEventProperties properties)
       {
           this.LogWorkflowEventProperties(properties);
       }

       /// <summary>
       /// A workflow was completed.
       /// </summary>
       public override void WorkflowCompleted(SPWorkflowEventProperties properties)
       {
           this.LogWorkflowEventProperties(properties);
       }

        /// <summary>
        /// Log workflow event properties
        /// </summary>
        /// <param name="properties"></param>
        private void LogWorkflowEventProperties(SPWorkflowEventProperties properties)
        {
           //  Specify the Logs list name
           string listName = "WorkflowEventLogger";

           //  Create the string builder object
           StringBuilder sb = new StringBuilder();
           
           //  Add properties that do not throw an exception
           sb.AppendFormat("AssociationData: {0}\n", properties.AssociationData);
           sb.AppendFormat("Cancel: {0}\n", properties.Cancel);
           sb.AppendFormat("CompletionType: {0}\n", properties.CompletionType);
           sb.AppendFormat("ErrorMessage: {0}\n", properties.ErrorMessage);
           sb.AppendFormat("EventType: {0}\n", properties.EventType);
           sb.AppendFormat("InitiationData: {0}\n", properties.InitiationData);
           sb.AppendFormat("InstanceId: {0}\n", properties.InstanceId);
           sb.AppendFormat("PostponedEvent: {0}\n", properties.PostponedEvent);
           sb.AppendFormat("ReceiverData: {0}\n", properties.ReceiverData);
           sb.AppendFormat("RedirectUrl: {0}\n", properties.RedirectUrl);
           sb.AppendFormat("RelativeWebUrl: {0}\n", properties.RelativeWebUrl);
           sb.AppendFormat("SiteId: {0}\n", properties.SiteId);
           sb.AppendFormat("Status: {0}\n", properties.Status);
           sb.AppendFormat("TerminatedByUserId: {0}\n", properties.TerminatedByUserId);
           sb.AppendFormat("WebUrl: {0}\n", properties.WebUrl);

           //  Get activation properties
           SPWorkflowActivationProperties activationProperties = properties.ActivationProperties;
           if (activationProperties != null)
           {
               sb.AppendFormat("ActivationProperties.AssociationData: {0}\n", activationProperties.AssociationData);
               sb.AppendFormat("ActivationProperties.HistoryListId: {0}\n", activationProperties.HistoryListId);
               sb.AppendFormat("ActivationProperties.HistoryListUrl: {0}\n", activationProperties.HistoryListUrl);
               sb.AppendFormat("ActivationProperties.InitiationData: {0}\n", activationProperties.InitiationData);
               sb.AppendFormat("ActivationProperties.ItemId: {0}\n", activationProperties.ItemId);
               sb.AppendFormat("ActivationProperties.ItemUrl: {0}\n", activationProperties.ItemUrl);
               sb.AppendFormat("ActivationProperties.ListId: {0}\n", activationProperties.ListId);
               sb.AppendFormat("ActivationProperties.ListUrl: {0}\n", activationProperties.ListUrl);
               sb.AppendFormat("ActivationProperties.Originator: {0}\n", activationProperties.Originator);
               sb.AppendFormat("ActivationProperties.OriginatorEmail: {0}\n", activationProperties.OriginatorEmail);
               sb.AppendFormat("ActivationProperties.SiteId: {0}\n", activationProperties.SiteId);
               sb.AppendFormat("ActivationProperties.SiteUrl: {0}\n", activationProperties.SiteUrl);
               sb.AppendFormat("ActivationProperties.TaskListId: {0}\n", activationProperties.TaskListId);
               sb.AppendFormat("ActivationProperties.TaskListUrl: {0}\n", activationProperties.TaskListUrl);
               sb.AppendFormat("ActivationProperties.TemplateName: {0}\n", activationProperties.TemplateName);
               sb.AppendFormat("ActivationProperties.WebId: {0}\n", activationProperties.WebId);
               sb.AppendFormat("ActivationProperties.WebUrl: {0}\n", activationProperties.WebUrl);
               sb.AppendFormat("ActivationProperties.WorkflowId: {0}\n", activationProperties.WorkflowId);

               //  Add properties that might throw an excpetion
               try
               {
                   sb.AppendFormat("ActivationProperties.Context: {0}\n", activationProperties.Context);
                   sb.AppendFormat("ActivationProperties.HistoryList: {0}\n", activationProperties.HistoryList);
                   sb.AppendFormat("ActivationProperties.Item: {0}\n", activationProperties.Item);
                   sb.AppendFormat("ActivationProperties.List: {0}\n", activationProperties.List);
                   sb.AppendFormat("ActivationProperties.OriginatorUser: {0}\n", activationProperties.OriginatorUser);
                   sb.AppendFormat("ActivationProperties.Site: {0}\n", activationProperties.Site);
                   sb.AppendFormat("ActivationProperties.TaskList: {0}\n", activationProperties.TaskList);
                   sb.AppendFormat("ActivationProperties.Web: {0}\n", activationProperties.Web);
                   sb.AppendFormat("ActivationProperties.Workflow: {0}\n", activationProperties.Workflow);
               }
               catch (Exception e)
               {
                   sb.AppendFormat("\nError accessing ActivationProperties property:\n\n {0}", e);
               }
           }
           else
           {
               sb.AppendFormat("ActivationProperties is null\n");
           }

           //  Log the event to the list
           this.EventFiringEnabled = false;
           Common.LogEvent(properties.ActivationProperties.Web, listName, properties.EventType, sb.ToString());
           this.EventFiringEnabled = true;
       }
    }
}
