using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Tax
    {
        private string id;
        private string name;
        private string percentage;
        private string apply;       
        private string country;       
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

        public string Percentage
        {
            get
            {
                return percentage;
            }

            set
            {
                percentage = value;
            }
        }

        public string Apply
        {
            get
            {
                return apply;
            }

            set
            {
                apply = value;
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
        public Tax() { }
        public Tax(string id, string name, string percentage, string apply, string country, string created, string orgID)
        {
            this.Id = id;
            this.Name = name;
            this.Percentage = percentage;
            this.Apply = apply;
            this.Country = country;
            this.Created = created;
            this.OrgID = orgID;
        }

        public static List<Tax> ListTax()
        {
            DBConnect.OpenConn();
            List<Tax> wards = new List<Tax>();
            string SQL = "SELECT * FROM tax";
            NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
            NpgsqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                Tax p = new Tax(Reader["id"].ToString(), Reader["name"].ToString(), Reader["percentage"].ToString(), Reader["apply"].ToString(), Reader["country"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString());
                wards.Add(p);
            }
            DBConnect.CloseConn();
            return wards;

        }
    }
}
