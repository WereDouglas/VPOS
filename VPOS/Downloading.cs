using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPOS.Model;

namespace VPOS
{
   public class Downloading
    {
        public Downloading()
        {

        }
        public static bool CheckDownload()
        {
            string query = "SELECT * FROM organisation  LIMIT 1";
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                return true;
            }
            else
            {

                return false;
            }

        }
        public static void DownloadUsers(string type)
        {
            string query = "";

            if (type=="Complete") {

                query = "SELECT * FROM users WHERE  orgID = '" + Helper.OrgID + "' ;";
            }
            else {

                query = "SELECT * FROM users WHERE STR_TO_DATE( created, '%Y-%m-%d %H:%i:%s')  >  STR_TO_DATE('" + Helper.lastSync + "', '%Y-%m-%d %H:%i:%s') AND   orgID = '" + Helper.OrgID + "' ;";

            }
            Users _user;
        
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
                MainForm._Form1.FeedBack("USERS TO BE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {                 
                    MainForm._Form1.FeedBack("DEALING WITH " + results[K]["name"].ToString());

                    string Query = "DELETE from users WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);

                    _user = new Users(results[K]["id"].ToString(), results[K]["idno"].ToString(), results[K]["contact"].ToString(), results[K]["contact2"].ToString(), results[K]["surname"].ToString(), results[K]["lastname"].ToString(), results[K]["othername"].ToString(), results[K]["email"].ToString(), results[K]["nationality"].ToString(), results[K]["address"].ToString(), results[K]["passwords"].ToString(), results[K]["gender"].ToString(), Helper.OrgID, results[K]["roles"].ToString(), results[K]["initialPassword"].ToString(), results[K]["account"].ToString(), results[K]["status"].ToString(), results[K]["image"].ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), results[K]["storeID"].ToString());
                    DBConnect.Insert(_user);                   
                    MainForm._Form1.FeedBack("DOWNLOADING USER " + results[K]["name"].ToString());
                }

            }
            MainForm._Form1.FeedBack("DOWNLOADING USERS COMPLETE");
        }
        public static void DownloadOrg()
        {
            string query = "";
                query = "SELECT * FROM organisation WHERE  orgID = '" + Helper.OrgID + "' ;";
           
          
            Organisation _org;

            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
                MainForm._Form1.FeedBack("COMPANY TO BE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    MainForm._Form1.FeedBack("DEALING WITH " + results[K]["name"].ToString());
                    string Query = "DELETE from organisation WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);
               
                    _org = new Organisation(results[K]["id"].ToString(),results[K]["name"].ToString(),results[K]["code"].ToString(), results[K]["registration"].ToString(), results[K]["contact"].ToString(), results[K]["address"].ToString(), results[K]["tin"].ToString(), results[K]["vat"].ToString(), results[K]["email"].ToString(), results[K]["country"].ToString(), results[K]["initialPassword"].ToString(), results[K]["account"].ToString(), results[K]["status"].ToString(), results[K]["expires"].ToString(),results[K]["image"].ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), results[K]["account"].ToString(),results[K]["company"].ToString(), results[K]["storeID"].ToString(), results[K]["sub"].ToString());
                    DBConnect.Insert(_org);

                    
                    MainForm._Form1.FeedBack("DOWNLOADING USER " + results[K]["name"].ToString());
                }

            }
            MainForm._Form1.FeedBack("DOWNLOADING USERS COMPLETE");
        }

        public static void DownloadItems(string type)
        {
            string query = "";

            if (type == "Complete")
            {
                query = "SELECT * FROM item WHERE  orgID = '" + Helper.OrgID + "' ;";
            }
            else
            {
                query = "SELECT * FROM item WHERE STR_TO_DATE( created, '%Y-%m-%d %H:%i:%s')  >  STR_TO_DATE('" + Helper.lastSync + "', '%Y-%m-%d %H:%i:%s') AND   orgID = '" + Helper.OrgID + "' ;";
            }
            Item _item;

            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
                MainForm._Form1.FeedBack("ITEMS TO BE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    MainForm._Form1.FeedBack("DEALING WITH " + results[K]["name"].ToString());

                    string Query = "DELETE from item WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);
                  
                    _item = new Item(results[K]["id"].ToString(), results[K]["name"].ToString(), results[K]["generic"].ToString(), results[K]["code"].ToString(), results[K]["description"].ToString(), results[K]["manufacturer"].ToString(), results[K]["country"].ToString(), results[K]["composition"].ToString(), results[K]["category"].ToString(), results[K]["barcode"].ToString(), results[K]["image"].ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), results[K]["strength"].ToString(), Helper.OrgID, results[K]["valid"].ToString(),results[K]["sub"].ToString());
                    DBConnect.Insert(_item);
                    MainForm._Form1.FeedBack("DOWNLOADING ITEM " + results[K]["name"].ToString());
                }
            }
            MainForm._Form1.FeedBack("DOWNLOADING ITEMS COMPLETE");
        }

        public static void DownloadRole(string type)
        {
            string query = "";

            if (type == "Complete")
            {
                query = "SELECT * FROM roles WHERE   orgID = '" + Helper.OrgID + "'";
            }
            else
            {
                query = "SELECT * FROM roles WHERE STR_TO_DATE( created, '%Y-%m-%d %H:%i:%s')  >  STR_TO_DATE('" + Helper.lastSync + "', '%Y-%m-%d %H:%i:%s') AND   orgID = '" + Helper.OrgID + "'";

            }
            Roles _role;
              string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                MainForm._Form1.FeedBack("DOWNLOADING ROLES" + resulted);
                JArray results = JArray.Parse(resulted);
                MainForm._Form1.FeedBack("CLIENTS TO BE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    MainForm._Form1.FeedBack("DEALING WITH ROLE " + results[K]["title"].ToString());
                    string Query2 = "DELETE from roles WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query2);

                    _role = new Roles(results[K]["id"].ToString(), results[K]["title"].ToString(),results[K]["views"].ToString(), results[K]["actions"].ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.StoreID);
                    
                    DBConnect.Insert(_role);                   

                    MainForm._Form1.FeedBack("ROLES " + (results.Count - 1).ToString());
                }
                MainForm._Form1.FeedBack("ROLES DOWNLOAD COMPLETE" + Helper.lastSync);
            }
        }
    }
}
