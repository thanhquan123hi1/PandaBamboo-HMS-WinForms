using BusinessAccessLayer;
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
    public partial class fCheckIn : Form
    {
        private int maPH;  // Biến lưu mã phòng
        public fCheckIn()
        {
            InitializeComponent();
        }
        public fCheckIn(int maPH)
        {
            InitializeComponent();
            this.maPH = maPH;
            lblMaPH.Text = "Phòng " + maPH; // Gán giá trị cho label
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            try
            {
                DatPhongService dbKhachDatPhong = new DatPhongService();
                string err = "";

                // Lấy dữ liệu từ các controls
                string tenKH = txtTen.Text.Trim();
                string cccd = txtCCCD.Text.Trim();
                string quocTich = txtQuocTich.Text.Trim();
                string sdt = txtSDT.Text.Trim();
                string hinhThucDP = cbxHinhThucDP.SelectedItem?.ToString();
                int maph = maPH;
                DateTime ngayNhanPH = dtpNgayCheckin.Value;
                DateTime ngayTraPH = dtpNgayCheckout.Value;

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(cccd) || string.IsNullOrEmpty(quocTich) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(hinhThucDP))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thực hiện thêm khách hàng và đặt phòng
                bool success = dbKhachDatPhong.InsertKhachHangDatPhong(tenKH, quocTich, cccd, sdt, maph, hinhThucDP, ngayNhanPH, ngayTraPH);

                if (success)
                {
                    MessageBox.Show("Đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lỗi đặt phòng: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
