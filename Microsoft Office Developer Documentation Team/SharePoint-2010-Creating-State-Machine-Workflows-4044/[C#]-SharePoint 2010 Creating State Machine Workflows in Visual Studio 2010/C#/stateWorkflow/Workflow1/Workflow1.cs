using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;

namespace stateWorkflow.Workflow1
{
    public sealed partial class Workflow1 : StateMachineWorkflowActivity
    {
        public Workflow1()
        {
            InitializeComponent();
        }

        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public Guid createTask1_TaskId1 = default(System.Guid);
        public SPWorkflowTaskProperties createTask1_TaskProperties1 = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        private void createTask1_MethodInvoking(object sender, EventArgs e)
        {
            createTask1_TaskId1 = Guid.NewGuid();
            createTask1_TaskProperties1.Title = "Finish Document";
            createTask1_TaskProperties1.AssignedTo = @"CONTOSO\sanjays";
            createTask1_TaskProperties1.DueDate = DateTime.Now.AddDays(1.0);
        }

        public SPWorkflowTaskProperties onTaskChanged1_AfterProperties1 = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChanged1_BeforeProperties1 = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        private void onTaskChanged1_Invoked(object sender, ExternalDataEventArgs e)
        {
            onTaskChanged1_AfterProperties1 = onTaskChanged1.AfterProperties;
            onTaskChanged1_BeforeProperties1 = onTaskChanged1.BeforeProperties;
        }
        private void ReadyForReview(object sender, ConditionalEventArgs e)
        {
            if (onTaskChanged1_AfterProperties1.PercentComplete == 1.0)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }

        public Guid createReviewTask_TaskId1 = default(System.Guid);
        public SPWorkflowTaskProperties createReviewTask_TaskProperties1 = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        private void createReviewTask_MethodInvoking(object sender, EventArgs e)
        {
            createReviewTask_TaskId1 = Guid.NewGuid();
            createReviewTask_TaskProperties1.Title = "Review Document";
            createReviewTask_TaskProperties1.AssignedTo = @"CONTOSO\andyj";
            createReviewTask_TaskProperties1.DueDate = DateTime.Now.AddDays(1.0);
        }

        public SPWorkflowTaskProperties onTaskChanged2_AfterProperties1 = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChanged2_BeforeProperties1 = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        private void onTaskChanged2_Invoked(object sender, ExternalDataEventArgs e)
        {
            onTaskChanged2_AfterProperties1 = onTaskChanged2.AfterProperties;
            onTaskChanged2_BeforeProperties1 = onTaskChanged2.BeforeProperties;
        }

        private void ReviewFinished(object sender, ConditionalEventArgs e)
        {
            if (onTaskChanged2_AfterProperties1.PercentComplete == 1.0)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }
        private void DocApproved(object sender, ConditionalEventArgs e)
        {
            if (onTaskChanged2_AfterProperties1.Description == "<DIV>Approved</DIV>")
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }
    }
}
