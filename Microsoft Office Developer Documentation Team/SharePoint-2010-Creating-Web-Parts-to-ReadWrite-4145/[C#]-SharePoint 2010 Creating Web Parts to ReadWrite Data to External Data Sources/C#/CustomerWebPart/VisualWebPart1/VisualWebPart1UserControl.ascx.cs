using System;
using System.Drawing;
using System.Web.UI;

using Microsoft.SharePoint.BusinessData.SharedService;
using Microsoft.BusinessData.MetadataModel;
using Microsoft.BusinessData.Runtime;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

using Microsoft.BusinessData.Infrastructure;
using Microsoft.BusinessData.MetadataModel.Collections;

namespace CustomerWebPart.VisualWebPart1
{
  public partial class VisualWebPart1UserControl : UserControl
  {
    #region Properties

    protected string EntityNamespace
    {
      get { return ECTNamespace.Text.Trim(); }
    }

    protected string EntityName
    {
      get { return ECTName.Text.Trim(); }
    }

    protected bool EntityValuesAreSet
    {
      get
      {
        if (EntityNamespace == string.Empty ||
          EntityName == string.Empty)
          return false;
        else
          return true;
      }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    #region Create Customer

    protected void CreateNewCustomer_Click(object sender,
      EventArgs e)
    {
      // Make sure we have values for the entity namespace and name.
      if (!EntityValuesAreSet)
      {
        DisplaySetPropertyValuePrompt(true);
        return;
      }
      else
        DisplaySetPropertyValuePrompt(false);

      try
      {
        using (new Microsoft.SharePoint.SPServiceContextScope(
          SPServiceContext.GetContext(SPContext.Current.Site)))
        {
          // Get the BDC service and metadata catalog.
          BdcService service =
           SPFarm.Local.Services.GetValue<BdcService>(String.Empty);
          IMetadataCatalog catalog =
            service.GetDatabaseBackedMetadataCatalog(
            SPServiceContext.Current);

          // Get the entity using the specified name and namespace.
          IEntity entity =
            catalog.GetEntity(EntityNamespace, EntityName);
          ILobSystemInstance LobSysteminstance =
            entity.GetLobSystem().GetLobSystemInstances()[0].Value;

          // Get the fields on the entity.
          IView createView =
            entity.GetCreatorView("CreateCustomer");
          IFieldValueDictionary valueDictionary =
            createView.GetDefaultValues();

          // Set the values of the entity fields.
          valueDictionary["EmailAddress"] = Email.Text;
          valueDictionary["FirstName"] = FirstName.Text;
          valueDictionary["LastName"] = LastName.Text;
          valueDictionary["MiddleName"] = MiddleName.Text;
          valueDictionary["Phone"] = Phone.Text;
          valueDictionary["Title"] = Title.Text;

          // Call the creator method and display the returned
          // Customer ID.
          Identity id = entity.Create(valueDictionary,
            LobSysteminstance);

          CustomerID.Text =
            id.GetIdentifierValues().GetValue(0).ToString();

          StatusLabel.ForeColor = Color.Green;
          StatusLabel.Text = "Customer successfully created.";
        }
      }
      catch (Exception ex)
      {
        StatusLabel.ForeColor = Color.Red;
        StatusLabel.Text = "Unable to create customer." +
          ex.Message;
      }
    }

    #endregion

    #region Find Customer By ID

    protected void FindCustomerByID_Click(
      object sender, EventArgs e)
    {
      // Make sure we have values for the entity namespace and name.
      if (!EntityValuesAreSet)
      {
        DisplaySetPropertyValuePrompt(true);
        return;
      }
      else
        DisplaySetPropertyValuePrompt(false);

      // Do simple validation of the customer ID. Make sure it is
      // an integer.  
      int customerID = -1;

      if (!ValidateCustomerID(CustomerID.Text, ref customerID))
      {
        ClearFields(false);
        StatusLabel.ForeColor = Color.Red;
        StatusLabel.Text =
          "Please enter an integer for the Customer ID value.";
        return;
      }

      try
      {
        using (new Microsoft.SharePoint.SPServiceContextScope(
          SPServiceContext.GetContext(SPContext.Current.Site)))
        {
          // Get the BDC service and metadata catalog.
          BdcService service =
           SPFarm.Local.Services.GetValue<BdcService>(String.Empty);
          IMetadataCatalog catalog =
            service.GetDatabaseBackedMetadataCatalog(
            SPServiceContext.Current);

          // Get the entity using the specified name and namespace.
          IEntity entity =
            catalog.GetEntity(EntityNamespace, EntityName);
          ILobSystemInstance LobSysteminstance =
            entity.GetLobSystem().GetLobSystemInstances()[0].Value;

          // Create an Identity based on the specified Customer ID.
          Identity identity = new Identity(customerID);

          // Get a method instance for the SpecificFinder method.
          IMethodInstance method =
            entity.GetMethodInstance("GetCustomerById",
            MethodInstanceType.SpecificFinder);

          // Execute the Specific Finder method to return the
          // customer data.
          IEntityInstance iei =
            entity.FindSpecific(identity, LobSysteminstance);

          // Display the data for the returned customer in the UI.
          Title.Text = iei["Title"] != null ?
            iei["Title"].ToString() : string.Empty;
          FirstName.Text = iei["FirstName"] != null ?
            iei["FirstName"].ToString() : string.Empty;
          MiddleName.Text = iei["MiddleName"] != null ?
            iei["MiddleName"].ToString() : string.Empty;
          LastName.Text = iei["LastName"] != null ?
            iei["LastName"].ToString() : string.Empty;
          Email.Text = iei["EmailAddress"] != null ?
            iei["EmailAddress"].ToString() : string.Empty;
          Phone.Text = iei["Phone"] != null ?
            iei["Phone"].ToString() : string.Empty;
        }
      }
      catch (Exception ex)
      {
        ClearFields(false);

        StatusLabel.ForeColor = Color.Red;
        StatusLabel.Text = "Unable to find customer with ID = " +
          CustomerID.Text + ". " + ex.Message;
      }
    }

    #endregion

    #region Update Customer

    protected void UpdateCustomer_Click(object sender, EventArgs e)
    {
      // Make sure we have values for the entity namespace and name.
      if (!EntityValuesAreSet)
      {
        DisplaySetPropertyValuePrompt(true);
        return;
      }
      else
        DisplaySetPropertyValuePrompt(false);

      // Do simple validation of the customer ID. Make sure it is
      // an integer.     
      int customerID = -1;

      if (!ValidateCustomerID(CustomerID.Text, ref customerID))
      {
        StatusLabel.ForeColor = Color.Red;
        StatusLabel.Text =
          "Please enter an integer for the Customer ID value.";
        return;
      }

      try
      {
        using (new Microsoft.SharePoint.SPServiceContextScope(
          SPServiceContext.GetContext(SPContext.Current.Site)))
        {
          // Get the BDC service and metadata catalog.
          BdcService service =
           SPFarm.Local.Services.GetValue<BdcService>(String.Empty);
          IMetadataCatalog catalog =
            service.GetDatabaseBackedMetadataCatalog(
            SPServiceContext.Current);

          // Get the entity using the specified name and namespace.
          IEntity entity =
            catalog.GetEntity(EntityNamespace, EntityName);
          ILobSystemInstance LobSysteminstance =
            entity.GetLobSystem().GetLobSystemInstances()[0].Value;

          // Create an Identity based on the specified Customer ID.
          Identity identity = new Identity(customerID);

          // Get a method instance for the Updater method.
          IMethodInstance method =
            entity.GetMethodInstance("UpdateCustomer",
            MethodInstanceType.Updater);

          // The UpdateCustomer method of the external content type
          // maps to the UpdateCustomer method in the AdventureWorks
          // web service. Looking at the source for the web service
          // shows that the UpdateCustomer method has the following
          // signature:
          //
          // public void UpdateCustomer(SalesCustomer customer)
          //
          // The SalesCustomer type has the following layout: 
          //
          // public class SalesCustomer
          // {
          //     public int CustomerId { get; set; }
          //     public String Title { get; set; }
          //     public String FirstName { get; set; }
          //     public String MiddleName { get; set; }
          //     public String LastName   { get; set; }
          //     public String EmailAddress { get; set; }
          //     public String Phone { get; set; }
          //     public DateTime ModifiedDate { get; set; }
          // }

          // Get the collection of parameters for the method.
          // In this case there is only one.
          IParameterCollection parameters =
            method.GetMethod().GetParameters();

          // Use type reflection to get an instance of a
          // SalesCustomer object to pass as a parameter.
          ITypeReflector reflector = parameters[0].TypeReflector;
          ITypeDescriptor rootTypeDescriptor =
            parameters[0].GetRootTypeDescriptor();

          object[] methodParamInstances =
            method.GetMethod().CreateDefaultParameterInstances(
            method);
          Object instance = methodParamInstances[0];

          // Get type descriptors for each of the SalesCustomer
          // members.
          ITypeDescriptor customerIDTypeDescriptor =
            rootTypeDescriptor.GetChildTypeDescriptors()[0];
          ITypeDescriptor titleTypeDescriptor =
            rootTypeDescriptor.GetChildTypeDescriptors()[1];
          ITypeDescriptor firstNameTypeDescriptor =
            rootTypeDescriptor.GetChildTypeDescriptors()[2];
          ITypeDescriptor middleNameTypeDescriptor =
            rootTypeDescriptor.GetChildTypeDescriptors()[3];
          ITypeDescriptor lastNameTypeDescriptor =
            rootTypeDescriptor.GetChildTypeDescriptors()[4];
          ITypeDescriptor emailAddressTypeDescriptor =
            rootTypeDescriptor.GetChildTypeDescriptors()[5];
          ITypeDescriptor phoneTypeDescriptor =
            rootTypeDescriptor.GetChildTypeDescriptors()[6];
          ITypeDescriptor modifiedDateTypeDescriptor =
            rootTypeDescriptor.GetChildTypeDescriptors()[7];

          // Set the values of the SalesCustomer object members
          // with the values specified by the user.
          reflector.Set(customerIDTypeDescriptor,
            rootTypeDescriptor,
            ref instance, customerID);
          reflector.Set(titleTypeDescriptor, rootTypeDescriptor,
            ref instance, Title.Text);
          reflector.Set(firstNameTypeDescriptor, rootTypeDescriptor,
            ref instance, FirstName.Text);
          reflector.Set(middleNameTypeDescriptor,
            rootTypeDescriptor,
            ref instance, MiddleName.Text);
          reflector.Set(lastNameTypeDescriptor, rootTypeDescriptor,
            ref instance, LastName.Text);
          reflector.Set(emailAddressTypeDescriptor,
            rootTypeDescriptor,
            ref instance, Email.Text);
          reflector.Set(phoneTypeDescriptor, rootTypeDescriptor,
            ref instance, Phone.Text);
          reflector.Set(modifiedDateTypeDescriptor,
            rootTypeDescriptor,
            ref instance, DateTime.Now);

          // Execute the updater method, passing the parameter.
          entity.Execute(method, LobSysteminstance,
            ref methodParamInstances);

          StatusLabel.ForeColor = Color.Green;
          StatusLabel.Text = "Customer successfully updated.";
        }
      }
      catch (Exception ex)
      {
        StatusLabel.ForeColor = Color.Red;
        StatusLabel.Text = "Unable to find customer with ID = " +
          CustomerID.Text + ". " + ex.Message;
      }
    }

    #endregion

    #region Helpers

    protected void DisplaySetPropertyValuePrompt(bool showPrompt)
    {
      if (showPrompt)
      {
        StatusLabel.ForeColor = Color.Red;
        StatusLabel.Text =
          "Please enter values for the ECT name and namespace!";
      }
      else
        StatusLabel.Text = string.Empty;
    }

    protected void ClearAllFields_Click(object sender, EventArgs e)
    {
      ClearFields(true);
    }

    protected bool ValidateCustomerID(string CustomerIDIn,
      ref int CustomerIDOut)
    {
      try
      {
        CustomerIDOut = Convert.ToInt32(CustomerIDIn);
        return true;
      }
      catch
      {
        CustomerIDOut = -1;
        return false;
      }
    }

    protected void ClearFields(bool clearCustomerID)
    {
      if (clearCustomerID)
        CustomerID.Text = string.Empty;

      Title.Text = string.Empty;
      FirstName.Text = string.Empty;
      MiddleName.Text = string.Empty;
      LastName.Text = string.Empty;
      Email.Text = string.Empty;
      Phone.Text = string.Empty;
    }

    #endregion
  }
}