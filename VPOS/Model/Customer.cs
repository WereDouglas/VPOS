using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Customer
    {
        private string id;
        private string name;
        private string contact;
        private string image; 
        private string address;
        private string created;
        private string orgID;
       
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

        public Customer() { }

        public Customer(string id, string name, string contact, string image, string address, string created, string orgID)
        {
            this.Id = id;
            this.Name = name;
            this.Contact = contact;
            this.Image = image;
            this.Address = address;
            this.Created = created;
            this.OrgID = orgID;
        }

        public static List<Customer> ListCustomer()
        {
            List<Customer> customers = new List<Customer>();
            string SQL = "SELECT * FROM customer";
            if (Helper.Type != "Lite")
            {
                DBConnect.OpenConn();
                NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
                NpgsqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Customer p = new Customer(Reader["id"].ToString(), Reader["name"].ToString(), Reader["contact"].ToString(), Reader["image"].ToString(), Reader["address"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString());
                    customers.Add(p);
                }
                DBConnect.CloseConn();              
            }
            else
            {               
                Reader = DBConnect.ReadingLite(SQL);
                while (Reader.Read())
                {
                    Customer p = new Customer(Reader["id"].ToString(), Reader["name"].ToString(), Reader["contact"].ToString(), Reader["image"].ToString(), Reader["address"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString());
                    customers.Add(p);
                }
                Reader.Close();               
            }
            return customers;
        }
    }
}
