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
    public partial class BillingForm : Form
    {
        Item _item;
        Billing _bill;
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
        string date;
        public BillingForm()
        {
            InitializeComponent();
            date = DateTime.Now.ToString("dd-MM-yyyy");
            LoadData(date, "All");
            Profile(Helper.OrgID);
        }
        private void Profile(string ID)
        {

            addressLbl.Text = Global._org.First(k => k.Id.Contains(ID)).Name + " " + Global._org.First(k => k.Id.Contains(ID)).Address + " " + Global._org.First(k => k.Id.Contains(ID)).Contact;
            Image img = Base64ToImage(Global._org.First(k => k.Id.Contains(ID)).Image);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            //Bitmap bps = new Bitmap(bmp, 50, 50);
            pictureBox1.Image = bmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        List<Item> _itemList = new List<Item>();
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
            foreach (var h in Global._sale.Where(g => g.No.Contains(no)))
            {
                if (h.Type == "Sale")
                {
                    t.Rows.Add(new object[] { b, Global._item.First(r => r.Id.Contains(h.ItemID)).Barcode, Global._item.First(r => r.Id.Contains(h.ItemID)).Name + Environment.NewLine + "PRICE:" + Global._stock.First(r => r.Id.Contains(h.ItemID)).Sale_price + Environment.NewLine + "MANUFACTURER :" + Global._item.First(r => r.Id.Contains(h.ItemID)).Manufacturer + Environment.NewLine + "EXPIRES:" + Global._stock.First(r => r.Id.Contains(h.ItemID)).Expire, h.Qty, Global._item.First(r => r.Id.Contains(h.ItemID)).Image });
                    double TotalCost = (Convert.ToDouble(h.Qty) * Convert.ToDouble(h.Price));
                    s.Rows.Add(new object[] { Global._item.First(r => r.Id.Contains(h.ItemID)).Name, h.Qty, Global._stock.First(r => r.Id.Contains(h.ItemID)).Sale_price, TotalCost.ToString("n0") });
                    TotalDictionary.Add(c++, TotalCost);
                }

                if (h.Type == "Purchase")
                {
                    t.Rows.Add(new object[] { b, Global._item.First(r => r.Id.Contains(h.ItemID)).Barcode, Global._item.First(r => r.Id.Contains(h.ItemID)).Name + Environment.NewLine + "PRICE:" + Global._stock.First(r => r.Id.Contains(h.ItemID)).Purchase_price + Environment.NewLine + "MANUFACTURER :" + Global._item.First(r => r.Id.Contains(h.ItemID)).Manufacturer + Environment.NewLine + "EXPIRES:" + Global._stock.First(r => r.Id.Contains(h.ItemID)).Expire, h.Qty, Global._item.First(r => r.Id.Contains(h.ItemID)).Image });
                    double TotalCost = (Convert.ToDouble(h.Qty) * Convert.ToDouble(h.Price));

                    s.Rows.Add(new object[] { Global._item.First(r => r.Id.Contains(h.ItemID)).Name, h.Qty, Global._stock.First(r => r.Id.Contains(h.ItemID)).Purchase_price, TotalCost.ToString("n0") });
                    TotalDictionary.Add(c++, TotalCost);
                }

            }
            noLbl.Text = no;
            dateLbl.Text = Global._billings.First(k => k.No.Contains(no)).Created;
            customerLbl.Text = Global._billings.First(k => k.No.Contains(no)).Reference;
            methodCbx.Text = Global._billings.First(k => k.No.Contains(no)).Method;
            balanceTxt.Text = (Convert.ToDouble(Global._billings.First(k => k.No.Contains(no)).Total)- Convert.ToDouble(Global._payment.Where(j=>j.No.Contains(no)).Sum(h=>Convert.ToDouble(h.Amount)))).ToString();
            amountTxt.Text = Global._payment.Where(j => j.No.Contains(no)).Sum(h => Convert.ToDouble(h.Amount)).ToString();
            totalLbl.Text = Global._billings.First(k => k.No.Contains(no)).Total;
            typeLbl.Text = Global._billings.First(k => k.No.Contains(no)).Type;

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
            date = Convert.ToDateTime(dateTimePicker1.Text).ToString("dd-MM-yyyy");
            LoadData(date, typeCbx.Text);
        }
        List<Billing> _billList = new List<Billing>();
        string Transactors = "";
        public void LoadData(string date, string type)
        {

            t = new DataTable();
            t.Columns.Add("Select");//1 
            t.Columns.Add("id");//1 
            t.Columns.Add("Date");//2                 
            t.Columns.Add("No");//3           
            t.Columns.Add("Total");//4 
            t.Columns.Add("Balance");//4 
           
            t.Columns.Add("Payment Date");//8       
            t.Columns.Add("Amount");//11
            t.Columns.Add("Method");//12
            t.Columns.Add("Transactors");//9 

            if (type == "All")
            {
                _billList = Billing.ListBilling().Where(g => g.Created.Contains(date)).ToList();
            }
            if (type == "")
            {
                _billList = Billing.ListBilling().Where(g => g.Created.Contains(date)).ToList();
            }
            if (type == "Sale")
            {
                _billList = Billing.ListBilling().Where(g => g.Created.Contains(date) && g.Type.Contains("Sale")).ToList();
            }
            if (type == "Purchase")
            {
                _billList = Billing.ListBilling().Where(g => g.Created.Contains(date) && g.Type.Contains("Purchase")).ToList();
            }
            SumDictionary.Clear();
            PaidDictionary.Clear();
            BalanceDictionary.Clear();
           
            foreach (Billing h in _billList)
            {
                try
                {
                    Transactors = Global._transactor.First(p => p.Id.Contains(h.TransactorID)).Name + " CONTACT: " + Global._transactor.First(p => p.Id.Contains(h.TransactorID)).Contact;
                }
                catch
                {
                    Transactors = h.TransactorID;
                }
                t.Rows.Add(new object[] { "Select", h.Id, h.Created, h.No, h.Total,(Convert.ToDouble( h.Total) - Global._payment.Where(m => m.No.Contains(h.No)).Sum(y=>Convert.ToDouble(y.Amount))), " " });
                double payments = 0;
                foreach (Payment k in Global._payment.Where(m=>m.No.Contains(h.No)))
                {
                   // PaymentDictionary.Add(h.Id, h.Total);
                    payments = payments + Convert.ToDouble(k.Amount);
                    t.Rows.Add(new object[] { "", "", "", "", "",  "", k.Created, k.Amount, k.Method, k.By });
                 
                    //PaidDictionary.Add(h.Id, h.Paid);
                    //BalanceDictionary.Add(h.Id, h.Balance);

                }
                SumDictionary.Add(h.Id, h.Total);
                PaidDictionary.Add(h.Id, h.Paid);
                BalanceDictionary.Add(h.Id, h.Balance);

            }
            itemGrid.DataSource = t;
            itemGrid.AllowUserToAddRows = false;
            itemGrid.Columns[1].Visible = false;
            t.Rows.Add(new object[] { "", "", "", "", "", "", "", "", "" });

            t.Rows.Add(new object[] { "", "Total", "", "", SumDictionary.Sum(m => Convert.ToDouble(m.Value)).ToString("n0"), PaidDictionary.Sum(m => Convert.ToDouble(m.Value)).ToString("n0"), "", BalanceDictionary.Sum(m => Convert.ToDouble(m.Value)).ToString("n0"), "" });
            itemGrid.Columns[0].DefaultCellStyle.BackColor = Color.Turquoise;
        }

        private void itemGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                LoadItem(itemGrid.Rows[e.RowIndex].Cells[3].Value.ToString());

            }
        }

        private void typeCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            date = Convert.ToDateTime(dateTimePicker1.Text).ToString("dd-MM-yyyy");
            LoadData(date, typeCbx.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("YES or NO?", "Are you sure you want to delete this transaction ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string Query = "DELETE from billing WHERE no ='" + noLbl.Text + "'";
                DBConnect.save(Query);
                string Query2 = "DELETE from sale WHERE no ='" + noLbl.Text + "'";
                DBConnect.save(Query2);
                MessageBox.Show("Information deleted");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        Payment _pay;
        private void button4_Click(object sender, EventArgs e)
        {
            if (paymentTxt.Text=="") {
                paymentTxt.BackColor = Color.Red;
                MessageBox.Show("No amount specified");
                return;

            }
            string ID = Guid.NewGuid().ToString();
            if (Convert.ToDouble(paymentTxt.Text) > 0)
            {
                _pay = new Payment(ID, noLbl.Text, methodCbx.Text, paymentTxt.Text, customerLbl.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.UserID, typeLbl.Text, Helper.StoreID);
                DBConnect.Insert(_pay);
                string Query = "UPDATE  billing SET balance = '"+newBalanceTxt.Text +"' WHERE no ='" + noLbl.Text + "'";
                DBConnect.save(Query);

                MessageBox.Show("Information saved");
                Global._payment.Add(_pay);
                LoadData(date, typeCbx.Text);
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
    }
}
