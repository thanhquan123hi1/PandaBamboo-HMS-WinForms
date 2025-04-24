using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessAccessLayer;

namespace QuanLyKhachSan
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }
        fTableManage tableManage = new fTableManage();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tBTenDN.Text;
            string password = tBMKDN.Text; // Bạn nên mã hóa password trước khi so sánh
            UserService user = new UserService(); // Tạo đối tượng
            if (user.KiemTraDangNhap(username, password))
            {
                this.Hide();
                tableManage.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");

            }
        }

        private void tBForget_Click(object sender, EventArgs e)
        {
            fXacNhanGmail form2 = new fXacNhanGmail();

            form2.Show();  // Mở Form2
            this.Hide();  // Ẩn Form1
        }

        private void tBMKDN_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
                e.SuppressKeyPress = true; // Ngăn chặn âm thanh "ding" khi nhấn Enter
            }
        }
    }
}
