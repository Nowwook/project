using HtmlAgilityPack;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server123
{
    public partial class Form3 : Form
    {
        string strConn =
            "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
            "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=hr;Password=hr;";

        OracleConnection conn;
        OracleCommand cmd;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Stockcnt2();
            BarColor2();
            Alarm2();
            Parsing1();
            Parsing2();
            //Parsing3();
        }
        public void Stockcnt2()
        {
            conn = new OracleConnection(strConn);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "select cnt from STOCK_Main where 이름='버번' ";
            cmd.ExecuteNonQuery();
            OracleDataReader ax = cmd.ExecuteReader();
            while (ax.Read())
            {
                toolTip1.SetToolTip(panel7, ax["cnt"].ToString());
                panel7.Width = Int32.Parse(ax["cnt"].ToString()) * 10;
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='엘더' ";
            cmd.ExecuteNonQuery();
            OracleDataReader bx = cmd.ExecuteReader();
            while (bx.Read())
            {
                toolTip2.SetToolTip(panel6, bx["cnt"].ToString());
                panel6.Width = Int32.Parse(bx["cnt"].ToString()) * 10;
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='피치' ";
            cmd.ExecuteNonQuery();
            OracleDataReader cx = cmd.ExecuteReader();
            while (cx.Read())
            {
                toolTip3.SetToolTip(panel5, cx["cnt"].ToString());
                panel5.Width = Int32.Parse(cx["cnt"].ToString()) * 10;
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='소컵' ";
            cmd.ExecuteNonQuery();
            OracleDataReader dx = cmd.ExecuteReader();
            while (dx.Read())
            {
                textBox4.Text = dx["cnt"].ToString();
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='대컵' ";
            cmd.ExecuteNonQuery();
            OracleDataReader ex = cmd.ExecuteReader();
            while (ex.Read())
            {
                textBox5.Text = ex["cnt"].ToString();
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='얼음' ";
            cmd.ExecuteNonQuery();
            OracleDataReader fx = cmd.ExecuteReader();
            while (fx.Read())
            {
                textBox6.Text = fx["cnt"].ToString();
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='탄산' ";
            cmd.ExecuteNonQuery();
            OracleDataReader gx = cmd.ExecuteReader();
            while (gx.Read())
            {
                textBox7.Text = gx["cnt"].ToString();
            }
            conn.Close();
        }
        public void BarColor2()
        {
            conn = new OracleConnection(strConn);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "select cnt from STOCK_Main where 이름='버번' ";
            cmd.ExecuteNonQuery();
            OracleDataReader ax = cmd.ExecuteReader();
            while (ax.Read())
            {
                if (Int32.Parse(ax["cnt"].ToString()) < 3)
                {
                    panel7.BackColor = Color.Black;
                }
                else
                {
                    panel7.BackColor = Color.Orange;
                }
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='엘더' ";
            cmd.ExecuteNonQuery();
            OracleDataReader bx = cmd.ExecuteReader();
            while (bx.Read())
            {
                if (Int32.Parse(bx["cnt"].ToString()) < 3)
                {
                    panel6.BackColor = Color.Black;
                }
                else
                {
                    panel6.BackColor = Color.Gold;
                }
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='피치' ";
            cmd.ExecuteNonQuery();
            OracleDataReader cx = cmd.ExecuteReader();
            while (cx.Read())
            {
                if (Int32.Parse(cx["cnt"].ToString()) < 3)
                {
                    panel5.BackColor = Color.Black;
                }
                else
                {
                    panel5.BackColor = Color.Red;
                }
            }
            conn.Close();
        }
        public void Alarm2()
        {
            conn = new OracleConnection(strConn);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;
            string x = "";
            bool y = false;
            cmd.CommandText = "select cnt from STOCK_Main where 이름='버번' ";
            cmd.ExecuteNonQuery();
            OracleDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (Int32.Parse(rdr["cnt"].ToString()) < 3)
                {
                    x += "버번\n";
                    y = true;
                }
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='얼음' ";
            cmd.ExecuteNonQuery();
            OracleDataReader rdr1 = cmd.ExecuteReader();
            while (rdr1.Read())
            {
                if (Int32.Parse(rdr1["cnt"].ToString()) < 3)
                {
                    x += "얼음\n";
                    y = true;
                }
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='탄산' ";
            cmd.ExecuteNonQuery();
            OracleDataReader rdr2 = cmd.ExecuteReader();
            while (rdr2.Read())
            {
                if (Int32.Parse(rdr2["cnt"].ToString()) < 3)
                {
                    x += "탄산\n";
                    y = true;
                }
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='엘더' ";
            cmd.ExecuteNonQuery();
            OracleDataReader rdr3 = cmd.ExecuteReader();
            while (rdr3.Read())
            {
                if (Int32.Parse(rdr3["cnt"].ToString()) < 3)
                {
                    x += "엘더\n";
                    y = true;
                }
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='피치' ";
            cmd.ExecuteNonQuery();
            OracleDataReader rdr6 = cmd.ExecuteReader();
            while (rdr6.Read())
            {
                if (Int32.Parse(rdr6["cnt"].ToString()) < 3)
                {
                    x += "피치\n";
                    y = true;
                }
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='소컵' ";
            cmd.ExecuteNonQuery();
            OracleDataReader rdr4 = cmd.ExecuteReader();
            while (rdr4.Read())
            {
                if (Int32.Parse(rdr4["cnt"].ToString()) < 3)
                {
                    x += "소컵\n";
                    y = true;
                }
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='대컵' ";
            cmd.ExecuteNonQuery();
            OracleDataReader rdr5 = cmd.ExecuteReader();
            while (rdr5.Read())
            {
                if (Int32.Parse(rdr5["cnt"].ToString()) < 3)
                {
                    x += "대컵\n";
                    y = true;
                }
            }
            if (y == true)
            {
                MessageBox.Show(x, "재고 부족");
                y = false;
            }
            conn.Close();
        }
        public void Parsing1()
        {
            var html_c = "https://www.thewhiskyexchange.com/p/15620/buffalo-trace-bourbon?source=awin&aw_publisherid=62838&aw_creativeid=0&aw_productid=0&aw_sitedomain=Conosr&awc=400_1660697120_6bdba4934158f72c61e8e27d79681294";
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html_c);
            var text = htmlDoc.DocumentNode.SelectSingleNode("//p[@ class ='product-action__price']").InnerText;

            var html_ = "https://www.google.com/search?q=%ED%8C%8C%EC%9A%B4%EB%93%9C+%ED%99%98%EC%9C%A8&source=lmns&bih=963&biw=1669&rlz=1C1CAFC_enKR950KR982&hl=ko&sa=X&ved=2ahUKEwiy8rrD4Mz5AhVF95QKHdwXD78Q_AUoAHoECAEQAA";
            HtmlAgilityPack.HtmlWeb we = new HtmlWeb();
            var htmlD = we.Load(html_);
            var text_ = htmlD.DocumentNode.SelectSingleNode("//span[@ class ='DFlfde SwHCTb']").InnerText;

            double dar = double.Parse(text_.Trim());
            double won = double.Parse(text.Trim().Remove(0, 1));
            won = Math.Round(won + won * dar, 0);
            textBox1.Text = Convert.ToString(won);
        }
        public void Parsing2()
        {
            var html_a = "https://shop.stgermainliqueur.com/products/carafe";
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html_a);
            var text = htmlDoc.DocumentNode.SelectSingleNode("//div[@ class ='product-price']").InnerText;

            var html_c = "https://www.google.com/search?q=%EB%8B%AC%EB%9F%AC%ED%99%98%EC%9C%A8&rlz=1C1CAFC_enKR950KR982&oq=ekffj&aqs=chrome.1.69i57j0i131i433i512l3j46i131i433i512j0i131i433i512l4j0i512.2147j0j4&sourceid=chrome&ie=UTF-8";
            HtmlAgilityPack.HtmlWeb we = new HtmlWeb();
            var htmlD = we.Load(html_c);
            var text_ = htmlD.DocumentNode.SelectSingleNode("//span[@ class ='DFlfde SwHCTb']").InnerText;

            double dar = double.Parse(text_.Trim());
            double won = double.Parse(text.Trim().Remove(0, 1));
            won = Math.Round(won + won * dar, 0);
            textBox2.Text = Convert.ToString(won);
        }
        public void Parsing3()
        {
            var html_c = "http://wine1865.co.kr/goods/list.php?category_code_fk=004&l_category_fk=001&mode=search&key_value=%ED%94%BC%EC%B9%98&x=0&y=0";
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html_c);
            var text = htmlDoc.DocumentNode.SelectSingleNode("//dd[@ class ='price']").InnerText;
            textBox3.Text = Convert.ToString(text.Remove(6, 1));
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            conn = new OracleConnection(strConn);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;

            int nu = Int32.Parse(textBox9.Text);

            cmd.CommandText = $"update STOCK_Main set cnt= cnt+{nu} where 이름 = '{textBox8.Text}'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "commit";
            conn.Close();

            Stockcnt2();
            BarColor2();
            Alarm2();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("Chrome.exe", "https://www.buffalotrace.com/");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start("Chrome.exe", "https://www.stgermainliqueur.com/us/en/");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process.Start("Chrome.exe", "https://www.dekuyperusa.com/flavor/dekuyper-peachtree-schnapps-liqueur/");
        }
    }
}
