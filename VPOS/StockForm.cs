using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPOS.Model;

namespace VPOS
{
    public partial class StockForm : Form
    {
        Item _item;
        WebCam webcam;
        DataTable t;
        string date;
        public StockForm()
        {
            InitializeComponent();
            date = DateTime.Now.ToString("dd-MM-yyyy");
            LoadData(date);
        }
        public void LoadData(string date)
        {

            t = new DataTable();
            t.Columns.Add(new DataColumn("Select", typeof(bool)));
            t.Columns.Add("id");//1 
            t.Columns.Add("Date");//1                 
            t.Columns.Add("Name");//3 
            t.Columns.Add("Quantity Sold");//4
            t.Columns.Add("Sales amount");//4              
            t.Columns.Add("Quantity Purchased");//5
            t.Columns.Add("Purchase amount");//5
            t.Columns.Add("Profit");//5

            foreach (Quantity h in Quantity.ListQuantity().Where(g=>g.Created.Contains(date)))
            {
                double salesAmount = Convert.ToDouble(h.Sale_qty) * Convert.ToDouble(Global._stock.First(p => p.Id.Contains(h.ItemID)).Sale_price);
                double purchaseAmount = Convert.ToDouble(h.Purchase_qty) * Convert.ToDouble(Global._stock.First(p => p.Id.Contains(h.ItemID)).Purchase_price);
                double profit = salesAmount - purchaseAmount;
                t.Rows.Add(new object[] { false, h.Id,h.Created, Global._item.First(p=>p.Id.Contains(h.ItemID)).Name, h.Sale_qty, salesAmount.ToString("n0"), h.Purchase_qty, purchaseAmount.ToString("n0"), profit.ToString("n0") });

            }
            dtGrid.DataSource = t;            
            dtGrid.AllowUserToAddRows = false;          
           
            dtGrid.Columns[1].Visible = false;
           
        }
        static string base64String = null;
        public System.Drawing.Image Base64ToImage(string bases)
        {
            byte[] imageBytes = Convert.FromBase64String(bases);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            date = Convert.ToDateTime(dateTimePicker1.Text).ToString("dd-MM-yyyy");
            LoadData(date);
        }
    }
}
