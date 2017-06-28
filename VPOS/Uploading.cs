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

                string Query3 = "UPDATE organisation SET sync='" + DateTime.Now.AddMinutes(-10).ToString("dd-MM-yyyy HH:mm:ss") + "'";
                DBConnect.save(Query3);
                Helper.lastSync = DateTime.Now.AddMinutes(-10).ToString("dd-MM-yyyy HH:mm:ss");
            }
            catch
            {
                string Query3 = "UPDATE organisation SET sync='" + DateTime.Now.AddMinutes(-10).ToString("dd-MM-yyyy HH:mm:ss") + "'";
                DBConnect.save(Query3);
                Helper.lastSync = DateTime.Now.AddMinutes(-10).ToString("dd-MM-yyyy HH:mm:ss");
            }
        }
        static Users _user;
        public static void Users()
        {
            foreach (var h in Global.users.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from users WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading users " + h.Surname + " " + h.Contact);
                _user = new Users(h.Id, h.IdNo, h.Contact, h.Contact2, h.Surname, h.Lastname, h.Othername, h.Email, h.Nationality, h.Address, h.Passwords, h.Gender, h.OrgID, h.Roles, h.InitialPassword, h.Account, h.Status, h.Image, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.StoreID);
                string Query = DBConnect.GenerateQuery(_user);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.Surname + " " + h.Contact);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.Surname + " " + h.Contact);
                }
            }
            MainForm._Form1.FeedBack("Uploading users complete ");
        }
        public static void Sale()
        {
            foreach (var h in Global.sale.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from sale WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Sale " + h.No);
                Sale _sale = new Sale(h.Id, h.No, h.ItemID, h.Qty, h.Date, h.Price, h.Total, h.Amount, h.Method, h.Balance, h.CustomerID, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.Tax, h.StoreID, h.OrgID, h.UserID);
                string Query = DBConnect.GenerateQuery(_sale);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.No);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.No);
                }
            }
            MainForm._Form1.FeedBack("Uploading sales complete ");
        }
        public static void Purchase()
        {
            foreach (var h in Global.purchase.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from purchase WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Purchase " + h.No);
                Purchase _purchase = new Purchase(h.Id, h.No, h.ItemID, h.Qty, h.Date, h.Price, h.Total, h.Amount, h.Method, h.Balance, h.SupplierID, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.Tax, h.StoreID, h.OrgID, h.UserID);
                string Query = DBConnect.GenerateQuery(_purchase);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.No);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.No);
                }
            }
            MainForm._Form1.FeedBack("Uploading purchases complete ");
        }
        static Store _store;
        public static void Store()
        {
            foreach (var h in Global.store.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from store WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Stores " + h.Name + " " + h.Location);
                _store = new Store(h.Id, h.Name, h.Location, h.Address, h.Contact, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.OrgID, h.Code, h.Current);
                string Query = DBConnect.GenerateQuery(_store);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.Name + " " + h.Location);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.Name + " " + h.Location);
                }
            }
            MainForm._Form1.FeedBack("Uploading stores complete ");
        }

        public static void Taking()
        {
            foreach (var h in Global.taking.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from taking WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Stock records " + h.Created + " ");
                Taking _take = new Taking(h.Id, h.Date, h.ItemID, h.Bf, h.Purchases, h.Sales, h.Total_stock, h.System_stock, h.Variance, h.Purchase_amount, h.Sale_amount, h.Profit, h.Physical_count, h.Damages, h.Shrinkable, Helper.OrgID, h.UserID, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.StoreID);
                string Query = DBConnect.GenerateQuery(_take);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.Created + " ");
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.Created + " ");
                }
            }
            MainForm._Form1.FeedBack("Uploading stock records complete ");
        }

        public static void Payment()
        {
            foreach (var h in Global.payment.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from payment WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading payment " + h.No + " " + h.Type);
                Payment _pay = new Payment(h.Id, h.No, h.Method, h.Amount, h.By, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, h.UserID, h.Type, Helper.StoreID, h.CustomerID, h.Balance);
                string Query = DBConnect.GenerateQuery(_pay);
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
            MainForm._Form1.FeedBack("Uploading payments complete ");
        }
        public static void Invoice()
        {
            foreach (var h in Global.invoices.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from invoice WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading invoice " + h.No + " " + h.Type);
                Invoice _pay = new Invoice(h.Id, h.No, h.Method, h.Amount, h.By, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, h.UserID, h.Type, Helper.StoreID, h.CustomerID, h.Balance);
                string Query = DBConnect.GenerateQuery(_pay);
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
            MainForm._Form1.FeedBack("Uploading invoices complete ");
        }

        public static void Item()
        {
            foreach (var h in Global.item.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from item WHERE id ='" + h.Id + "'";
                MainForm._Form1.FeedBack("Uploading item " + h.Name + " " + h.Description);
                Item _item = new Item(h.Id, h.Name, h.Generic, h.Code, h.Description, h.Manufacturer, h.Country, h.Composition, h.Category, h.Barcode, h.Image, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.Strength, Helper.OrgID, h.Valid,h.Sub);
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
        static Stock _stock;
        public static void Stock()
        {
            foreach (var h in Global.stock.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from stock WHERE id ='" + h.Id + "'";
                MainForm._Form1.FeedBack("Uploading Stock " + h.Barcode);
                _stock = new Stock(h.Id, h.ItemID, h.Qty, h.Sale_price, h.Purchase_price, h.Previous_price, h.Total_value, h.Batch, h.Expire, h.Packing, h.Units, h.Barcode, h.Date_manufactured, h.Qty, h.Min_qty, h.Counts, h.Taking, h.Tax, h.Promo_price, h.Promo_start, h.Promo_end, h.Created, Helper.StoreID, h.OrgID, h.UserID);
                string Query = DBConnect.GenerateQuery(_stock);
                // MainForm._Form1.FeedBack("QUERY " + Query);
                string URL = Helper.genUrl + "api/uploading";
                NameValueCollection formData = new NameValueCollection();
                formData["save"] = Query;
                formData["delete"] = Query2;
                string results = Helper.send(URL, formData);
                if (results == "true")
                {
                    MainForm._Form1.FeedBack("Uploading Successful " + h.Barcode);
                }
                else
                {
                    MainForm._Form1.FeedBack("Uploading failed " + h.Barcode);
                }
            }
            MainForm._Form1.FeedBack("Uploading Stock complete ");
        }

        public static void Organisation()
        {
            foreach (var h in Global.org.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from organisation WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Organisation " + h.Name + " " + h.Country);
                Organisation _org = new Organisation(h.Id, h.Name, h.Code, h.Registration, h.Contact, h.Address, h.Tin, h.Vat, h.Email, h.Country, h.Initialpassword, h.Account, h.Status, h.Expires, h.Image, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.Sync, h.Counts, h.Company, h.StoreID,h.Category);
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

        public static void Roles()
        {
            foreach (var h in Global.roles.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from roles WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Sale " + h.Title + " ");
                Roles _role = new Roles(h.Id, h.Title, h.Views, h.Actions, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.StoreID);
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

        public static void Customer()
        {
            foreach (var h in Global.customer.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from customer WHERE id ='" + h.Id + "'";
                MainForm._Form1.FeedBack("Uploading customer " + h.Name + " ");
                Customer _trans = new Customer(h.Id, h.Name, h.Contact, h.Image, h.Address, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.OrgID);
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
            MainForm._Form1.FeedBack("Uploading Customer complete ");
        }
        public static void Supplier()
        {
            foreach (var h in Global.supplier.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from supplier WHERE id ='" + h.Id + "'";
                MainForm._Form1.FeedBack("Uploading supplier " + h.Name + " ");
                Supplier _trans = new Supplier(h.Id, h.Name, h.Contact, h.Image, h.Address, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.OrgID);
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
            MainForm._Form1.FeedBack("Uploading Customer complete ");
        }
        static Category _cat;
        public static void Category()
        {
            foreach (var h in Global.category.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from category WHERE id ='" + h.Id + "'";
                MainForm._Form1.FeedBack("Uploading transactor " + h.Name + " ");
                _cat = new Category(h.Id, h.Name, h.Description, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.OrgID, Helper.StoreID, h.Image);
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
        public static void Expense()
        {
            foreach (var h in Global.expense.Where(e => Convert.ToDateTime(e.Created) > Convert.ToDateTime(Helper.lastSync)))
            {
                string Query2 = "DELETE from expense WHERE id ='" + h.Id + "'";

                MainForm._Form1.FeedBack("Uploading Expense " + h.No + " " + h.Type);
                _exp = new Expense(h.Id, h.No, h.ItemID, h.Qty, h.Date, h.Price, h.Type, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), h.OrgID, h.UserID, h.Total, Helper.StoreID);
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
