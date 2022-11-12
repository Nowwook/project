using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server123
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        string strConn =
            "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
            "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=hr;Password=hr;";
        OracleConnection conn;
        OracleCommand cmd;
        private void button1_Click(object sender, EventArgs e)
        {
            conn = new OracleConnection(strConn);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "select date_for,sum(cnt) from oder group by date_for order by date_for";
            OracleDataReader sum1 = cmd.ExecuteReader();
            while (sum1.Read())
            {
                chart2.Series["총 합"].Points.AddXY(sum1["date_for"].ToString(),Int32.Parse(sum1["sum(cnt)"].ToString()));
            }
            cmd.CommandText = "select date_for,sum(cnt) from oder where name='버번' group by date_for order by date_for";
            OracleDataReader sum2 = cmd.ExecuteReader();
            while (sum2.Read())
            {
                chart2.Series["올드 패션드"].Points.Add(Int32.Parse(sum2["sum(cnt)"].ToString()));
            }
            cmd.CommandText = "select date_for,sum(cnt) from oder where name='엘더' group by date_for order by date_for ";
            OracleDataReader sum3 = cmd.ExecuteReader();
            while (sum3.Read())
            {
                chart2.Series["엘더플라워"].Points.Add(Int32.Parse(sum3["sum(cnt)"].ToString()));
            }
            cmd.CommandText = "select date_for,sum(cnt) from oder where name='피치' group by date_for order by date_for";
            OracleDataReader sum4 = cmd.ExecuteReader();
            while (sum4.Read())
            {
                chart2.Series["피치 크러쉬"].Points.Add(Int32.Parse(sum4["sum(cnt)"].ToString()));
            }
            cmd.CommandText = "commit";
            conn.Close();
        }
    }
}
