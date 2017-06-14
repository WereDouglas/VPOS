using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
        private string batch;      
        private string expire;      
        private string packing;//packaging
        private string units;//packaging
        private string barcode;      
        private string date_manufactured;//for age     
        private string quantity;
        private string min_qty;       
        private string counts;
        private string taking;
        private string tax;      
        private string promo_price;
        private string promo_start;
        private string promo_end;
        private string created;
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

        public string Batch
        {
            get
            {
                return batch;
            }

            set
            {
                batch = value;
            }
        }

        public string Expire
        {
            get
            {
                return expire;
            }

            set
            {
                expire = value;
            }
        }

        public string Packing
        {
            get
            {
                return packing;
            }

            set
            {
                packing = value;
            }
        }

        public string Units
        {
            get
            {
                return units;
            }

            set
            {
                units = value;
            }
        }

        public string Barcode
        {
            get
            {
                return barcode;
            }

            set
            {
                barcode = value;
            }
        }

        public string Date_manufactured
        {
            get
            {
                return date_manufactured;
            }

            set
            {
                date_manufactured = value;
            }
        }

        public string Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

        public string Min_qty
        {
            get
            {
                return min_qty;
            }

            set
            {
                min_qty = value;
            }
        }

        public string Counts
        {
            get
            {
                return counts;
            }

            set
            {
                counts = value;
            }
        }

        public string Taking
        {
            get
            {
                return taking;
            }

            set
            {
                taking = value;
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

        public string Promo_price
        {
            get
            {
                return promo_price;
            }

            set
            {
                promo_price = value;
            }
        }

        public string Promo_start
        {
            get
            {
                return promo_start;
            }

            set
            {
                promo_start = value;
            }
        }

        public string Promo_end
        {
            get
            {
                return promo_end;
            }

            set
            {
                promo_end = value;
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
        public Stock() { }
        public Stock(string id, string itemID, string qty, string sale_price, string purchase_price, string previous_price, string total_value, string batch, string expire, string packing, string units, string barcode, string date_manufactured, string quantity, string min_qty, string counts, string taking, string tax, string promo_price, string promo_start, string promo_end, string created, string storeID, string orgID, string userID)
        {
            this.Id = id;
            this.ItemID = itemID;
            this.Qty = qty;
            this.Sale_price = sale_price;
            this.Purchase_price = purchase_price;
            this.Previous_price = previous_price;
            this.Total_value = total_value;
            this.Batch = batch;
            this.Expire = expire;
            this.Packing = packing;
            this.Units = units;
            this.Barcode = barcode;
            this.Date_manufactured = date_manufactured;
            this.Quantity = quantity;
            this.Min_qty = min_qty;
            this.Counts = counts;
            this.Taking = taking;
            this.Tax = tax;
            this.Promo_price = promo_price;
            this.Promo_start = promo_start;
            this.Promo_end = promo_end;
            this.Created = created;
            this.StoreID = storeID;
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
                Stock p = new Stock(Reader["id"].ToString(), Reader["itemID"].ToString(), Reader["qty"].ToString(), Reader["sale_price"].ToString(), Reader["purchase_price"].ToString(), Reader["previous_price"].ToString(), Reader["total_value"].ToString(), Reader["batch"].ToString(), Reader["expire"].ToString(), Reader["packaging"].ToString(), Reader["units"].ToString(), Reader["barcode"].ToString(),Reader["date_manufactured"].ToString(), Reader["quantity"].ToString(), Reader["min_qty"].ToString(), Reader["counts"].ToString(), Reader["taking"].ToString(),Reader["tax"].ToString(), Reader["storeid"].ToString(), Reader["promo_price"].ToString(), Reader["promo_start"].ToString(), Reader["promo_end"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString());
                categories.Add(p);
            }
            DBConnect.CloseConn();
            return categories;

        }
        public static List<Stock> ListStockLite()
        {
            DBConnect.OpenConn();
            List<Stock> categories = new List<Stock>();
            string SQL = "SELECT * FROM stock";
            Reader = DBConnect.ReadingLite(SQL);
            while (Reader.Read())
            {
                Stock p = new Stock(Reader["id"].ToString(), Reader["itemID"].ToString(), Reader["qty"].ToString(), Reader["sale_price"].ToString(), Reader["purchase_price"].ToString(), Reader["previous_price"].ToString(), Reader["total_value"].ToString(), Reader["batch"].ToString(), Reader["expire"].ToString(), Reader["packaging"].ToString(), Reader["units"].ToString(), Reader["barcode"].ToString(), Reader["date_manufactured"].ToString(), Reader["quantity"].ToString(), Reader["min_qty"].ToString(), Reader["counts"].ToString(), Reader["taking"].ToString(), Reader["tax"].ToString(), Reader["storeid"].ToString(), Reader["promo_price"].ToString(), Reader["promo_start"].ToString(), Reader["promo_end"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString());
                categories.Add(p);
            }
            Reader.Close();
            return categories;

        }


    }

}
