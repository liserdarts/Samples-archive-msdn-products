using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Linq;
using Microsoft.BusinessData.Runtime;

namespace CustomerModel.BdcModel1
{
  public partial class CustomerService
  {
    private const string dataFilePath = @"C:\Dev\Temp\CustomerData.xml";

    public static IEnumerable<Customer> ReadList()
    {
      try
      {
        var customers =
          XElement.Load(dataFilePath).Elements("Customer");

        var customersList =
          from cust in customers
          orderby (string)cust.Attribute("ID")
          select new Customer
          {
            CustomerID = (Int32)cust.Attribute("ID"),
            CustomerName = (string)cust.Element("CustomerName")
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

    public static Customer ReadItem(int customerID)
    {
      try
      {
        var customers =
          XElement.Load(dataFilePath).Elements("Customer");

        var customerList =
          from cust in customers
          where (Int32)cust.Attribute("ID") == customerID
          select new Customer
          {
            CustomerID = (Int32)cust.Attribute("ID"),
            CustomerName = (string)cust.Element("CustomerName")
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

    public static Customer Create(Customer newCustomer)
    {
      try
      {
        XElement customers = XElement.Load(dataFilePath);
        Int32 nextCustomerId =
          (Int32)customers.Attribute("NextCustomerId");

        Customer returnCustomer = new Customer();

        returnCustomer.CustomerID = nextCustomerId;
        returnCustomer.CustomerName = newCustomer.CustomerName;

        XElement newCustomerElement = new XElement("Customer");
        newCustomerElement.SetAttributeValue("ID", nextCustomerId);

        XElement newCustomerNameElement = new XElement("CustomerName",
          returnCustomer.CustomerName);

        newCustomerElement.Add(newCustomerNameElement);
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

    public static void Update(Customer customer, Int32 customerID)
    {
      try
      {
        XElement customers = XElement.Load(dataFilePath);

        var customerNameElements =
          from cust in customers.Elements("Customer")
          where (Int32)cust.Attribute("ID") == customer.CustomerID
          select cust.Element("CustomerName");

        // The following will throw InvalidOperationException if
        // the customerNameElements collection is empty meaning a
        // customer with the specified ID does not exist.
        XElement customerNameElement = customerNameElements.First();
        customerNameElement.SetValue(customer.CustomerName);

        customers.Save(dataFilePath);
      }
      catch (InvalidOperationException)
      {
        throw new ObjectNotFoundException(
          "Unable to update the customer with ID = " +
          customerID.ToString() +
          ". The customer no longer exists.");
      }
      catch (Exception generalException)
      {
        throw new RuntimeException(
          "There was a problem updating the customer with ID = " +
          customerID.ToString() + ".", generalException);
      }
    }

    public static void Delete(int customerID)
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
