using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GlassProductManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ManagerService : IManagerService
    {
        public List<Person> GetPersonsDataJSON()
        {
            Person p1 = new Person();
            p1.ID = 1;
            p1.Name = "Balram";

            Person p2 = new Person();
            p2.ID = 2;
            p2.Name = "Kishor";

            Person p3 = new Person();
            p3.ID = 3;
            p3.Name = "Jay";

            List<Person> persons = new List<Person>();
            persons.Add(p1);
            persons.Add(p2);
            persons.Add(p3);

            return persons;
        }

        public WorksheetItem GetWorksheetItemDetails(string worksheetItemID)
        {
            string[] worksheetInputData = worksheetItemID.Split('-');

            WorksheetItem workItemDetails = BusinessLogic.GetWorksheetItemDetails(worksheetInputData[0],worksheetInputData[1],worksheetInputData[2]);

            workItemDetails.WSNumber = worksheetInputData[0];
            return workItemDetails;
        }

        public bool Login(User user)
        {
            bool result = false;
            
            result = BusinessLogic.IsValidUser(user.Name, user.Password);
            return result;
        }

        public void UpdateGlassItemStatus(WorksheetItemIDStatus item)
        {
            BusinessLogic.UpdateGlassItemStatus(item.ID.ToString(), item.Status);            
        }

        public void PrintBarcodeLabel(BarcodeLabel item)
        {
            int i = 0;
            //GlassProductManager.BarcodePrinter.PrintLineItem(item.WSNumber, item.LineID, item.ItemID);
        }
    }
}
