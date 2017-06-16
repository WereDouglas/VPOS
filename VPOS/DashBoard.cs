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
            try
            {
                LoadData();
            }
            catch (Exception g ){ MessageBox.Show(g.Message.ToString()); }
            Bar(); //Show bar chart
            SplineChart();

            Pie();

        }
        public void Pie()
        {
            this.chart3.Series.Clear();            
            Series series = this.chart3.Series.Add("Activity Payments and Expenses");
            series.ChartType = SeriesChartType.Pie;

            try
            {
                double Pay = (double)Global._payment.Where(m => m.Created.Contains(date) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Amount));
                series.Points.AddXY("Purchases:" + Pay.ToString("n0"), Pay);
            }
            catch (Exception c) { MessageBox.Show("Payment:"+c.Message.ToString()); }
            try
            {
                double Spend = (double)Global._expense.Where(m => m.Date.Contains(date)).Sum(p => Convert.ToDouble(p.Total));

                series.Points.AddXY("Expenses:" + Spend.ToString("n0"), Spend);
            }
            catch (Exception c) { MessageBox.Show("Expense:" + c.Message.ToString()); }
            try
            {
                double Sale = (double)Global._payment.Where(m => m.Created.Contains(date) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Amount));

                series.Points.AddXY("Sale:" + Sale.ToString("n0"), Sale);
            }
            catch (Exception c) { MessageBox.Show("Sale:" + c.Message.ToString()); }
        }
        public void Bar()
        {
            this.chart1.Series.Clear();
            DateTime today = DateTime.Today;
            int currentDayOfWeek = (int)today.DayOfWeek;
            DateTime sunday = today.AddDays(-currentDayOfWeek);
            DateTime monday = sunday.AddDays(1);
            // If we started on Sunday, we should actually have gone *back*
            // 6 days instead of forward 1...
            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
                Console.WriteLine(monday.ToString("dd-MM-yyyy"));

            }
            var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();


            double mon = (double)Global._sale.Where(m => m.Date.Contains(dates[0].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Total));
            double tue = (double)Global._sale.Where(m => m.Date.Contains(dates[1].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Total));
            double wed = (double)Global._sale.Where(m => m.Date.Contains(dates[2].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Total));
            double thur = (double)Global._sale.Where(m => m.Date.Contains(dates[3].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Total));
            double fri = (double)Global._sale.Where(m => m.Date.Contains(dates[4].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Total));
            double sat = (double)Global._sale.Where(m => m.Date.Contains(dates[5].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Total));
            double sun = (double)Global._sale.Where(m => m.Date.Contains(dates[6].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Total));

            Series series = this.chart1.Series.Add("Purchases this week");
            series.ChartType = SeriesChartType.Spline;
            series.Points.AddXY("Mon", mon);
            series.Points.AddXY("Tue", tue);
            series.Points.AddXY("Wed", wed);
            series.Points.AddXY("Thur", thur);
            series.Points.AddXY("Fri", fri);
            series.Points.AddXY("Sat", sat);
            series.Points.AddXY("Sun", sun);

        }
        private void SplineChart()
        {
            this.chart2.Series.Clear();

            this.chart2.Titles.Add("Sales this week");

            DateTime today = DateTime.Today;
            int currentDayOfWeek = (int)today.DayOfWeek;
            DateTime sunday = today.AddDays(-currentDayOfWeek);
            DateTime monday = sunday.AddDays(1);
            // If we started on Sunday, we should actually have gone *back*
            // 6 days instead of forward 1...
            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
                Console.WriteLine(monday.ToString("dd-MM-yyyy"));

            }
            var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();


            double mon = (double)Global._sale.Where(m => m.Date.Contains(dates[0].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Total));
            double tue = (double)Global._sale.Where(m => m.Date.Contains(dates[1].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Total));
            double wed = (double)Global._sale.Where(m => m.Date.Contains(dates[2].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Total));
            double thur = (double)Global._sale.Where(m => m.Date.Contains(dates[3].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Total));
            double fri = (double)Global._sale.Where(m => m.Date.Contains(dates[4].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Total));
            double sat = (double)Global._sale.Where(m => m.Date.Contains(dates[5].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Total));
            double sun = (double)Global._sale.Where(m => m.Date.Contains(dates[6].Date.ToString("dd-MM-yyyy")) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Total));


            Series series = this.chart2.Series.Add("Total Sales this week");
            series.ChartType = SeriesChartType.Bar;
            series.Points.AddXY("Mon", mon);
            series.Points.AddXY("Tue", tue);
            series.Points.AddXY("Wed", wed);
            series.Points.AddXY("Thur", thur);
            series.Points.AddXY("Fri", fri);
            series.Points.AddXY("Sat", sat);
            series.Points.AddXY("Sun", sun);
        }
        public void LoadData()
        {
            salesLbl.Text = Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Qty)).ToString("n0");
            //   stockLbl.Text = Global._item.Sum(p => Convert.ToDouble(p.Quantity)).ToString("n0");
            purchaseLbl.Text = Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Qty)).ToString("n0");
            purchaseamountLbl.Text = "Purchase today: " + Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Purchase")).Sum(p => Convert.ToDouble(p.Total)).ToString("n0");
            saleamountLbl.Text = "Sales today: " + Global._sale.Where(m => m.Date.Contains(date) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Total)).ToString("n0");
            expenseLbl.Text = Global._expense.Where(m => m.Date.Contains(date)).Sum(p => Convert.ToDouble(p.Total)).ToString("n0");
            stockLbl.Text = Global._item.Count().ToString();
            profitLbl.Text = Global._payment.Where(m => m.Created.Contains(date) && m.Type.Contains("Sale")).Sum(p => Convert.ToDouble(p.Amount)).ToString("n0");
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
