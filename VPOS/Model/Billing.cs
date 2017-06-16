using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Billing
    {
        private string id;
        private string no;
        private string pos;
        private string paid;
        private string method;
        private string reference;
        private string total;
        private string balance;
        private string bank;
        private string account;
        private string transactorID;
        private string created;
        private string type;
        private string orgID;
        private string userID;
        private string tax;
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

        public string Pos
        {
            get
            {
                return pos;
            }

            set
            {
                pos = value;
            }
        }

        public string Paid
        {
            get
            {
                return paid;
            }

            set
            {
                paid = value;
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

        public string Reference
        {
            get
            {
                return reference;
            }

            set
            {
                reference = value;
            }
        }

        public string Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
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

        public string Bank
        {
            get
            {
                return bank;
            }

            set
            {
                bank = value;
            }
        }

        public string Account
        {
            get
            {
                return account;
            }

            set
            {
                account = value;
            }
        }

        public string TransactorID
        {
            get
            {
                return transactorID;
            }

            set
            {
                transactorID = value;
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
        public Billing() { }
        public Billing(string id, string no, string pos, string paid, string method, string reference, string total, string balance, string bank, string account, string transactorID, string created, string type, string orgID, string userID, string tax, string storeID)
        {
            this.Id = id;
            this.No = no;
            this.Pos = pos;
            this.Paid = paid;
            this.Method = method;
            this.Reference = reference;
            this.Total = total;
            this.Balance = balance;
            this.Bank = bank;
            this.Account = account;
            this.TransactorID = transactorID;
            this.Created = created;
            this.Type = type;
            this.OrgID = orgID;
            this.UserID = userID;
            this.Tax = tax;
            this.StoreID = storeID;
        }
        static SQLiteDataReader Reader;
        public static List<Billing> ListBilling(string start, string end)
        {
            if (Helper.Type != "Lite")
            {
                DBConnect.OpenConn();
                List<Billing> billing = new List<Billing>();
                string SQL = "SELECT * FROM billing WHERE created::date >= '" + start + "'::date AND  created::date <= '" + end + "'::date";
                NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
                NpgsqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Billing p = new Billing(Reader["id"].ToString(), Reader["no"].ToString(), Reader["pos"].ToString(), Reader["paid"].ToString(), Reader["method"].ToString(), Reader["reference"].ToString(), Reader["total"].ToString(), Reader["balance"].ToString(), Reader["bank"].ToString(), Reader["account"].ToString(), Reader["transactorID"].ToString(), Reader["created"].ToString(), Reader["type"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString(), Reader["tax"].ToString(), Reader["storeid"].ToString());
                    billing.Add(p);
                }
                DBConnect.CloseConn();
                return billing;
            }
            else
            {
                List<Billing> billing = new List<Billing>();
               // string SQL = "SELECT * FROM billing WHERE strftime('%s', created) >= strftime('%s','" + start + "') AND  strftime('%s',created) <= strftime('%s','" + end + "')";
                //string SQL = "SELECT *, (substr(created, 7, 4) || '-' || substr(created, 1, 2) || '-' || substr(created, 4, 2)) AS SomeDate FROM billing WHERE somedate BETWEEN DATE('" + start + "') AND DATE('now', '+1 day')";
                string SQL = "SELECT * FROM billing";
                Reader = DBConnect.ReadingLite(SQL);
                while (Reader.Read())
                {
                    Billing p = new Billing(Reader["id"].ToString(), Reader["no"].ToString(), Reader["pos"].ToString(), Reader["paid"].ToString(), Reader["method"].ToString(), Reader["reference"].ToString(), Reader["total"].ToString(), Reader["balance"].ToString(), Reader["bank"].ToString(), Reader["account"].ToString(), Reader["transactorID"].ToString(), Reader["created"].ToString(), Reader["type"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString(), Reader["tax"].ToString(), Reader["storeid"].ToString());
                    billing.Add(p);
                }
                Reader.Close();
                return billing;

            }

        }
        public static List<Billing> ListWhere(string no)
        {
            if (Helper.Type != "Lite")
            {
                DBConnect.OpenConn();
                List<Billing> billing = new List<Billing>();
                string SQL = "SELECT * FROM billing WHERE no = '" + no + "'";
                NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
                NpgsqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Billing p = new Billing(Reader["id"].ToString(), Reader["no"].ToString(), Reader["pos"].ToString(), Reader["paid"].ToString(), Reader["method"].ToString(), Reader["reference"].ToString(), Reader["total"].ToString(), Reader["balance"].ToString(), Reader["bank"].ToString(), Reader["account"].ToString(), Reader["transactorID"].ToString(), Reader["created"].ToString(), Reader["type"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString(), Reader["tax"].ToString(), Reader["storeid"].ToString());
                    billing.Add(p);
                }
                DBConnect.CloseConn();
                return billing;
            }
            else
            {
                List<Billing> billing = new List<Billing>();
                  string SQL = "SELECT * FROM billing WHERE no = '" + no + "'";
                Reader = DBConnect.ReadingLite(SQL);
                while (Reader.Read())
                {
                    Billing p = new Billing(Reader["id"].ToString(), Reader["no"].ToString(), Reader["pos"].ToString(), Reader["paid"].ToString(), Reader["method"].ToString(), Reader["reference"].ToString(), Reader["total"].ToString(), Reader["balance"].ToString(), Reader["bank"].ToString(), Reader["account"].ToString(), Reader["transactorID"].ToString(), Reader["created"].ToString(), Reader["type"].ToString(), Reader["orgID"].ToString(), Reader["userID"].ToString(), Reader["tax"].ToString(), Reader["storeid"].ToString());
                    billing.Add(p);
                }
                Reader.Close();
                return billing;

            }

        }

    }
}
