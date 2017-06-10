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
    public partial class ExpenseDialog : Form
    {
        Expense _exp;
        public ExpenseDialog()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string ID = Guid.NewGuid().ToString();

            if (totalTxt.Text == "0")
            {
                totalTxt.BackColor = Color.Red;
                MessageBox.Show("You cant have spent nothing !");
                return;
            }
            if (typeCbx.Text == "")
            {
                typeCbx.BackColor = Color.Red;
                MessageBox.Show("what type of expense is this  !");
                return;
            }

            _exp = new Expense(ID, particularTxt.Text, detailTxt.Text, qtyTxt.Text, Convert.ToDateTime(dateTimePicker1.Text).ToString("dd-MM-yyyy"), priceTxt.Text, typeCbx.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.UserID, totalTxt.Text, Helper.StoreID);
            DBConnect.Insert(_exp);
            Global._expense.Add(_exp);
            MessageBox.Show("Information Saved");
            totalTxt.Text = "0";
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void qtyTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                totalTxt.Text = (Convert.ToDouble(qtyTxt.Text) * Convert.ToDouble(priceTxt.Text)).ToString();
            }
            catch { }
        }
    }
}
