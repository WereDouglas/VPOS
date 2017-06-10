using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Transactor
    {
        private string id;
        private string name;
        private string contact;
        private string image;
        private string type;       
        private string created;
        private string address;
        private string orgID;
        private string storeID;

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

        public Transactor(string id, string name, string contact, string image, string type, string created,string address,string orgID, string storeID)
        {
            this.Id = id;
            this.Name = name;
            this.Contact = contact;
            this.Image = image;
            this.Type = type;
            this.Created = created;
            this.Address = address;
            this.OrgID = orgID;
            this.StoreID = storeID;
        }

        public static List<Transactor> ListTransactor()
        {
            DBConnect.OpenConn();
            List<Transactor> wards = new List<Transactor>();
            string SQL = "SELECT * FROM transactor";
            NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
            NpgsqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                Transactor p = new Transactor(Reader["id"].ToString(), Reader["name"].ToString(), Reader["contact"].ToString(), Reader["image"].ToString(), Reader["type"].ToString(),Reader["created"].ToString(), Reader["address"].ToString(), Reader["orgID"].ToString(), Reader["storeid"].ToString());
                wards.Add(p);
            }
            DBConnect.CloseConn();
            return wards;

        }
    }
}
