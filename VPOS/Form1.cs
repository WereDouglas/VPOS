using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPOS.Model;

namespace VPOS
{
    public partial class Form1 : Form
    {
        Organisation _org;
        List<Organisation> _orgList;
        Roles _role;
        string originalPassword;
        public Form1()
        {
            InitializeComponent();
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
                Helper.StoreID = _userList.First().StoreID;
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
    }
}
