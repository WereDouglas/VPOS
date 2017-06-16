using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Category
    {
        private string id;
        private string name;
        private string description;
        private string created;
        private string orgID;
        private string storeID;
        private string image;


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

        public Category() { }
        public Category(string id, string name, string description, string created, string orgID, string storeID, string image)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Created = created;
            this.OrgID = orgID;
            this.StoreID = storeID;
            this.Image = image;
        }
        static SQLiteDataReader Reader;
        public static List<Category> ListCategory()
        {
            if (Helper.Type != "Lite")
            {
                DBConnect.OpenConn();
                List<Category> categories = new List<Category>();
                string SQL = "SELECT * FROM category";
                NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
                NpgsqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Category p = new Category(Reader["id"].ToString(), Reader["name"].ToString(), Reader["description"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["storeID"].ToString(), Reader["image"].ToString());
                    categories.Add(p);
                }
                DBConnect.CloseConn();
                return categories;
            }
            else
            {
                DBConnect.OpenConn();
                List<Category> categories = new List<Category>();
                string SQL = "SELECT * FROM category";
                Reader = DBConnect.ReadingLite(SQL);
                while (Reader.Read())
                {
                    Category p = new Category(Reader["id"].ToString(), Reader["name"].ToString(), Reader["description"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["storeID"].ToString(), Reader["image"].ToString());
                    categories.Add(p);
                }
                Reader.Close();
                return categories;

            }

        }


    }



}
