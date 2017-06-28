using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Sub
    {
        private string id;
        private string name;
        private string catID;
        private string description;
        private string created;
        private string orgID;    
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

        public string CatID
        {
            get
            {
                return catID;
            }

            set
            {
                catID = value;
            }
        }

        public Sub() { }
        public Sub(string id, string name,string catID, string description, string created, string orgID, string image)
        {
            this.Id = id;
            this.Name = name;
            this.CatID = catID;
            this.Description = description;
            this.Created = created;
            this.OrgID = orgID;
          
            this.Image = image;
        }

       
        static SQLiteDataReader Reader;
        public static List<Sub> ListSub()
        {
            if (Helper.Type != "Lite")
            {
                DBConnect.OpenConn();
                List<Sub> categories = new List<Sub>();
                string SQL = "SELECT * FROM sub";
                NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
                NpgsqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Sub p = new Sub(Reader["id"].ToString(), Reader["name"].ToString(), Reader["catID"].ToString(), Reader["description"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(),  Reader["image"].ToString());
                    categories.Add(p);
                }
                DBConnect.CloseConn();
                return categories;
            }
            else
            {
                DBConnect.OpenConn();
                List<Sub> categories = new List<Sub>();
                string SQL = "SELECT * FROM sub";
                Reader = DBConnect.ReadingLite(SQL);
                while (Reader.Read())
                {
                    Sub p = new Sub(Reader["id"].ToString(), Reader["name"].ToString(), Reader["catID"].ToString(), Reader["description"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(),  Reader["image"].ToString());
                    categories.Add(p);
                }
                Reader.Close();
                return categories;

            }

        }


    }



}
