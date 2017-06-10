using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Item
    {
        private string id;
        private string name;
        private string code;
        private string description;
        private string manufacturer;
        private string country;
        private string batch;
        private string purchase_price;
        private string sale_price;
        private string composition;
        private string expire;
        private string category;
        private string formulation;
        private string barcode;
        private string image;
        private string created;
        private string department;
        private string date_manufactured;//for age
        private string generic;
        private string strength;
        private string quantity;
        private string min_qty;
        private string orgID;
        private string counts;
        private string taking;
        private string valid;
        private string tax;
        private string storeID;
        private string promo_price;
        private string promo_start;
        private string promo_end;

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

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }

            set
            {
                manufacturer = value;
            }
        }

        public string Country
        {
            get
            {
                return country;
            }

            set
            {
                country = value;
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

        public string Composition
        {
            get
            {
                return composition;
            }

            set
            {
                composition = value;
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

        public string Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
            }
        }

        public string Formulation
        {
            get
            {
                return formulation;
            }

            set
            {
                formulation = value;
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

        public string Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
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

        public string Department
        {
            get
            {
                return department;
            }

            set
            {
                department = value;
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

        public string Generic
        {
            get
            {
                return generic;
            }

            set
            {
                generic = value;
            }
        }

        public string Strength
        {
            get
            {
                return strength;
            }

            set
            {
                strength = value;
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

        public string Valid
        {
            get
            {
                return valid;
            }

            set
            {
                valid = value;
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

        public Item(string id, string name, string code, string description, string manufacturer, string country, string batch, string purchase_price, string sale_price, string composition, string expire, string category, string formulation, string barcode, string image, string created, string department, string date_manufactured, string generic, string strength,string quantity,string min_qty,string orgID,string counts,string taking,string valid,string tax,string storeID,string promo_price,string promo_start,string promo_end)
        {
            this.Id = id;
            this.Name = name;
            this.Code = code;
            this.Description = description;
            this.Manufacturer = manufacturer;
            this.Country = country;
            this.Batch = batch;
            this.Purchase_price = purchase_price;
            this.Sale_price = sale_price;
            this.Composition = composition;
            this.Expire = expire;
            this.Category = category;
            this.Formulation = formulation;
            this.Barcode = barcode;
            this.Image = image;
            this.Created = created;
            this.Department = department;
            this.Date_manufactured = date_manufactured;
            this.Generic = generic;
            this.Strength = strength;
            this.Quantity = quantity;
            this.Min_qty = min_qty;
            this.OrgID = orgID;
            this.Counts = counts;
            this.Taking = taking;
            this.Valid = valid;
            this.Tax = tax;
            this.StoreID = storeID;
            this.Promo_price = promo_price;
            this.Promo_start = promo_start;
            this.Promo_end = promo_end;
        }

        public static List<Item> ListItem()
        {
            DBConnect.OpenConn();
            List<Item> wards = new List<Item>();
            string SQL = "SELECT * FROM item";
            NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
            NpgsqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                Item p = new Item(Reader["id"].ToString(), Reader["name"].ToString(), Reader["code"].ToString(), Reader["description"].ToString(), Reader["manufacturer"].ToString(), Reader["country"].ToString(), Reader["batch"].ToString(), Reader["purchase_price"].ToString(), Reader["sale_price"].ToString(), Reader["composition"].ToString(), Reader["expire"].ToString(), Reader["category"].ToString(), Reader["formulation"].ToString(), Reader["barcode"].ToString(), Reader["image"].ToString(), Reader["created"].ToString(), Reader["department"].ToString(), Reader["date_manufactured"].ToString(), Reader["generic"].ToString(), Reader["strength"].ToString(), Reader["quantity"].ToString(), Reader["min_qty"].ToString(), Reader["orgID"].ToString(), Reader["counts"].ToString(), Reader["taking"].ToString(), Reader["valid"].ToString(),Reader["tax"].ToString(), Reader["storeid"].ToString(), Reader["promo_price"].ToString(), Reader["promo_start"].ToString(), Reader["promo_end"].ToString());
                wards.Add(p);
            }
            DBConnect.CloseConn();
            return wards;

        }
    }
}
