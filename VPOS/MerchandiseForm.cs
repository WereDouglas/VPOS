using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPOS.Model;

namespace VPOS
{
    public partial class MerchandiseForm : Form
    {
        Item _item;
        WebCam webcam;
        DataTable t;

        public MerchandiseForm()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            t = new DataTable();
            t.Columns.Add("Manage");//26
            t.Columns.Add("Stock");//26
            t.Columns.Add("id");//1
            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));//   2        
            t.Columns.Add("Name");//3 
            t.Columns.Add("Generic name");//4     
            t.Columns.Add("Code");//4              
            t.Columns.Add("Description");//5
            t.Columns.Add("Manufacturer");//6
            t.Columns.Add("Country");//7          
            t.Columns.Add("Composition");//11           
            t.Columns.Add("Category");//13 
            t.Columns.Add("Sub category");//13          
            t.Columns.Add("Barcode");//15
            t.Columns.Add("image");//16  
            t.Columns.Add("Created");//17           
            t.Columns.Add("Strength");//21
            t.Columns.Add("Delete");//22           
            t.Columns.Add("Validity");//26
          


            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Gray), 00, 00);
            }
            foreach (Item h in Global.item)
            {

                t.Rows.Add(new object[] {"View", "Stock", h.Id, b, h.Name,h.Generic, h.Code, h.Description, h.Manufacturer, h.Country, h.Composition,h.Category,h.Sub, h.Barcode, h.Image, h.Created, h.Strength, "Delete",h.Valid});

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
            dtGrid.Columns["Delete"].DefaultCellStyle.BackColor = Color.Orange;
            dtGrid.Columns["Manage"].DefaultCellStyle.BackColor = Color.Teal;
            dtGrid.Columns["Stock"].DefaultCellStyle.BackColor = Color.PaleGoldenrod;
            dtGrid.RowTemplate.Height = 60;
            dtGrid.Columns["id"].Visible = false;
            dtGrid.Columns["image"].Visible = false;
        }
    
        public System.Drawing.Image Base64ToImage(string bases)
        {
            byte[] imageBytes = Convert.FromBase64String(bases);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
        string filterField = "Name";
        private void DateTxt_TextChanged(object sender, EventArgs e)
        {
            t.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", filterField, searchTxt.Text);
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
        private static void CreateBarcode(string code)
        {
            var myBitmap = new Bitmap(500, 50);
            var g = Graphics.FromImage(myBitmap);
            var jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            g.Clear(Color.White);

            var strFormat = new StringFormat { Alignment = StringAlignment.Center };
            g.DrawString(code, new Font("Free 3 of 9", 50), Brushes.Black, new RectangleF(0, 0, 500, 50), strFormat);

            var myEncoder = System.Drawing.Imaging.Encoder.Quality;
            var myEncoderParameters = new EncoderParameters(1);

            var myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            myBitmap.Save(@"c:\Barcode.jpg", jgpEncoder, myEncoderParameters);
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            var codecs = ImageCodecInfo.GetImageDecoders();

            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (ItemDialog form = new ItemDialog(""))
            {
                // DentalDialog form1 = new DentalDialog(item.Text, ItemID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // MessageBox.Show(form.state);
                    LoadData();
                }
            }
        }

        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dtGrid.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                if (MessageBox.Show("YES or No?", "Are you sure you want to delete this information? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DBConnect.Delete("item", dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    MessageBox.Show("Information deleted");
                    LoadData();

                }
            }
            if (e.ColumnIndex == dtGrid.Columns["Stock"].Index && e.RowIndex >= 0)
            {
                using (StockDialog form = new StockDialog(dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString()))
                {
                    DialogResult dr = form.ShowDialog();
                    if (dr == DialogResult.OK)
                    {                       
                        LoadData();
                    }
                }

            }
            if (e.ColumnIndex == dtGrid.Columns["Manage"].Index && e.RowIndex >= 0)
            {
                using (ItemDialog form = new ItemDialog(dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString()))
                {                   
                    DialogResult dr = form.ShowDialog();
                    if (dr == DialogResult.OK)
                    {                       
                        LoadData();
                    }
                }

            }

        }

        private void dtGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (String.IsNullOrEmpty(dtGrid.Rows[e.RowIndex].Cells["Name"].Value.ToString()))
            {

                MessageBox.Show("Please input a name ");
                return;

            }        
          
            string updateID = dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString();
            _item = new Item(updateID,dtGrid.Rows[e.RowIndex].Cells["name"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Generic name"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Code"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Description"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Manufacturer"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Country"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Composition"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Barcode"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["image"].Value.ToString(),DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), dtGrid.Rows[e.RowIndex].Cells["Strength"].Value.ToString(),Helper.OrgID, dtGrid.Rows[e.RowIndex].Cells["Validity"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Sub category"].Value.ToString());

            DBConnect.Update(_item, updateID);
            Global.item.RemoveAll(x => x.Id == updateID);
            Global.item.Add(_item);
        }

        private void searchCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterField = searchCbx.Text;
        }
    }
}
