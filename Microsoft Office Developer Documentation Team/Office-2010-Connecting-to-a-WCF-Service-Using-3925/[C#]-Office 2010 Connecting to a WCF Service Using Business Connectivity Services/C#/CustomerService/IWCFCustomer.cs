using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text; 

namespace CustomerService
{
  [ServiceContract]
  public interface IWCFCustomer
  {
    [OperationContract]
    IEnumerable<WCFCustomerType> ReadList();

    [OperationContract]
    WCFCustomerType ReadItem(int customerID);

    [OperationContract]
    WCFCustomerType Create(WCFCustomerType newCustomer);

    [OperationContract]
    void Update(WCFCustomerType customer);

    [OperationContract]
    void Delete(int customerID);
  }

  [DataContract]
  public class WCFCustomerType
  {
    Int32 customerID;
    string firstName;
    string lastName;

    [DataMember]
    public Int32 CustomerID
    {
      get { return customerID; }
      set { customerID = value; }
    }

    [DataMember]
    public string FirstName
    {
      get { return firstName; }
      set { firstName = value; }
    }

    [DataMember]
    public string LastName
    {
      get { return lastName; }
      set { lastName = value; }
    }
  }
}