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

namespace seqWorkflow.Workflow1
{
    public sealed partial class Workflow1 : SequentialWorkflowActivity
    {
        public Workflow1()
        {
            InitializeComponent();
        }

        Boolean bIsWorkflowPending = true;

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();

        private void onWorkflowActivated(object sender, ExternalDataEventArgs e)
        {
            CheckStatus();
        }

        private void isWorkflowPending(object sender, ConditionalEventArgs e)
        {
            e.Result = bIsWorkflowPending;
        }

        private void onWorkflowItemChanged(object sender, ExternalDataEventArgs e)
        {
            CheckStatus();
        }
        private void CheckStatus()
        { 
        if ( workflowProperties.Item["Document Status"].ToString() == "Review Complete")
        {
            bIsWorkflowPending = false;
        }
        }
    }
}
