using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using VPOS.Model;
using VPOS.SQLite;

namespace VPOS
{
    public partial class Form1 : Form
    {
        Organisation _org;
        List<Organisation> _orgList;
        Roles _role;
        string originalPassword;
        Connection dbobject = new Connection();
        SQLiteConnection SQLconnect = new SQLiteConnection();
        string IP;
        public Form1()
        {
            createSqlliteDB();
            InitializeComponent();
            LoadSettings();
            Global.LoadData();
            if (Global._org.Count() < 1)
            {
               
                using (OrganisationDialog form = new OrganisationDialog(""))
                {
                    // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                    DialogResult dr = form.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        // MessageBox.Show(form.state);

                    }
                }

            }
            else {

                Helper.OrgID = Global._org.FirstOrDefault().Id;
                Helper.lastSync = Global._org.First().Sync;
                Helper.stocktaking = Global._org.First().Counts;
                Helper.StoreID = Global._org.First().StoreID;
                if (String.IsNullOrEmpty(Helper.StoreID)) {
                    using (OrganisationDialog form = new OrganisationDialog(Helper.OrgID))
                    {
                        // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                        DialogResult dr = form.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            // MessageBox.Show(form.state);

                        }
                    }

                }
                if (String.IsNullOrEmpty(Helper.stocktaking)) { Helper.stocktaking = DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"); }
            }
            if (Global._users.Count() < 1)
            {
                string ids = Guid.NewGuid().ToString();
                _role = new Roles(ids, "Administrator", "All item pos daily purchases merchandise inventory expenses cash flow suppliers users suppliers catgories transactions ledgers logs profile ", "create update delete log ", DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.StoreID);

                DBConnect.Insert(_role);
                using (UserDialog form = new UserDialog(""))
                {
                    // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                    DialogResult dr = form.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        // MessageBox.Show(form.state);

                    }
                }
            }
            autocomplete();
        }
        private void LoadSettings()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load("LocalXMLFile.xml");
                var servers = from person in xmlDoc.Descendants("Server")
                              select new
                              {

                                  Name = person.Element("Name").Value,
                                  Type = person.Element("Type").Value
                              };

                foreach (var server in servers)
                {
                    

                    Helper.ServerName = server.Name;
                    Helper.Type = server.Type;
                    if (Helper.Type.Contains("Lite"))
                    {

                        Global.LoadData();

                    }
                    else
                    {
                        if (IPAddressCheck(server.Name) != "")
                        {
                            Helper.ServerIP = IPAddressCheck(Helper.ServerName);
                            lblStatus.Text += IPAddressCheck(Helper.ServerName);
                            // IP = IPAddressCheck(Helper.serverName);
                            if (TestServerConnection())
                            {
                                lblStatus.Text = lblStatus.Text + " Server connected   you can continue to login";
                                lblStatus.ForeColor = Color.Green;
                                Global.LoadData();
                            }
                            else
                            {
                                using (SettingDialog form = new SettingDialog())
                                {                                   
                                    DialogResult dr = form.ShowDialog();
                                    if (dr == DialogResult.OK)
                                    {
                                        LoadSettings();
                                    }
                                }

                                lblStatus.Text = ("You are not able to connect to the server contact the administrator for further assistance");
                                lblStatus.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please start the server");
                            return;
                        }
                    }
                }
                // MessageBox.Show(Helper.serverIP);
                

            }
            catch
            {
                //using (SettingDialog form = new SettingDialog())
                //{
                 
                //    DialogResult dr = form.ShowDialog();
                //    if (dr == DialogResult.OK)
                //    {
                //        LoadSettings();
                //    }
                //}
            }

        }
        private String IPAddressCheck(string HostName)
        {
            //var ip = System.Net.Dns.GetHostEntry("JacksLaptop");
            //  string ipStrings = ip.AddressList[0].ToString();
            //var host =;
            var IPAddr = Dns.GetHostEntry(HostName);
            IPAddress ipString = null;

            foreach (var IP in IPAddr.AddressList)
            {
                if (IPAddress.TryParse(IP.ToString(), out ipString) && IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    break;
                }
            }
            // Helper.serverIP = ipString.ToString();
            return ipString.ToString();
        }
        static NpgsqlDataReader Reader = null;
        private bool TestServerConnection()
        {
            //try
            //{

            string SQL = "SELECT * FROM users;";
            Reader = DBConnect.Reading(SQL);
            if (Reader != null)
            {
                lblStatus.Text = lblStatus.Text + ("Local server connection successful");
                lblStatus.ForeColor = Color.Green;
                Reader.Close();
                return true;
            }
            else
            {
                Reader.Close();
                return false;

            }
            // }
            //catch
            //{
            //    lblStatus.Text = ("You are not able to connect to the server contact the administrator for further assistance");
            //    lblStatus.ForeColor = Color.Red;
            //    return false;
            //}
        }
        private void GrantAccess(string fullPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
        }
        Users _users = new Users();
        Billing _bill = new Billing();
        Category _cat = new Category();
        Expense _exp = new Expense();
        Item _item = new Item();
        Organisation _organisation = new Organisation();
        Payment _pay = new Payment();
        Purchase _purchase = new Purchase();
        Quantity _qty = new Quantity();
        Roles _roles = new Roles();
        Sale _sale = new Sale();
        Stock _stock = new Stock();
        Store _store = new Store();
        Taking _take = new Taking();
        Transactor _trans = new Transactor();

        private void createSqlliteDB()
        {           
              //  string fullFilePath = Path.Combine(appPath, "casesLite.txt");            
              //  string SQL = DBConnect.CreateDBSQL(_users);
           Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_users));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_bill));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_cat));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_exp));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_item));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_organisation));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_pay));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_purchase));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_qty));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_roles));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_sale));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_stock));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_take));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_trans));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(_store));


        }
        private void autocomplete()
        {
            AutoCompleteStringCollection AutoItem = new AutoCompleteStringCollection();
            foreach (Users r in Global._users)
            {
                AutoItem.Add(r.Contact);
            }
            contactTxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            contactTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            contactTxt.AutoCompleteCustomSource = AutoItem;
           
            DBConnect.CloseConn();

        }
        List<Users> _userList;
        private void button1_Click(object sender, EventArgs e)
        {
            _userList = Global._users.Where(j => j.Contact.Contains(contactTxt.Text) && (j.Passwords.Contains(Helper.MD5Hash(passwordTxt.Text)) || j.InitialPassword.Contains(Helper.MD5Hash(passwordTxt.Text)))).ToList();
            if (_userList.Count()>0)
            {
                Helper.UserID = _userList.First().Id;
              
                Helper.OrgID = Global._org.First().Id;
                Helper.Code = Global._org.First().Code;
                Helper.Image = _userList.First().Image;
                Helper.Username = _userList.First().Surname +" " + _userList.First().Lastname ;
                MainForm fm = new MainForm();
                fm.Show();
            }         
            else{
                MessageBox.Show("Invalid User Check contact and password !");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void passwordTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (SettingDialog form = new SettingDialog())
            {
                // DentalDialog form1 = new DentalDialog(item.Text, ItemID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    LoadSettings();
                }
            }
        }
    }
}
