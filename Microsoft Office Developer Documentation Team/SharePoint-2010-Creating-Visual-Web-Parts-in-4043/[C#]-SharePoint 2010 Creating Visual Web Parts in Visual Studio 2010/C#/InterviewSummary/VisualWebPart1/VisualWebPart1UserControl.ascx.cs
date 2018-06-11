
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.Linq;
using System.Linq;
using Microsoft.SharePoint;


namespace InterviewSummary.VisualWebPart1
{
    public partial class VisualWebPart1UserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SPWeb thisWeb = SPContext.Current.Web;

            using (EntitiesDataContext ctx = new EntitiesDataContext("http://intranet.contos.com"))
            {
                try
                {
                    var query = from interviews in ctx.Interviews
                                select new { interviews.Title, interviews.Candidate.Name, interviews.Candidate.HomeCity, interviews.InterviewerImnName };

                    foreach (var item in query)
                    {
                        TreeNode interviewTreeNode = new TreeNode("Interview Details:" + item.Title, null, null, thisWeb.Lists["Interviews"].DefaultViewUrl, "_self");
                        TreeNode applicantTreeNode = new TreeNode("Applicant:" + item.Name, null, null, thisWeb.Lists["Candidates"].DefaultViewUrl, "_self");
                        TreeNode homecityTreeNode = new TreeNode("Home City:" + item.HomeCity, null, null, thisWeb.Lists["Candidates"].DefaultViewUrl, "_self");
                        TreeNode interviewerTreeNode = new TreeNode("SplInterviewer:" + item.InterviewerImnName, null, null, thisWeb.Lists["Interviews"].DefaultViewUrl, "_self");

                        interviewTreeNode.ChildNodes.Add(applicantTreeNode);
                        interviewTreeNode.ChildNodes.Add(homecityTreeNode);
                        interviewTreeNode.ChildNodes.Add(interviewerTreeNode);
                        TreeView1.Nodes.Add(interviewTreeNode);

                    }
                }
                catch (Exception ex)
                {
                    TreeView1.Nodes.Add(new TreeNode("Err" + ex.Message));
                }
            }

        }
    }
}

