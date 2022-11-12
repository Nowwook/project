using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server123
{
    public partial class Form1 : Form
    {
        string[] a = new string[4];
        string txt, E;
        int A, B, C, D, pix = 1;
        int[] arr2 = new int[4] { 0, 0, 0, 0 }; //대컵,소컵,탄산,얼음

        static string strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=hr;Password=hr;";

        OracleCommand cmd = new OracleCommand();
        OracleConnection conn = new OracleConnection(strConn);

        static Socket clientSock;
        static Thread recvThread;
        public Form1()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread srvthread = new Thread(serverFunc);
            srvthread.IsBackground = true;
            srvthread.Start();
            Stockcnt2();
            BarColor2();

            Thread del = new Thread(update);
            del.Start();

        }
        public void Stockcnt2()
        {
            conn = new OracleConnection(strConn);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "select cnt from STOCK_Main where 이름='버번' ";
            OracleDataReader ax = cmd.ExecuteReader();
            while (ax.Read())
            {
                toolTip1.SetToolTip(panel11, ax["cnt"].ToString());
                panel11.Width = Int32.Parse(ax["cnt"].ToString()) * 10;
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='엘더' ";
            OracleDataReader bx = cmd.ExecuteReader();
            while (bx.Read())
            {
                toolTip2.SetToolTip(panel2, bx["cnt"].ToString());
                panel2.Width = Int32.Parse(bx["cnt"].ToString()) * 10;
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='피치' ";
            OracleDataReader cx = cmd.ExecuteReader();
            while (cx.Read())
            {
                toolTip3.SetToolTip(panel3, cx["cnt"].ToString());
                panel3.Width = Int32.Parse(cx["cnt"].ToString()) * 10;
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='소컵' ";
            OracleDataReader dx = cmd.ExecuteReader();
            while (dx.Read())
            {
                toolTip4.SetToolTip(panel5, dx["cnt"].ToString());
                panel5.Width = Int32.Parse(dx["cnt"].ToString()) * 10;
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='대컵' ";
            OracleDataReader ex = cmd.ExecuteReader();
            while (ex.Read())
            {
                toolTip5.SetToolTip(panel6, ex["cnt"].ToString());
                panel6.Width = Int32.Parse(ex["cnt"].ToString()) * 10;
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='얼음' ";
            OracleDataReader fx = cmd.ExecuteReader();
            while (fx.Read())
            {
                toolTip6.SetToolTip(panel7, fx["cnt"].ToString());
                panel7.Width = Int32.Parse(fx["cnt"].ToString()) * 10;
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='탄산' ";
            OracleDataReader gx = cmd.ExecuteReader();
            while (gx.Read())
            {
                toolTip7.SetToolTip(panel8, gx["cnt"].ToString());
                panel8.Width = Int32.Parse(gx["cnt"].ToString()) * 10;
            }
            conn.Close();
            cmd.Dispose();
        }
        public void BarColor2()
        {
            conn = new OracleConnection(strConn);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "select cnt from STOCK_Main where 이름='버번' ";
            OracleDataReader ax = cmd.ExecuteReader();
            while (ax.Read())
            {
                if (Int32.Parse(ax["cnt"].ToString()) < 3)
                {
                    panel11.BackColor = Color.Black;
                }
                else
                {
                    panel11.BackColor = Color.Orange;
                }
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='엘더' ";
            OracleDataReader bx = cmd.ExecuteReader();
            while (bx.Read())
            {
                if (Int32.Parse(bx["cnt"].ToString()) < 3)
                {
                    panel2.BackColor = Color.Black;
                }
                else
                {
                    panel2.BackColor = Color.Gold;
                }
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='피치' ";
            OracleDataReader cx = cmd.ExecuteReader();
            while (cx.Read())
            {
                if (Int32.Parse(cx["cnt"].ToString()) < 3)
                {
                    panel3.BackColor = Color.Black;
                }
                else
                {
                    panel3.BackColor = Color.Red;
                }
            }
            conn.Close();
            cmd.Dispose();
        }
        void serverFunc(object obj)
        {
            try
            {
                Socket serverSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 12300);
                serverSock.Bind(endPoint);
                serverSock.Listen(10);
                clientSock = serverSock.Accept();
                recvThread = new Thread(new ThreadStart(Recv));
                recvThread.IsBackground = true;
                recvThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void Recv()
        {
            while (true)
            {
                byte[] recvBytes = new byte[1024];
                clientSock.Receive(recvBytes);
                txt = Encoding.UTF8.GetString(recvBytes, 0, recvBytes.Length);
                richTextBox1.Text += (txt);
                richTextBox1.Text += "\r\n";

                a = txt.Split(' ');
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 order = new Form3();
            order.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 stat = new Form2();
            stat.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Oderif();
            stockup();

            timer1_Tick(this, e);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form4 stat = new Form4();
            stat.ShowDialog();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            Arram();
        }
        public void stockup()
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(strConn);
            OracleTransaction STrans = null;
            conn.Open();
            STrans = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = STrans;
            if (a[2] == "유")
            {
                if (a[1] == "대")
                {
                    arr2[3] = 2 * Int32.Parse(a[3]);
                }
                if (a[1] == "소")
                {
                    arr2[3] = Int32.Parse(a[3]);
                }
            }
            if (a[1] == "대")
            {
                arr2[0] = Int32.Parse(a[3]);
                arr2[2] = 2 * Int32.Parse(a[3]);
            }
            if (a[1] == "소")
            {
                arr2[1] = Int32.Parse(a[3]);
                arr2[2] = Int32.Parse(a[3]);
            }

            if (a[0] != "버번")
            {
                cmd.CommandText = $"UPDATE STOCK_Main SET CNT=CNT- {arr2[2]} where 이름='탄산'";
                cmd.ExecuteNonQuery();
            }
            cmd.CommandText = "UPDATE STOCK_Main SET CNT=CNT- " + arr2[3] + " where 이름='얼음'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "UPDATE STOCK_Main SET CNT=CNT- " + arr2[0] + " where 이름='대컵'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "UPDATE STOCK_Main SET CNT=CNT- " + arr2[1] + " where 이름='소컵'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"UPDATE STOCK_Main SET CNT=CNT- {Int32.Parse(a[3])} where 이름='{a[0]}'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"insert into oder (cnt,name,date_for)  values({Int32.Parse(a[3])},'{a[0]}',to_char(TO_CHAR(SYSDATE, 'YY-MM-DD')))";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "commit";
            cmd.Transaction.Commit();
            conn.Close();
            cmd.Dispose();
        }
        public void Oderif()
        {
            if (a[0] == "올드패션드")
            {
                a[0] = "버번";
            }
            if (a[0] == "하이볼")
            {
                a[0] = "엘더";
            }
            if (a[0] == "피치크러쉬")
            {
                a[0] = "피치";
            }
            pictureBox2.Visible = true;
            if (a[2] == "유")
            {
                if (a[1] == "대")
                {
                    if (a[0] == "버번")
                    {
                        E = "b";
                        D = 22;
                    }
                    if (a[0] == "엘더")
                    {
                        E = "d";
                        D = 21;
                    }
                    if (a[0] == "피치")
                    {
                        E = "f";
                        D = 21;
                    }
                }
                else if (a[1] == "소")
                {
                    if (a[0] == "버번")
                    {
                        E = "a";
                        D = 20;
                    }
                    if (a[0] == "엘더")
                    {
                        E = "c";
                        D = 21;
                    }
                    if (a[0] == "피치")
                    {
                        E = "e";
                        D = 21;
                    }
                }
            }
            if (a[2] == "무")
            {
                if (a[1] == "대")
                {
                    if (a[0] == "버번")
                    {
                        E = "h";
                        D = 18;
                    }
                    if (a[0] == "엘더")
                    {
                        E = "j";
                        D = 17;
                    }
                    if (a[0] == "피치")
                    {
                        E = "l";
                        D = 17;
                    }
                }
                else if (a[1] == "소")
                {
                    if (a[0] == "버번")
                    {
                        E = "g";
                        D = 16;
                    }
                    if (a[0] == "엘더")
                    {
                        E = "i";
                        D = 17;
                    }
                    if (a[0] == "피치")
                    {
                        E = "k";
                        D = 17;
                    }
                }
            }

        }
        public void Arram()
        {
            conn = new OracleConnection(strConn);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;

            string x = "";
            bool y = false;

            cmd.CommandText = "select date_for,sum(cnt) from oder where name='버번' and date_for=TO_DATE(sysdate-1,'yyyy/mm/dd') group by date_for order by date_for";
            OracleDataReader sum2 = cmd.ExecuteReader();
            while (sum2.Read())
            {
                A = Int32.Parse(sum2["sum(cnt)"].ToString());
            }

            cmd.CommandText = "select cnt from STOCK_Main where 이름='버번' ";
            OracleDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (Int32.Parse(rdr["cnt"].ToString()) < A * 0.8)
                {
                    x += "버번\n";
                    y = true;
                }
            }
            cmd.CommandText = "select date_for,sum(cnt) from oder where name='엘더' and date_for=TO_DATE(sysdate-1,'yyyy/mm/dd') group by date_for order by date_for ";
            OracleDataReader sum3 = cmd.ExecuteReader();
            while (sum3.Read())
            {
                B = Int32.Parse(sum3["sum(cnt)"].ToString());
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='엘더' ";
            OracleDataReader rdr3 = cmd.ExecuteReader();
            while (rdr3.Read())
            {
                if (Int32.Parse(rdr3["cnt"].ToString()) < B * 0.8)
                {
                    x += "엘더\n";
                    y = true;
                }
            }
            cmd.CommandText = "select date_for,sum(cnt) from oder where name='피치' and date_for=TO_DATE(sysdate-1,'yyyy/mm/dd') group by date_for order by date_for";
            OracleDataReader sum4 = cmd.ExecuteReader();
            while (sum4.Read())
            {
                C = Int32.Parse(sum4["sum(cnt)"].ToString());
            }
            cmd.CommandText = "select cnt from STOCK_Main where 이름='피치' ";
            OracleDataReader rdr6 = cmd.ExecuteReader();
            while (rdr6.Read())
            {
                if (Int32.Parse(rdr6["cnt"].ToString()) < C * 0.8)
                {
                    x += "피치\n";
                    y = true;
                }
            }
            if (y == true)
            {
                MessageBox.Show($"오늘 하루\n{x}높은 확률로 재고 부족", "예측");
                y = false;
            }
            else
            {
                MessageBox.Show("오늘은 충분", "예측");
            }
            conn.Close();
            cmd.Dispose();
        }
        public void anime(string b, int c)
        {
            timer1.Enabled = true;
            pictureBox2.Image = Image.FromFile(System.Environment.CurrentDirectory + $"/{b}/" + pix + ".png");
            pix++;
            if (pix == c)
            {
                pix = 1;
                timer1.Enabled = false;
                pictureBox2.Visible = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            anime(E, D);
        }
        public void update()
        {
            while (true)
            {
                Thread.Sleep(8000);
                Stockcnt2();
                BarColor2();
            }
        }
    }
}
