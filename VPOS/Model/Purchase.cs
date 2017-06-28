using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Purchase
    {
        private string id;
        private string no;
        private string itemID;
        private string qty;
        private string date;
        private string price;
        private string total;
        private string amount;
        private string method;
        private string balance;
        private string supplierID;
        private string created;
        private string tax;
        private string storeID;
        private string orgID;
        private string userID;
        static SQLiteDataReader Reader;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string No
        {
            get
            {
                return no;
            }

            set
            {
                no = value;
            }
        }

        public string ItemID
        {
            get
            {
                return itemID;
            }

            set
            {
                itemID = value;
            }
        }

        public string Qty
        {
            get
            {
                return qty;
            }

            set
            {
                qty = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public string Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public string Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
            }
        }

        public string Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
            }
        }

        public string Method
        {
            get
            {
                return method;
            }

            set
            {
                method = value;
            }
        }

        public string Balance
        {
            get
            {
                return balance;
            }

            set
            {
                balance = value;
            }
        }

        public string SupplierID
        {
            get
            {
                return supplierID;
            }

            set
            {
                supplierID = value;
            }
        }

        public string Created
        {
            get
            {
                return created;
            }

            set
            {
                created = value;
            }
        }

        public string Tax
        {
            get
            {
                return tax;
            }

            set
            {
                tax = value;
            }
        }

        public string StoreID
        {
            get
            {
                return storeID;
            }

            set
            {
                storeID = value;
            }
        }

        public string OrgID
        {
            get
            {
                return orgID;
            }

            set
            {
                orgID = value;
            }
        }

        public string UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public Purchase() { }

        public Purchase(string id, string no, string itemID, string qty, string date, string price, string total, string amount, string method, string balance, string supplierID, string created, string tax, string storeID, string orgID, string userID)
        {
            this.Id = id;
            this.No = no;
            this.ItemID = itemID;
            this.Qty = qty;
            this.Date = date;
            this.Price = price;
            this.Total = total;
            this.Amount = amount;
            this.Method = method;
            this.Balance = balance;
            this.SupplierID = supplierID;
            this.Created = created;
            this.Tax = tax;
            this.StoreID = storeID;
            this.OrgID = orgID;
            this.UserID = userID;
        }

        public static List<Purchase> ListPurchase()
        {
            List<Purchase> purs = new List<Purchase>();
            string SQL = "SELECT * FROM purchase";
            if (Helper.Type!="Lite") {
                DBConnect.OpenConn();
               
                NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
                NpgsqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                   Purchase p = new Purchase(Reader["id"].ToString(), Reader["no"].ToString(), Reader["itemID"].ToString(), Reader["qty"].ToString(), Reader["date"].ToString(), Reader["price"].ToString(), Reader["total"].ToString(), Reader["amount"].ToString(), Reader["method"].ToString(), Reader["balance"].ToString(), Reader["customerID"].ToString(), Reader["created"].ToString(), Reader["tax"].ToString(), Reader["storeid"].ToString(), Reader["orgid"].ToString(), Reader["userid"].ToString());
                    purs.Add(p);
                }
                DBConnect.CloseConn();
             
            }
            else {
                
                Reader = DBConnect.ReadingLite(SQL);
                while (Reader.Read())
                {
                    Purchase p = new Purchase(Reader["id"].ToString(), Reader["no"].ToString(), Reader["itemID"].ToString(), Reader["qty"].ToString(), Reader["date"].ToString(), Reader["price"].ToString(), Reader["total"].ToString(), Reader["amount"].ToString(), Reader["method"].ToString(), Reader["balance"].ToString(), Reader["customerID"].ToString(), Reader["created"].ToString(), Reader["tax"].ToString(), Reader["storeid"].ToString(), Reader["orgid"].ToString(), Reader["userid"].ToString());
                    purs.Add(p);
                }
                Reader.Close();
            }
            return purs;

        }
      
    }



}
