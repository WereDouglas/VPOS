using System;
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
    public partial class StockDialog : Form
    {
        string Barcode;
        public StockDialog(string barcode)
        {
            InitializeComponent();
            Barcode = barcode;
            if (Global.stock.Where(x=>x.Barcode.Contains(barcode)).Count()>0) {
                Stock(barcode);
            }
            Image img = Base64ToImage(Global.item.First(k => k.Barcode.Contains(barcode)).Image);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            Bitmap bps = new Bitmap(bmp, 50, 50);
            imgCapture.Image = bmp;
            imgCapture.SizeMode = PictureBoxSizeMode.StretchImage;
            nameLbl.Text = Global.item.First(q => q.Barcode.Contains(barcode)).Name;
            barcodeTxt.Text = Global.item.First(q => q.Barcode.Contains(barcode)).Barcode;

        }
        void Stock(string barcode) {

            

            qtyTxt.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Qty;
            batchTxt.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Batch;
            saleTxt.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Sale_price;
            purchaseTxt.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Purchase_price;
            expireDate.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Expire;
            manufactureDate.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Date_manufactured;
            formulationCbx.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Packing;
            unitsTxt.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Units;
            minTxt.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Min_qty;
            takingTxt.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Taking;
            qtyTxt.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Qty;
            promoPriceTxt.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Promo_price;
            try
            {
                promoStart.Text = Convert.ToDateTime(Global.stock.First(q => q.Barcode.Contains(barcode)).Promo_start).ToString();
                promoEnd.Text = Convert.ToDateTime(Global.stock.First(q => q.Barcode.Contains(barcode)).Promo_end).ToString();
            }
            catch { }
            taxTxt.Text = Global.stock.First(q => q.Barcode.Contains(barcode)).Tax;

            barcodeTxt.Text = Global.item.First(q => q.Barcode.Contains(barcode)).Barcode;
            //Image img = Base64ToImage(Global.item.First(k => k.Barcode.Contains(barcode)).Image);
            //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            ////Bitmap bps = new Bitmap(bmp, 50, 50);
            //imgCapture.Image = bmp;
            //imgCapture.SizeMode = PictureBoxSizeMode.StretchImage;
            nameLbl.Text = Global.item.First(q => q.Barcode.Contains(barcode)).Name;


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

        private void button2_Click(object sender, EventArgs e)
        {
            if (Global.stock.Where(a => a.Barcode.Contains(Barcode) && a.StoreID.Contains(Helper.OrgID)).Count() > 0)
            {
                string id = Global.stock.First(y => y.Barcode.Contains(Barcode) && y.StoreID.Contains(Helper.OrgID)).Id;
                double totalValue = Convert.ToDouble(qtyTxt.Text) * Convert.ToDouble(saleTxt.Text);
                Stock _stk = new Stock(id, Barcode, qtyTxt.Text, saleTxt.Text, purchaseTxt.Text, saleTxt.Text, totalValue.ToString(), batchTxt.Text, Convert.ToDateTime(expireDate.Text).ToString("dd-MM-yyyy"), formulationCbx.Text, unitsTxt.Text, barcodeTxt.Text, Convert.ToDateTime(manufactureDate.Text).ToString("dd-MM-yyyy"), qtyTxt.Text, minTxt.Text, qtyTxt.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), taxTxt.Text, promoPriceTxt.Text, Convert.ToDateTime(promoStart.Text).ToString("dd-MM-yyyy"), Convert.ToDateTime(promoEnd.Text).ToString("dd-MM-yyyy"), DateTime.Now.ToString("dd-MM-yyyy H:m:s"), Helper.StoreID, Helper.OrgID, Helper.UserID);
                DBConnect.Update(_stk, Barcode);
                Global.stock.RemoveAll(x => x.Barcode == Barcode);
                Global.stock.Add(_stk);
                MessageBox.Show("Information Saved");
                this.DialogResult = DialogResult.OK;
                this.Dispose();

            }
            else
            {
                double totalValue = Convert.ToDouble(qtyTxt.Text) * Convert.ToDouble(saleTxt.Text);
                string id = Guid.NewGuid().ToString();
                Stock _stk = new Stock(id, Barcode, qtyTxt.Text, saleTxt.Text, purchaseTxt.Text, saleTxt.Text, totalValue.ToString(), batchTxt.Text, Convert.ToDateTime(expireDate.Text).ToString("dd-MM-yyyy"), formulationCbx.Text, unitsTxt.Text, barcodeTxt.Text, Convert.ToDateTime(manufactureDate.Text).ToString("dd-MM-yyyy"), qtyTxt.Text, minTxt.Text, qtyTxt.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"), taxTxt.Text, promoPriceTxt.Text, Convert.ToDateTime(promoStart.Text).ToString("dd-MM-yyyy"), Convert.ToDateTime(promoEnd.Text).ToString("dd-MM-yyyy"), DateTime.Now.ToString("dd-MM-yyyy H:m:s"), Helper.StoreID, Helper.OrgID, Helper.UserID);
                DBConnect.Insert(_stk);
                Global.stock.Add(_stk);
                MessageBox.Show("Information Saved");
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}
