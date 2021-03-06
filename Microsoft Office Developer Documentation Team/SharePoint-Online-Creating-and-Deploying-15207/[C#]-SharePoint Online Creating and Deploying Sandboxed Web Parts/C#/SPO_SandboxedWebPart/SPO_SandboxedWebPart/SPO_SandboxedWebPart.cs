﻿using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SPO_SandboxedWebPart.SPO_SandboxedWebPart
{
    [ToolboxItemAttribute(false)]
    public class SPO_SandboxedWebPart : WebPart
    {
        //Note: The Visual Studio 2010 Visual Web Part project template
        //is not compatible with the sandbox. To create a Web Part for
        //SharePoint Online, create an empty SharePoint project and ensure 
        //that it runs in the sandbox (Check the Sandboxed Solution 
        //property of the project). Then add a Web Part item to the 
        //Project

        //To test this solution before deployment, set the Site URL 
        //property of the project to match your test SharePoint farm, then
        //use F5

        //To deploy this project to your SharePoint Online site, upload
        //the SPO_SandboxedWebPart.wsp solution file from the bin/debug
        //folder to your solution gallery. Activate the solution and add
        //the web part to a page. 

        //Web part controls
        Label labelWebName;
        Label labelSandboxCheck;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            //Get the SPWeb object for the current site
            SPWeb currentWeb = SPContext.Current.Web;
            labelWebName = new Label();
            labelWebName.Text = "The SharePoint site is called: " + currentWeb.Title;
            this.Controls.Add(labelWebName);
            //Check if this is in the Sandbox
            labelSandboxCheck = new Label();
            if (checkSandbox()) {
                labelSandboxCheck.Text = "This Web Part is sandboxed";
            }
            else
            {
                labelSandboxCheck.Text = "This Web Part is NOT sandboxed";
            }
            this.Controls.Add(labelSandboxCheck);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            EnsureChildControls();
            labelWebName.RenderControl(writer);
            writer.Write("<br /><br />");
            labelSandboxCheck.RenderControl(writer);
        }

        private bool checkSandbox()
        {
            //This method returns true only if the code is running in the sandbox
            if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Sandbox"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
