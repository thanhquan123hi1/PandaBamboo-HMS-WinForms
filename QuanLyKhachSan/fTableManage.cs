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
    public partial class fTableManage : Form
    {
        public fTableManage()
        {
            InitializeComponent();
        }
        private void container(object _form)
        {
            // Nếu panel đã có form nào thì xoá đi trước
            if (pnlContainer.Controls.Count > 0)
                pnlContainer.Controls.Clear();

            // Ép kiểu object thành Form
            Form fm = _form as Form;

            // Cấu hình form con để hiển thị trong panel
            fm.TopLevel = false; // Phải để false để thêm vào panel
            fm.FormBorderStyle = FormBorderStyle.None; // Bỏ viền
            fm.Dock = DockStyle.Fill; // Phóng to vừa panel

            // Thêm form vào panel và hiển thị
            pnlContainer.Controls.Add(fm);
            pnlContainer.Tag = fm;
            fm.Show();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            container(new fKhachHang());
        }

        private void btnQuanLyPhong_Click(object sender, EventArgs e)
        {
            container(new fPhong());
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            container(new fDichVu());
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            container(new fHoaDon());
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
