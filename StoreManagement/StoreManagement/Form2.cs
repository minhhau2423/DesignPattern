using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace StoreManagement
{
    public partial class Home : Form
    {
        string msg = "";
        public Home()
        {
            InitializeComponent();
        }
        public Home(UserControl screen)
        {
            InitializeComponent();
        }
      
        private void Home_Load(object sender, EventArgs e)
        {
           
            homeControl1.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginCommand User = new LoginSystem();
            User.Logout(); 
            this.Close();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            selectPanel.Top = btnTk.Top;
            statisticalControl1.BringToFront();
          


        }

        private void button4_Click(object sender, EventArgs e)
        {
            selectPanel.Top = btnKho.Top;
            warehouseControl1.BringToFront();
            
        }

        private void btnNv_Click_1(object sender, EventArgs e)
        {
            selectPanel.Top = btnNv.Top;
            staffControl2.BringToFront();
            staffControl2.show();
            
            //staffControl1.HienThiNV();

        }

        private void btnHome_Click_1(object sender, EventArgs e)
        {
            selectPanel.Top = btnHome.Top;
            homeControl1.BringToFront();
           
        }

        private void btnKh_Click(object sender, EventArgs e)
        {
            selectPanel.Top = btnKh.Top;
            customerControl1.BringToFront();
          
            
        }

        private void btnSp_Click(object sender, EventArgs e)
        {
            selectPanel.Top = btnSp.Top;
            productControl1.BringToFront();
           
        }

        private void btnHd_Click(object sender, EventArgs e)
        {
            selectPanel.Top = btnHd.Top;
            ordersControl2.BringToFront();
            ordersControl2.show();
          
            

            // ordersControl1.HienThiSP();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void staffControl1_Load(object sender, EventArgs e)
        {

        }

        private void btnGH_Click(object sender, EventArgs e)
        {

        }
       public void ReLoad()
        {
            this.Close();
            Home h = new Home();
            h.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ReLoad();
        }
    }
}
