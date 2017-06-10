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
    public partial class StockRecords : Form
    {
        Taking _take;
        DataTable t;
        public StockRecords()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {

            t = new DataTable();
            t.Columns.Add(new DataColumn("Select", typeof(bool)));
            t.Columns.Add("id");//1
          
            t.Columns.Add("Date");//3    
            t.Columns.Add("Name");//3 
            t.Columns.Add("Balance B/F");//4              
            t.Columns.Add("Purchases");//5
            t.Columns.Add("Sales");//6
            t.Columns.Add("Total stock");//7
            t.Columns.Add("System stock");//8
            t.Columns.Add("Variance");//9
            t.Columns.Add("Purchase amount");//10
            t.Columns.Add("Sale amount");//11          
            t.Columns.Add("Profit");//15
            t.Columns.Add("Physical count");//16  
            t.Columns.Add("Damages");//17
            t.Columns.Add("Shrinkable");//18
            t.Columns.Add("orgid");//19
            t.Columns.Add("userid");//20
            t.Columns.Add("created");//21
            t.Columns.Add("image");//22
            t.Columns.Add("Delete");//26

            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Gray), 00, 00);
            }
            foreach (Taking h in Global._taking)
            {

                t.Rows.Add(new object[] { false, h.Id,h.Date,Global._item.First(j=>j.Id.Contains(h.ItemID)).Name, h.Bf, h.Purchases, h.Sales, h.Total_stock, h.System_stock, h.Variance, h.Purchase_amount, h.Sale_amount, h.Profit, h.Physical_count, h.Damages, h.Shrinkable,h.OrgID, h.UserID, h.Created, Global._item.First(j => j.Id.Contains(h.ItemID)).Image, "Delete"});

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
            dtGrid.Columns[19].DefaultCellStyle.BackColor = Color.Orange;
            //dtGrid.RowTemplate.Height = 60;
            dtGrid.Columns["id"].Visible = false;
            dtGrid.Columns["image"].Visible = false;
            dtGrid.Columns["userid"].Visible = false;
            dtGrid.Columns["orgid"].Visible = false;
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
        string filterField = "Name";
        private void DateTxt_TextChanged(object sender, EventArgs e)
        {

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
    }
}
