using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text; 

using System.Xml.Linq;
using Microsoft.BusinessData.Runtime;

namespace CustomerService
{
  [ServiceBehavior(Namespace = "urn:CustomerService:WCFCustomer")]
  public class WCFCustomer : IWCFCustomer
  {
    private const string dataFilePath =
      @"C:\Dev\CustomerService\CustomerService\App_Data\CustomerData.xml";

    public IEnumerable<WCFCustomerType> ReadList()
    {
      try
      {
        var customers =
          XElement.Load(dataFilePath).Elements("Customer");

        var customersList =
          from cust in customers
          orderby (string)cust.Attribute("ID")
          select new WCFCustomerType
          {
            CustomerID = (Int32)cust.Attribute("ID"),
            FirstName = (string)cust.Element("CustomerFName"),
            LastName = (string)cust.Element("CustomerLName")
          };

        return customersList;
      }
      catch (Exception generalException)
      {
        throw new RuntimeException(
          "There was a problem reading customer data.",
          generalException);
      }
    }

    public WCFCustomerType ReadItem(int customerID)
    {
      try
      {
        var customers =
          XElement.Load(dataFilePath).Elements("Customer");

        var customerList =
          from cust in customers
          where (Int32)cust.Attribute("ID") == customerID
          select new WCFCustomerType
          {
            CustomerID = (Int32)cust.Attribute("ID"),
            FirstName = (string)cust.Element("CustomerFName"),
            LastName = (string)cust.Element("CustomerLName")
          };

        // The following will throw InvalidOperationException if the
        // customerNameElements collection is empty meaning a
        // customer with the specified ID does not exist.
        return customerList.First();
      }
      catch (InvalidOperationException)
      {
        throw new ObjectNotFoundException(
          "Unable to read data for the customer with ID = " +
          customerID.ToString() +
          ". The customer no longer exists.");
      }
      catch (Exception generalException)
      {
        throw new RuntimeException(
          "There was a problem reading customer data.",
          generalException);
      }
    }

    public WCFCustomerType Create(WCFCustomerType newCustomer)
    {
      try
      {
        XElement customers = XElement.Load(dataFilePath);
        Int32 nextCustomerId =
          (Int32)customers.Attribute("NextCustomerId");

        WCFCustomerType returnCustomer = new WCFCustomerType();

        returnCustomer.CustomerID = nextCustomerId;
        returnCustomer.FirstName = newCustomer.FirstName;
        returnCustomer.LastName = newCustomer.LastName;

        XElement newCustomerElement = new XElement("Customer");
        newCustomerElement.SetAttributeValue("ID",
          nextCustomerId);

        XElement newCustomerFNameElement =
          new XElement("CustomerFName", returnCustomer.FirstName);

        XElement newCustomerLNameElement =
          new XElement("CustomerLName", returnCustomer.LastName);

        newCustomerElement.Add(newCustomerFNameElement);
        newCustomerElement.Add(newCustomerLNameElement);
        customers.Add(newCustomerElement);

        customers.SetAttributeValue("NextCustomerId",
          nextCustomerId + 1);
        customers.Save(dataFilePath);

        return returnCustomer;
      }
      catch (Exception generalException)
      {
        throw new RuntimeException(
          "There was a problem creating a new customer.",
          generalException);
      }
    }

    public void Update(WCFCustomerType customer)
    {
      try
      {
        XElement customers = XElement.Load(dataFilePath);

        var customerFNameElements =
          from cust in customers.Elements("Customer")
          where (Int32)cust.Attribute("ID") == customer.CustomerID
          select cust.Element("CustomerFName");

        var customerLNameElements =
          from cust in customers.Elements("Customer")
          where (Int32)cust.Attribute("ID") == customer.CustomerID
          select cust.Element("CustomerLName");

        // The following will throw InvalidOperationException if
        // the collections are empty meaning a customer with
        // the specified ID does not exist.

        XElement customerFNameElement =
          customerFNameElements.First();
        customerFNameElement.SetValue(customer.FirstName);

        XElement customerLNameElement =
          customerLNameElements.First();
        customerLNameElement.SetValue(customer.LastName);

        customers.Save(dataFilePath);
      }
      catch (InvalidOperationException)
      {
        throw new ObjectNotFoundException(
          "Unable to update the customer with ID = " +
          customer.CustomerID.ToString() +
          ". The customer no longer exists.");
      }
      catch (Exception innerExeption)
      {
        throw new RuntimeException(
          "There was a problem updating the customer with ID = " +
          customer.CustomerID.ToString() + ".", innerExeption);
      }
    }

    public void Delete(int customerID)
    {
      try
      {
        XElement customers = XElement.Load(dataFilePath);

        var customerElements =
          from cust in customers.Elements("Customer")
          where (Int32)cust.Attribute("ID") == customerID
          select cust;

        // The following will throw InvalidOperationException if the
        // customerElements collection is empty meaning a customer
        // with the specified ID does not exist.
        XElement customer = customerElements.First();
        customer.Remove();

        customers.Save(dataFilePath);
      }
      catch (InvalidOperationException)
      {
        throw new ObjectNotFoundException(
          "Unable to delete the customer with ID = " +
          customerID.ToString() +
          ". The customer no longer exists.");
      }
      catch (Exception generalException)
      {
        throw new RuntimeException(
          "There was a problem deleting the customer with ID = " +
          customerID.ToString() + ".", generalException);
      }
    }
  }
}