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

            _categorys = Global.category;
            t = new DataTable();
            // create and execute query 



            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));//   2 
            t.Columns.Add("id");//1 
            t.Columns.Add("Name");//2
            t.Columns.Add("Description");// 3       
            t.Columns.Add("Created");// 4
            t.Columns.Add("Delete");// 5
            t.Columns.Add("image");// 5          
            t.Columns.Add("Sub category");// 5
            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Gray), 00, 00);
            }
            foreach (Category r in Global.category)
            {

                t.Rows.Add(new object[] { b, r.Id, r.Name, r.Description, r.Created, "Delete", r.Image, "View Subs" });

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
            dtGrid.Columns["Sub category"].DefaultCellStyle.BackColor = Color.Green;
            dtGrid.AllowUserToAddRows = false;
            dtGrid.RowTemplate.Height = 60;
        }
        public void LoadSub(string name)
        {

            _categorys = Global.category;
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
            foreach (Sub r in Global.sub.Where(f=>f.Name.Contains(name)))
            {
                t.Rows.Add(new object[] { b, r.Id, r.Name, r.Description, r.Created, "Delete", r.Image });
            }
            subGrid.DataSource = t;
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
            subGrid.Columns["id"].Visible = false;
            subGrid.Columns["image"].Visible = false;
            subGrid.Columns["delete"].DefaultCellStyle.BackColor = Color.OrangeRed;
            subGrid.AllowUserToAddRows = false;
            subGrid.RowTemplate.Height = 60;
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


        }

        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == dtGrid.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                if (MessageBox.Show("YES or No?", "Are you sure you want to delete this information? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string ID = Guid.NewGuid().ToString();
                    DBConnect.Delete("category", dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    //Deletion _del = new Deletion(ID, "files", dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString(), "id", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Helper.orgID);
                    //DBConnect.Insert(_del);
                    //Global.deletions.Add(_del);

                    MessageBox.Show("Information deleted");
                    LoadData();

                }
            }
            if (e.ColumnIndex == dtGrid.Columns["Sub category"].Index && e.RowIndex >= 0)
            {

                LoadSub(dtGrid.Rows[e.RowIndex].Cells["name"].Value.ToString());

            }



        }

        private void dtGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string updateID = dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            _category = new Category(dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[2].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[3].Value.ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.StoreID, dtGrid.Rows[e.RowIndex].Cells["image"].Value.ToString());
            DBConnect.Update(_category, updateID);
            Global.category.RemoveAll(x => x.Id == updateID);
            Global.category.Add(_category);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void saveBtn_Click_1(object sender, EventArgs e)
        {
            using (CategoryDialog form = new CategoryDialog())
            {
                // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    LoadData();

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SubDialog form = new SubDialog())
            {
                // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {                   
                   // LoadSub();

                }
            }
        }
    }
}
