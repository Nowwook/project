using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server123
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constring = 
                "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=hr;Password=hr;";
            OracleConnection conDataBase = new OracleConnection(constring);
            OracleCommand cmdDataBase = new OracleCommand("select * from STOCK_Main", conDataBase);

            try
            {
                OracleDataAdapter SD = new OracleDataAdapter();
                SD.SelectCommand = cmdDataBase;
                DataTable DB = new DataTable();
                SD.Fill(DB);

                BindingSource Source = new BindingSource();
                Source.DataSource = DB;
                dataGridView1.DataSource = Source;
                SD.Update(DB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
