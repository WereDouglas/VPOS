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
    public partial class PurchaseForm : Form
    {
        Item _item;
        DataTable t;
        DataTable s;
        DataTable m;
        private string barcode = string.Empty;
        Dictionary<int, double> TotalDictionary = new Dictionary<int, double>();
        Dictionary<string, string> BarDictionary = new Dictionary<string, string>();
        Dictionary<string, string> CustomerDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ContactDictionary = new Dictionary<string, string>();
        public PurchaseForm()
        {
            Global.LoadVital();
            InitializeComponent();
            dateLbl.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //noLbl.Text = DateTime.Now.ToString("ddHHs");
            autocomplete();
            autocustomer();
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
        public void LoadItem()
        {

            t = new DataTable();
            s = new DataTable();

            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));//   0
            t.Columns.Add("id");//1       
            t.Columns.Add("Name");//2  
            t.Columns.Add("Quantity");//3        
            t.Columns.Add("image");//4
            t.Columns.Add("Remove");//5
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
            foreach (var h in SelectedItems)
            {
                t.Rows.Add(new object[] { b, Global._item.First(r => r.Barcode.Contains(h.Key)).Barcode, Global._item.First(r => r.Barcode.Contains(h.Key)).Name + Environment.NewLine + "PRICE:" + Global._item.First(r => r.Barcode.Contains(h.Key)).Purchase_price + Environment.NewLine + "MANUFACTURER :" + Global._item.First(r => r.Barcode.Contains(h.Key)).Manufacturer + Environment.NewLine + "EXPIRES:" + Global._item.First(r => r.Barcode.Contains(h.Key)).Expire, h.Value, Global._item.First(r => r.Barcode.Contains(h.Key)).Image, "Remove" });
                double TotalCost = (Convert.ToDouble(h.Value) * Convert.ToDouble(Global._item.First(r => r.Barcode.Contains(h.Key)).Purchase_price));

                s.Rows.Add(new object[] { Global._item.First(r => r.Barcode.Contains(h.Key)).Name, h.Value, Global._item.First(r => r.Barcode.Contains(h.Key)).Purchase_price, TotalCost.ToString("n0") });
                TotalDictionary.Add(c++, TotalCost);
            }
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
            dtGrid2.Columns[5].DefaultCellStyle.BackColor = Color.Orange;
            dtGrid2.RowTemplate.Height = 90;
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
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }


        private void SaleForm_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
        Dictionary<string, int> SelectedItems = new Dictionary<string, int>();
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            barcodeTxt.Text = "";
            if (e.KeyChar == '\r')
            {
                barcodeTxt.Text = barcode;
                barcode = barcode.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace(" ", "");
                if (SelectedItems.ContainsKey(barcode))
                {
                    int count = SelectedItems[barcode];
                    SelectedItems.Remove(barcode);
                    SelectedItems.Add(barcode, (count + 1));
                    LoadItem();
                }
                else
                {

                    SelectedItems.Add(barcode, 1);
                    LoadItem();
                }

                barcode = string.Empty;
            }
            barcode += e.KeyChar;
        }

        private void nameTxt_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        Dictionary<string, string> ItemDictionary = new Dictionary<string, string>();
        private void autocomplete()
        {
            AutoCompleteStringCollection AutoItem = new AutoCompleteStringCollection();
            AutoCompleteStringCollection AutoItem2 = new AutoCompleteStringCollection();
            foreach (Item p in Global._item)
            {
                AutoItem.Add(p.Name);
                AutoItem2.Add(p.Barcode);
                ItemDictionary.Add(p.Barcode, p.Name);
                BarDictionary.Add(p.Barcode, p.Name);
            }

            nameTxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            nameTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            nameTxt.AutoCompleteCustomSource = AutoItem;

            barSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
            barSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            barSearch.AutoCompleteCustomSource = AutoItem2;
            LoadItem("");
        }
        private void autocustomer()
        {
            AutoCompleteStringCollection AutoItem = new AutoCompleteStringCollection();
            AutoCompleteStringCollection AutoItem2 = new AutoCompleteStringCollection();
            foreach (Transactor p in Global._transactor)
            {
                AutoItem.Add(p.Name);
                AutoItem2.Add(p.Contact);
                CustomerDictionary.Add(p.Name, p.Id);
                ContactDictionary.Add(p.Contact, p.Id);
            }

            customerTxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            customerTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            customerTxt.AutoCompleteCustomSource = AutoItem;

            contactTxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            contactTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            contactTxt.AutoCompleteCustomSource = AutoItem2;

        }
        List<Item> _itemList = new List<Item>();

        public void LoadItem(string name)
        {
            m = new DataTable();
            m.Columns.Add("Name");//2  
            m.Columns.Add("id");//1     
            m.Columns.Add("Department");//4         
            m.Columns.Add("Description");//5
            m.Columns.Add("Add");//6         
            if (name == "")
            {

                _itemList = Global._item;
            }
            else
            {

                _itemList = Global._item.Where(e => e.Name.Contains(name)).ToList();
            }
            foreach (Item h in _itemList)
            {
                m.Rows.Add(new object[] { h.Name + Environment.NewLine + "COST:" + h.Purchase_price + Environment.NewLine + "MANUFACTURER :" + h.Manufacturer, h.Barcode, h.Department, h.Description, "Add" });

            }
            itemGrid.DataSource = m;
            itemGrid.AllowUserToAddRows = false;
            itemGrid.Columns[4].DefaultCellStyle.BackColor = Color.Turquoise;
            itemGrid.Columns[1].Visible = false;

        }

        private void nameTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //   var value = ItemDictionary.FirstOrDefault(x => x.Value.Contains(nameTxt.Text)).Key;
                // var value = ItemDictionary[diagnosisCbx.Text];
                LoadItem(nameTxt.Text);
            }
            catch
            {

            }
        }

        private void nameTxt_Leave(object sender, EventArgs e)
        {
            nameTxt_TextChanged(null, null);
        }

        private void itemGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                barcodeTxt.Text = "";
                barcode = itemGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                barcodeTxt.Text = barcode;
                barcode = barcode.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace(" ", "");
                if (SelectedItems.ContainsKey(barcode))
                {
                    int count = SelectedItems[barcode];
                    SelectedItems.Remove(barcode);
                    SelectedItems.Add(barcode, (count + 1));
                    LoadItem();
                }
                else
                {
                    SelectedItems.Add(barcode, 1);
                    LoadItem();
                }

                barcode = string.Empty;
            }

        }

        private void dtGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtGrid2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 || e.ColumnIndex == 0)
            {
                barcode = (dtGrid2.Rows[e.RowIndex].Cells[1].Value.ToString()).Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace(" ", "");
                if (SelectedItems.ContainsKey(barcode))
                {
                    int count = SelectedItems[barcode];
                    if (count > 1)
                    {
                        SelectedItems.Remove(barcode);
                        SelectedItems.Add(barcode, (count - 1));
                        LoadItem();

                    }
                    else
                    {
                        SelectedItems.Remove(barcode);
                        LoadItem();
                    }

                }
            }

        }

        private void barSearch_TextChanged(object sender, EventArgs e)
        {

            try
            {
                var value = BarDictionary.FirstOrDefault(x => x.Value.Contains(barSearch.Text)).Value;
                // var value = BarDictionary[barSearch.Text];
                LoadItem(value);
            }
            catch
            {

            }
        }
        string customerID="Walk in";
        Transactor _transactor;

        private void contactTxt_Leave(object sender, EventArgs e)
        {
            customerID = "";
            customerLbl.Text = "";
            try
            {                  // var value = ItemDictionary.FirstOrDefault(x => x.Value.Contains(nameTxt.Text)).Key;
                customerID = CustomerDictionary[customerTxt.Text];
                customerLbl.Text = "CUSTOMER :" + customerTxt.Text + Environment.NewLine + "CONTACT: " + Global._transactor.First(y => y.Id.Contains(customerID)).Contact + Environment.NewLine + "ADDRESS " + Global._transactor.First(y => y.Id.Contains(customerID)).Address;
            }
            catch
            {

            }
            try
            {                  // var value = ItemDictionary.FirstOrDefault(x => x.Value.Contains(nameTxt.Text)).Key;
                customerID = ContactDictionary[contactTxt.Text];
                customerLbl.Text = "CUSTOMER :" + customerTxt.Text + Environment.NewLine + "CONTACT: " + Global._transactor.First(y => y.Id.Contains(customerID)).Contact + Environment.NewLine + "ADDRESS " + Global._transactor.First(y => y.Id.Contains(customerID)).Address;
            }
            catch
            {

            }
            try
            {
                Image img = Base64ToImage(Global._transactor.First(y => y.Id.Contains(customerID)).Image);
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
                //Bitmap bps = new Bitmap(bmp, 50, 50);
                imgCapture.Image = bmp;
                imgCapture.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch { }
            if (Global._transactor.Select(k => k.Contact.Contains(contactTxt.Text)).Count() < 1)
            {
                if (MessageBox.Show("YES or No?", "This customer does not exist would you like to save this information ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    customerID = Guid.NewGuid().ToString();

                    MemoryStream stream = ImageToStream(imgCapture.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
                    string fullimage = ImageToBase64(stream);
                    _transactor = new Transactor(customerID, nameTxt.Text, contactTxt.Text, fullimage, "Customer", DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), contactTxt.Text, Helper.OrgID);

                    if (DBConnect.Insert(_transactor) != "")
                    {
                        //string query = "insert into transactor (id, transactorNo,contact,surname,lastname,email,dob,nationality,address,kin,kincontact,gender,created) values ('"+ id + "', '"+ transactorNoTxt.Text + "', '"+ contactTxt.Text + "', '" + surnameTxt.Text + "', '" + lastnameTxt.Text + "', '" + emailTxt.Text + "', '" +Convert.ToDateTime(dobdateTimePicker1.Text).ToString("dd-MM-yyyy") + "', '" + nationalityTxt.Text + "', '" + addressTxt.Text + "', '" + kinTxt.Text + "','" + kincontactTxt.Text + "', '" + genderCbx.Text + "','"+DateTime.Now.ToString("dd-MM-yyyy H:m:s")+"');";
                        Global._transactor.Add(_transactor);
                        MessageBox.Show("Information Saved");

                    }

                }
            }
        }

        public string ImageToBase64(MemoryStream images)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromStream(images))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        public MemoryStream ImageToStream(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, format);
            return ms;
        }
        Billing _billing;
        Sale _sale;
        Payment _pay;
        private void button1_Click(object sender, EventArgs e)
        {
            string ID = Guid.NewGuid().ToString();
            string paid = "";

            if (balanceTxt.Text == "0")
            {
                paid = "Yes";
            }
            if (Helper.Exists("billing","no",invoiceTxt.Text)) {

                MessageBox.Show("This transaction is already saved !");
                return;
            }
            if (String.IsNullOrEmpty(invoiceTxt.Text))
            {
                invoiceTxt.BackColor = Color.Red;
                MessageBox.Show("Please input the Invoice number !");
                return;
            }
            if (methodCbx.Text =="")
            {
                methodCbx.BackColor = Color.Red;
                MessageBox.Show("Please select the mode/method of payment !");
                return;
            }
            _billing = new Billing(ID, invoiceTxt.Text, "",amountTxt.Text, methodCbx.Text, refTxt.Text, totalLbl.Text, balanceTxt.Text, "", contactTxt.Text, customerID, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),"Purchase",Helper.OrgID,Helper.UserID,vatAmountTxt.Text);

            if (DBConnect.Insert(_billing) != "")
            {
                if (Convert.ToDouble(amountTxt.Text)>0) {
                    _pay = new Payment(ID, invoiceTxt.Text, methodCbx.Text, amountTxt.Text, customerID, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.UserID, "Purchase");
                    DBConnect.Insert(_pay);
                }
                Global._billings.Add(_billing);
                foreach (var h in SelectedItems)
                {
                    double TotalCost = (Convert.ToDouble(h.Value) * Convert.ToDouble(Global._item.First(r => r.Barcode.Contains(h.Key)).Purchase_price));

                    string IDs = Guid.NewGuid().ToString();
                    _sale = new Sale(IDs, invoiceTxt.Text, Global._item.First(r => r.Barcode.Contains(h.Key)).Id, h.Value.ToString(), dateLbl.Text, Global._item.First(r => r.Barcode.Contains(h.Key)).Purchase_price,"Purchase", DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),Helper.OrgID, Helper.UserID,TotalCost.ToString());

                    double cQty = Convert.ToDouble(Global._item.First(g => g.Id.Contains(Global._item.First(r => r.Barcode.Contains(h.Key)).Id)).Quantity);
                    double newQty = cQty + h.Value;

                    string Query2 = "UPDATE item SET  quantity='" + newQty + "',created = '" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + "'  WHERE id ='" + Global._item.First(r => r.Barcode.Contains(h.Key)).Id + "'";
                    DBConnect.save(Query2);
                    DBConnect.Insert(_sale);
                    Global._sale.Add(_sale);
                }
                MessageBox.Show("Information Saved");

            }
        }

        private void amountTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {

                balanceTxt.Text = (Convert.ToDouble(totalLbl.Text) - Convert.ToDouble(amountTxt.Text)).ToString();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("YES or NO?", "Are you sure you want to delete this transaction ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string Query = "DELETE from billing WHERE no ='" + invoiceTxt.Text + "'";
                DBConnect.save(Query);
                string Query2 = "DELETE from sale WHERE no ='" + invoiceTxt.Text + "'";
                DBConnect.save(Query2);
                MessageBox.Show("Information deleted");
            }
        }
    }
}
