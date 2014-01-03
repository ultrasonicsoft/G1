using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GlassProductManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IManagerService
    {
        [WebGet(UriTemplate = "/personsdataJSON",
  ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Person> GetPersonsDataJSON();

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "Login",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
          )]
        bool Login(User user);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "GetWorksheetItemDetails",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
          )]
        WorksheetItem GetWorksheetItemDetails(string worksheetItemID);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Person
    {
        int id;
        string name;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    [DataContract]
    public class WorksheetItem
    {
        string description;
        string wsNumber;
        string quantity;
        string status;
        int id;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [DataMember]
        public string WSNumber
        {
            get { return wsNumber; }
            set { wsNumber = value; }
        }

        [DataMember]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        [DataMember]
        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }

    [DataContract]
    public class User
    {
        string name;
        string password;

        [DataMember(Name = "Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember(Name = "Password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
