using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Quantity
    {
        private string id;
        private string itemID;
        private string sale_qty;      
        private string purchase_qty;     
        private string created;
        private string orgID;
        private string userID;
        private string date;
        private string storeID;
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

        public string Sale_qty
        {
            get
            {
                return sale_qty;
            }

            set
            {
                sale_qty = value;
            }
        }

        public string Purchase_qty
        {
            get
            {
                return purchase_qty;
            }

            set
            {
                purchase_qty = value;
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
        public Quantity() { }
        public Quantity(string id, string itemID, string sale_qty, string purchase_qty, string created, string orgID, string userID,string date, string storeID)
        {
            this.Id = id;
            this.ItemID = itemID;
            this.Sale_qty = sale_qty;
            this.Purchase_qty = purchase_qty;
            this.Created = created;
            this.OrgID = orgID;
            this.UserID = userID;
            this.Date = date;
            this.StoreID = storeID;
        }

        public static List<Quantity> ListQuantity()
        {
            DBConnect.OpenConn();
            List<Quantity> categories = new List<Quantity>();
            string SQL = "SELECT * FROM quantity";
            NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
            NpgsqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                Quantity p = new Quantity(Reader["id"].ToString(), Reader["itemID"].ToString(), Reader["sale_qty"].ToString(), Reader["purchase_qty"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString(),Reader["date"].ToString(), Reader["storeid"].ToString());
                categories.Add(p);
            }
            DBConnect.CloseConn();
            return categories;

        }
        public static List<Quantity> ListQuantityLite()
        {
            DBConnect.OpenConn();
            List<Quantity> categories = new List<Quantity>();
            string SQL = "SELECT * FROM quantity";
            Reader = DBConnect.ReadingLite(SQL);
            while (Reader.Read())
            {
                Quantity p = new Quantity(Reader["id"].ToString(), Reader["itemID"].ToString(), Reader["sale_qty"].ToString(), Reader["purchase_qty"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString(), Reader["date"].ToString(), Reader["storeid"].ToString());
                categories.Add(p);
            }
            Reader.Close();
            return categories;

        }

    }
   
}
