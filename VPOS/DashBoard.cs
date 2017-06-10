using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using VPOS.Model;

namespace VPOS
{
    public partial class DashBoard : Form
    {
        Item _item;
        Billing _bill;
        string date;
        public DashBoard()
        {
            InitializeComponent();
            date = DateTime.Now.ToString("dd-MM-yyyy");
            LoadData();
            BarExample(); //Show bar chart
            SplineChartExample();
        }
        public void BarExample()
        {
            this.chart1.Series.Clear();

            // Data arrays
            string[] seriesArray = { "Cat", "Dog", "Bird", "Monkey" };
            int[] pointsArray = { 2, 1, 7, 5 };

            // Set palette
            this.chart1.Palette = ChartColorPalette.EarthTones;

            // Set title
            this.chart1.Titles.Add("Animals");

            // Add series.
            for (int i = 0; i < seriesArray.Length; i++)
            {
                Series series = this.chart1.Series.Add(seriesArray[i]);
                series.Points.Add(pointsArray[i]);
            }
        }
        private void SplineChartExample()
        {
            this.chart2.Series.Clear();

            this.chart2.Titles.Add("Total Income");

            Series series = this.chart2.Series.Add("Total Income");
            series.ChartType = SeriesChartType.Spline;
            series.Points.AddXY("September", 100);
            series.Points.AddXY("Obtober", 300);
            series.Points.AddXY("November", 800);
            series.Points.AddXY("December", 200);
            series.Points.AddXY("January", 600);
            series.Points.AddXY("February", 400);
        }
        public void LoadData()
        {
            salesLbl.Text = Global._sale.Where(m=>m.Date.Contains(date) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Qty)).ToString("n0");
         //   stockLbl.Text = Global._item.Sum(p => Convert.ToDouble(p.Quantity)).ToString("n0");
            purchaseLbl.Text = Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Qty)).ToString("n0");
            purchaseamountLbl.Text = "Purchase today: " + Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Total)).ToString("n0");
            saleamountLbl.Text ="Sales today: "+ Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Total)).ToString("n0");

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
