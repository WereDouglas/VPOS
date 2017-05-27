﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Stock
    {
        private string id;
        private string itemID;
        private string qty;
        private string sale_price;
        private string purchase_price;
        private string previous_price;
        private string total_value;
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

        public string Sale_price
        {
            get
            {
                return sale_price;
            }

            set
            {
                sale_price = value;
            }
        }

        public string Purchase_price
        {
            get
            {
                return purchase_price;
            }

            set
            {
                purchase_price = value;
            }
        }

        public string Previous_price
        {
            get
            {
                return previous_price;
            }

            set
            {
                previous_price = value;
            }
        }

        public string Total_value
        {
            get
            {
                return total_value;
            }

            set
            {
                total_value = value;
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

        public Stock(string id, string itemID, string qty, string sale_price, string purchase_price, string previous_price, string total_value, string created, string orgID, string userID)
        {
            this.Id = id;
            this.ItemID = itemID;
            this.Qty = qty;
            this.Sale_price = sale_price;
            this.Purchase_price = purchase_price;
            this.Previous_price = previous_price;
            this.Total_value = total_value;
            this.Created = created;
            this.OrgID = orgID;
            this.UserID = userID;
        }

        public static List<Stock> ListStock()
        {
            DBConnect.OpenConn();
            List<Stock> categories = new List<Stock>();
            string SQL = "SELECT * FROM stock";
            NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
            NpgsqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                Stock p = new Stock(Reader["id"].ToString(), Reader["itemID"].ToString(), Reader["qty"].ToString(), Reader["sale_price"].ToString(), Reader["purchase_price"].ToString(), Reader["previous_price"].ToString(), Reader["total_value"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString());
                categories.Add(p);
            }
            DBConnect.CloseConn();
            return categories;

        }

    }
   
}
