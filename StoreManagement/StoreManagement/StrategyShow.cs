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
using System.Windows;

namespace StoreManagement
{
    abstract class StrategyShow
    {
        public DBfactory Sqlconn = SQLdatabase.getInstanceSQL();  //Gọi SQL từ Factory Pattern
        public abstract void Show(Object x);
    }
    class ShowControls
    {
        Object x;
        public ShowControls(Object x){
            this.x = x;
        }
        public void  Show(string str)
        {
            if (str.Equals("NV"))
            {
                new ShowStaff().Show(x);
                
            }else if (str.Equals("SPinOrders"))
            {
                new ShowProductsInOrders().Show(x);

            }
        }
    }
    class ShowStaff : StrategyShow
    {
        public override void Show(Object x)
        {
            var data = new DataGridView();
            data = (DataGridView)x;
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            string sqlSelect = "SELECT MaNV as 'ID', TenNV as 'Name', GioiTinh as 'Gender',NgaySinh as 'Birthday' , Email as 'Mail' , SDT as 'Phone', DiaChi as 'Address', ViTri as 'Position' FROM NhanVien";
            var cmd = (SqlCommand)Sqlconn.CreateCommand(sqlSelect, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            data.DataSource = dt; 

        }
    }
    class ShowProductsInOrders : StrategyShow
    {
        public override void Show(Object x)
        {
            var data = new FlowLayoutPanel();
            data = (FlowLayoutPanel)x;
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            //Lấy số lượng Sản phẩm để hiển thị
            string sqlSelect = "SELECT MaSP FROM SanPham";
            var tmp = (SqlCommand)Sqlconn.CreateCommand(sqlSelect, conn);
            SqlDataReader dr = tmp.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            string[] MaSP = new string[(int)dt.Rows.Count];
            for (int i = 0; i < (int)dt.Rows.Count; i++)
            {
                MaSP[i] = dt.Rows[i][0].ToString();

            }
            for (int i = 0; i < MaSP.Count(); i++)
            {

                string StrSelect = "SELECT ImageSP, DonGia FROM SanPham WHERE MaSP=@MaSP";
                var cmd = (SqlCommand)Sqlconn.CreateCommand(StrSelect, conn);
                cmd.Parameters.AddWithValue("@MaSP", MaSP[i]);
                SqlDataReader drr = cmd.ExecuteReader();
                DataTable t = new DataTable();
                t.Load(drr);
                var price = new Label();
                price.Text = t.Rows[0][1].ToString() + " VNĐ";
                price.Height = 10;
                price.ForeColor = Color.Blue;
                //
                var ID = new Label();
                ID.Text = MaSP[i];
                ID.Dock = DockStyle.Bottom;
                ID.BackColor = Color.AliceBlue;
                ID.ForeColor = Color.Red;
                ID.TextAlign = ContentAlignment.MiddleCenter;

                //
                byte[] b = (byte[])cmd.ExecuteScalar();
                var pic = new PictureBox();
                pic.Width = 100;
                pic.Height = 130;
                pic.BackgroundImageLayout = ImageLayout.Stretch;
                pic.BorderStyle = BorderStyle.FixedSingle;
                pic.Controls.Add(price);
                pic.Controls.Add(ID);
                MemoryStream ms = new MemoryStream(b);
                pic.BackgroundImage = Image.FromStream(ms);
                data.Controls.Add(pic);
                ID.Click += new System.EventHandler(this.picClick);
            }
            dr.Close();
            conn.Close();
        }
        
        

        void picClick(object sender, EventArgs e)
        {
            var pic = (Label)sender;
        }

       
    }
}
