using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagement
{
    public partial class StaffControl : UserControl
    {
        public StaffControl()
        {
            InitializeComponent();
           
        }

        private void StaffControl_Load(object sender, EventArgs e)
        {
        }
        public void show()
        {
            ShowControls s = new ShowControls(dataQLNV);
            s.Show("NV");
        }

       /* public void HienThiNV()
        {
            DBfactory Sqlconn = SQLdatabase.getInstanceSQL();  //Gọi SQL từ Factory Pattern
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            string sqlSelect = "SELECT MaNV as 'ID', TenNV as 'Name', GioiTinh as 'Gender',NgaySinh as 'Birthday' , Email as 'Mail' , SDT as 'Phone', DiaChi as 'Address', ViTri as 'Position' FROM NhanVien";
            var cmd = (SqlCommand)Sqlconn.CreateCommand(sqlSelect, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataQLNV.DataSource = dt;
        }*/
    }
}
