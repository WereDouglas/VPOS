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
    public partial class MainForm : Form
    {
        public static MainForm _Form1;
        Organisation _org;
        List<Organisation> _orgList;
        private BackgroundWorker bwLite = new BackgroundWorker();
        public MainForm()
        {
            InitializeComponent();
            nameLbl.Text = Helper.Username;
            Image img = Base64ToImage(Helper.Image);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);          
            imageDropDown.Image = bmp;
            bwLite.DoWork += backgroundWorker1_DoWork;
            bwLite.ProgressChanged += backgroundWorker1_ProgressChanged;
            bwLite.WorkerReportsProgress = true;
            _Form1 = this;
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1 * 60 * 1000;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            lastSyncLbl.Text = Helper.lastSync;

        }
         void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!bwLite.IsBusy)
            {
                bwLite.RunWorkerAsync();
            }
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
           
            if (Uploading.CheckServer())
            {
                
                for (int i = 0; i < 16; i++)
                {
                    FeedBack("PROCESS " + i.ToString());
                    //try
                    //{
                        process(i);
                    //}
                    //catch (Exception c)
                    //{
                    //    FeedBack("PROCESSING ERROR " + i.ToString() + " " + c.Message.ToString());
                    //}

                    bwLite.ReportProgress(i);
                    Thread.Sleep(1500);

                }
            }
            else
            {

                FeedBack("No internet connection ");
                bwLite.Dispose();


            }

        }
        private void process(int count)
        {
            int val = count;
            switch (val)
            {
                case 1: 
                    Uploading.UploaOrganisation();
                  
                    break;
                case 2: 

                    Uploading.UploadUsers();

                    break;
                case 3:

                    Uploading.UploadSale();
                    break;
                case 4: 

                    Uploading.UploadItem();
                    break;
                case 5:
                    Uploading.UploadBilling();
                    break;
                case 6:
                    Uploading.UploadRoles();
                    break;
                case 7:
                    Uploading.UploadTransactor();
                    break;
                case 8:
                    Uploading.UploadCategory();
                    break;
                case 9:
                    Uploading.UploadExpense();
                    break;
                case 10:
                    Uploading.UploadPayment();
                    break;
                case 11:
                    Uploading.UploadStore();
                    break;
                case 12:
                    Uploading.UploadTaking();
                    break;
                case 13:
                    Uploading.UploadStock();
                    break;
                case 14: 
                 
                    if (Uploading.CheckServer())
                    {
                        Uploading.updateSyncTime();
                        FeedBack("Uploading and Downloading of information complete");

                        FeedBack("LAST SYNC " + Helper.lastSync);
                        bwLite.Dispose();
                        return;
                    }
                    else
                    {
                        FeedBack("No valid connection ");

                    }
                    break;              
                default:
                    FeedBack("Processing");
                    break;
            }
      
        }
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                toolStripProgressBar1.Value = e.ProgressPercentage;
            }
            catch { }
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
        public void FeedBack(string text)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    processLbl.Text = processLbl.Text + text + "\r\n";
                    processLbl.ForeColor = Color.White;
                });
            }
            catch
            {


            }

        }
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            using (ItemDialog form = new ItemDialog(""))
            {
                // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // MessageBox.Show(form.state);

                }
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            SaleForm frm = new SaleForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DashBoard frm = new DashBoard();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            MerchandiseForm frm = new MerchandiseForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {

            ManageForm frm = new ManageForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            BillingForm frm = new BillingForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            CategoryForm frm = new CategoryForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            TransactorForm frm = new TransactorForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            StockForm frm = new StockForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            TransactionForm frm = new TransactionForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            CashFlowForm frm = new CashFlowForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            ExpenseForm frm = new ExpenseForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            PurchaseForm frm = new PurchaseForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            using (UserDialog form = new UserDialog(""))
            {
                // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // MessageBox.Show(form.state);

                }
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DashBoard frm = new DashBoard();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RoleForm form = new RoleForm())
            {
                // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // MessageBox.Show(form.state);

                }
            }
        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OrganisationDialog form = new OrganisationDialog(Helper.OrgID))
            {
                // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // MessageBox.Show(form.state);

                }
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewUsers frm = new ViewUsers();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void myProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (UserDialog form = new UserDialog(Helper.UserID))
            {
                // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // MessageBox.Show(form.state);

                }
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            if (processLbl.Visible == true)
            {
                processLbl.Visible = false;
            }
            else
            {
                processLbl.Visible = true;
            }
        }

        private void cashBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashBook frm = new CashBook();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            CTRegister frm = new CTRegister();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            StoreForm frm = new StoreForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {
           StockRecords frm = new StockRecords();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {

        }

        private void profitAndLossToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
