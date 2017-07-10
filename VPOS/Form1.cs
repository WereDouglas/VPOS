using AutoUpdaterDotNET;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using VPOS.Model;
using VPOS.SQLite;

namespace VPOS
{
    public partial class Form1 : Form
    {
        Organisation org;
        List<Organisation> orgList;
        Roles _role;
        string originalPassword;
        Connection dbobject = new Connection();
        SQLiteConnection SQLconnect = new SQLiteConnection();
        string IP;
        private BackgroundWorker bwLite = new BackgroundWorker();
        public static Form1 _Form1;
        string start;
        string end;
        public Form1()
        {


            start = DateTime.Now.ToString("dd-MM-yyyy");
            end = DateTime.Now.ToString("dd-MM-yyyy");
            createSqlliteDB();
            InitializeComponent();
            //in setting is where am loading Global 
            LoadSettings();
            _Form1 = this;

            lblStatus.Text = "";
            bwLite.DoWork += backgroundWorker1_DoWork;
            bwLite.ProgressChanged += backgroundWorker1_ProgressChanged;
            bwLite.WorkerReportsProgress = true;
        }
        public void hides()
        {
            Invoke((MethodInvoker)delegate
            {
                processLbl.Visible = true;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;


            });
        }
        private void process(int count)
        {
            int val = count;
            switch (val)
            {
                case 1:
                    hides();
                    Refreshing.Payment();
                    break;
                case 2:
                    Refreshing.Stores();

                    break;
                case 3:
                    Refreshing.Role();
                    break;
                case 4:
                    Refreshing.Items();
                    break;
                case 5:
                    Refreshing.Users();
                    break;
                case 6:
                    Refreshing.Purchase();
                    Refreshing.Sale();
                    break;
                case 7:
                    Refreshing.Categories();
                    break;
                case 8:
                    Refreshing.Invoice();
                    break;
                case 9:
                    Refreshing.Expenses();
                    break;
                case 10:
                    Refreshing.Bill();
                    break;
                case 11:
                    Refreshing.Stock();
                    break;
                case 12:
                    Refreshing.Org();
                    break;
                case 13:
                    Refreshing.Customer();
                    break;
                case 14:
                    Refreshing.Supplier();
                    break;
                case 15:
                    // Upload.updateSyncTime();
                    FeedBack("Uploading and Downloading of information complete");
                    Helper.lastSync = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    FeedBack("LAST SYNC " + Helper.lastSync);
                    // shows();
                    break;
                case 16:
                    shows();
                    break;
                default:
                    FeedBack("Processing");
                    break;
            }

            //DownloadWallet()
            //autocompleteLite()
        }
        public void shows()
        {


            Invoke((MethodInvoker)delegate
            {
                autocomplete();
                processLbl.Text = "....Complete..";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                autocomplete();

            });
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            for (int i = 0; i < 17; i++)
            {
                FeedBack("STEP " + i.ToString());
                //try
                //{
                process(i);
                //}
                //catch (Exception c)
                //{
                //    FeedBack("PROCESSING ERROR " + i.ToString() + "  " + c.Message.ToString());
                //}
                Thread.Sleep(1500);
                bwLite.ReportProgress(i);
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void LoadSettings()
        {

            try
            {
                XDocument xmlDoc = XDocument.Load(Connection.XMLFile());
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
                    if (Helper.Type == "")
                    {
                        using (SettingDialog form = new SettingDialog())
                        {
                            DialogResult dr = form.ShowDialog();
                            if (dr == DialogResult.OK)
                            {
                                LoadSettings();
                            }
                        }
                        return;
                    }
                    if (Helper.Type.Contains("Lite"))
                    {
                        Global.LoadData(start, end);
                        autocomplete();
                        return;
                    }
                    if (Helper.Type.Contains("Enterprise"))
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
                                Global.LoadData(start, end);
                                autocomplete();
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
                using (SettingDialog form = new SettingDialog())
               {

                    DialogResult dr = form.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        LoadSettings();
                    }
                }
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
     

        private void createSqlliteDB()
        {
            //  string fullFilePath = Path.Combine(appPath, "casesLite.txt");            
            //  string SQL = DBConnect.CreateDBSQL(_users);
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Users()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Billing()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Category()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Customer()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Expense()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Invoice()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Item()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Organisation()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Payment()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Purchase()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Quantity()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Roles()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Sale()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Stock()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Store()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Supplier()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Taking()));
            Connection.createSQLLiteDB(DBConnect.CreateDBSQL(new Sub()));



        }
        private void autocomplete()
        {
            AutoCompleteStringCollection AutoItem = new AutoCompleteStringCollection();
            foreach (Users r in Users.ListUsers())
            {
                AutoItem.Add(r.Contact);
            }
            contactTxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            contactTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            contactTxt.AutoCompleteCustomSource = AutoItem;

           

        }
        List<Users> _userList;
        private void button1_Click(object sender, EventArgs e)
        {
            if (contactTxt.Text == "" || passwordTxt.Text == "")
            {
                MessageBox.Show("Insert login credentials");
                //  loginBtn.Visible = true;
                return;
            }
            try
            {
                _userList = Global.users.Where(j => j.Contact.Contains(contactTxt.Text) && (j.Passwords.Contains(Helper.MD5Hash(passwordTxt.Text)) || j.InitialPassword.Contains(Helper.MD5Hash(passwordTxt.Text)))).ToList();

            }
            catch
            {

                MessageBox.Show("No Users defined");
                return;

            }

            if (_userList.Count() > 0)
            {
                Helper.UserID = _userList.First().Id;
                Helper.Code = Global.org.First().Code;
                Helper.Image = _userList.First().Image;
                Helper.Username = _userList.First().Surname + " " + _userList.First().Lastname;


                Helper.OrgID = Global.org.FirstOrDefault().Id;
                Helper.lastSync = Global.org.First().Sync;
                Helper.stocktaking = Global.org.First().Counts;
                Helper.StoreID = Global.org.First().StoreID;
                if (String.IsNullOrEmpty(Helper.StoreID))
                {
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

                MainForm fm = new MainForm();
                fm.Show();
            }
            else
            {
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (contactTxt.Text == "" || passwordTxt.Text == "")
            {
                MessageBox.Show("Insert login credentials");
                button1.Visible = true;
                return;
            }

            if (MessageBox.Show("YES or NO?", "Downloading your information /n *(Working internet connection\n required ) ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (Downloading.CheckDownload())
                {
                    uploadTxt.Visible = true;
                    if (OnlineCheck())
                    {
                        bwLite.RunWorkerAsync();
                    }
                }
                else
                {
                    MessageBox.Show("No connection to server detected");
                    return;

                }
            }
        }
        public void FeedBack(string text)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + text + "\r\n";
                    uploadTxt.ForeColor = Color.Orange;
                    uploadTxt.ScrollToCaret();
                    uploadTxt.ScrollToCaret();


                });
            }
            catch
            {


            }

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            uploadTxt.SelectionStart = uploadTxt.Text.Length;
            uploadTxt.ScrollToCaret();
        }
        private bool OnlineCheck()
        {
            bool state;
            List<String> userCount = new List<string>();
            string query = "SELECT * FROM users WHERE contact = '" + contactTxt.Text + "' AND passwords = '" + Helper.MD5Hash(passwordTxt.Text) + "'";
            string URL = Helper.genUrl + "api/request";
            NameValueCollection formData = new NameValueCollection();
            formData["query"] = query;
            string resulted = Helper.get(URL, formData);
            if (resulted != "false")
            {
                Form1._Form1.FeedBack("DOWNLOADING USERS " + resulted);
                JArray results = JArray.Parse(resulted);
                Form1._Form1.FeedBack("USERS TO BE DOWNLOADED " + results.ToString());
                for (int K = 0; K < results.Count; K++)
                {
                    Helper.username = results[K]["surname"].ToString() + " " + results[K]["lastname"].ToString();
                    Helper.contact = results[K]["contact"].ToString();
                    Helper.designation = results[K]["roles"].ToString();
                    Helper.email = results[K]["email"].ToString();
                    Helper.image = results[K]["image"].ToString();
                    Helper.OrgID = results[K]["orgID"].ToString();
                    userCount.Add(results[K]["surname"].ToString());

                }
                button1.Visible = false;
                state = true;
            }

            int count = userCount.Count();
            if (count > 0)
            {
                state = true;
            }
            else
            {
                state = false;
            }


            return state;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            using (OrganisationDialog form = new OrganisationDialog(""))
            {
                // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    LoadSettings();
                    // MessageBox.Show(form.state);
                    autocomplete();

                }
            }

        }
        //
        private void Form1_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start(Helper.fileUrl + "update.xml");

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // DBConnect.save(string query);
            if (MessageBox.Show("YES or No?", "Are you sure you want to delete all database information? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
               

                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Users()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Billing()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Category()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Customer()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Expense()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Invoice()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Item()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Organisation()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Payment()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Purchase()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Quantity()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Roles()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Sale()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Stock()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Store()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Supplier()));
                Connection.createSQLLiteDB(DBConnect.EmptyDBSQL(new Taking()));
            }
        }
    }
}
