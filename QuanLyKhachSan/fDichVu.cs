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
using DataAccessLayer.EntityClass;

namespace QuanLyKhachSan
{
    public partial class fDichVu : Form
    {
        private readonly DichVuService _dichVu;

        public fDichVu()
        {
            _dichVu = new DichVuService();
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, ev) => dtpTimeDatDV.Value = DateTime.Now;
            timer.Start();
        }
        // Đặt dịch vụ
        private void btnSuDungDV_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ TextBox
            string TenKh = txtUserName.Text.Trim();
            string Sdt = txtSDT.Text.Trim();
            decimal TT;
            int MDV, SLDV;

            // Kiểm tra và chuyển đổi dữ liệu từ TextBox
            if (!int.TryParse(txtMDV.Text, out MDV) || !int.TryParse(txtSLDV.Text, out SLDV) || !decimal.TryParse(txtTongTien.Text, out TT))
            {
                MessageBox.Show("Mã dịch vụ và số lượng dịch vụ phải là số nguyên!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime dateTime = DateTime.Now;

            // Tạo đối tượng database
            KhachHangService dBKhachHang = new KhachHangService();
            int IdKH = dBKhachHang.KiemTraHTAndSDT(TenKh, Sdt);
            if (IdKH == -1) // Nếu không tìm thấy khách hàng
            {
                MessageBox.Show("Khách hàng không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Nếu khách hàng tồn tại, kiểm tra họ đã sử dụng dịch vụ chưa
            SuDungDVService dBSuDungDichVu = new SuDungDVService();
            HoaDonService dBHoaDon = new HoaDonService();
            TT *= Convert.ToInt32(txtSLDV.Text);
            string TenDV = txtTenDV.Text.Trim();

            if (dBSuDungDichVu.KiemTraSuDungDichVu(IdKH, MDV)) // Nếu đã sử dụng dịch vụ
            {
                string err = "";
                bool isUpdated = dBSuDungDichVu.UpdateSuDungDichVu(ref err, IdKH, MDV, SLDV, dateTime);
                int MaHD = dBHoaDon.KiemTraKhachDaSDDV(IdKH, TenDV);
                bool isUpdated1 = dBHoaDon.UpdateHoaDon1(MaHD, TT);

                if (isUpdated && isUpdated1)
                {
                    MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại! Lỗi: ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int MaHD = dBHoaDon.KiemTraKhachDaSDDV(IdKH, TenDV);
                bool isUpdated = dBSuDungDichVu.ThemSuDungDichVu(IdKH, MDV, SLDV, dateTime);
                bool isUpdated1 = dBHoaDon.InsertHoaDon1(IdKH, TenDV, TT, "Chua Thanh Toan", "ChuaTT", DateTime.Now);
                if (isUpdated && isUpdated1)
                {
                    MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại! Lỗi: ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Hàm chọn dịch vụ
        private void ChonDichVu(string keyword)
        {
            List<DichVu> dsDichVu = _dichVu.GetAllDichVu();
            DichVu dv = dsDichVu.FirstOrDefault(d => d.TenDV.Contains(keyword));

            if (dv != null)
            {
                txtMDV.Text = dv.MaDV.ToString();
                txtTenDV.Text = dv.TenDV;
                txtTongTien.Text = dv.GiaDV.ToString("N0");
                txtNgSDDV.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
            else
            {
                MessageBox.Show("Không tìm thấy dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút đặt nhà hàng
        private void btnNhaHang_Click(object sender, EventArgs e)
        {
            List<DichVu> dsDichVu = _dichVu.GetAllDichVu();
            int currentHour = DateTime.Now.Hour;

            if (currentHour >= 5 && currentHour < 9)
            {
                ChonDichVu("sáng");
            }
            else if (currentHour >= 9 && currentHour < 17)
            {
                ChonDichVu("trưa");
            }
            else
            {
                ChonDichVu("đêm");
            } 
        }
        // Nút đặt bar
        private void btnBar_Click(object sender, EventArgs e)
        {
            ChonDichVu("Bar");
        }
        // Nút đặt hồ bơi
        private void btnHoBoi_Click(object sender, EventArgs e)
        {
            ChonDichVu("Bể bơi");
        }
        // Nút đặt xông hơi
        private void btnXongHoi_Click(object sender, EventArgs e)
        {
            ChonDichVu("Xông hơi");
        }
        // Nút đặt giặc ủi
        private void btnGiac_Click(object sender, EventArgs e)
        {
            ChonDichVu("Giặt");
        }
    }
}
