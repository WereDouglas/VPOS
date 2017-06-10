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

            _categorys = Global._category;
            t = new DataTable();
            // create and execute query 

            t.Columns.Add(new DataColumn("Select", typeof(bool)));
            t.Columns.Add("id");//1
            t.Columns.Add("Name");//2
            t.Columns.Add("Description");// 3       
            t.Columns.Add("Created");// 4
            t.Columns.Add("Delete");// 5

            foreach (Category r in Global._category)
            {

                t.Rows.Add(new object[] { false, r.Id, r.Name, r.Description, r.Created, "Delete" });

            }
            dtGrid.DataSource = t;
            dtGrid.Columns[1].Visible = false;
            dtGrid.Columns[5].DefaultCellStyle.BackColor = Color.OrangeRed;
            dtGrid.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (nameTxt.Text == "")
            {
                nameTxt.BackColor = Color.Red;
                return;
            }

            string id = Guid.NewGuid().ToString();
            _category = new Category(id, nameTxt.Text, descriptionTxt.Text, DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),Helper.OrgID, Helper.StoreID);

            if (DBConnect.Insert(_category) != "")
            {
                Global._category.Add(_category);
                nameTxt.Text = "";
                descriptionTxt.Text = "";
                MessageBox.Show("Information Saved");
                LoadData();

            }
            else
            {
                return;

            }

        }

        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show("YES or No?", "Are you sure you want to delete this information? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DBConnect.Delete("category", dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString());

                    MessageBox.Show("Information deleted");
                    LoadData();

                }
            }
           
        }

        private void dtGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
          string  updateID = dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            _category = new Category(dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[2].Value.ToString(), dtGrid.Rows[e.RowIndex].Cells[3].Value.ToString(), DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"),Helper.OrgID, Helper.StoreID);
            DBConnect.Update(_category, updateID);
            Global._category.RemoveAll(x => x.Id == updateID);
            Global._category.Add(_category);
        }
    }
}
