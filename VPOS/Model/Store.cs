using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Store
    {
        private string id;
        private string name;
        private string location;
        private string address;

        private string contact;
        private string created;
        private string orgID;
        private string code;


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

        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public string Contact
        {
            get
            {
                return contact;
            }

            set
            {
                contact = value;
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

        public Store(string id, string name, string location, string address, string contact, string created, string orgID,string code)
        {
            this.Id = id;
            this.Name = name;
            this.Location = location;
            this.Address = address;
            this.Contact = contact;
            this.Created = created;
            this.OrgID = orgID;
            this.Code = code;
            

        }

        public static List<Store> ListStore()
        {
            DBConnect.OpenConn();
            List<Store> categories = new List<Store>();
            string SQL = "SELECT * FROM store";
            NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
            NpgsqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                Store p = new Store(Reader["id"].ToString(), Reader["name"].ToString(), Reader["location"].ToString(), Reader["address"].ToString(), Reader["contact"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(),Reader["code"].ToString());
                categories.Add(p);
            }
            DBConnect.CloseConn();
            return categories;

        }

    }
   
}
