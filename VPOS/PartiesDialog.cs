﻿using System;
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
    public partial class PartiesDialog : Form
    {

        Transactor _transactor;
        WebCam webcam;
        DataTable t;
        List<Transactor> _transactorList = new List<Transactor>();
        static string base64String = null;
        public PartiesDialog()
        {
            InitializeComponent();
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
            autocomplete();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
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
            _transactor = new Transactor(id, surnameTxt.Text, contactTxt.Text, fullimage, typeCbx.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), addressTxt.Text, Helper.OrgID, Helper.StoreID);

            if (DBConnect.Insert(_transactor) != "")
            {
                //string query = "insert into transactor (id, transactorNo,contact,surname,lastname,email,dob,nationality,address,kin,kincontact,gender,created) values ('"+ id + "', '"+ transactorNoTxt.Text + "', '"+ contactTxt.Text + "', '" + surnameTxt.Text + "', '" + lastnameTxt.Text + "', '" + emailTxt.Text + "', '" +Convert.ToDateTime(dobdateTimePicker1.Text).ToString("dd-MM-yyyy") + "', '" + nationalityTxt.Text + "', '" + addressTxt.Text + "', '" + kinTxt.Text + "','" + kincontactTxt.Text + "', '" + genderCbx.Text + "','"+DateTime.Now.ToString("dd-MM-yyyy H:m:s")+"');";
                Global._transactor.Add(_transactor);
                MessageBox.Show("Information Saved");
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
            else
            {


            }
            surnameTxt.Text = "";
            contactTxt.Text = "";
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
    }
}