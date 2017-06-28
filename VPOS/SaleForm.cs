using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPOS.Model;

namespace VPOS
{
    public partial class SaleForm : Form
    {
        Item item;
        DataTable t;
        DataTable s;
        DataTable m;
        private string barcode = string.Empty;
        Dictionary<int, double> TotalDictionary = new Dictionary<int, double>();
        Dictionary<string, string> BarDictionary = new Dictionary<string, string>();
        Dictionary<string, string> CustomerDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ContactDictionary = new Dictionary<string, string>();
        NpgsqlDataReader Reader = null;
        private PrintDocument printDocument = new PrintDocument();
        private static String RECEIPT = Environment.CurrentDirectory + @"\comprovantes\comprovante.txt";
        private String stringToPrint = "";
        public SaleForm()
        {
            Global.LoadVital();
            InitializeComponent();
            dateLbl.Text = DateTime.Now.ToString("dd-MM-yyyy");
            int follow = Global.sale.Count();
            int next = follow + 1;
            noLbl.Text = Helper.Code + "-" + DateTime.Now.ToString("dd:MM:yyyy") + "-" + next;
            autocomplete();
            autocustomer();
            Profile(Helper.OrgID);

        }

        private void Profile(string ID)
        {

            addressLbl.Text = Global.org.First(k => k.Id.Contains(ID)).Name + " " + Global.org.First(k => k.Id.Contains(ID)).Address + " " + Global.org.First(k => k.Id.Contains(ID)).Contact;
            Image img = Base64ToImage(Global.org.First(k => k.Id.Contains(ID)).Image);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            //Bitmap bps = new Bitmap(bmp, 50, 50);
            pictureBox1.Image = bmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        Dictionary<int, double> TaxDictionary = new Dictionary<int, double>();

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
            s.Columns.Add("barcode");//1    
            s.Columns.Add("Quantity");//4              
            s.Columns.Add("Unit Cost");//5
            s.Columns.Add("Total Cost");//6  
            s.Columns.Add("Tax");//6          

            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Gray), 00, 00);
            }
            TotalDictionary.Clear();
            TaxDictionary.Clear();
            int c = 0;
            foreach (var h in SelectedItems)
            {
                try
                {
                    t.Rows.Add(new object[] { b, Global.item.First(r => r.Barcode.Contains(h.Key)).Barcode, Global.item.First(r => r.Barcode.Contains(h.Key)).Name + Environment.NewLine + "PRICE:" + Global.stock.First(r => r.Barcode.Contains(h.Key)).Sale_price + Environment.NewLine + "MANUFACTURER :" + Global.item.First(r => r.Barcode.Contains(h.Key)).Manufacturer + Environment.NewLine + "EXPIRES:" + Global.stock.First(r => r.Barcode.Contains(h.Key)).Expire, h.Value, Global.item.First(r => r.Barcode.Contains(h.Key)).Image, "Remove" });
                    double TotalCost = (Convert.ToDouble(h.Value) * Convert.ToDouble(Global.stock.First(r => r.Barcode.Contains(h.Key)).Sale_price));
                    double valueBeforeTax = 0;
                    double tax = 0;
                    if (Global.stock.First(r => r.Barcode.Contains(h.Key)).Tax != "0" || String.IsNullOrEmpty(Global.stock.First(r => r.Barcode.Contains(h.Key)).Tax))
                    {
                        valueBeforeTax = Math.Round((TotalCost) * (100 / (100 + Convert.ToDouble(Global.stock.First(r => r.Barcode.Contains(h.Key)).Tax))), 0);
                        tax = TotalCost - valueBeforeTax;
                    }

                    s.Rows.Add(new object[] { Global.item.First(r => r.Barcode.Contains(h.Key)).Name, Global.item.First(r => r.Barcode.Contains(h.Key)).Barcode, h.Value, Global.stock.First(r => r.Barcode.Contains(h.Key)).Sale_price, TotalCost.ToString("n0"), tax });
                    TotalDictionary.Add(c++, TotalCost);
                    TaxDictionary.Add(c++, tax);
                }
                catch {

                   
                }
            }
            // dtGrid2.DataSource = t;
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



            saleGrid.DataSource = s;
            saleGrid.AllowUserToAddRows = false;
            totalLbl.Text = TotalDictionary.Sum(p => Convert.ToDouble(p.Value)).ToString("n0");
            vatAmountTxt.Text = TaxDictionary.Sum(p => Convert.ToDouble(p.Value)).ToString("n0");
            saleGrid.Columns["barcode"].Visible = false;
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

        private void panel2Paint(object sender, PaintEventArgs e)
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
            foreach (Item p in Global.item)
            {
                if (!ItemDictionary.ContainsKey(p.Barcode))
                {
                    AutoItem.Add(p.Name);
                    AutoItem2.Add(p.Barcode);
                    ItemDictionary.Add(p.Barcode, p.Name);
                    BarDictionary.Add(p.Barcode, p.Name);
                }
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
            foreach (Customer p in Global.customer)
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
        List<Item> itemList = new List<Item>();

        public void LoadItem(string name)
        {
            itemList = new List<Item>();
            listView1.Clear();
            m = new DataTable();
            m.Columns.Add("Name");//2  
            m.Columns.Add("id");//1     
            m.Columns.Add("Department");//4         
            m.Columns.Add("Description");//5
            m.Columns.Add("Add");//6         
            if (name == "")
            {
                itemList = Global.item;
            }
            else
            {
                itemList = Global.item.Where(e => e.Name.Contains(name)).ToList();
            }
            ImageList il = new ImageList();
            foreach (Item h in itemList)
            {
                Image img = Base64ToImage(h.Image);

                il.Images.Add(img);
            }
            il.ImageSize = new Size(60, 60);
            int count = 0;
            listView1.LargeImageList = il;
            foreach (Item h in itemList)
            {
                try
                {
                    m.Rows.Add(new object[] { h.Name + Environment.NewLine + "PRICE:" + Global.stock.First(r => r.ItemID.Contains(h.Id)).Purchase_price + Environment.NewLine + "MANUFACTURER :" + h.Manufacturer, h.Barcode, h.Category, h.Description, "Add" });
                }
                catch { }
                ListViewItem lst = new ListViewItem();
                lst.Text = h.Name + "\n\r" + h.Category + "\n\r" + h.Description;
                lst.ForeColor = Color.DimGray;
                lst.Tag = h.Barcode;
                lst.ImageIndex = count++;
                listView1.Items.Add(lst);


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

        private void dtGrid2CellContent_Click(object sender, DataGridViewCellEventArgs e)
        {

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
        string customerID = "Walk in";
        Customer customer;

        private void contactTxt_Leave(object sender, EventArgs e)
        {
            customerID = "";
            customerLbl.Text = "";
            try
            {                  // var value = ItemDictionary.FirstOrDefault(x => x.Value.Contains(nameTxt.Text)).Key;
                customerID = CustomerDictionary[customerTxt.Text];
                customerLbl.Text = "CUSTOMER :" + customerTxt.Text + Environment.NewLine + "CONTACT: " + Global.customer.First(y => y.Id.Contains(customerID)).Contact + Environment.NewLine + "ADDRESS " + Global.customer.First(y => y.Id.Contains(customerID)).Address;
            }
            catch
            {
                customerID = nameTxt.Text;
            }
            try
            {                  // var value = ItemDictionary.FirstOrDefault(x => x.Value.Contains(nameTxt.Text)).Key;
                customerID = ContactDictionary[contactTxt.Text];
                customerLbl.Text = "CUSTOMER :" + customerTxt.Text + Environment.NewLine + "CONTACT: " + Global.customer.First(y => y.Id.Contains(customerID)).Contact + Environment.NewLine + "ADDRESS " + Global.customer.First(y => y.Id.Contains(customerID)).Address;
            }
            catch
            {
                customerID = contactTxt.Text;
            }
            try
            {
                Image img = Base64ToImage(Global.customer.First(y => y.Id.Contains(customerID)).Image);
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
                //Bitmap bps = new Bitmap(bmp, 50, 50);
                imgCapture.Image = bmp;
                imgCapture.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch { }
            if (Global.customer.Select(k => k.Contact.Contains(contactTxt.Text)).Count() < 1)
            {
                if (MessageBox.Show("YES or No?", "This customer does not exist would you like to save this information ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    customerID = Guid.NewGuid().ToString();
                    MemoryStream stream = ImageToStream(imgCapture.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
                    string fullimage = ImageToBase64(stream);
                    Customer customer = new Customer(customerID, nameTxt.Text, contactTxt.Text, fullimage, "", DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID);

                    if (DBConnect.Insert(customer) != "")
                    {
                        //string query = "insert into customer (id, customerNo,contact,surname,lastname,email,dob,nationality,address,kin,kincontact,gender,created) values ('"+ id + "', '"+ customerNoTxt.Text + "', '"+ contactTxt.Text + "', '" + surnameTxt.Text + "', '" + lastnameTxt.Text + "', '" + emailTxt.Text + "', '" +Convert.ToDateTime(dobdateTimePicker1.Text).ToString("dd-MM-yyyy") + "', '" + nationalityTxt.Text + "', '" + addressTxt.Text + "', '" + kinTxt.Text + "','" + kincontactTxt.Text + "', '" + genderCbx.Text + "','"+DateTime.Now.ToString("dd-MM-yyyy H:m:s")+"');";
                        Global.customer.Add(customer);
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
        Billing billing;

        Sale sale;
        Payment pay;
        private void button1_Click(object sender, EventArgs e)
        {
            string ID = Guid.NewGuid().ToString();
            string paid = "";

            if (balanceTxt.Text == "0")
            {
                paid = "Yes";
            }
            if (methodCbx.Text == "")
            {
                methodCbx.BackColor = Color.Red;
                MessageBox.Show("Please select the mode/method of payment !");
                return;
            }           

            if (Global.sale.Where(g => g.No.Contains(noLbl.Text)).Count() > 0)
            {
               noLbl.BackColor = Color.Red;
                MessageBox.Show("Transaction already saved !");
                return;
            }
            if (Convert.ToDouble(amountTxt.Text) > 0)
            {
                Invoice inv = new Invoice(ID, noLbl.Text, methodCbx.Text, Convert.ToDouble(amountTxt.Text).ToString(), customerID, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.UserID, "Sale", Helper.StoreID, customerID, Convert.ToDouble(balanceTxt.Text).ToString());
                DBConnect.Insert(inv);
                Global.invoices.Add(inv);
            }

            foreach (var h in SelectedItems)
            {
                double TotalCost = (Convert.ToDouble(h.Value) * Convert.ToDouble(Global.stock.First(r => r.Barcode.Contains(h.Key)).Purchase_price));

                string IDs = Guid.NewGuid().ToString();
                double tax = 0;
                if (Global.stock.First(r => r.Barcode.Contains(h.Key)).Tax != "0" || String.IsNullOrEmpty(Global.stock.First(r => r.Barcode.Contains(h.Key)).Tax))
                {
                    tax = Math.Round((TotalCost) * (100 / (100 + Convert.ToDouble(Global.stock.First(r => r.Barcode.Contains(h.Key)).Tax))), 0);
                }
                Sale sale = new Sale(IDs, noLbl.Text, Global.item.First(r => r.Barcode.Contains(h.Key)).Id, h.Value.ToString(), dateLbl.Text, Global.stock.First(r => r.Barcode.Contains(h.Key)).Sale_price, TotalCost.ToString(), TotalCost.ToString(), methodCbx.Text, "0", customerID, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), tax.ToString(), Helper.StoreID, Helper.OrgID, Helper.UserID);

                double cQty = Convert.ToDouble(Global.stock.First(g => g.Id.Contains(Global.stock.First(r => r.Barcode.Contains(h.Key)).Id)).Quantity);
                double newQty = cQty - h.Value;

                string Query2 = "UPDATE stock SET  quantity='" + newQty + "',created = '" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + "'  WHERE itemID ='" + Global.item.First(r => r.Barcode.Contains(h.Key)).Id + "'";
                DBConnect.save(Query2);

                DBConnect.Insert(sale);
                Global.sale.Add(sale);
            }
            try
            {
                print();
            }
            catch { }
            MessageBox.Show("Information Saved");


        }
        PrintDocument pdoc = null;
        int ticketNo;
        DateTime TicketDate;
        String Source, Destination, DrawnBy;
        float Amount;
        public void print()
        {
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 12);


            PaperSize psize = new PaperSize("Custom", 100, 200);
            //ps.DefaultPageSettings.PaperSize = psize;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 820;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;

            pdoc.PrintPage += new PrintPageEventHandler(pdocPrintPage);

            //DialogResult result = pd.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            PrintPreviewDialog pp = new PrintPreviewDialog();
            pp.Document = pdoc;
            //result = pp.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            pdoc.Print();
            //}
            //}

        }
        void pdocPrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            graphics.DrawString("VugaPOS", new Font("Courier New", 12),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Ticket No:" + noLbl.Text,
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Ticket Date :" + DateTime.Now.ToString("dd-MM-yyyy"),
                     new Font("Courier New", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "------------------------------------------";
            foreach (var h in SelectedItems)
            {
                double TotalCost = (Convert.ToDouble(h.Value) * Convert.ToDouble(Global.stock.First(r => r.Barcode.Contains(h.Key)).Purchase_price));

                Offset = Offset + 10;
                graphics.DrawString(h.Value + " " + Global.item.First(r => r.Barcode.Contains(h.Key)).Name + " " + Global.stock.First(r => r.Barcode.Contains(h.Key)).Sale_price + " " + TotalCost.ToString("n0"), new Font("Courier New", 8),
                         new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;

            }
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            String Source = Helper.Username;
            graphics.DrawString("From " + Source + " To " + Destination, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            String Grosstotal = "AMOUNT:" + totalLbl.Text;

            Offset = Offset + 20;
            underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(Grosstotal, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

        }

        private void amountTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {

                balanceTxt.Text = (Convert.ToDouble(totalLbl.Text) - Convert.ToDouble(amountTxt.Text)).ToString();

            }
            catch { }
            try
            {
                vatAmountTxt.Text = (18 * Convert.ToDouble(totalLbl.Text) / 100).ToString();
                beforeTxt.Text = totalLbl.Text;
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            // Acquire SelectedItems reference.
            var selectedItems = listView1.SelectedItems;
            if (selectedItems.Count > 0)
            {
                // Display text of first item selected.
                this.Text = selectedItems[0].Text;
                //  MessageBox.Show(selectedItems[0].Tag.ToString());


                barcodeTxt.Text = "";
                barcode = (selectedItems[0].Tag.ToString()).Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace(" ", "");
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
            else
            {
                // Display default string.
                this.Text = "Empty";
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void saleGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3 || e.ColumnIndex == 2 || e.ColumnIndex == 1 || e.ColumnIndex == 0)
                {
                    barcode = (saleGrid.Rows[e.RowIndex].Cells["barcode"].Value.ToString()).Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace(" ", "");
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
            catch { }

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
                Close();
            }
        }

        private void itemGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
