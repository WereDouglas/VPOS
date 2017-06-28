using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Payment
    {
        private string id;
        private string no;
        private string method;       
        private string amount;      
        private string by;        
        private string created;        
        private string orgID;
        private string userID;
        private string type;//credit/debit
        private string storeID;
        private string customerID;
        private string balance;
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

        public string No
        {
            get
            {
                return no;
            }

            set
            {
                no = value;
            }
        }

        public string Method
        {
            get
            {
                return method;
            }

            set
            {
                method = value;
            }
        }

        public string Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
            }
        }

        public string By
        {
            get
            {
                return by;
            }

            set
            {
                by = value;
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

        public string CustomerID
        {
            get
            {
                return customerID;
            }

            set
            {
                customerID = value;
            }
        }

        public string Balance
        {
            get
            {
                return balance;
            }

            set
            {
                balance = value;
            }
        }

        public Payment() { }
        public Payment(string id, string no, string method, string amount, string by, string created, string orgID, string userID,string type, string storeID,string customerID,string balance)
        {
            this.Id = id;
            this.No = no;
            this.Method = method;
            this.Amount = amount;
            this.By = by;
            this.Created = created;
            this.OrgID = orgID;
            this.UserID = userID;
            this.Type = type;
            this.OrgID = orgID;
            this.StoreID = storeID;
            this.CustomerID = customerID;
            this.Balance = balance;
        }

        public static List<Payment> ListPayment()
        {
            if (Helper.Type != "Lite")
            {
                DBConnect.OpenConn();
                List<Payment> payment = new List<Payment>();
                string SQL = "SELECT * FROM payment";
                NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
                NpgsqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Payment p = new Payment(Reader["id"].ToString(), Reader["no"].ToString(), Reader["method"].ToString(), Reader["amount"].ToString(), Reader["by"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString(), Reader["type"].ToString(), Reader["storeid"].ToString(), Reader["customerid"].ToString(), Reader["balance"].ToString());
                    payment.Add(p);
                }
                DBConnect.CloseConn();
                return payment;
            }
            else {

                List<Payment> payment = new List<Payment>();
                string SQL = "SELECT * FROM payment";
                Reader = DBConnect.ReadingLite(SQL);
                while (Reader.Read())
                {
                    Payment p = new Payment(Reader["id"].ToString(), Reader["no"].ToString(), Reader["method"].ToString(), Reader["amount"].ToString(), Reader["by"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString(), Reader["type"].ToString(), Reader["storeid"].ToString(), Reader["customerid"].ToString(), Reader["balance"].ToString());
                    payment.Add(p);
                }
                Reader.Close();
                return payment;

            }

        }
        public static List<Payment> ListWhere(string no)
        {
            List<Payment> payment = new List<Payment>();
            string SQL = "SELECT * FROM payment WHERE no = '" + no + "'";
            if (Helper.Type != "Lite")
            {
                DBConnect.OpenConn();
                NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
                NpgsqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Payment p = new Payment(Reader["id"].ToString(), Reader["no"].ToString(), Reader["method"].ToString(), Reader["amount"].ToString(), Reader["by"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString(), Reader["type"].ToString(), Reader["storeid"].ToString(), Reader["customerid"].ToString(), Reader["balance"].ToString());
                    payment.Add(p);
                }
                DBConnect.CloseConn();

            }
            else
            {

                Reader = DBConnect.ReadingLite(SQL);
                while (Reader.Read())
                {
                    Payment p = new Payment(Reader["id"].ToString(), Reader["no"].ToString(), Reader["method"].ToString(), Reader["amount"].ToString(), Reader["by"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString(), Reader["type"].ToString(), Reader["storeid"].ToString(), Reader["customerid"].ToString(), Reader["balance"].ToString());
                    payment.Add(p);
                }
                Reader.Close();


            }
            return payment;
        }

    }
}
