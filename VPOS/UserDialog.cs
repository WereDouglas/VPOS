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
    public partial class UserDialog : Form
    {
        Users _user;
        WebCam webcam;
        string UserID = "";
        Roles _role;
        Dictionary<string, string> storeDictionary = new Dictionary<string, string>();
        public UserDialog(string userID)
        {
            InitializeComponent();
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
            autocomplete();

            if (Global._roles.Count() < 1)
            {
                string ids = Guid.NewGuid().ToString();
                _role = new Roles(ids, "Administrator", "All item pos daily purchases merchandise inventory expenses cash flow suppliers users suppliers catgories transactions ledgers logs profile ", "create update delete log ", DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.StoreID);
                DBConnect.Insert(_role);
                Global._roles.Add(_role);

            }
            foreach (Roles r in Global._roles)
            {
                roleCbx.Items.Add(r.Title);
            }
            if (userID == "")
            {
                passwordTxt.Enabled = false;
            }
            else
            {
                UserID = userID;
                Profile(UserID);
            }
            foreach (Store s in Global._store)
            {

                storeCbx.Items.Add(s.Name);
                storeDictionary.Add(s.Name, s.Id);
            }

        }
        private void Profile(string ID)
        {

            surnameTxt.Text = Global._users.First(k => k.Id.Contains(ID)).Surname;
            firstnameTxt.Text = Global._users.First(k => k.Id.Contains(ID)).Lastname;
            othernameTxt.Text = Global._users.First(k => k.Id.Contains(ID)).Othername;
            idTxt.Text = Global._users.First(k => k.Id.Contains(ID)).IdNo;
            primaryTxt.Text = Global._users.First(k => k.Id.Contains(ID)).Contact;
            secondaryTxt.Text = Global._users.First(k => k.Id.Contains(ID)).Contact2;
            emailTxt.Text = Global._users.First(k => k.Id.Contains(ID)).Email;
            nationalityTxt.Text = Global._users.First(k => k.Id.Contains(ID)).Nationality;
            genderCbx.Text = Global._users.First(k => k.Id.Contains(ID)).Gender;
            roleCbx.Text = Global._users.First(k => k.Id.Contains(ID)).Roles;
            originalPassword = Global._users.First(k => k.Id.Contains(ID)).Passwords;
            initialTxt.Text = Global._users.First(k => k.Id.Contains(ID)).InitialPassword;
            accountTxt.Text = Global._users.First(k => k.Id.Contains(ID)).Account;
            statusCbx.Text = Global._users.First(k => k.Id.Contains(ID)).Status;
            addressTxt.Text = Global._users.First(k => k.Id.Contains(ID)).Address;
            Image img = Base64ToImage(Global._users.First(k => k.Id.Contains(ID)).Image);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            //Bitmap bps = new Bitmap(bmp, 50, 50);
            imgCapture.Image = bmp;
            imgCapture.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
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
        string originalPassword = "";
        private void button2_Click(object sender, EventArgs e)
        {
            string password = "";
            if (firstnameTxt.Text == "")
            {
                firstnameTxt.BackColor = Color.Red;
                return;
            }
            if (primaryTxt.Text == "")
            {
                primaryTxt.BackColor = Color.Red;
                return;
            }
            if (roleCbx.Text == "")
            {
                roleCbx.BackColor = Color.Red;
                return;
            }
            if (String.IsNullOrEmpty(storeID))
            {
                storeCbx.BackColor = Color.Red;
                return;
            }
            if (passwordTxt.Text != "")
            {
                password = Helper.MD5Hash(passwordTxt.Text);
            }
            else
            {
                password = originalPassword;
            }
            string id = Guid.NewGuid().ToString();
            MemoryStream stream = ImageToStream(imgCapture.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
            string fullimage = ImageToBase64(stream);
            _user = new Users(id, idTxt.Text, primaryTxt.Text, secondaryTxt.Text, surnameTxt.Text, firstnameTxt.Text, othernameTxt.Text, emailTxt.Text, nationalityTxt.Text, addressTxt.Text, password, genderCbx.Text, Helper.OrgID, roleCbx.Text, Helper.MD5Hash(initialTxt.Text), accountTxt.Text, statusCbx.Text, fullimage, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),Helper.StoreID);

            if (UserID != "")
            {
                _user = new Users(UserID, idTxt.Text, primaryTxt.Text, secondaryTxt.Text, surnameTxt.Text, firstnameTxt.Text, othernameTxt.Text, emailTxt.Text, nationalityTxt.Text, addressTxt.Text, password, genderCbx.Text, Helper.OrgID, roleCbx.Text, Helper.MD5Hash(initialTxt.Text), accountTxt.Text, statusCbx.Text, fullimage, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.StoreID);

                DBConnect.Update(_user, UserID);
                Global._users.RemoveAll(x => x.Id == UserID);
                Global._users.Add(_user);
                MessageBox.Show("Information Updated ");
                Close();
            }
            else
            {
                if (DBConnect.Insert(_user) != "")
                {
                    Global._users.Add(_user);
                    
                    MessageBox.Show("Information Saved");
                    Close();
                }
            }
            firstnameTxt.Text = "";
            primaryTxt.Text = "";
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
    }
}
