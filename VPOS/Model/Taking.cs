using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Taking
    {
        private string id;
        private string date;
        private string itemID;
        private string bf;
        private string purchases;
        private string sales;
        private string total_stock;
        private string system_stock;
        private string variance;
        private string purchase_amount;
        private string sale_amount;
        private string profit;
        private string physical_count;
        private string damages;
        private string shrinkable;
        private string orgID;
        private string userID;
        private string created;

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

        public string Bf
        {
            get
            {
                return bf;
            }

            set
            {
                bf = value;
            }
        }

        public string Purchases
        {
            get
            {
                return purchases;
            }

            set
            {
                purchases = value;
            }
        }

        public string Sales
        {
            get
            {
                return sales;
            }

            set
            {
                sales = value;
            }
        }

        public string Total_stock
        {
            get
            {
                return total_stock;
            }

            set
            {
                total_stock = value;
            }
        }

        public string System_stock
        {
            get
            {
                return system_stock;
            }

            set
            {
                system_stock = value;
            }
        }

        public string Variance
        {
            get
            {
                return variance;
            }

            set
            {
                variance = value;
            }
        }

        public string Purchase_amount
        {
            get
            {
                return purchase_amount;
            }

            set
            {
                purchase_amount = value;
            }
        }

        public string Sale_amount
        {
            get
            {
                return sale_amount;
            }

            set
            {
                sale_amount = value;
            }
        }

        public string Profit
        {
            get
            {
                return profit;
            }

            set
            {
                profit = value;
            }
        }

        public string Physical_count
        {
            get
            {
                return physical_count;
            }

            set
            {
                physical_count = value;
            }
        }

        public string Damages
        {
            get
            {
                return damages;
            }

            set
            {
                damages = value;
            }
        }

        public string Shrinkable
        {
            get
            {
                return shrinkable;
            }

            set
            {
                shrinkable = value;
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

        public Taking(string id, string date, string itemID, string bf, string purchases, string sales, string total_stock, string system_stock, string variance, string purchase_amount, string sale_amount, string profit, string physical_count, string damages, string shrinkable, string orgID, string userID, string created)
        {
            this.Id = id;
            this.Date = date;
            this.ItemID = itemID;
            this.Bf = bf;
            this.Purchases = purchases;
            this.Sales = sales;
            this.Total_stock = total_stock;
            this.System_stock = system_stock;
            this.Variance = variance;
            this.Purchase_amount = purchase_amount;
            this.Sale_amount = sale_amount;
            this.Profit = profit;
            this.Physical_count = physical_count;
            this.Damages = damages;
            this.Shrinkable = shrinkable;
            this.OrgID = orgID;
            this.UserID = userID;
            this.Created = created;
        }

        public static List<Taking> ListTaking()
        {
            DBConnect.OpenConn();
            List<Taking> categories = new List<Taking>();
            string SQL = "SELECT * FROM taking";
            NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
            NpgsqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                Taking p = new Taking(Reader["id"].ToString(), Reader["date"].ToString(), Reader["itemID"].ToString(), Reader["bf"].ToString(), Reader["purchases"].ToString(), Reader["sales"].ToString(), Reader["total_stock"].ToString(), Reader["system_stock"].ToString(), Reader["variance"].ToString(), Reader["purchase_amount"].ToString(), Reader["sale_amount"].ToString(), Reader["profit"].ToString(), Reader["physical_count"].ToString(), Reader["damages"].ToString(), Reader["shrinkable"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString(), Reader["created"].ToString());
                categories.Add(p);
            }
            DBConnect.CloseConn();
            return categories;

        }
    }



}
