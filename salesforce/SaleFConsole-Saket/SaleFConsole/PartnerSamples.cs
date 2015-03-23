using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;
using System.Collections;
using SaleFConsole.Api;

namespace SaleFConsole
{
	class PartnerSamples
	{
		private SforceService binding=new SforceService();
        const int defaultTimeout = 30000;
         public static Dictionary<Guid, List<sObject>> asyncResults;
         string serverurl;
         string server_sessionid;

		static void Main(string[] args)
		{
			PartnerSamples samples = new PartnerSamples();
            if (samples.login())
            {
                Console.WriteLine("Logged In");
                Console.WriteLine("Welcome to Salesforce");
                Console.WriteLine("Enter Appropriate Choice To Proceed");
                Console.WriteLine("1 : To get Contacts");
                Console.WriteLine("2 : To get Leads");
                Console.WriteLine("3 : To get Campains");
                string i = Console.ReadLine();
                if (i == "1")
                {
                    samples.getContact();
                }
                else if (i == "2")
                {
                    samples.getLeads();
                }
                else if (i == "3")
                {
                    samples.getCampaign();
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Ërror in logged in");
                Console.ReadLine();
            } 
		}

        private bool login()
        {
            string username = "saket.tamgadge@tudip.nl";
            string password = "theghost27456tudipMK29ZoXjEdgRQhJTT4dVmnIj3";
            binding = new SforceService();
            LoginResult lr=binding.login(username,password);
            binding.Url = lr.serverUrl;
            serverurl = lr.serverUrl;
            server_sessionid = lr.sessionId;
            binding.SessionHeaderValue = new SessionHeader() { sessionId = lr.sessionId };
            if (lr.passwordExpired)
            {
                return false;
            }
            else
            {
                return true;  
            }  
        }

		private GetUserInfoResult UserInfo()
		{
			GetUserInfoResult ir = binding.getUserInfo();
			return ir;
		}

        #region Contact

        private void SetupService()
        {
            string username = "saket.tamgadge@tudip.nl";
            string password = "theghost27456tudipMK29ZoXjEdgRQhJTT4dVmnIj3";
            binding = new SforceService();
            LoginResult lr = binding.login(username, password);
            binding.Url = lr.serverUrl;
            binding.SessionHeaderValue = new SessionHeader() { sessionId = lr.sessionId };
        }

         public List<T> Query<T>(string soql) where T : sObject, new()
         {
             binding = new SforceService();
             binding.Timeout = defaultTimeout;
             asyncResults = new Dictionary<Guid, List<sObject>>();
             List<T> returnList = new List<T>();
             binding.Url = serverurl;
             binding.SessionHeaderValue = new SessionHeader() { sessionId = server_sessionid };

             QueryResult results = binding.query(soql);

             for (int i = 0; i < results.size; i++)
             {
                 T item = results.records[i] as T;

                 if (item != null)
                     returnList.Add(item);
             }

             return returnList;
         }

        private void getContact()
        {
            string soql =
               @"SELECT ID,Email, FirstName, LastName, Name
                 FROM Contact";
            List<Contact> contacts =Query<Contact>(soql);

            foreach (Contact name in contacts)
            {
                Console.WriteLine(name.FirstName.ToString());
            }
        }

        private void getLeads()
        {
            string soql =
               @"SELECT ID,Email, FirstName, LastName, Name
                 FROM Lead";
            List<Lead> leads = Query<Lead>(soql);

            foreach (Lead name in leads)
            {
                Console.WriteLine(name.FirstName.ToString());
            }
        }

        private void getCampaign()
        {
            string soql =
               @"SELECT  Name
                 FROM Campaign";
            List<Campaign> campaign = Query<Campaign>(soql);

            foreach (Campaign name in campaign)
            {
                Console.WriteLine(name.Name.ToString());
            }
        }
        #endregion
    }
}
