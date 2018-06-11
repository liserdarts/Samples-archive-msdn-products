using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.WebControls;
using SharePoint.CustomControls.WebControls;

namespace SharePoint.CustomControls
{
    public class InterestCalculatorField : SPFieldText
    {
        private string _interestRate;
        private static string _interestRateValue;

        public string InterestRate
        {
            get
            {
                if (_interestRateValue == string.Empty || _interestRateValue == null)
                {
                    return _interestRate;
                }
                else
                {
                    return _interestRateValue;
                }
            }
            set
            {
                this._interestRate = value;
            }
        }

        public InterestCalculatorField(SPFieldCollection fields, string fieldName)
            : base(fields, fieldName)
        {
            this.Init();
        }

        public InterestCalculatorField(SPFieldCollection fields, string typeName, string displayName)
            : base(fields, typeName, displayName)
        {
            this.Init();
        }


        public void UpdateCustomProperty(string interestRateValue)
        {
            _interestRateValue = interestRateValue;
        }

        private void Init()
        {
            this.InterestRate = this.GetCustomProperty("InterestRate") == null ? null : this.GetCustomProperty("InterestRate").ToString();
        }

        public override void Update()
        {
            this.SetCustomProperty("InterestRate", this.InterestRate);
            base.Update();
            _interestRateValue = string.Empty;
        }

        public override void OnAdded(SPAddFieldOptions op)
        {
            base.OnAdded(op);
            Update();
        }

        public override BaseFieldControl FieldRenderingControl
        {
            [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
            get
            {
                BaseFieldControl fieldControl = new InterestCalculatorFieldControl();
                fieldControl.FieldName = this.InternalName;
                return fieldControl;
            }
        }

        public override string GetValidatedString(object value)
        {
            if ((this.Required == true) && (value == null || value.ToString().Equals(string.Empty)))
            {
                throw new SPFieldValidationException(this.Title + " must have a value.");
            }
            else
            {
                Decimal tempValue;
                if (Decimal.TryParse(value.ToString(), out tempValue))
                {
                    Decimal interest = Decimal.Parse(this.InterestRate);
                    Decimal principal = tempValue;
                    decimal interestPaid = principal * (interest / 100);
                    decimal totalPI = principal + interestPaid;
                    return base.GetValidatedString(totalPI);
                }
                else
                {
                    throw new SPFieldValidationException("Enter only integer value");
                }
            }
        }
    }
}