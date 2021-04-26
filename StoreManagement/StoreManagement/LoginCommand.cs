using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StoreManagement
{
    class Account
    {
        public string UserName, Password;
        public Account(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }
        public string  getUserName()
        {
            return UserName;
        }
        public string  getPassword()
        {
            return Password;
        }
    }
    abstract class LoginCommand
    {
        public Boolean success = true;
        public abstract void Login();
        public abstract void Logout();

    }
    class LoginSystem : LoginCommand {
        public Account account;
        public LoginSystem(Account account) {
            this.account = account;
        }
        public LoginSystem()
        {

        }
        public override void Login()
        {
            DBfactory Sqlconn = SQLdatabase.getInstanceSQL();  //Gọi SQL từ Factory Pattern
            
            var conn = Sqlconn.CreateConnection();
            conn.Open();
            string sqlSelect = "SELECT count(*)  FROM Users WHERE NameLogin = @NameLogin and PassW=@PassW";
            var cmd  = (SqlCommand) Sqlconn.CreateCommand(sqlSelect, conn);
            cmd.Parameters.AddWithValue("NameLogin", account.getUserName());
            cmd.Parameters.AddWithValue("PassW", account.getPassword());
            var dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.success = true;
                Home f = new Home();
                f.Show();
            }
            else
            {
                this.success = false;
                MessageBox.Show("Erorr: Username or Password incorect!");
            }
           
        }
        public override void Logout()
        {
            Form1 f = new Form1();
            f.Show();
        }
    }

}
