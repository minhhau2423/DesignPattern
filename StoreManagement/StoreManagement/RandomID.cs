using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement
{
    class RandomID
    {
        DBfactory Sqlconn = SQLdatabase.getInstanceSQL();
        public string RandomChar(int numberRD)
        {
            string randomStr = "";
            try
            {
                string[] myIntArray = new string[numberRD];
                int x;
                Random autoRand = new Random();
                for (x = 0; x < numberRD; x++)
                {
                    myIntArray[x] = Convert.ToChar(Convert.ToInt32(autoRand.Next(65, 87))).ToString();
                    randomStr += (myIntArray[x].ToString());
                }
            }
            catch (Exception ex)
            {
                randomStr = "error";
            }
            return randomStr;
        }
        public string MaHD()
        {
            string MaHD;
            MaHD = "HD_" + RandomChar(5);
            string sqlSelect = "SELECT count(*) FROM HoaDon WHERE MaHD = @MaHD";
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            var cmd = (SqlCommand)Sqlconn.CreateCommand(sqlSelect, conn);
            cmd.Parameters.AddWithValue("MaHD", MaHD);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows[0][0].ToString() != "0")
            {
                MaHD = "HD_" + RandomChar(5);
            }
            return MaHD;
        }
        public string MaKH()
        {
            string MaKH;
            MaKH = "KH_" + RandomChar(5);
            string sqlSelect = "SELECT count(*) FROM KhachHang WHERE MaKH = @MaKH";
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            var cmd = (SqlCommand)Sqlconn.CreateCommand(sqlSelect, conn);
            cmd.Parameters.AddWithValue("MaKH", MaKH);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows[0][0].ToString() != "0")
            {
                MaKH = "KH_" + RandomChar(5);
            }
            return MaKH;
        }

        public string MaSP()
        {
            string MaSP;
            MaSP = "SP_" + RandomChar(5);
            string sqlSelect = "SELECT count(*) FROM SanPham WHERE MaSP = @MaSP";
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            var cmd = (SqlCommand)Sqlconn.CreateCommand(sqlSelect, conn);
            cmd.Parameters.AddWithValue("MaSP", MaSP);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows[0][0].ToString() != "0")
            {
                MaSP = "SP_" + RandomChar(5);
            }
            return MaSP;
        }

        public string MaNV()
        {
            string MaNV;
            MaNV = "NV_" + RandomChar(5);
            string sqlSelect = "SELECT count(*) FROM NhanVien WHERE MaNV = @MaNV";
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            var cmd = (SqlCommand)Sqlconn.CreateCommand(sqlSelect, conn);
            cmd.Parameters.AddWithValue("MaSP", MaNV);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows[0][0].ToString() != "0")
            {
                MaNV = "NV_" + RandomChar(5);
            }
            return MaNV;
        }
    }
}
