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
    abstract class SaveBillsTemplate
    {
        public void Save()
        {
            Add_Khach_hang();
            Add_HoaDon();
            Add_ChiTietHoaDon();
            MessageBox.Show("Save sucessful.");

        } 
        public abstract void Add_Khach_hang();
        public abstract void Add_HoaDon();
        public abstract void Add_ChiTietHoaDon();

    }
    class Order : SaveBillsTemplate
    {
        DBfactory Sqlconn = SQLdatabase.getInstanceSQL();
        int sl;
        string[] str = new string[10];
        DataGridView dataGridView;
        public Order(string[] str, DataGridView data)
        {
            this.str = str;
            this.dataGridView = data;
        }
        public override void Add_Khach_hang()
        {
            String query = "INSERT INTO KhachHang VALUES(@MaKH, @TenKH, @SdtKH, @DiaChiKH)";
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            var command = (SqlCommand)Sqlconn.CreateCommand(query, conn);
            command.Parameters.AddWithValue("MaKH", str[0]);
            command.Parameters.AddWithValue("TenKH", str[1]);
            command.Parameters.AddWithValue("SdtKH", str[2]);
            command.Parameters.AddWithValue("DiaChiKH", str[3]);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public override void Add_HoaDon()
        {
            String query = "INSERT INTO HoaDon VALUES(@MaHD, @MaNV, @MaKH, @NgayLap, @TongTien)";
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            var command = (SqlCommand)Sqlconn.CreateCommand(query, conn);
            command.Parameters.AddWithValue("MaHD", str[4]);
            command.Parameters.AddWithValue("MaNV", str[5]);
            command.Parameters.AddWithValue("MaKH", str[0]);
            command.Parameters.AddWithValue("NgayLap", DateTime.Parse(str[6]));
            command.Parameters.AddWithValue("TongTien", str[7]);
            command.ExecuteNonQuery();
            conn.Close();

        }
        public override void Add_ChiTietHoaDon()
        {

            int count = dataGridView.Rows.Count;
            var conn = Sqlconn.CreateConnection();
            for (int i = 0; i < count; i++)
            {

                String queryn = "SELECT Count(*) FROM ChiTietHoaDon WHERE SanPham=@SanPham AND MaHD=@MaHD";

                var commandn = (SqlCommand)Sqlconn.CreateCommand(queryn, conn);
                conn.Open();
                commandn.Parameters.AddWithValue("MaHD", str[4]);
                commandn.Parameters.AddWithValue("SanPham", dataGridView[0, i].Value);
                SqlDataReader dtr = commandn.ExecuteReader();
                DataTable dtt = new DataTable();
                dtt.Load(dtr);
                if (dtt.Rows[0][0].ToString() != "0")
                {
                    String querym = "UPDATE ChiTietHoaDon SET SoLuong = Soluong + @SoLuong, ThanhTien = ThanhTien+@tt WHERE SanPham=@SanPham AND MaHD=@MaHD ";
                    SqlCommand commandm = (SqlCommand)Sqlconn.CreateCommand(querym, conn);
                    commandm.Parameters.AddWithValue("SoLuong", int.Parse(dataGridView[3, i].Value.ToString()));
                    commandm.Parameters.AddWithValue("SanPham", dataGridView[0, i].Value);
                    commandm.Parameters.AddWithValue("MaHD", str[4]);
                    commandm.Parameters.AddWithValue("tt", int.Parse(dataGridView[5, i].Value.ToString()));
                    commandm.ExecuteNonQuery();
                }
                else
                {
                    String query = "INSERT INTO ChiTietHoaDon VALUES(@MaHD, @SanPham, @SoLuong, @ThanhTien)";
                    SqlCommand command = (SqlCommand)Sqlconn.CreateCommand(query, conn);
                    command.Parameters.AddWithValue("MaHD", str[4]);
                    command.Parameters.AddWithValue("SanPham", dataGridView[0, i].Value);
                    command.Parameters.AddWithValue("SoLuong", dataGridView[3, i].Value);
                    command.Parameters.AddWithValue("ThanhTien", dataGridView[5, i].Value);
                    command.ExecuteNonQuery();
                }




                //

                String query1 = "UPDATE SanPham SET SoLuong = @SoLuong WHERE MaSP=@MaSP ";
                SqlCommand command1 = (SqlCommand)Sqlconn.CreateCommand(query1, conn);
                sl = (sl - int.Parse(dataGridView[3, i].Value.ToString()));
                command1.Parameters.AddWithValue("SoLuong", sl);
                command1.Parameters.AddWithValue("MaSP", dataGridView[0, i].Value);
                command1.ExecuteNonQuery();


                String query2 = "UPDATE Kho SET NgayXuatGanNhat = @Ngay, SoLuongXuatGanNhat=@sl WHERE MaSP=@MaSP ";
                SqlCommand command2 = (SqlCommand)Sqlconn.CreateCommand(query2, conn);
                command2.Parameters.AddWithValue("sl", dataGridView[3, i].Value);
                command2.Parameters.AddWithValue("ngay", DateTime.Parse(str[6]));
                command2.Parameters.AddWithValue("MaSP", dataGridView[0, i].Value);
                command2.ExecuteNonQuery();


            }
            conn.Close();
        }
    }
}
