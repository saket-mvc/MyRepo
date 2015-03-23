using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetToSalesforce.Api;
using NetToSalesforce.Salesforce;

namespace NetToSalesforce.Controllers
{
    public class ApiController : Controller
    {
        public ActionResult Index()
        {
            return View("List");
        }

        public ActionResult List()
        {
//            string soql =
//               @"SELECT c.ID, c.Email, c.FirstName, c.LastName, c.Name
//                 FROM Contact c
//                 WHERE c.Email <> ''";

			string soql =
               @"SELECT ID,Email, FirstName, LastName, Name
                 FROM Contact";

            using (ApiService api = new ApiService())
            {
                List<Contact> contacts = api.Query<Contact>(soql);
                return View(contacts);
            }
        }

        public ActionResult Edit(string id)
        {
            string soql =
               String.Format(@"SELECT c.ID, c.Email, c.FirstName, c.LastName, c.Name
                               FROM Contact c
                               WHERE c.ID = '{0}'", id);

            using (ApiService api = new ApiService())
            {
                Contact contact = api.QuerySingle<Contact>(soql);

                return View(contact);
            }
        }

        public ActionResult Update(string id, string email)
        {
            Contact modifiedContact = new Contact() { Id = id, Email = email };
            
            // To reset a field to null, the field name needs to be added to the 
            // fieldsToNull array.  The line below is an example of how to do this.
            //modifiedContact.fieldsToNull = new string[] { "FirstName", "LastName" };

            using (ApiService api = new ApiService())
            {
                sObject[] updates = new sObject[] { modifiedContact };
                api.Update(updates);
            }

            return RedirectToAction("List");
        }

        public ActionResult ListAsync()
        {
            return View();
        }

        public JsonResult StartQuery()
        {
            Guid id;
            string soql =
               @"SELECT c.ID, c.Email, c.FirstName, c.LastName, c.Name
                 FROM Contact c";

            using (ApiService api = new ApiService())
            {
                id = api.QueryAsync(soql);

                return Json(new { id = id });
            }
        }

        public JsonResult GetQuery(Guid id)
        {
            bool success = ApiService.asyncResults.ContainsKey(id);
            List<Contact> contacts = new List<Contact>();

            if (success)
            {
                ApiService.asyncResults[id].ForEach(i => contacts.Add(i as Contact));
                ApiService.asyncResults.Remove(id);
            }

            return Json(new { success = success, contacts = contacts });
        }
    }
}
