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
    public partial class CTRegister : Form
    {
        Item _item;
        DataTable t;
        private string barcode = string.Empty;
        string date;
        public CTRegister()
        {
            Global.LoadVital();
            InitializeComponent();
            date = DateTime.Now.ToString("dd-MM-yyyy");
            LoadData();
            lastCountLbl.Text = Helper.stocktaking;
        }
        public void LoadData()
        {
            t = new DataTable();
            t.Columns.Add("Select");//0
            t.Columns.Add("id");//1
            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));//   2 
            t.Columns.Add("Barcode");//3       
            t.Columns.Add("Name");//4           
            t.Columns.Add("Purchase price");//5   
            t.Columns.Add("Sale price");//6       
            t.Columns.Add("Formulation");//7           
            t.Columns.Add("image");//8   
            t.Columns.Add("B/F");//9    
            t.Columns.Add("Purchases");//10  
            t.Columns.Add("Sales");//15         
            t.Columns.Add("Total Stock");//11
            t.Columns.Add("System Stock");//19
            t.Columns.Add("Variance");//17
            t.Columns.Add("Purchases Amount");//12           
            t.Columns.Add("Sales Amount");//14                  
            t.Columns.Add("Profit");//16 
            t.Columns.Add("Physical count");//13
            t.Columns.Add("Stock Variance");//18            
            t.Columns.Add("Damages");//18
            t.Columns.Add("Shrinkable");//20
            t.Columns.Add("Approve");//21

            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Gray), 00, 00);
            }
            foreach (Item h in Item.ListItem())
            {
                int purchases = Global._sale.Where(r => r.Type.Contains("Purchase") && r.ItemID.Contains(h.Id) && Convert.ToDateTime(r.Created) > Convert.ToDateTime(h.Taking)).Count();
                int sales = Global._sale.Where(r => r.Type.Contains("Sale") && r.ItemID.Contains(h.Id) && Convert.ToDateTime(r.Created) > Convert.ToDateTime(h.Taking)).Count();

                t.Rows.Add(new object[] { "Department :" + h.Department + Environment.NewLine + "Manufactured on :" + h.Date_manufactured + Environment.NewLine + h.Generic + Environment.NewLine + h.Strength, h.Id, b, h.Barcode, h.Name + Environment.NewLine + Environment.NewLine + h.Description + Environment.NewLine + h.Manufacturer + Environment.NewLine + h.Country + Environment.NewLine + h.Batch, h.Purchase_price, h.Sale_price, h.Formulation + Environment.NewLine + h.Composition + Environment.NewLine + h.Expire + Environment.NewLine + h.Category, h.Image, h.Counts, purchases, sales, "0.0", h.Quantity, "0", "0", "0", "0", "0", "0", "0", "0", "Approve" });

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
            dtGrid.Columns[9].DefaultCellStyle.Font = new Font("Calibri", 14.5F, FontStyle.Bold, GraphicsUnit.Pixel);
            dtGrid.AllowUserToAddRows = false;
            dtGrid.Columns[9].DefaultCellStyle.BackColor = Color.Orange;
            dtGrid.Columns["Approve"].DefaultCellStyle.BackColor = Color.Gray;
            dtGrid.Columns[0].DefaultCellStyle.BackColor = Color.MediumTurquoise;
            dtGrid.RowTemplate.Height = 70;
            dtGrid.Columns[1].Visible = false;
            dtGrid.Columns[8].Visible = false;
            dtGrid.Columns["B/F"].DefaultCellStyle.Font = new Font("Calibri", 14.5F, FontStyle.Bold, GraphicsUnit.Pixel);
            dtGrid.Columns["Variance"].DefaultCellStyle.Font = new Font("Calibri", 14.5F, FontStyle.Bold, GraphicsUnit.Pixel);
            dtGrid.Columns[12].DefaultCellStyle.Font = new Font("Calibri", 14.5F, FontStyle.Bold, GraphicsUnit.Pixel);
            dtGrid.Columns[13].DefaultCellStyle.Font = new Font("Calibri", 14.5F, FontStyle.Bold, GraphicsUnit.Pixel);
            dtGrid.Columns[14].DefaultCellStyle.Font = new Font("Calibri", 14.5F, FontStyle.Bold, GraphicsUnit.Pixel);


        }
        string filterField = "Name";
        private void DateTxt_TextChanged(object sender, EventArgs e)
        {
            t.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", filterField, searchTxt.Text);
        }
        public System.Drawing.Image Base64ToImage(string bases)
        {
            byte[] imageBytes = Convert.FromBase64String(bases);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        string currentID;
        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        Quantity _qty;
        List<Quantity> _qtyList;
        Stock _stk;
        private void button3_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void imgCapture_Click(object sender, EventArgs e)
        {

        }
        private void qtySold_TextChanged(object sender, EventArgs e)
        {

        }
        private void qtyLbl_Click(object sender, EventArgs e)
        {
        }

        private void qtyPurchased_TextChanged(object sender, EventArgs e)
        {
            qtySold_TextChanged(null, null);
        }

        private void qtyTxt_TextChanged(object sender, EventArgs e)
        {


        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {

        }

        private void dtGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        Sale _sale;
        private void dtGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string itemID = dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString();
            //  _transactor = new Transactor(updateID, dtGrid.Rows[e.RowIndex].Cells[3].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[4].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[6].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[5].Value.ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), dtGrid.Rows[e.RowIndex].Cells[7].Value.ToString(), Helper.OrgID);

            dtGrid.Rows[e.RowIndex].Cells["Total Stock"].Value = (Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["B/F"].Value) + Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Purchases"].Value) - Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Sales"].Value)).ToString();
            dtGrid.Rows[e.RowIndex].Cells["Purchases Amount"].Value = ((Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Purchases"].Value) * Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Purchase price"].Value))).ToString();
            dtGrid.Rows[e.RowIndex].Cells["Sales Amount"].Value = ((Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Sales"].Value) * Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Sale price"].Value))).ToString();

            dtGrid.Rows[e.RowIndex].Cells["Variance"].Value = (Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["System stock"].Value) - Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Total stock"].Value)).ToString();

            dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Value = (Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Physical count"].Value) - Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["System stock"].Value)).ToString();

            if (Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Total Stock"].Value) != Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["System stock"].Value))
            {
                dtGrid.Rows[e.RowIndex].Cells["Variance"].Style.ForeColor = Color.Red;
                dtGrid.Rows[e.RowIndex].Cells["Variance"].Style.Font = new Font("Calibri", 17.5F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            else
            {
                dtGrid.Rows[e.RowIndex].Cells["Variance"].Style.ForeColor = Color.Teal;
                dtGrid.Rows[e.RowIndex].Cells["Variance"].Style.Font = new Font("Calibri", 14.5F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            dtGrid.Rows[e.RowIndex].Cells["Profit"].Value = (Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Sales Amount"].Value) - Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Purchases Amount"].Value)).ToString();


            if (Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Physical count"].Value) == Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["System stock"].Value))
            {
                dtGrid.Rows[e.RowIndex].Cells["Physical count"].Style.ForeColor = Color.Green;
                dtGrid.Rows[e.RowIndex].Cells["Physical count"].Style.Font = new Font("Calibri", 12.5F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            else
            {
                dtGrid.Rows[e.RowIndex].Cells["Physical count"].Style.ForeColor = Color.OrangeRed;
                dtGrid.Rows[e.RowIndex].Cells["Physical count"].Style.Font = new Font("Calibri", 17.5F, FontStyle.Bold, GraphicsUnit.Pixel);


            }
            if (Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Variance"].Value) != 0)
            {
                dtGrid.Rows[e.RowIndex].Cells["Variance"].Style.ForeColor = Color.Red;
                dtGrid.Rows[e.RowIndex].Cells["Variance"].Style.Font = new Font("Calibri", 17.5F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            else
            {
                dtGrid.Rows[e.RowIndex].Cells["Variance"].Style.ForeColor = Color.Green;
                dtGrid.Rows[e.RowIndex].Cells["Variance"].Style.Font = new Font("Calibri", 12.5F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            if (Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Value) < 0)
            {
                if (Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Value) == - Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Shrinkable"].Value))
                {
                    dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Style.ForeColor = Color.Green;
                    dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Style.Font = new Font("Calibri", 12.5F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                else
                {

                    dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Style.ForeColor = Color.Red;
                    dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Style.Font = new Font("Calibri", 14.5F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
            }
            else
            {

                if (Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Value) == Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Damages"].Value))
                {
                    dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Style.ForeColor = Color.Green;
                    dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Style.Font = new Font("Calibri", 12.5F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                else
                {

                    dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Style.ForeColor = Color.Red;
                    dtGrid.Rows[e.RowIndex].Cells["Stock Variance"].Style.Font = new Font("Calibri", 14.5F, FontStyle.Bold, GraphicsUnit.Pixel);
                }



            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("YES or No?", "Are you sure you want to update this information? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string SQL = "UPDATE organisation SET counts = '" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + "' WHERE id= '" + Helper.OrgID + "'";

                DBConnect.Execute(SQL);
                MessageBox.Show("Stock Taking Done !");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //  dtGrid_CellEndEdit(null, null);
        }

        private void dtGrid_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                dtGrid_CellEndEdit(sender, e);
            }

            if (e.ColumnIndex == 22)
            {
                if (Convert.ToDouble(dtGrid.Rows[e.RowIndex].Cells["Physical count"].Value) != 0)
                {
                }
                else
                {
                    MessageBox.Show("Please input current stock count");
                    return;

                }
                if (MessageBox.Show("YES or No?", "Stock counts Confirmed ?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    // dtGrid_CellEndEdit(sender, e);

                }
            }
        }
    }
}
