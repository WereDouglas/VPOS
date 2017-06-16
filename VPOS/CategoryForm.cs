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
    public partial class CategoryForm : Form
    {
        DataTable t;
        List<Category> _categorys = new List<Category>();
        Category _category;
        public CategoryForm()
        {
            InitializeComponent();
            LoadData();
            
        }
        public void LoadData()
        {

            _categorys = Global._category;
            t = new DataTable();
            // create and execute query 

          
           
            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));//   2 
            t.Columns.Add("id");//1 
            t.Columns.Add("Name");//2
            t.Columns.Add("Description");// 3       
            t.Columns.Add("Created");// 4
            t.Columns.Add("Delete");// 5
            t.Columns.Add("image");// 5
            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Gray), 00, 00);
            }
            foreach (Category r in Global._category)
            {

                t.Rows.Add(new object[] { b, r.Id,r.Name, r.Description, r.Created, "Delete",r.Image });

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
            dtGrid.Columns["id"].Visible = false;
            dtGrid.Columns["image"].Visible = false;
            dtGrid.Columns["delete"].DefaultCellStyle.BackColor = Color.OrangeRed;
            dtGrid.AllowUserToAddRows = false;
            dtGrid.RowTemplate.Height = 60;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
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

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (nameTxt.Text == "")
            {
                nameTxt.BackColor = Color.Red;
                return;
            }

            MemoryStream stream = ImageToStream(imgCapture.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
            string fullimage = ImageToBase64(stream);

            string id = Guid.NewGuid().ToString();
            _category = new Category(id, nameTxt.Text, descriptionTxt.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),Helper.OrgID, Helper.StoreID,fullimage);

            if (DBConnect.Insert(_category) != "")
            {
                Global._category.Add(_category);
                nameTxt.Text = "";
                descriptionTxt.Text = "";
                MessageBox.Show("Information Saved");
                LoadData();

            }
            else
            {
                return;

            }

        }

        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show("YES or No?", "Are you sure you want to delete this information? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DBConnect.Delete("category", dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString());

                    MessageBox.Show("Information deleted");
                    LoadData();

                }
            }
           
        }

        private void dtGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
          string  updateID = dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            _category = new Category(dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[2].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[3].Value.ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),Helper.OrgID, Helper.StoreID, dtGrid.Rows[e.RowIndex].Cells["image"].Value.ToString());
            DBConnect.Update(_category, updateID);
            Global._category.RemoveAll(x => x.Id == updateID);
            Global._category.Add(_category);
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
