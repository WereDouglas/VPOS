using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Users
    {
        private string id;
        private string idNo;
        private string contact;
        private string contact2;
        private string surname;
        private string lastname;
        private string othername;
        private string email;
        private string nationality;
        private string address;      
        private string passwords;       
        private string gender;
        private string orgID;
        private string roles;
        private string initialPassword;
        private string account;
        private string status;
        private string image;
        private string created;

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

        public string IdNo
        {
            get
            {
                return idNo;
            }

            set
            {
                idNo = value;
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

        public string Contact2
        {
            get
            {
                return contact2;
            }

            set
            {
                contact2 = value;
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }

            set
            {
                surname = value;
            }
        }

        public string Lastname
        {
            get
            {
                return lastname;
            }

            set
            {
                lastname = value;
            }
        }

        public string Othername
        {
            get
            {
                return othername;
            }

            set
            {
                othername = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string Nationality
        {
            get
            {
                return nationality;
            }

            set
            {
                nationality = value;
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

        public string Passwords
        {
            get
            {
                return passwords;
            }

            set
            {
                passwords = value;
            }
        }

        public string Gender
        {
            get
            {
                return gender;
            }

            set
            {
                gender = value;
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

        public string Roles
        {
            get
            {
                return roles;
            }

            set
            {
                roles = value;
            }
        }

        public string InitialPassword
        {
            get
            {
                return initialPassword;
            }

            set
            {
                initialPassword = value;
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

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
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

        public Users(string id, string idNo, string contact, string contact2, string surname, string lastname, string othername, string email, string nationality, string address, string passwords, string gender, string orgID, string roles, string initialPassword, string account, string status, string image, string created)
        {
            this.Id = id;
            this.IdNo = idNo;
            this.Contact = contact;
            this.Contact2 = contact2;
            this.Surname = surname;
            this.Lastname = lastname;
            this.Othername = othername;
            this.Email = email;
            this.Nationality = nationality;
            this.Address = address;
            this.Passwords = passwords;
            this.Gender = gender;
            this.OrgID = orgID;
            this.Roles = roles;
            this.InitialPassword = initialPassword;
            this.Account = account;
            this.Status = status;
            this.Image = image;
            this.Created = created;
        }

        public static List<Users> ListUsers()
        {
            DBConnect.OpenConn();
            List<Users> wards = new List<Users>();
            string SQL = "SELECT * FROM users";
            NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
            NpgsqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                Users p = new Users(Reader["id"].ToString(), Reader["idno"].ToString(), Reader["contact"].ToString(), Reader["contact2"].ToString(), Reader["surname"].ToString(),Reader["lastname"].ToString(), Reader["othername"].ToString(), Reader["email"].ToString(), Reader["nationality"].ToString(), Reader["address"].ToString(), Reader["passwords"].ToString(), Reader["gender"].ToString(), Reader["orgID"].ToString(), Reader["roles"].ToString(), Reader["initialpassword"].ToString(), Reader["account"].ToString(), Reader["status"].ToString(), Reader["image"].ToString(), Reader["created"].ToString());
                wards.Add(p);
            }
            DBConnect.CloseConn();
            return wards;

        }
    }
}
