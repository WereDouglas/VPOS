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
    public partial class ManageForm : Form
    {
        Item _item;
        DataTable t;
        private string barcode = string.Empty;
        string date;
        public ManageForm()
        {
            InitializeComponent();
            date = DateTime.Now.ToString("dd-MM-yyyy");
            LoadData();
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
            t.Columns.Add("Quantity");//8

            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Gray), 00, 00);
            }
            foreach (Item h in Item.ListItem())
            {
                t.Rows.Add(new object[] { "Department :" + h.Department + Environment.NewLine + "Manufactured on :" + h.Date_manufactured + Environment.NewLine + h.Generic + Environment.NewLine + h.Strength, h.Id, b, h.Barcode, h.Name + Environment.NewLine + Environment.NewLine + h.Description + Environment.NewLine + h.Manufacturer + Environment.NewLine + h.Country + Environment.NewLine + h.Batch, h.Purchase_price, h.Sale_price, h.Formulation + Environment.NewLine + h.Composition + Environment.NewLine + h.Expire + Environment.NewLine + h.Category, h.Image, h.Quantity });

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
            dtGrid.Columns[9].DefaultCellStyle.BackColor = Color.Orange;
            dtGrid.Columns[0].DefaultCellStyle.BackColor = Color.MediumTurquoise;
            dtGrid.RowTemplate.Height = 70;
            dtGrid.Columns[1].Visible = false;
            dtGrid.Columns[8].Visible = false;


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
            qtyTxt.Text = "0";
            nameLbl.Text = "0";
            purchaseTxt.Text = "0";
            saleTxt.Text = "0";
            qtyLbl.Text = "0";
            previousLbl.Text = "0";
            stockBalanceLbl.Text = "0";
         
            if (e.ColumnIndex == 0)
            {
                currentID = "";
                currentID = dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                nameLbl.Text = dtGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                purchaseTxt.Text = dtGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
                saleTxt.Text = dtGrid.Rows[e.RowIndex].Cells[6].Value.ToString();
                qtyLbl.Text = dtGrid.Rows[e.RowIndex].Cells[9].Value.ToString();
                previousLbl.Text = dtGrid.Rows[e.RowIndex].Cells[6].Value.ToString();

                saleQtyLbl.Text = Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Sale") && m.ItemID.Contains(currentID)).Sum(p => Convert.ToDouble(p.Qty)).ToString("n0");
                purchaseQtyLbl.Text = Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Purchase") && m.ItemID.Contains(currentID)).Sum(p => Convert.ToDouble(p.Qty)).ToString("n0");

                // purchaseamountLbl.Text = "Purchase today: " + Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Total)).ToString("n0");
                saleAmountLbl.Text = (Convert.ToDouble(saleQtyLbl.Text) * Convert.ToDouble(saleTxt.Text)).ToString("n0");
                purchaseAmountLbl.Text = (Convert.ToDouble(purchaseQtyLbl.Text) * Convert.ToDouble(purchaseTxt.Text)).ToString("n0");

                try
                {
                    stockBalanceLbl.Text = (Convert.ToDouble(qtyLbl.Text) + Convert.ToDouble(purchaseQtyLbl.Text) - Convert.ToDouble(saleQtyLbl.Text)).ToString();
                }
                catch { }

                try
                {
                    Image img = Base64ToImage(dtGrid.Rows[e.RowIndex].Cells[8].Value.ToString());
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
                    //Bitmap bps = new Bitmap(bmp, 50, 50);
                    imgCapture.Image = bmp;
                    imgCapture.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch { }
            }
        }
        Quantity _qty;
        List<Quantity> _qtyList;
        Stock _stk;
        private void button3_Click(object sender, EventArgs e)
        {
            if (qtyTxt.Text == "0")
            {
                qtyTxt.BackColor = Color.Red;
                MessageBox.Show("Please input values");
                return;
            }
            if (qtySold.Text == "0")
            {
                qtySold.BackColor = Color.Red;
                MessageBox.Show("Please Quantity sold");
                return;
            }

            string id = Guid.NewGuid().ToString();
            _qtyList = Quantity.ListQuantity();
            if (_qtyList.Where(t => t.Created.Contains(Convert.ToDateTime(dateTimePicker1.Text).ToString("dd-MM-yyyy")) && t.ItemID.Contains(currentID)).Count() < 1)
            {
                string Query = "UPDATE item SET  purchase_price='" + purchaseTxt.Text + "' ,sale_price='" + saleTxt.Text + "' ,quantity = '" + qtyTxt.Text + "'  WHERE id ='" + currentID + "'";
                DBConnect.save(Query);

                if (!Helper.ExistsAnd("quantity", "date", "itemid", date, currentID))
                {
                    _qty = new Quantity(id, currentID, qtySold.Text, qtyPurchased.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), Helper.OrgID, Helper.UserID, date);
                    DBConnect.Insert(_qty);
                    double totalValue = Convert.ToDouble(qtyTxt.Text) * Convert.ToDouble(saleTxt.Text);
                    string Query2 = "UPDATE stock SET  qty='" + qtyTxt.Text + "' ,sale_price='" + saleTxt.Text + "' ,purchase_price = '" + purchaseTxt.Text + "',previous_price ='" + previousLbl.Text + "',total_value = '" + totalValue.ToString() + "',created = '" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + "'  WHERE itemID ='" + currentID + "'";
                    DBConnect.save(Query2);
                    MessageBox.Show("Information Saved");

                    qtySold.Text = "0";
                    qtyPurchased.Text = "0";
                }
                else
                {
                    MessageBox.Show("Stock taking already done today ");
                }
            }
            else
            {
                MessageBox.Show("Stock taking already done for today ");
                return;
            }


            LoadData();

        }

        private void imgCapture_Click(object sender, EventArgs e)
        {

        }

        private void qtySold_TextChanged(object sender, EventArgs e)
        {
            
            try
            {  
                purchaseCountAmountLbl.Text = (Convert.ToDouble(qtyPurchased.Text) * Convert.ToDouble(purchaseTxt.Text)).ToString("n0"); ;
                saleCountAmountLbl.Text = (Convert.ToDouble(qtySold.Text) * Convert.ToDouble(saleTxt.Text)).ToString("n0");

                noReceiptSaleTxt.Text = (Convert.ToDouble(qtySold.Text) - Convert.ToDouble(saleQtyLbl.Text)).ToString("n0");
                noReceiptPurchaseTxt.Text = (Convert.ToDouble(qtyPurchased.Text) - Convert.ToDouble(purchaseQtyLbl.Text)).ToString("n0");
            }
            catch { }
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
            offsetLbl.Text = "0";           
            try
            {
                offsetLbl.Text = (Convert.ToDouble(stockBalanceLbl.Text) - Convert.ToDouble(qtyTxt.Text)).ToString();
                  }
            catch { }
           

        }
      
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            date = Convert.ToDateTime(dateTimePicker1.Text).ToString("dd-MM-yyyy");
        }

        private void dtGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
