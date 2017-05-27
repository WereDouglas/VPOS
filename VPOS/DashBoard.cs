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
    public partial class DashBoard : Form
    {
        Item _item;
        Billing _bill;
        string date;
        public DashBoard()
        {
            InitializeComponent();
            date = DateTime.Now.ToString("dd-MM-yyyy");
            LoadData();
        }
        public void LoadData()
        {
            salesLbl.Text = Global._sale.Where(m=>m.Date.Contains(date) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Qty)).ToString("n0");
         //   stockLbl.Text = Global._item.Sum(p => Convert.ToDouble(p.Quantity)).ToString("n0");
            purchaseLbl.Text = Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Qty)).ToString("n0");
            purchaseamountLbl.Text = "Purchase today: " + Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Total)).ToString("n0");
            saleamountLbl.Text ="Sales today: "+ Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Total)).ToString("n0");

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
