using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SharePoint.CustomControls.WebControls
{
    public partial class InterestCalculatorEditor : UserControl, IFieldEditor
    {
        InterestCalculatorField _interestCalculatorField = null;

        public bool DisplayAsNewSection
        {
            get { return false; }
        }

        void IFieldEditor.InitializeWithField(SPField field)
        {
            _interestCalculatorField = field as InterestCalculatorField;
            if (Page.IsPostBack)
            {
                return;
            }
            if (_interestCalculatorField != null && _interestCalculatorField.InterestRate != null)
            {
                InterestRate.Text = _interestCalculatorField.InterestRate;
            }
        }

        public void OnSaveChange(Microsoft.SharePoint.SPField field, bool isNewField)
        {
            InterestCalculatorField interestCalculatorField = (InterestCalculatorField)field;
            string interestRate = InterestRate.Text == string.Empty ? "0" : InterestRate.Text;
            if (isNewField)
            {
                interestCalculatorField.UpdateCustomProperty(interestRate);
            }
            else
            {
                interestCalculatorField.InterestRate = interestRate;
            }
        }
    }
}