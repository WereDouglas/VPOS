﻿using System;
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
    public partial class InvoiceForm : Form
    {
        Item item;
        Sale _sale;
        WebCam webcam;
        DataTable t;
        DataTable s;
        DataTable m;
        private string barcode = string.Empty;
        Dictionary<int, double> TotalDictionary = new Dictionary<int, double>();
        Dictionary<string, string> SumDictionary = new Dictionary<string, string>();
        Dictionary<string, string> PaymentDictionary = new Dictionary<string, string>();
        Dictionary<string, string> PaidDictionary = new Dictionary<string, string>();
        Dictionary<string, string> BalanceDictionary = new Dictionary<string, string>();
        Dictionary<string, string> BarDictionary = new Dictionary<string, string>();
        Dictionary<string, string> CustomerDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ContactDictionary = new Dictionary<string, string>();
        Dictionary<string, int> SelectedItems = new Dictionary<string, int>();
        string start;
        string end;
        public InvoiceForm()
        {
            InitializeComponent();
            start = DateTime.Now.ToString("dd-MM-yyyy");
            end = DateTime.Now.ToString("dd-MM-yyyy");
            LoadData(start, end);
            Profile(Helper.OrgID);
        }
        private void Profile(string ID)
        {

             Image img = Base64ToImage(Global.org.First(k => k.Id.Contains(ID)).Image);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            //Bitmap bps = new Bitmap(bmp, 50, 50);
            pictureBox1.Image = bmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        List<Item> itemList = new List<Item>();
        public void LoadItem(string no)
        {
            dtGrid2.RowTemplate.Height = 90;
            t = new DataTable();
            s = new DataTable();

            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));//   0
            t.Columns.Add("id");//1       
            t.Columns.Add("Name");//2  
            t.Columns.Add("Quantity");//3        
            t.Columns.Add("image");//4
            /////
            /*******************/
            s.Columns.Add("Name");//3 
            s.Columns.Add("Quantity");//4              
            s.Columns.Add("Unit Cost");//5
            s.Columns.Add("Total Cost");//6          

            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Gray), 00, 00);
            }
            TotalDictionary.Clear();
            int c = 0;
            foreach (var h in Global.sale.Where(g => g.No.Contains(no)))
            {
               
                   
                        t.Rows.Add(new object[] { b, Global.item.First(r => r.Id.Contains(h.ItemID)).Barcode, Global.item.First(r => r.Id.Contains(h.ItemID)).Name + Environment.NewLine + "PRICE:" + Global.stock.First(r => r.ItemID.Contains(h.ItemID)).Sale_price + Environment.NewLine + "MANUFACTURER :" + Global.item.First(r => r.Id.Contains(h.ItemID)).Manufacturer + Environment.NewLine + "EXPIRES:" + Global.stock.First(r => r.ItemID.Contains(h.ItemID)).Expire, h.Qty, Global.item.First(r => r.Id.Contains(h.ItemID)).Image });
                   
                        double TotalCost = (Convert.ToDouble(h.Qty) * Convert.ToDouble(h.Price));
                   
                        s.Rows.Add(new object[] { Global.item.First(r => r.Id.Contains(h.ItemID)).Name, h.Qty, Global.stock.First(r => r.ItemID.Contains(h.ItemID)).Sale_price, TotalCost.ToString("n0") });

                  
                    TotalDictionary.Add(c++, TotalCost);
            }
            noLbl.Text = no;
            dateLbl.Text = Invoice.ListWhere(no).First(k => k.No.Contains(no)).Created;
            try {
                customerLbl.Text = Global.customer.First(t => t.Id.Contains(Invoice.ListWhere(no).First(k => k.No.Contains(no)).CustomerID)).Name;
            } catch { }

            methodCbx.Text = Invoice.ListWhere(no).First(k => k.No.Contains(no)).Method;
            balanceTxt.Text = (Convert.ToDouble(Invoice.ListWhere(no).Sum(h => Convert.ToDouble(h.Amount)) - Convert.ToDouble(Global.sale.Where(j => j.No.Contains(no)).Sum(h => Convert.ToDouble(h.Amount)))).ToString());
            amountTxt.Text = Convert.ToDouble(Global.sale.Where(j => j.No.Contains(no)).Sum(h => Convert.ToDouble(h.Amount))).ToString();
            totalLbl.Text = Convert.ToDouble(Global.sale.Where(j => j.No.Contains(no)).Sum(h => Convert.ToDouble(h.Amount))).ToString();


            dtGrid2.DataSource = t;
            ThreadPool.QueueUserWorkItem(delegate
            {
                foreach (DataRow row in t.Rows)
                {
                    try
                    {
                        Image img = Base64ToImage(row["image"].ToString());
                        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
                        Bitmap bps = new Bitmap(bmp, 50, 50);
                        row["Img"] = bps;
                    }
                    catch
                    {

                    }
                }
            });
            dtGrid2.AllowUserToAddRows = false;

            dtGrid2.Columns[1].Visible = false;
            dtGrid2.Columns[4].Visible = false;
            saleGrid.DataSource = s;
            totalLbl.Text = TotalDictionary.Sum(p => Convert.ToDouble(p.Value)).ToString("n0");
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
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            start = Convert.ToDateTime(startDate.Text).ToString("dd-MM-yyyy");
            end = Convert.ToDateTime(endDate.Text).ToString("dd-MM-yyyy");
            LoadData(start, end);
        }
        List<Invoice> _invoiceList = new List<Invoice>();
        string Customers = "";
        public void LoadData(string start, string end)
        {

            t = new DataTable();
            t.Columns.Add("Select");//1 
            t.Columns.Add("Count");//1 
            t.Columns.Add("id");//1 
            t.Columns.Add("Date");//2                 
            t.Columns.Add("No");//3           
            t.Columns.Add("Total");//4 
            t.Columns.Add("Balance");//4 
            t.Columns.Add("Payment Date");//8       
           
            t.Columns.Add("Method");//12
            t.Columns.Add("Customers");//9 

          
                _invoiceList = Invoice.ListInvoice().ToList();
         
            
            SumDictionary.Clear();
            PaidDictionary.Clear();
            BalanceDictionary.Clear();
            int count = 1;
            foreach (Invoice h in _invoiceList)
            {
                try
                {
                    Customers = Global.customer.First(p => p.Id.Contains(h.CustomerID)).Name + " CONTACT: " + Global.customer.First(p => p.Id.Contains(h.CustomerID)).Contact;
                }
                catch
                {
                    Customers = h.CustomerID;
                }
                t.Rows.Add(new object[] { "Select", count++, h.Id, h.Created, h.No, h.Amount,h.Balance,h.Created,h.Method,Customers });
               
                SumDictionary.Add(h.Id, h.Amount);
                
                BalanceDictionary.Add(h.Id, h.Balance);

            }

            t.Rows.Add(new object[] { "", "", "", "", "", "", "", "", "" });

            t.Rows.Add(new object[] { "", "", "Total", "", "", SumDictionary.Sum(m => Convert.ToDouble(m.Value)).ToString("n0"), PaidDictionary.Sum(m => Convert.ToDouble(m.Value)).ToString("n0"), "", BalanceDictionary.Sum(m => Convert.ToDouble(m.Value)).ToString("n0"), "" });
            itemGrid.DataSource = t;
            // itemGrid.AllowUserToAddRows = false;
            itemGrid.Columns["id"].Visible = false;

            string balance = "";
            string amount = "";
            string Select = "";
            foreach (DataGridViewRow row in itemGrid.Rows)
            {
                //myDGV.Rows[theRowIndex].Cells[7].Value
                try
                {
                    balance = row.Cells["Balance"].Value.ToString();
                    amount = row.Cells["Amount"].Value.ToString();

                    if (balance != "0")
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        row.DefaultCellStyle.Font = new Font("Calibri", 12.5F, FontStyle.Regular, GraphicsUnit.Pixel);

                    }
                    else
                    {

                        row.DefaultCellStyle.ForeColor = Color.Green;
                        row.DefaultCellStyle.Font = new Font("Calibri", 10.5F, FontStyle.Regular, GraphicsUnit.Pixel);
                    }
                    if (amount != "")
                    {
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.Font = new Font("Calibri", 12.5F, FontStyle.Regular, GraphicsUnit.Pixel);

                    }
                }
                catch { }

                try
                {
                    Select = row.Cells["Select"].Value.ToString();

                    if (Select == "Select")
                    {
                        row.DefaultCellStyle.BackColor = Color.GhostWhite;

                        row.DefaultCellStyle.Font = new Font("Calibri", 10.5F, FontStyle.Regular, GraphicsUnit.Pixel);
                        // itemGrid.Columns["Select"].DefaultCellStyle.BackColor =;
                    }
                }
                catch { }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void paymentTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                newBalanceTxt.Text = (Convert.ToDouble(totalLbl.Text) - (Convert.ToDouble(amountTxt.Text) + Convert.ToDouble(paymentTxt.Text))).ToString();
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (paymentTxt.Text == "")
            {
                paymentTxt.BackColor = Color.Red;
                MessageBox.Show("No amount specified");
                return;

            }
            string ID = Guid.NewGuid().ToString();
            if (Convert.ToDouble(paymentTxt.Text) > 0)
            {
                string Query = "UPDATE  invoice SET balance= '0' WHERE no ='" + noLbl.Text + "'";
                DBConnect.save(Query);
                Invoice _pay = new Invoice(ID,noLbl.Text, methodCbx.Text, paymentTxt.Text, customerLbl.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID,Helper.UserID,"",Helper.StoreID,customerLbl.Text,newBalanceTxt.Text);
                DBConnect.Insert(_pay);
              

                MessageBox.Show("Information saved");
                Global.invoices.Add(_pay);
                LoadData(start, end);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("YES or NO?", "Are you sure you want to delete this transaction ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string Query = "DELETE from invoice WHERE no ='" + noLbl.Text + "'";
                DBConnect.save(Query);
                string Query2 = "DELETE from sale WHERE no ='" + noLbl.Text + "'";
                DBConnect.save(Query2);
                MessageBox.Show("Information deleted");
            }
        }

        private void itemGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {
                LoadItem(itemGrid.Rows[e.RowIndex].Cells["No"].Value.ToString());

            }
        }

        private void endDate_CloseUp(object sender, EventArgs e)
        {
            start = Convert.ToDateTime(startDate.Text).ToString("dd-MM-yyyy");
            end = Convert.ToDateTime(endDate.Text).ToString("dd-MM-yyyy");
            LoadData(start, end);
        }
    }
}
