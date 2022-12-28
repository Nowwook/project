using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp4.ucPanel
{
	
    public partial class ucScreen3 : UserControl
    {
        public ucScreen3()
        {
            InitializeComponent();
			
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		

		private void ucScreen3_Load(object sender, EventArgs e)
		{
			
			comboBox1.SelectedIndex = 0;
			comboBox2.SelectedIndex = 0;
			comboBox3.SelectedIndex = 0;


			string normal = Program.f_function.cnt_normal();
			string abnormal = Program.f_function.cnt_abnormal();

			string hot = Program.f_function.cnt_hot();
			string dead = Program.f_function.cnt_dead();
			string stuck = Program.f_function.cnt_stuck();


			chart1.Series[0].Points.AddXY($"정상\n{Int32.Parse(normal)}", Int32.Parse(normal));
			chart1.Series[0].Points.AddXY($"비정상\n{Int32.Parse(abnormal)}", Int32.Parse(abnormal));

			chart3.Series[0].Points.AddXY($"핫픽셀\n{Int32.Parse(hot)}", Int32.Parse(hot));
			chart3.Series[0].Points.AddXY($"데드픽셀\n{Int32.Parse(dead)}", Int32.Parse(dead));
			chart3.Series[0].Points.AddXY($"스턱픽셀\n{ Int32.Parse(stuck)}", Int32.Parse(stuck));			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string str_inch = comboBox1.SelectedItem.ToString();
			string str_panel = comboBox2.SelectedItem.ToString();
			string str_hz = comboBox3.SelectedItem.ToString();

			DataTable dt = Program.f_function.select_PRD(str_inch, str_panel, str_hz);
			dataGridView1.DataSource = dt;
            Chartupdate(Program.f_function.Date(str_inch, str_panel, str_hz));
		}


        public void Chartupdate(int[] date)
        {
            chart1.Series[0].Points.Clear();
            try
            {
                if (chart1.Series.IndexOf("new") != -1)
                {
                    chart1.Series.Clear();
                }
            }
            catch { }
            chart1.Series.Add("new");
            chart1.Series["new"].ChartType = SeriesChartType.Doughnut;
            chart1.Series["new"].Palette = ChartColorPalette.SeaGreen;
            chart1.Series["new"].Points.AddXY($"정상\n{date[0]}", date[0]);
            chart1.Series["new"].Points.AddXY($"비정상\n{date[1]}", date[1]);

            chart3.Series[0].Points.Clear();
            try
            {
                if (chart3.Series.IndexOf("new") != -1)
                {
                    chart3.Series.Clear();
                }
            }
            catch { }
            chart3.Series.Add("new");
            chart3.Series["new"].ChartType = SeriesChartType.Doughnut;
            chart3.Series["new"].Palette = ChartColorPalette.SeaGreen;
            chart3.Series["new"].Points.AddXY($"핫픽셀\n{date[2]}",date[2]);
            chart3.Series["new"].Points.AddXY($"데드픽셀\n{date[3]}", date[3]);
            chart3.Series["new"].Points.AddXY($"스턱픽셀\n{date[4]}", date[4]);
        }

    }
}
