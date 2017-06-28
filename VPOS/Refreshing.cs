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
    public class Refreshing
    {
        public Refreshing()
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
        public static void Users()
        {
            string query = "";

            query = "SELECT * FROM users WHERE  orgID = '" + Helper.OrgID + "' ;";
            
            Users _user;

            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
               // Form1._Form1.FeedBack("USERS TO BE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["surname"].ToString());

                    string Query = "DELETE from users WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);

                    _user = new Users(results[K]["id"].ToString(), results[K]["idno"].ToString(), results[K]["contact"].ToString(), results[K]["contact2"].ToString(), results[K]["surname"].ToString(), results[K]["lastname"].ToString(), results[K]["othername"].ToString(), results[K]["email"].ToString(), results[K]["nationality"].ToString(), results[K]["address"].ToString(), results[K]["passwords"].ToString(), results[K]["gender"].ToString(), Helper.OrgID, results[K]["roles"].ToString(), results[K]["initialPassword"].ToString(), results[K]["account"].ToString(), results[K]["status"].ToString(), results[K]["image"].ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), results[K]["storeID"].ToString());
                    DBConnect.Insert(_user);
                    Form1._Form1.FeedBack("DOWNLOADING USER " + results[K]["surname"].ToString());
                }

            }
            Form1._Form1.FeedBack("DOWNLOADING USERS COMPLETE");
        }
        public static void Org()
        {
            string query = "";
            query = "SELECT * FROM organisation WHERE  id = '" + Helper.OrgID + "' ;";


            Organisation _org;

            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
               // Form1._Form1.FeedBack("COMPANY TO BE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["name"].ToString());
                    string Query = "DELETE from organisation WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);

                    _org = new Organisation(results[K]["id"].ToString(), results[K]["name"].ToString(), results[K]["code"].ToString(), results[K]["registration"].ToString(), results[K]["contact"].ToString(), results[K]["address"].ToString(), results[K]["tin"].ToString(), results[K]["vat"].ToString(), results[K]["email"].ToString(), results[K]["country"].ToString(), results[K]["initialpassword"].ToString(), results[K]["account"].ToString(), results[K]["status"].ToString(), results[K]["expires"].ToString(), results[K]["image"].ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), results[K]["counts"].ToString(), results[K]["company"].ToString(), results[K]["storeID"].ToString(), results[K]["category"].ToString());
                    DBConnect.Insert(_org);


                    Form1._Form1.FeedBack("DOWNLOADING USER " + results[K]["name"].ToString());
                }

            }
            Form1._Form1.FeedBack("DOWNLOADING USERS COMPLETE");
        }

        public static void Items()
        {
            string query = "SELECT * FROM item WHERE  orgID = '" + Helper.OrgID + "' ;";          
            Item _item;
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
               // Form1._Form1.FeedBack("ITEMS TO BE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["name"].ToString());

                    string Query = "DELETE from item WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);

                    _item = new Item(results[K]["id"].ToString(), results[K]["name"].ToString(), results[K]["generic"].ToString(), results[K]["code"].ToString(), results[K]["description"].ToString(), results[K]["manufacturer"].ToString(), results[K]["country"].ToString(), results[K]["composition"].ToString(), results[K]["category"].ToString(), results[K]["barcode"].ToString(), results[K]["image"].ToString(), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), results[K]["strength"].ToString(), Helper.OrgID, results[K]["valid"].ToString(), results[K]["sub"].ToString());
                    DBConnect.Insert(_item);
                    Form1._Form1.FeedBack("DOWNLOADING ITEM " + results[K]["name"].ToString());
                }
            }
            Form1._Form1.FeedBack("DOWNLOADING ITEMS COMPLETE");
        }
        public static void Stock()
        {
            string query = "";
            query = "SELECT * FROM stock WHERE  orgID = '" + Helper.OrgID + "' ;";
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
               // Form1._Form1.FeedBack("STOCK TO BE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                   
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["barcode"].ToString());
                   
                    string Query = "DELETE from stock WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);
                    Stock _stk = new Stock(results[K]["id"].ToString(), results[K]["itemid"].ToString(), results[K]["qty"].ToString(), results[K]["sale_price"].ToString(), results[K]["purchase_price"].ToString(), results[K]["previous_price"].ToString(), results[K]["total_value"].ToString(), results[K]["batch"].ToString(), results[K]["expire"].ToString(),results[K]["packing"].ToString(), results[K]["units"].ToString(),  results[K]["barcode"].ToString(),results[K]["date_manufactured"].ToString(), results[K]["quantity"].ToString(), results[K]["min_qty"].ToString(), results[K]["counts"].ToString(), results[K]["taking"].ToString(), results[K]["tax"].ToString(), results[K]["promo_price"].ToString(), results[K]["promo_start"].ToString(), results[K]["promo_end"].ToString(), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), results[K]["storeID"].ToString(), results[K]["orgID"].ToString(), results[K]["userID"].ToString());

                   // Stock _stk = new Stock(results[K]["id"].ToString(), results[K]["itemid"].ToString(), results[K]["qty"].ToString(), results[K]["sale_price"].ToString(),  results[K]["purchase_price"].ToString(), results[K]["previous_price"].ToString(), "","", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                    DBConnect.Insert(_stk);
                    Form1._Form1.FeedBack("DOWNLOADING STOCK " + results[K]["barcode"].ToString());
                }
            }
            Form1._Form1.FeedBack("DOWNLOADING STOCK COMPLETE");
        }
        public static void Sale()
        {
            string query = "";
            query = "SELECT * FROM sale WHERE  orgID = '" + Helper.OrgID + "' ;";
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
              //  Form1._Form1.FeedBack("SALE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["no"].ToString());

                    string Query = "DELETE from sale WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);
                    Sale _stk = new Sale(results[K]["id"].ToString(), results[K]["no"].ToString(),results[K]["itemID"].ToString(), results[K]["qty"].ToString(), results[K]["date"].ToString(), results[K]["price"].ToString(),results[K]["total"].ToString(), results[K]["amount"].ToString(), results[K]["method"].ToString(), results[K]["balance"].ToString(), results[K]["customerid"].ToString(), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), results[K]["tax"].ToString(), results[K]["storeID"].ToString(), Helper.OrgID, results[K]["userID"].ToString());

                    // Stock _stk = new Stock(results[K]["id"].ToString(), results[K]["itemid"].ToString(), results[K]["qty"].ToString(), results[K]["sale_price"].ToString(),  results[K]["purchase_price"].ToString(), results[K]["previous_price"].ToString(), "","", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                    DBConnect.Insert(_stk);
                    Form1._Form1.FeedBack("DOWNLOADING SALE " + results[K]["no"].ToString());
                }
            }
            Form1._Form1.FeedBack("DOWNLOADING SALES COMPLETE");
        }
        public static void Purchase()
        {
            string query = "";
            query = "SELECT * FROM purchase WHERE  orgID = '" + Helper.OrgID + "' ;";
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
              //  Form1._Form1.FeedBack("SALE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["no"].ToString());

                    string Query = "DELETE from purchase WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);
                    Purchase _stk = new Purchase(results[K]["id"].ToString(), results[K]["no"].ToString(), results[K]["itemID"].ToString(), results[K]["qty"].ToString(), results[K]["date"].ToString(), results[K]["price"].ToString(), results[K]["total"].ToString(), results[K]["amount"].ToString(), results[K]["method"].ToString(), results[K]["balance"].ToString(), results[K]["supplierid"].ToString(), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), results[K]["tax"].ToString(), results[K]["storeID"].ToString(), Helper.OrgID, results[K]["userID"].ToString());

                    // Stock _stk = new Stock(results[K]["id"].ToString(), results[K]["itemid"].ToString(), results[K]["qty"].ToString(), results[K]["purchase_price"].ToString(),  results[K]["purchase_price"].ToString(), results[K]["previous_price"].ToString(), "","", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                    DBConnect.Insert(_stk);
                    Form1._Form1.FeedBack("DOWNLOADING SALE " + results[K]["no"].ToString());
                }
            }
            Form1._Form1.FeedBack("DOWNLOADING SALES COMPLETE");
        }
        public static void Bill()
        {
            string query = "";
            query = "SELECT * FROM billing WHERE  orgID = '" + Helper.OrgID + "' ;";
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
               // Form1._Form1.FeedBack("BILLING DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["no"].ToString());

                    string Query = "DELETE from billing WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);
                    Billing _stk = new Billing(results[K]["id"].ToString(), results[K]["no"].ToString(), results[K]["pos"].ToString(),results[K]["paid"].ToString(),results[K]["method"].ToString(),results[K]["reference"].ToString(), results[K]["total"].ToString(),results[K]["balance"].ToString(),results[K]["bank"].ToString(), results[K]["account"].ToString(),results[K]["transactorID"].ToString(), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), results[K]["type"].ToString(),Helper.OrgID,results[K]["userID"].ToString(), results[K]["tax"].ToString(), results[K]["storeID"].ToString());

                    DBConnect.Insert(_stk);
                    Form1._Form1.FeedBack("DOWNLOADING BILL " + results[K]["no"].ToString());
                }
            }
            Form1._Form1.FeedBack("DOWNLOADING BILLS COMPLETE");
        }
        public static void Payment()
        {
            string query = "";
            query = "SELECT * FROM payment WHERE  orgID = '" + Helper.OrgID + "' ;";
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
               // Form1._Form1.FeedBack("Payment DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["no"].ToString());

                    string Query = "DELETE from payment WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);
                    Payment _stk = new Payment(results[K]["id"].ToString(),results[K]["no"].ToString(), results[K]["method"].ToString(), results[K]["amount"].ToString(), results[K]["by"].ToString(),DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), Helper.OrgID, results[K]["userID"].ToString(), results[K]["type"].ToString(), results[K]["storeID"].ToString(), results[K]["customerID"].ToString(), results[K]["balance"].ToString());

                    DBConnect.Insert(_stk);
                    Form1._Form1.FeedBack("DOWNLOADING PAY " + results[K]["no"].ToString());
                }
            }
            Form1._Form1.FeedBack("DOWNLOADING PAYMENT COMPLETE");
        }
        public static void Invoice()
        {
            string query = "";
            query = "SELECT * FROM invoice WHERE  orgID = '" + Helper.OrgID + "' ;";
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
               // Form1._Form1.FeedBack("Payment DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["no"].ToString());

                    string Query = "DELETE from invoice WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);
                    Payment _stk = new Payment(results[K]["id"].ToString(), results[K]["no"].ToString(), results[K]["method"].ToString(), results[K]["amount"].ToString(), results[K]["by"].ToString(), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), Helper.OrgID, results[K]["userID"].ToString(), results[K]["type"].ToString(), results[K]["storeID"].ToString(), results[K]["customerID"].ToString(), results[K]["balance"].ToString());

                    DBConnect.Insert(_stk);
                    Form1._Form1.FeedBack("DOWNLOADING INVOICE " + results[K]["no"].ToString());
                }
            }
            Form1._Form1.FeedBack("DOWNLOADING INVOICE COMPLETE");
        }
        public static void Expenses()
        {
            string query = "";
            query = "SELECT * FROM expense WHERE  orgID = '" + Helper.OrgID + "' ;";
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
               // Form1._Form1.FeedBack("EXPENSE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["no"].ToString());
                    string Query = "DELETE from expense WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);
                    Expense _stk = new Expense(results[K]["id"].ToString(), results[K]["no"].ToString(), results[K]["itemid"].ToString(),results[K]["qty"].ToString(), results[K]["date"].ToString(), results[K]["price"].ToString(), results[K]["type"].ToString(), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), Helper.OrgID,results[K]["userID"].ToString(), results[K]["total"].ToString(), results[K]["storeID"].ToString());

                    DBConnect.Insert(_stk);
                    Form1._Form1.FeedBack("DOWNLOADING Expense " + results[K]["no"].ToString());
                }
            }
            Form1._Form1.FeedBack("DOWNLOADING EXPENSE COMPLETE");
        }
        public static void Customer()
        {
            string query = "";
            query = "SELECT * FROM customer WHERE  orgID = '" + Helper.OrgID + "' ;";
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
               // Form1._Form1.FeedBack("CUSTOMER DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["name"].ToString());
                    string Query = "DELETE from customer WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);
                    Customer _stk = new Customer(results[K]["id"].ToString(), results[K]["name"].ToString(), results[K]["contact"].ToString(), results[K]["image"].ToString(), results[K]["address"].ToString(), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), Helper.OrgID);

                    DBConnect.Insert(_stk);
                    Form1._Form1.FeedBack("DOWNLOADING TRANS " + results[K]["name"].ToString());
                }
            }
            Form1._Form1.FeedBack("DOWNLOADING CUSTOMER COMPLETE");
        }
        public static void Supplier()
        {
            string query = "";
            query = "SELECT * FROM supplier WHERE  orgID = '" + Helper.OrgID + "' ;";
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                JArray results = JArray.Parse(resulted);
                // Form1._Form1.FeedBack("CUSTOMER DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH " + results[K]["name"].ToString());
                    string Query = "DELETE from supplier WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query);
                   Supplier _stk = new Supplier(results[K]["id"].ToString(), results[K]["name"].ToString(), results[K]["contact"].ToString(), results[K]["image"].ToString(), results[K]["address"].ToString(), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), Helper.OrgID);

                    DBConnect.Insert(_stk);
                    Form1._Form1.FeedBack("DOWNLOADING SUPPLIER " + results[K]["name"].ToString());
                }
            }
            Form1._Form1.FeedBack("DOWNLOADING SUPPLIER COMPLETE");
        }

        public static void Role()
        {
            string query = "SELECT * FROM roles WHERE orgID = '" + Helper.OrgID + "'";          
            Roles _role;
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
               // Form1._Form1.FeedBack("DOWNLOADING ROLES" + resulted);
                JArray results = JArray.Parse(resulted);
               
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH ROLE " + results[K]["title"].ToString());
                    string Query2 = "DELETE from roles WHERE id ='" + results[K]["id"].ToString() + "' OR title ='" + results[K]["title"].ToString() + "' ";
                    DBConnect.save(Query2);                  
                    _role = new Roles(results[K]["id"].ToString(), results[K]["title"].ToString(), results[K]["views"].ToString(), results[K]["actions"].ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, results[K]["storeID"].ToString());
                    DBConnect.Insert(_role);
                    Form1._Form1.FeedBack("ROLES " + (results.Count - 1).ToString());
                }
                Form1._Form1.FeedBack("ROLES DOWNLOAD COMPLETE" + Helper.lastSync);
            }
        }
        public static void Categories()
        {
            string query = "SELECT * FROM category ";
           
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
               // Form1._Form1.FeedBack("DOWNLOADING CATEGORIES" + resulted);
                JArray results = JArray.Parse(resulted);
                Form1._Form1.FeedBack("CLIENTS TO BE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH category " + results[K]["name"].ToString());
                    string Query2 = "DELETE from category WHERE name ='" + results[K]["name"].ToString() + "'";
                    DBConnect.save(Query2);
                   Category _cat = new Category(results[K]["id"].ToString(), results[K]["name"].ToString(), results[K]["description"].ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),Helper.OrgID,"", results[K]["image"].ToString());
                    DBConnect.Insert(_cat);
                    Form1._Form1.FeedBack("ROLES " + (results.Count - 1).ToString());
                }
                Form1._Form1.FeedBack("ROLES DOWNLOAD COMPLETE" + Helper.lastSync);
            }
        }

        public static void Stores()
        {
            string query = "SELECT * FROM store WHERE orgID = '" + Helper.OrgID + "'";

            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                Form1._Form1.FeedBack("DOWNLOADING STORES" + resulted);
                JArray results = JArray.Parse(resulted);
               // Form1._Form1.FeedBack("STORES TO BE DOWNLOADED" + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Form1._Form1.FeedBack("DEALING WITH STORE " + results[K]["name"].ToString());
                    string Query2 = "DELETE from store WHERE id ='" + results[K]["id"].ToString() + "'";
                    DBConnect.save(Query2);
                    Store _cat = new Store(results[K]["id"].ToString(), results[K]["name"].ToString(), results[K]["location"].ToString(), results[K]["address"].ToString(), results[K]["contact"].ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, results[K]["code"].ToString(),"false");
                    DBConnect.Insert(_cat);
                    Form1._Form1.FeedBack("Stores " + (results.Count - 1).ToString());
                }
                Form1._Form1.FeedBack("Store DOWNLOAD COMPLETE" + Helper.lastSync);
            }
        }
    }
}
