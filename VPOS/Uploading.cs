using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPOS.Model;

namespace VPOS
{
    public class Uploading
    {

        public Uploading()
        {

        }
        public static bool CheckServer()
        {
            string query = "SELECT * FROM organisation";
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
        public static void updateSyncTime()
        {
            try
            {

                string Query3 = "UPDATE organisation SET sync='" + DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                DBConnect.save(Query3);
                Helper.lastSync = DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
                string Query3 = "UPDATE organisation SET sync='" + DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                DBConnect.save(Query3);
                Helper.lastSync = DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
       static Users _user;
        public static void UploadUsers()
        {
            foreach (var h in Global._users.Where(e=>Convert.ToDateTime(e.Created) > Convert.ToDateTime( Helper.lastSync))) { 
                string Query2 = "DELETE from users WHERE id ='" + h.Id + "'";
              
                MainForm._Form1.FeedBack("Uploading users " + h.Surname + " " + h.Contact);
                   _user = new Users(h.Id, h.IdNo, h.Contact,h.Contact2,h.Surname,h.Lastname,h.Othername,h.Email,h.Nationality,h.Address,h.Passwords,h.Gender,h.OrgID,h.Roles,h.InitialPassword,h.Account,h.Status,h.Image, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"));
                string Query = DBConnect.GenerateQuery(_user);
               // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " +  h.Surname + " " + h.Contact);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.Surname + " " + h.Contact);
                }
            }
            MainForm._Form1.FeedBack("Uploading users complete ");
        }
       static Billing _bill;
        public static void UploadBilling()
        {
            foreach (var h in Global._billings.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from billing WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Billing " + h.No + " " + h.Method);
                _bill = new Billing(h.Id, h.No,h.Pos, h.Paid,h.Method, h.Reference, h.Total, h.Balance, h.Bank, h.Account, h.TransactorID, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),h.Type,h.OrgID,h.UserID,h.Tax);
                string Query = DBConnect.GenerateQuery(_bill);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.No + " " + h.Method);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.No + " " + h.Method);
                }
            }
            MainForm._Form1.FeedBack("Uploading users complete ");
        }
        static Sale _sale;
        public static void UploadSale()
        {
            foreach (var h in Global._sale.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from sale WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Sale " + h.No + " " + h.Type);
                _sale = new Sale(h.Id, h.No,h.ItemID, h.Qty, h.Date, h.Price, h.Type, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.OrgID, h.UserID,h.Total);
                string Query = DBConnect.GenerateQuery(_sale);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.No + " "+ h.Type);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.No + " "+ h.Type);
                }
            }
            MainForm._Form1.FeedBack("Uploading users complete ");
        }
        static Item _item;
        public static void UploadItem()
        {
            foreach (var h in Global._item.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from item WHERE id ='" + h.Id + "'";
                MainForm._Form1.FeedBack("Uploading item " + h.Name + " " + h.Description);
                _item = new Item(h.Id, h.Name, h.Code, h.Description, h.Manufacturer, h.Country, h.Batch, h.Purchase_price,h.Sale_price,h.Composition,h.Expire,h.Category,h.Formulation,h.Barcode,h.Image,DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),h.Department,h.Date_manufactured,h.Generic,h.Strength,h.Quantity,h.Min_qty, h.OrgID,h.Counts,h.Taking,h.Valid);
                string Query = DBConnect.GenerateQuery(_item);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.Name + " " + h.Description);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.Name + " " + h.Description);
                }
            }
            MainForm._Form1.FeedBack("Uploading Items complete ");
        }
        static Organisation _org;
        public static void UploaOrganisation()
        {
            foreach (var h in Global._org.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from organisation WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Organisation " + h.Name + " " + h.Country);
                _org = new Organisation(h.Id,h.Name,h.Code,h.Registration,h.Contact, h.Address, h.Tin, h.Vat, h.Email, h.Country, h.Initialpassword, h.Account, h.Status, h.Expires, h.Image,DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),h.Sync,h.Counts,h.Company);
                string Query = DBConnect.GenerateQuery(_org);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Organisation Successful " + h.Name + " " + h.Country);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.Name + " " + h.Country);
                }
            }
            MainForm._Form1.FeedBack("Uploading Organisation complete ");
        }
        static Roles _role;
        public static void UploadRoles()
        {
            foreach (var h in Global._roles.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from roles WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Sale " + h.Title + " ");
                _role = new Roles(h.Id, h.Title, h.Views, h.Actions, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),h.OrgID);
                string Query = DBConnect.GenerateQuery(_role);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.Title);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.Title);
                }
            }
            MainForm._Form1.FeedBack("Uploading roles complete ");
        }
        static Transactor _trans;
        public static void UploadTransactor()
        {
            foreach (var h in Global._transactor.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from transactor WHERE id ='" + h.Id + "'";
                MainForm._Form1.FeedBack("Uploading transactor " + h.Name + " ");
                _trans = new Transactor(h.Id, h.Name, h.Contact, h.Image, h.Type,DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),h.Address, h.OrgID);
                string Query = DBConnect.GenerateQuery(_trans);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.Name);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.Name);
                }
            }
            MainForm._Form1.FeedBack("Uploading Transactors complete ");
        }
        static Category _cat;
        public static void UploadCategory()
        {
            foreach (var h in Global._category.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from category WHERE id ='" + h.Id + "'";
                MainForm._Form1.FeedBack("Uploading transactor " + h.Name + " ");
                _cat = new Category(h.Id,h.Name,h.Description, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),h.OrgID);
                string Query = DBConnect.GenerateQuery(_cat);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Category Successful " + h.Name);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.Name);
                }
            }
            MainForm._Form1.FeedBack("Uploading category complete ");
        }
        static Expense _exp;
        public static void UploadExpense()
        {
            foreach (var h in Global._expense.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from expense WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Expense " + h.No + " " + h.Type);
                _exp = new Expense(h.Id, h.No, h.ItemID, h.Qty, h.Date, h.Price, h.Type, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.OrgID, h.UserID, h.Total);
                string Query = DBConnect.GenerateQuery(_exp);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.No + " " + h.Type);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.No + " " + h.Type);
                }
            }
            MainForm._Form1.FeedBack("Uploading Expense complete ");
        }
    }
   

}
