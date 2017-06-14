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
    public partial class StoreDialog : Form
    {
        DataTable t;
        
        public StoreDialog(string id)
        {
           
            InitializeComponent();
            codeTxt.Text = Helper.Code;
        }

        private void titleTxt_TextChanged(object sender, EventArgs e)
        {

        }

        Store _store;
        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (nameTxt.Text == "")
            {
                nameTxt.BackColor = Color.Red;
                return;

            }
            if (addressTxt.Text == "")
            {
                addressTxt.BackColor = Color.Red;
                return;

            }
            if (currentCbx.Text == "")
            {
                currentCbx.BackColor = Color.OrangeRed;
                return;

            }
            string id = Guid.NewGuid().ToString();
            _store = new Store(id, nameTxt.Text, locationTxt.Text, addressTxt.Text, contactTxt.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, codeTxt.Text,currentCbx.Text);


            if (DBConnect.Insert(_store) != "")

                Global._store.Add(_store);
            nameTxt.Text = "";
            addressTxt.Text = "";
            locationTxt.Text = "";
            MessageBox.Show("Information Saved");
            this.DialogResult = DialogResult.OK;
            this.Dispose();




        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}
