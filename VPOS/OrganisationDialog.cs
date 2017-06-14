using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPOS.Model;

namespace VPOS
{
    public partial class OrganisationDialog : Form
    {
        Organisation _org;
        WebCam webcam;
        string OrgID = "";
        DataTable t;
        Dictionary<string, string> storeDictionary = new Dictionary<string, string>();
        public OrganisationDialog(string orgID)
        {
            InitializeComponent();
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
            autocomplete();
        
            if (orgID != "")
            {
                OrgID = orgID;
                Profile(OrgID);
            }
            LoadStores();
        }
        private void LoadStores() {

            foreach (Store s in Global._store)
            {
                storeCbx.Items.Add(s.Name);
                storeDictionary.Add(s.Name, s.Id);
            }


        }
        private void Profile(string ID)
        {

            nameTxt.Text = Global._org.First(k => k.Id.Contains(ID)).Name;
            codeTxt.Text = Global._org.First(k => k.Id.Contains(ID)).Code;
            registrationTxt.Text = Global._org.First(k => k.Id.Contains(ID)).Registration;
            contactTxt.Text = Global._org.First(k => k.Id.Contains(ID)).Contact;
            addressTxt.Text = Global._org.First(k => k.Id.Contains(ID)).Address;
            tinTxt.Text = Global._org.First(k => k.Id.Contains(ID)).Tin;
            vatTxt.Text = Global._org.First(k => k.Id.Contains(ID)).Vat;
            nationalityTxt.Text = Global._org.First(k => k.Id.Contains(ID)).Country;
            emailTxt.Text = Global._org.First(k => k.Id.Contains(ID)).Email;
            originalPassword = Global._org.First(k => k.Id.Contains(ID)).Initialpassword;
            accountTxt.Text = Global._org.First(k => k.Id.Contains(ID)).Account;
            statusCbx.Text = Global._org.First(k => k.Id.Contains(ID)).Status;
            expireDate.Text = Global._org.First(k => k.Id.Contains(ID)).Expires;
            syncDate.Text = Global._org.First(k => k.Id.Contains(ID)).Sync;
            try {
                storeCbx.Text = Global._store.First(p => p.Id.Contains(Global._org.First(k => k.Id.Contains(ID)).StoreID)).Name;
            }
            catch { }
            Image img = Base64ToImage(Global._org.First(k => k.Id.Contains(ID)).Image);
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
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        Roles _role;
        string originalPassword;
        private void button2_Click(object sender, EventArgs e)
        {
          
           // Helper.StoreID = "";
            string password = "";

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
            if (initialTxt.Text != "")
            {
                password = Helper.MD5Hash(initialTxt.Text);
            }
            else
            {
                password = originalPassword;
            }
            string id = Guid.NewGuid().ToString();

            MemoryStream stream = ImageToStream(imgCapture.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
            string fullimage = ImageToBase64(stream);
            _org = new Organisation(id, nameTxt.Text,codeTxt.Text, registrationTxt.Text, contactTxt.Text, addressTxt.Text, tinTxt.Text, vatTxt.Text, emailTxt.Text, nationalityTxt.Text, password, accountTxt.Text, statusCbx.Text, Convert.ToDateTime(expireDate.Text).ToString("dd-MM-yyyy"), fullimage, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Convert.ToDateTime(syncDate.Text).ToString("dd-MM-yyyy H:mm:ss"), countsTxt.Text,companyCode.Text,Helper.StoreID);
            if (OrgID != "")
            {
                DBConnect.Update(_org, OrgID);
                MessageBox.Show("Information Updated ");
                Close();
            }
            else
            {
                if (DBConnect.Insert(_org) != "")
                {
                    string SQL = "UPDATE organisation SET counts = '" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + "' WHERE id= '" + id + "'";

                    DBConnect.Execute(SQL);
                    MessageBox.Show("Information Saved");
                    Close();
                    if (Global._users.Count() < 1)
                    {
                        Helper.OrgID = id;
                        string ids = Guid.NewGuid().ToString();
                        _role = new Roles(ids, "Administrator", "All item pos daily purchases merchandise inventory expenses cash flow suppliers users suppliers catgories transactions ledgers logs profile ", "create update delete log ", DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.StoreID);

                        DBConnect.Insert(_role);
                        Global._roles.Add(_role);
                        using (UserDialog form = new UserDialog(""))
                        {
                            // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                            DialogResult dr = form.ShowDialog();
                            if (dr == DialogResult.OK)
                            {
                                // MessageBox.Show(form.state);

                            }
                        }
                    }
                }

            }
            nameTxt.Text = "";
            codeTxt.Text = "";
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

        private void button3_Click(object sender, EventArgs e)
        {

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
       


        private void percentageTxt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (StoreDialog form = new StoreDialog(""))
            {
                // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // MessageBox.Show(form.state);
                    LoadStores();
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (storeCbx.Text =="") {
                storeCbx.BackColor = Color.PaleVioletRed;
                return;
            }
            MemoryStream stream = ImageToStream(imgCapture.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
            string fullimage = ImageToBase64(stream);
            _org = new Organisation(OrgID, nameTxt.Text, codeTxt.Text, registrationTxt.Text, contactTxt.Text, addressTxt.Text, tinTxt.Text, vatTxt.Text, emailTxt.Text, nationalityTxt.Text, initialTxt.Text, accountTxt.Text, statusCbx.Text,Convert.ToDateTime(expireDate.Text).ToString("dd-MM-yyyy"), fullimage, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Convert.ToDateTime(syncDate.Text).ToString("dd-MM-yyyy H:mm:ss"), countsTxt.Text, companyCode.Text, StoreID);
            if (OrgID != "")
            {
                DBConnect.Update(_org, OrgID);
                MessageBox.Show("Information Updated ");
                Close();
            }
        }
        string StoreID="";
        private void storeCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            StoreID = storeDictionary[storeCbx.Text];
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
        }
    }
}
