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
    public partial class ExpenseForm : Form
    {
        Expense _exp;
        public ExpenseForm()
        {
            InitializeComponent();
            date = DateTime.Now.ToString("dd-MM-yyyy");
            LoadData(date);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ExpenseDialog form = new ExpenseDialog())
            {
                // DentalDialog form1 = new DentalDialog(item.Text, TransactorID);
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // MessageBox.Show(form.state);
                    LoadData(date);
                }
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        string date;
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            date = Convert.ToDateTime(dateTimePicker1.Text).ToString("dd-MM-yyyy");
            LoadData(date);
        }
        List<Expense> _expenseList = new List<Expense>();
        Item _item;
        Expense _expense;
        WebCam webcam;
        DataTable t;
        Dictionary<string, string> SumDictionary = new Dictionary<string, string>();
        public void LoadData(string date)
        {            

            t = new DataTable();         
           
            t.Columns.Add("Date");//0    
            t.Columns.Add("id");//1             
            t.Columns.Add("Particular");//3 
            t.Columns.Add("Details");//3 
            t.Columns.Add("Quantity");//4
            t.Columns.Add("Price/Cost");//4
            t.Columns.Add("Total");//4  
            t.Columns.Add("Remove");//4   
            _expenseList = Expense.ListExpense().Where(g => g.Created.Contains(date)).ToList();

            SumDictionary.Clear();           
            foreach (Expense h in _expenseList)
            {      
                t.Rows.Add(new object[] { h.Created,h.Id, h.No,h.ItemID,h.Qty,h.Price,h.Total,"Remove"});
                SumDictionary.Add(h.Id, h.Total);
            }
            itemGrid.DataSource = t;
            itemGrid.AllowUserToAddRows = false;
            itemGrid.Columns[1].Visible = false;
            t.Rows.Add(new object[] { "", "", "", "", "", "" });

            t.Rows.Add(new object[] { "", "", "", "", "", "Total", SumDictionary.Sum(m => Convert.ToDouble(m.Value)).ToString("n0") });
            itemGrid.Columns[7].DefaultCellStyle.BackColor = Color.OrangeRed;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
