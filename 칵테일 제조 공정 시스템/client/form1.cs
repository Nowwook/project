using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Form1 : Form
    {
        Socket socket;
        public Form1()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label_watermark.Visible = textBox1.Text.Length < 1;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. 소켓만들기
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 2. 연결
            IPEndPoint serverEp = new IPEndPoint(IPAddress.Parse("192.168.0.36"), 12300);
            socket.Connect(serverEp);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string str = "";
                string num = textBox1.Text;
                // 메뉴 주문

                // 올드패션드
                if (radioButton_menu_old.Checked == true)
                {
                    str += radioButton_menu_old.Text;
                    // 사이즈 대
                    if (radioButton_size_big.Checked == true)
                    {
                        if (radioButton_ice_big.Checked == true)
                        {
                            str += " " + radioButton_size_big.Text+" " + radioButton_ice_big.Text + " " + num;
                        }
                        else if (radioButton_ice_sm.Checked == true)
                        {
                            str += " " + radioButton_size_big.Text+" " + radioButton_ice_sm.Text + " " + num;
                        }
                    }
                    // 사이즈 소
                    if (radioButton_size_sm.Checked == true)
                    {
                        str += " " + radioButton_size_sm.Text;
                        if (radioButton_ice_big.Checked == true)
                        {
                            str += " " + radioButton_ice_big.Text + " " + num;
                        }
                        else if (radioButton_ice_sm.Checked == true)
                        {
                            str += " " + radioButton_ice_sm.Text + " " + num;
                        }
                    }
                }
                // 하이볼
                if (radioButton_menu_high.Checked == true)
                {
                    str += radioButton_menu_high.Text;
                    // 사이즈 대
                    if (radioButton_size_big.Checked == true)
                    {
                        str += " " + radioButton_size_big.Text;
                        if (radioButton_ice_big.Checked == true)
                        {
                            str += " " + radioButton_ice_big.Text + " " + num;
                        }
                        else if (radioButton_ice_sm.Checked == true)
                        {
                            str += " " + radioButton_ice_sm.Text + " " + num;
                        }
                    }
                    // 사이즈 소
                    if (radioButton_size_sm.Checked == true)
                    {
                        str += " " + radioButton_size_sm.Text;
                        if (radioButton_ice_big.Checked == true)
                        {
                            str += " " + radioButton_ice_big.Text + " " + num;
                        }
                        else if (radioButton_ice_sm.Checked == true)
                        {
                            str += " " + radioButton_ice_sm.Text + " " + num;
                        }
                    }
                }
                // 피치크러쉬
                if (radioButton_menu_peach.Checked == true)
                {
                    str += radioButton_menu_peach.Text;
                    // 사이즈 대
                    if (radioButton_size_big.Checked == true)
                    {
                        str += " " + radioButton_size_big.Text;
                        if (radioButton_ice_big.Checked == true)
                        {
                            str += " " + radioButton_ice_big.Text + " " + num;
                        }
                        else if (radioButton_ice_sm.Checked == true)
                        {
                            str += " " + radioButton_ice_sm.Text + " " + num;
                        }
                    }
                    // 사이즈 소
                    if (radioButton_size_sm.Checked == true)
                    {
                        str += " " + radioButton_size_sm.Text;
                        if (radioButton_ice_big.Checked == true)
                        {
                            str += " " + radioButton_ice_big.Text + " " + num;
                        }
                        else if (radioButton_ice_sm.Checked == true)
                        {
                            str += " " + radioButton_ice_sm.Text + " " + num;
                        }
                    }
                }

                MessageBox.Show("주문이 완료되었습니다!", "알림", MessageBoxButtons.OK);
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                socket.Send(buffer);
            }
            else
            {
                MessageBox.Show("주문 내역을 확인해주세요", "알림", MessageBoxButtons.OK);
            }
        }
    }
}
