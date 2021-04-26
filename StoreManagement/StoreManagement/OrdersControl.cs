using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagement
{

    public partial class OrdersControl : UserControl
    {
        public int count = 0;


        public OrdersControl()
        {
            InitializeComponent();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        //
        public void show()
        {
            count++;
            if (count > 1) return;
            ShowControls s = new ShowControls(flowLayoutPanel1);
            s.Show("SPinOrders");


        }

        private void button7_Click(object sender, EventArgs e)//save
        {
            string[] str = new string[10];
            //
            str[0] = txtMaKH.Text;
            str[1] = txtTenKH.Text;
            str[2] = txtSdtKH.Text;
            str[3] = txtDiaChiKH.Text;
            str[4] = new RandomID().MaHD();
            str[5] = "NvVIP";
            str[6] = DateTime.Now.ToString();
            str[7] = txtTong.Text;
            //
            SaveBillsTemplate order = new Order(str, dataGridView1);
            order.Save();
        }

        private void OrdersControl_Load(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        public void test(string msg)
        {
            txtMaSP.Text = msg;
            this.Refresh();
        }
       
       
    }



   
}
