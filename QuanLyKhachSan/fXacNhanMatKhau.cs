using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BusinessAccessLayer;

namespace QuanLyKhachSan
{
    public partial class fXacNhanMatKhau : Form
    {
        public string gmail { get; set; }
        public fXacNhanMatKhau()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            fXacNhanOTP fr1 = new fXacNhanOTP();
            fr1.Show();
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
             UserService user = new UserService();

            while (true) // Lặp vô hạn đến khi người dùng nhập đúng
            {
                // Kiểm tra nếu người dùng chưa nhập mật khẩu
                if (string.IsNullOrEmpty(tBMK1.Text) || string.IsNullOrEmpty(tBMK2.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Không tiếp tục nếu chưa nhập đủ
                }

                // Kiểm tra mật khẩu có trùng khớp không
                if (tBMK1.Text != tBMK2.Text)
                {
                    MessageBox.Show("Mật khẩu không khớp. Vui lòng nhập lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Xóa nội dung ô nhập mật khẩu để người dùng nhập lại
                    tBMK1.Clear();
                    tBMK2.Clear();
                    tBMK1.Focus(); // Di chuyển con trỏ vào ô nhập mật khẩu đầu tiên
                    return; // Kết thúc sự kiện, chờ người dùng nhập lại
                }
                else
                {
                    try
                    {
                        // Cập nhật mật khẩu nếu đúng
                        user.UpdataPassword(this.gmail, tBMK1.Text);

                        // Hiển thị thông báo thành công
                        MessageBox.Show("Cập nhật mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Đóng form hiện tại
                        fLogin fr1 = new fLogin();
                        fr1.Show();
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi cập nhật mật khẩu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break; // Thoát khỏi vòng lặp khi cập nhật thành công
                }
            }
        }
    }
}
