using Npgsql;
using System;
using System.Collections.Generic;
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
        private string type;      
        private string created;
        private string orgID;
        private string userID;

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

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
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

        public Purchase(string id, string no, string itemID, string qty, string date, string price, string type, string created, string orgID, string userID)
        {
            this.Id = id;
            this.No = no;
            this.ItemID = itemID;
            this.Qty = qty;
            this.Date = date;
            this.Price = price;
            this.Type = type;
            this.Created = created;
            this.OrgID = orgID;
            this.UserID = userID;
        }

        public static List<Purchase> ListPurchase()
        {
            DBConnect.OpenConn();
            List<Purchase> categories = new List<Purchase>();
            string SQL = "SELECT * FROM purchase";
            NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
            NpgsqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                Purchase p = new Purchase(Reader["id"].ToString(), Reader["no"].ToString(), Reader["itemID"].ToString(), Reader["qty"].ToString(), Reader["date"].ToString(), Reader["price"].ToString(), Reader["type"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString());
                categories.Add(p);
            }
            DBConnect.CloseConn();
            return categories;

        }
    }



}
