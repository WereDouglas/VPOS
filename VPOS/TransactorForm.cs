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
    public partial class TransactorForm : Form
    {
        Transactor _transactor;      
        WebCam webcam;
        DataTable t;
        List<Transactor> _transactorList = new List<Transactor>();
        public TransactorForm()
        {
            InitializeComponent();
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
            autocomplete();
            LoadData();
        }

        public void LoadData()
        {
            _transactorList = new List<Transactor>();
            _transactorList = Transactor.ListTransactor();

            t = new DataTable();
            t.Columns.Add(new DataColumn("Select", typeof(bool)));
            t.Columns.Add("id");//1
            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));//   2        
            t.Columns.Add("Name");//3              
            t.Columns.Add("Contact");//4
            t.Columns.Add("Type");//5   
            t.Columns.Add("image");//6  
            t.Columns.Add("Address");//7
            t.Columns.Add("Delete");//8
          

            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Gray), 00, 00);
            }
            foreach (Transactor transactor in _transactorList)
            {

                t.Rows.Add(new object[] { false, transactor.Id, b, transactor.Name, transactor.Contact,transactor.Type, transactor.Image, transactor.Address, "Delete" });

            }
            dtGrid.DataSource = t;
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
            dtGrid.AllowUserToAddRows = false;
            dtGrid.Columns[8].DefaultCellStyle.BackColor = Color.OrangeRed;         
            dtGrid.RowTemplate.Height = 60;
            dtGrid.Columns[1].Visible = false;
            dtGrid.Columns[6].Visible = false;


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
        public MemoryStream ImageToStream(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, format);
            return ms;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
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

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (surnameTxt.Text == "")
            {            
                surnameTxt.BackColor = Color.Red;
                return;
            }
            if (contactTxt.Text == "")
            {

                contactTxt.BackColor = Color.Red;
                return;
            }

            string id = Guid.NewGuid().ToString();

            MemoryStream stream = ImageToStream(imgCapture.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
            string fullimage = ImageToBase64(stream);
            _transactor = new Transactor(id, surnameTxt.Text, contactTxt.Text,fullimage, typeCbx.Text,DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),addressTxt.Text, Helper.OrgID);

            if (DBConnect.Insert(_transactor) != "")
            {
                //string query = "insert into transactor (id, transactorNo,contact,surname,lastname,email,dob,nationality,address,kin,kincontact,gender,created) values ('"+ id + "', '"+ transactorNoTxt.Text + "', '"+ contactTxt.Text + "', '" + surnameTxt.Text + "', '" + lastnameTxt.Text + "', '" + emailTxt.Text + "', '" +Convert.ToDateTime(dobdateTimePicker1.Text).ToString("dd-MM-yyyy") + "', '" + nationalityTxt.Text + "', '" + addressTxt.Text + "', '" + kinTxt.Text + "','" + kincontactTxt.Text + "', '" + genderCbx.Text + "','"+DateTime.Now.ToString("dd-MM-yyyy H:m:s")+"');";
                Global._transactor.Add(_transactor);
                MessageBox.Show("Information Saved");
                LoadData();
            }
            else
            {


            }
            surnameTxt.Text = "";
            contactTxt.Text = "";
        }

        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                if (MessageBox.Show("YES or No?", "Are you sure you want to delete this information? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DBConnect.Delete("transactor", dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
                    MessageBox.Show("Information deleted");
                    LoadData();

                }
            }

        }

        private void dtGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

          string  updateID = dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            _transactor = new Transactor(updateID, dtGrid.Rows[e.RowIndex].Cells[3].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[4].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[6].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[5].Value.ToString(),DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), dtGrid.Rows[e.RowIndex].Cells[7].Value.ToString(), Helper.OrgID);

            DBConnect.Update(_transactor, updateID);
            Global._transactor.RemoveAll(x => x.Id == updateID);
            Global._transactor.Add(_transactor);
        }
    }
}
