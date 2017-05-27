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
    public partial class TransactionForm : Form
    {
        Item _item;
        Billing _bill;
        WebCam webcam;
        DataTable t;
        DataTable s;
        DataTable m;
        private string barcode = string.Empty;
        Dictionary<int, double> TotalDictionary = new Dictionary<int, double>();
        Dictionary<string, string> BarDictionary = new Dictionary<string, string>();
        Dictionary<string, string> CustomerDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ContactDictionary = new Dictionary<string, string>();
        Dictionary<string, int> SelectedItems = new Dictionary<string, int>();
        string date;
        public TransactionForm()
        {
            InitializeComponent();
            date = DateTime.Now.ToString("dd-MM-yyyy");
            LoadData(date);
        }
        public void LoadData(string date)
        {
            t = new DataTable();
           
            t.Columns.Add("id");//1 
            t.Columns.Add("Date");//1                 
            t.Columns.Add("No");//3 
            t.Columns.Add("Particulars");//3 
            t.Columns.Add("Quantity");//4
            t.Columns.Add("Price");//4              
            t.Columns.Add("Amount");//5
            t.Columns.Add("Type");//5
            foreach (Sale h in Sale.ListSale().Where(g => g.Created.Contains(date)))
            {
                double amount = (Convert.ToDouble(h.Qty)* Convert.ToDouble(h.Price));
                t.Rows.Add(new object[] {  h.Id, h.Created, h.No, Global._item.First(p => p.Id.Contains(h.ItemID)).Name, h.Qty, h.Price,  amount.ToString("n0"),h.Type });

            }
            itemGrid.DataSource = t;
            itemGrid.AllowUserToAddRows = false;
            itemGrid.Columns[0].Visible = false;
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            date = Convert.ToDateTime(dateTimePicker1.Text).ToString("dd-MM-yyyy");
            LoadData(date);
        }
    }
}
