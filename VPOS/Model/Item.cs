using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Item
    {
        private string id;
        private string name;
        private string generic;
        private string code;
        private string description;
        private string manufacturer;
        private string country;       
        private string composition;       
        private string category;       
        private string barcode;
        private string image;
        private string created;      
        private string strength;       
        private string orgID;      
        private string valid;
        private string sub;

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

        public string Sub
        {
            get
            {
                return sub;
            }

            set
            {
                sub = value;
            }
        }

        public Item() { }
        public Item(string id, string name, string generic, string code, string description, string manufacturer, string country, string composition, string category, string barcode, string image, string created, string strength, string orgID, string valid,string sub)
        {
            this.Id = id;
            this.Name = name;
            this.Generic = generic;
            this.Code = code;
            this.Description = description;
            this.Manufacturer = manufacturer;
            this.Country = country;
            this.Composition = composition;
            this.Category = category;
            this.Barcode = barcode;
            this.Image = image;
            this.Created = created;
            this.Strength = strength;
            this.OrgID = orgID;
            this.Valid = valid;
            this.Sub = sub;
        }

        public static List<Item> ListItem()
        {
            if (Helper.Type !="Lite") {
                DBConnect.OpenConn();
                List<Item> wards = new List<Item>();
                string SQL = "SELECT * FROM item";
                NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
                NpgsqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Item p = new Item(Reader["id"].ToString(), Reader["name"].ToString(), Reader["generic"].ToString(), Reader["code"].ToString(), Reader["description"].ToString(), Reader["manufacturer"].ToString(), Reader["country"].ToString(), Reader["composition"].ToString(), Reader["category"].ToString(), Reader["barcode"].ToString(), Reader["image"].ToString(), Reader["created"].ToString(), Reader["strength"].ToString(), Reader["orgID"].ToString(), Reader["valid"].ToString(), Reader["sub"].ToString());
                    wards.Add(p);
                }
                DBConnect.CloseConn();
                return wards;
            }
            else {
                List<Item> wards = new List<Item>();
                string SQL = "SELECT * FROM item";
                Reader = DBConnect.ReadingLite(SQL);
                while (Reader.Read())
                {
                    Item p = new Item(Reader["id"].ToString(), Reader["name"].ToString(), Reader["generic"].ToString(), Reader["code"].ToString(), Reader["description"].ToString(), Reader["manufacturer"].ToString(), Reader["country"].ToString(), Reader["composition"].ToString(), Reader["category"].ToString(), Reader["barcode"].ToString(), Reader["image"].ToString(), Reader["created"].ToString(), Reader["strength"].ToString(), Reader["orgID"].ToString(), Reader["valid"].ToString(), Reader["sub"].ToString());
                    wards.Add(p);
                }
                Reader.Close();
                return wards;

            }

        }
       
    }
}
