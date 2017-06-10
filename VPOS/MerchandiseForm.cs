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
            t.Columns.Add(new DataColumn("Select", typeof(bool)));
            t.Columns.Add("id");//1
            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));//   2        
            t.Columns.Add("Name");//3 
            t.Columns.Add("Code");//4              
            t.Columns.Add("Description");//5
            t.Columns.Add("Manufacturer");//6
            t.Columns.Add("Country");//7
            t.Columns.Add("Batch No.");//8
            t.Columns.Add("Purchase price");//9
            t.Columns.Add("Sale price");//10
            t.Columns.Add("Composition");//11
            t.Columns.Add("Date of Expiry");//12
            t.Columns.Add("Category");//13
            t.Columns.Add("Formulation");//14
            t.Columns.Add("Barcode");//15
            t.Columns.Add("image");//16  
            t.Columns.Add("Created");//17
            t.Columns.Add("Department");//18
            t.Columns.Add("Date Manufactured");//19
            t.Columns.Add("Generic");//20
            t.Columns.Add("Strength");//21
            t.Columns.Add("Delete");//22
            t.Columns.Add("Quantity");//23
            t.Columns.Add("Min Threshold");//24
            t.Columns.Add("Last Stock Count");//25
            t.Columns.Add("Date of stock taking");//26
            t.Columns.Add("Validity");//26
            t.Columns.Add("Tax");//26
            t.Columns.Add("Promo Price");//26
            t.Columns.Add("Promo Start");//26
            t.Columns.Add("Promo End");//26

            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Gray), 00, 00);
            }
            foreach (Item h in Global._item)
            {

                t.Rows.Add(new object[] { false, h.Id, b, h.Name, h.Code, h.Description, h.Manufacturer, h.Country, h.Batch, h.Purchase_price, h.Sale_price, h.Composition, h.Expire, h.Category, h.Formulation, h.Barcode, h.Image, h.Created, h.Department, h.Date_manufactured, h.Generic, h.Strength, "Delete", h.Quantity, h.Min_qty,h.Counts,h.Taking,h.Valid,h.Tax ,h.Promo_price,h.Promo_start,h.Promo_end});

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
            dtGrid.Columns[22].DefaultCellStyle.BackColor = Color.Orange;
            dtGrid.RowTemplate.Height = 60;
            dtGrid.Columns[1].Visible = false;
            dtGrid.Columns[16].Visible = false;
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
            if (e.ColumnIndex == 22)
            {
                if (MessageBox.Show("YES or No?", "Are you sure you want to delete this information? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DBConnect.Delete("item", dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    MessageBox.Show("Information deleted");
                    LoadData();

                }
            }
            if (e.ColumnIndex == 0)
            {
                using (ItemDialog form = new ItemDialog(dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString()))
                {

                    this.Close();
                    // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                    DialogResult dr = form.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        // MessageBox.Show(form.state);
                        LoadData();
                    }
                }

            }

        }

        private void dtGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!Regex.IsMatch(dtGrid.Rows[e.RowIndex].Cells["Quantity"].Value.ToString(), @"^\d+$"))
            {

                MessageBox.Show("Quantity is not a numnber ");
                return;

            }
        
          
            string updateID = dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            _item = new Item(updateID, dtGrid.Rows[e.RowIndex].Cells[3].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[4].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[5].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[6].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[7].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[8].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[9].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[10].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[11].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[12].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[13].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[14].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[15].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[16].Value.ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), dtGrid.Rows[e.RowIndex].Cells[18].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[19].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[20].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[21].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[23].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[24].Value.ToString(),Helper.OrgID, dtGrid.Rows[e.RowIndex].Cells[25].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[26].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[27].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[28].Value.ToString(),Helper.StoreID, dtGrid.Rows[e.RowIndex].Cells["Promo Price"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Promo Start"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Promo End"].Value.ToString());

            DBConnect.Update(_item, updateID);
            Global._item.RemoveAll(x => x.Id == updateID);
            Global._item.Add(_item);
        }

        private void searchCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterField = searchCbx.Text;
        }
    }
}
