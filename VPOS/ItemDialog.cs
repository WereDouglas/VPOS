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
        WebCam webcam;

        private delegate void SetTextDeleg(string text);
        public ItemDialog()
        {
            InitializeComponent();
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
            autocomplete();
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

            MemoryStream stream = ImageToStream(imgCapture.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
            string fullimage = ImageToBase64(stream);
            _item = new Item(id, nameTxt.Text, codeTxt.Text, descriptionTxt.Text,manufactureTxt.Text,nationalityTxt.Text,barcodeTxt.Text,purchaseTxt.Text,saleTxt.Text,compositionTxt.Text,Convert.ToDateTime(expireDate.Text).ToString("dd-MM-yyyy"),categoryTxt.Text,formulationCbx.Text,barcodeTxt.Text,fullimage, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),"",Convert.ToDateTime(manufactureDate.Text).ToString("dd-MM-yyyy"),genericTxt.Text,"",qtyTxt.Text,minTxt.Text,Helper.OrgID,qtyTxt.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),"false");

            if (DBConnect.Insert(_item) != "")
            {
                double totalValue = Convert.ToDouble(qtyTxt.Text) * Convert.ToDouble(saleTxt.Text);
                _stk = new Stock(id, id, qtyTxt.Text, saleTxt.Text, purchaseTxt.Text, saleTxt.Text, totalValue.ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.UserID);
                DBConnect.Insert(_stk);
                //string query = "insert into transactor (id, transactorNo,contact,surname,lastname,email,dob,nationality,address,kin,kincontact,gender,created) values ('"+ id + "', '"+ transactorNoTxt.Text + "', '"+ contactTxt.Text + "', '" + surnameTxt.Text + "', '" + lastnameTxt.Text + "', '" + emailTxt.Text + "', '" +Convert.ToDateTime(dobdateTimePicker1.Text).ToString("dd-MM-yyyy") + "', '" + nationalityTxt.Text + "', '" + addressTxt.Text + "', '" + kinTxt.Text + "','" + kincontactTxt.Text + "', '" + genderCbx.Text + "','"+DateTime.Now.ToString("dd-MM-yyyy H:m:s")+"');";
                Global._item.Add(_item);


                _qty = new Quantity(id, id, "0", qtyTxt.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.UserID, DateTime.Now.ToString("dd-MM-yyyy"));
                DBConnect.Insert(_qty);
                MessageBox.Show("Information Saved");
                
            }
            else
            {


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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            barcodeTxt.Text = "";
            if (e.KeyChar == '\r')
            {
                barcodeTxt.Text = barcode;
                barcode = string.Empty;
            }
            barcode += e.KeyChar;
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
    }
}
