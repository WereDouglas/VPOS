using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPOS.Model;

namespace VPOS
{
    public partial class StoreForm : Form
    {
        public StoreForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        DataTable t;
        public void LoadData()
        {
            //dtGrid.Refresh();

            t = new DataTable();
            // create and execute query 
            t.Columns.Add("id");//2 
            t.Columns.Add(new DataColumn("Select", typeof(bool)));
            t.Columns.Add("Name");//2
            t.Columns.Add("Contact");//2
            t.Columns.Add("Location");//
            t.Columns.Add("Address");// 
            t.Columns.Add("created");// 

            foreach (Store r in Global._store)
            {
                byte[] MyData = new byte[0];

                t.Rows.Add(new object[] { r.Id, false, r.Name, r.Contact, r.Location, r.Address, r.Created });

            }
            dtGrid.DataSource = t;
            dtGrid.AllowUserToAddRows = false;
            dtGrid.Columns["id"].Visible = false;

            // btnDelete.DisplayIndex = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StoreDialog form = new StoreDialog(""))
            {
                // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // MessageBox.Show(form.state);
                    LoadData();
                }
            }
        }
        Store _store;
        private void dtGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dtGrid.Rows[e.RowIndex].Cells["Name"].Value.ToString()=="")
            {

                MessageBox.Show("Please input a name !");
                return;

            }
            string updateID = dtGrid.Rows[e.RowIndex].Cells["id"].Value.ToString();
            _store = new Store(updateID, dtGrid.Rows[e.RowIndex].Cells["Name"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Location"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Address"].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells["Contact"].Value.ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),Helper.OrgID, Helper.Code);

            DBConnect.Update(_store, updateID);
            Global._store.RemoveAll(x => x.Id == updateID);
            Global._store.Add(_store);
        }
    }
}
