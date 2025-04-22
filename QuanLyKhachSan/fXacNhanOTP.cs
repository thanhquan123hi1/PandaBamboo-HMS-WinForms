using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class fXacNhanOTP : Form
    {
        public string gmail { get; set; }
        public string otpCode { get; set; }
        public fXacNhanOTP()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string s = tBOTP.Text.Trim();

            if (this.otpCode == s)
            {
                fXacNhanMatKhau form4 = new fXacNhanMatKhau();
                form4.gmail = gmail;
                form4.Show();  // Mở Form2
                this.Close();   // Ẩn Form1
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            fXacNhanGmail xacNhanGmail = new fXacNhanGmail();
            xacNhanGmail.Show();
            this.Close();
        }
    }
}
