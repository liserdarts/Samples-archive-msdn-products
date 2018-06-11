using System;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.WebControls;

namespace SharePoint.CustomControls.WebControls
{
    public class InterestCalculatorFieldControl : TextField
    {
        protected Label InterestCalculatorFieldForDisplay;

        protected override string DefaultTemplateName
        {
            get
            {
                if (this.ControlMode == SPControlMode.Display)
                {
                    return this.DisplayTemplateName;
                }
                else
                {
                    return "InterestCalculatorFieldForEdit";
                }
            }
        }

        public override string DisplayTemplateName
        {
            get
            {
                return "InterestCalculatorFieldForDisplay";
            }
            set
            {
                base.DisplayTemplateName = value;
            }
        }

        protected override void CreateChildControls()
        {
            if (this.Field != null)
            {
                base.CreateChildControls();
                this.InterestCalculatorFieldForDisplay = (Label)TemplateContainer.FindControl("InterestValueForDisplay");

                if (this.ControlMode == SPControlMode.Display)
                {
                    InterestCalculatorFieldForDisplay.Text = (String)this.ItemFieldValue;
                }
            }
        }

        public override object Value
        {
            get
            {
                EnsureChildControls();
                return base.Value;
            }
            set
            {
                EnsureChildControls();
                base.Value = (String)value;
            }
        }
    }
}