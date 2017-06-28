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
    public partial class SupplierForm : Form
    {
        Supplier _supplier;
        DataTable t;
        public SupplierForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (PartiesDialog form = new PartiesDialog())
            {
                // DentalDialog form1 = new DentalDialog(item.Text, SupplierID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // MessageBox.Show(form.state);
                    LoadData();
                }
            }
        }
        public void LoadData()
        {

            t = new DataTable();
            t.Columns.Add(new DataColumn("Select", typeof(bool)));
            t.Columns.Add("id");//1
            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));//   2        
            t.Columns.Add("Name");//3              
            t.Columns.Add("Contact");//4
            t.Columns.Add("image");//6  
            t.Columns.Add("Address");//7
            t.Columns.Add("Delete");//8


            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Gray), 00, 00);
            }
            foreach (Supplier transactor in Global.supplier)
            {
                t.Rows.Add(new object[] { false, transactor.Id, b, transactor.Name, transactor.Contact, transactor.Image, transactor.Address, "Delete" });

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
            dtGrid.Columns["Delete"].DefaultCellStyle.BackColor = Color.OrangeRed;
            dtGrid.RowTemplate.Height = 60;
            dtGrid.Columns["id"].Visible = false;
            dtGrid.Columns["image"].Visible = false;


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


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dtGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string updateID = dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString();
            Supplier _transactor = new Supplier(updateID, dtGrid.Rows[e.RowIndex].Cells["name"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["contact"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["image"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["address"].Value.ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID);
            DBConnect.Update(_transactor, updateID);
            Global.supplier.RemoveAll(x => x.Id == updateID);
            Global.supplier.Add(_transactor);
        }

        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dtGrid.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                if (MessageBox.Show("YES or No?", "Are you sure you want to delete this information? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DBConnect.Delete("supplier", dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    MessageBox.Show("Information deleted");
                    LoadData();
                }
            }
        }
    }
}
