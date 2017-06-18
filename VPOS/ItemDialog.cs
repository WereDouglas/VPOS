using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPOS.Model;

namespace VPOS
{
    public partial class ItemDialog : Form
    {
        SerialPort _serialPort;
        private Dictionary<TextBox, TextBox> TextBoxOrder = new Dictionary<TextBox, TextBox>();
        Item _item;
        Stock _stock;
        WebCam webcam;
        string ItemID = "";
        private delegate void SetTextDeleg(string text);
        Dictionary<string, string> storeDictionary = new Dictionary<string, string>();
        public ItemDialog(string itemID)
        {
            InitializeComponent();
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
            autocomplete();
            if (!String.IsNullOrEmpty(itemID))
            {
                ItemID = itemID;
                LoadItem(ItemID);
            }
            foreach (Store s in Global._store)
            {
                storeCbx.Items.Add(s.Name);
                storeDictionary.Add(s.Name, s.Id);
            }

        }
        private void LoadItem(string ID)
        {
            //  _item = new Item(Helper.OrgID, qtyTxt.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), "false", taxTxt.Text);

            nameTxt.Text = Global._item.First(k => k.Id.Contains(ID)).Name;
            codeTxt.Text = Global._item.First(k => k.Id.Contains(ID)).Code;
            descriptionTxt.Text = Global._item.First(k => k.Id.Contains(ID)).Description;
            manufactureTxt.Text = Global._item.First(k => k.Id.Contains(ID)).Manufacturer;
            nationalityTxt.Text = Global._item.First(k => k.Id.Contains(ID)).Country;
            barcodeTxt.Text = Global._item.First(k => k.Id.Contains(ID)).Barcode;
            purchaseTxt.Text = Global._stock.First(k => k.ItemID.Contains(ID)).Purchase_price;
            saleTxt.Text = Global._stock.First(k => k.Id.Contains(ID)).Sale_price;
            compositionTxt.Text = Global._item.First(k => k.Id.Contains(ID)).Composition;
            expireDate.Text = Global._stock.First(k => k.Id.Contains(ID)).Expire;
            categoryTxt.Text = Global._item.First(k => k.Id.Contains(ID)).Category;
            formulationCbx.Text = Global._stock.First(k => k.Id.Contains(ID)).Packing;
            unitsTxt.Text = Global._stock.First(k => k.Id.Contains(ID)).Units;
            manufactureDate.Text = Global._stock.First(k => k.Id.Contains(ID)).Date_manufactured;
            genericTxt.Text = Global._item.First(k => k.Id.Contains(ID)).Generic;
            qtyTxt.Text = Global._stock.First(k => k.Id.Contains(ID)).Quantity;
            minTxt.Text = Global._stock.First(k => k.Id.Contains(ID)).Min_qty;
            taxTxt.Text = Global._stock.First(k => k.Id.Contains(ID)).Tax;
            takingTxt.Text = Global._stock.First(k => k.Id.Contains(ID)).Taking;


            Image img = Base64ToImage(Global._item.First(k => k.Id.Contains(ID)).Image);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            //Bitmap bps = new Bitmap(bmp, 50, 50);
            imgCapture.Image = bmp;
            imgCapture.SizeMode = PictureBoxSizeMode.StretchImage;
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
        public MemoryStream ImageToStream(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, format);
            return ms;
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        private void autocomplete()
        {
            AutoCompleteStringCollection AutoItem = new AutoCompleteStringCollection();
            for (int w = 0; w < CountryArrays.Names.Count(); w++)
            {

                AutoItem.Add(CountryArrays.Names[w]);

            }

            nationalityTxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            nationalityTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            nationalityTxt.AutoCompleteCustomSource = AutoItem;
            foreach (Category c in Global._category)
            {
                categoryTxt.Items.Add(c.Name);
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void imgVideo_Click(object sender, EventArgs e)
        {

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
        Quantity _qty;
        List<Quantity> _qtyList;
        Stock _stk;

        private void button2_Click(object sender, EventArgs e)
        {
            if (nameTxt.Text == "")
            {
                nameTxt.BackColor = Color.Red;
                return;
            }
            if (categoryTxt.Text == "")
            {
                categoryTxt.BackColor = Color.Red;
                return;
            }
            if (codeTxt.Text == "")
            {
                codeTxt.BackColor = Color.Red;
                return;
            }
            if (saleTxt.Text == "")
            {
                saleTxt.BackColor = Color.Red;
                return;
            }
            string id = Guid.NewGuid().ToString();
            if (barcodeTxt.Text == "")
            {
                barcodeTxt.Text = id;
            }
            if (Helper.StoreID == "")
            {
                storeCbx.BackColor = Color.Red;
                return;
            }

            MemoryStream stream = ImageToStream(imgCapture.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
            string fullimage = ImageToBase64(stream);
            _item = new Item(id, nameTxt.Text,genericTxt.Text,codeTxt.Text, descriptionTxt.Text, manufactureTxt.Text, nationalityTxt.Text,compositionTxt.Text,categoryTxt.Text, barcodeTxt.Text,fullimage, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), strengthTxt.Text, Helper.OrgID, "false");
            int index = Global._item.FindIndex(g => g.Name.Contains(nameTxt.Text));
            if (index >= 0)
            {
                MessageBox.Show("Item of the same exact name already exists ");
                nameTxt.BackColor = Color.Red;
                return;
            }

            int bar = Global._item.FindIndex(g => g.Barcode.Contains(barcodeTxt.Text));
            if (bar >= 0)
            {
                MessageBox.Show("Item of the same exact bar code already exists");
                barcodeTxt.BackColor = Color.Red;
                return;
            }
            if (DBConnect.Insert(_item) != "")
            {
                double totalValue = Convert.ToDouble(qtyTxt.Text) * Convert.ToDouble(saleTxt.Text);
                _stk = new Stock(id, id, qtyTxt.Text, saleTxt.Text, purchaseTxt.Text, saleTxt.Text, totalValue.ToString(),batchTxt.Text, Convert.ToDateTime(expireDate.Text).ToString("dd-MM-yyyy"),formulationCbx.Text,unitsTxt.Text,barcodeTxt.Text, Convert.ToDateTime(manufactureDate.Text).ToString("dd-MM-yyyy"),qtyTxt.Text,minTxt.Text,qtyTxt.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),taxTxt.Text,promoPriceTxt.Text, Convert.ToDateTime(promoStart.Text).ToString("dd-MM-yyyy"), Convert.ToDateTime(promoEnd.Text).ToString("dd-MM-yyyy"), DateTime.Now.ToString("dd-MM-yyyy H:m:s"),Helper.StoreID, Helper.OrgID, Helper.UserID);
                DBConnect.Insert(_stk);
                //string query = "insert into transactor (id, transactorNo,contact,surname,lastname,email,dob,nationality,address,kin,kincontact,gender,created) values ('"+ id + "', '"+ transactorNoTxt.Text + "', '"+ contactTxt.Text + "', '" + surnameTxt.Text + "', '" + lastnameTxt.Text + "', '" + emailTxt.Text + "', '" +Convert.ToDateTime(dobdateTimePicker1.Text).ToString("dd-MM-yyyy") + "', '" + nationalityTxt.Text + "', '" + addressTxt.Text + "', '" + kinTxt.Text + "','" + kincontactTxt.Text + "', '" + genderCbx.Text + "','"+DateTime.Now.ToString("dd-MM-yyyy H:m:s")+"');";
                Global._item.Add(_item);
                Global._stock.Add(_stk);
                _qty = new Quantity(id, id, "0", qtyTxt.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.UserID, DateTime.Now.ToString("dd-MM-yyyy"), Helper.StoreID);
                DBConnect.Insert(_qty);
                MessageBox.Show("Information Saved");
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Error Inserting data ");
                return;
            }

            nameTxt.Text = "";
            codeTxt.Text = "";
        }
        private string barcode = string.Empty;
        private void ItemDialog_Load(object sender, EventArgs e)
        {
            //_serialPort = new SerialPort("COM10", 19200, Parity.None, 8, StopBits.One);
            //_serialPort.Handshake = Handshake.None;
            //_serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            //_serialPort.ReadTimeout = 500;
            //_serialPort.WriteTimeout = 500;
            //_serialPort.Open();
        }
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            string data = _serialPort.ReadLine();
            this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
        }

        private void si_DataReceived(string data)
        {
            barcodeTxt.Text = data.Trim();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();

                _serialPort.Write("SI\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
            }
        }

        private void ItemDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void ItemDialog_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }



        private void bntStart_Click(object sender, EventArgs e)
        {
            webcam.Start();
        }

        private void bntStop_Click(object sender, EventArgs e)
        {
            webcam.Stop();
        }

        private void bntContinue_Click(object sender, EventArgs e)
        {
            webcam.Continue();
        }

        private void bntCapture_Click(object sender, EventArgs e)
        {
            imgCapture.Image = imgVideo.Image;
            imgCapture.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                imgCapture.Image = new Bitmap(open.FileName);
                imgCapture.SizeMode = PictureBoxSizeMode.StretchImage;
                fileUrlTxtBx.Text = open.FileName;
            }
        }
        string storeID;
        private void storeCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                storeID = "";
                storeID = storeDictionary[storeCbx.Text];
            }
            catch { }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            MemoryStream stream = ImageToStream(imgCapture.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
            string fullimage = ImageToBase64(stream);
            _item = new Item(ItemID, nameTxt.Text, genericTxt.Text, codeTxt.Text, descriptionTxt.Text, manufactureTxt.Text, nationalityTxt.Text, compositionTxt.Text, categoryTxt.Text, barcodeTxt.Text, fullimage, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), strengthTxt.Text, Helper.OrgID, "false");

            if (ItemID != "")
            {
                DBConnect.Update(_item, ItemID);
              
                Global._item.RemoveAll(x => x.Id == ItemID);
                Global._item.Add(_item);
                MessageBox.Show("Information Updated ");
                this.DialogResult = DialogResult.OK;
                this.Dispose();
                MerchandiseForm frm = new MerchandiseForm();
                frm.MdiParent = MainForm.ActiveForm;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
        }

        private void categoryTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Image img = Base64ToImage(Global._category.First(k => k.Name.Contains(categoryTxt.Text)).Image);
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
                //Bitmap bps = new Bitmap(bmp, 50, 50);
                imgCapture.Image = bmp;
                imgCapture.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch { }
        }
    }
}
