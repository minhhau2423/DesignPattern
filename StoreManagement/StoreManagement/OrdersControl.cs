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
        public int sl;
        DBfactory Sqlconn = SQLdatabase.getInstanceSQL();
        public OrdersControl()
        {
            InitializeComponent();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dataGridView.AllowUserToAddRows = false;
            /* if (txtSL.Text == "" || txtThanhTien.Text == "")
             {
                 MessageBox.Show("Hãy điền thông tin sản phẩm.");
             }
             else ADD_VALUE();*/
            ADD_VALUE();
        }
        //
        private void ADD_VALUE()
        {
            dataGridView.Rows.Add(1);
            int indexRow = dataGridView.Rows.Count - 1 ;
            dataGridView[0, indexRow].Value = txtMaSP.Text;
            dataGridView[1, indexRow].Value = txtTenSP.Text;
            dataGridView[2, indexRow].Value = txtDonGia.Text;
            dataGridView[3, indexRow].Value = txtSL.Text;
            dataGridView[4, indexRow].Value = txtKM.Text;
            dataGridView[5, indexRow].Value = txtThanhTien.Text;
            int tmp = int.Parse(txtThanhTien.Text);
            if (txtTong.Text == "")
            {
                txtTong.Text = txtThanhTien.Text;
            }
            else
            {
                txtTong.Text = (int.Parse(txtTong.Text) + tmp).ToString();
            }
        }
        public void thanhtien()
        {
            if (txtKM.Text != "")
            {
                int x = (int.Parse(txtSL.Text) * int.Parse(txtDonGia.Text));
                txtThanhTien.Text = (x - x / 100 * int.Parse(txtKM.Text.ToString())).ToString();
            }
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
            SaveBillsTemplate order = new Order(str, dataGridView);
            order.Save();
        }

        private void OrdersControl_Load(object sender, EventArgs e)
        {
            LoadMaSP();
            txtSL.Text = "0";
            txtKM.Text = "0";
            txtDonGia.Text = "0";
            txtMaKH.Text = new RandomID().MaKH();
            thanhtien();
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtSL.Text = (int.Parse(txtSL.Text) + 1).ToString();
            thanhtien();
        }
        public void test(string msg)
        {
            txtMaSP.Text = msg;
            this.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!txtSL.Text.Equals("0"))
            {
                txtSL.Text = (int.Parse(txtSL.Text) - 1).ToString();
            }
            thanhtien();
        }

        private void txtKM_Leave(object sender, EventArgs e)
        {
            if(!txtKM.Text.Equals("")) thanhtien();
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = dataGridView.CurrentRow.Index;
            txtTong.Text = (int.Parse(txtTong.Text) - int.Parse(dataGridView[5, RowIndex].Value.ToString())).ToString();
            dataGridView.Rows.RemoveAt(RowIndex);
        }
        void LoadMaSP()
        {
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            var cm = (SqlCommand)Sqlconn.CreateCommand("SELECT MaSP FROM SanPham ", conn);
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())

            {
                txtMaSP.Items.Add(dr[0]).ToString();
            }
            conn.Close();
        }

        private void txtMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            var cmd = (SqlCommand)Sqlconn.CreateCommand("SELECT TenSP, DonGia, Soluong FROM SanPham WHERE MaSP=@MaSP", conn);
            cmd.Parameters.AddWithValue("MaSP", txtMaSP.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable d = new DataTable();
            d.Load(dr);
            txtTenSP.Text = d.Rows[0][0].ToString();
            txtDonGia.Text = d.Rows[0][1].ToString();
            sl = int.Parse(d.Rows[0][2].ToString());
            thanhtien();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }



   
}
