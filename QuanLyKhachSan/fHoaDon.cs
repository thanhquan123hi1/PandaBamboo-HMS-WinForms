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
    public partial class fHoaDon : Form
    {
        private readonly HoaDonService dbHoaDon;
        public fHoaDon()
        {
            dbHoaDon = new HoaDonService();
            InitializeComponent();
            LoadHoaDon();
        }
        private void LoadHoaDon()
        {
            string tuKhoa = txtSearchHD.Text.Trim();
            Decimal tongTien = dbHoaDon.tongTienHD(tuKhoa);
            dgvHoaDon.DataSource = dbHoaDon.dsHoaDon(tuKhoa);
            if (!string.IsNullOrEmpty(txtSearchHD.Text))
            {
                txtTongTien.Text = "Tổng tiền: " + tongTien.ToString("N0") + " VND";
                txtThue.Text = "Thuế VAT: " + (tongTien * 0.1m).ToString("N0") + " VND";
                txtThanhTien.Text = "Thành tiền: " + (tongTien + (tongTien * 0.1m)).ToString("N0") + " VND";
            }
        }
        private void txtSearchHD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadHoaDon();
                e.SuppressKeyPress = true;
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearchHD.Text)) // Kiểm tra có hóa đơn nào trong danh sách không
            {
                string error = "";
                int soHoaDonThanhToan = 0;

                foreach (DataGridViewRow row in dgvHoaDon.Rows)
                {
                    if (row.Cells["TinhTrangTT"].Value.ToString() == "Chua Thanh Toan") // Chỉ thanh toán hóa đơn chưa thanh toán
                    {
                        int maHD = Convert.ToInt32(row.Cells["MaHD"].Value);
                        string hinhThucTT = cbxPTThanhToan.Text.ToString();
                        DateTime ngayTT = DateTime.Now;
                        int maKH = Convert.ToInt32(row.Cells["MaKH"].Value);
                        int maPH = Convert.ToInt32(row.Cells["MaPH"].Value);




                        //// Gọi hàm thanh toán hóa đơn
                        //bool result = dbHoaDon.ThanhToanHoaDon(maKH, maHD, hinhThucTT, ngayTT, maPH);
                        //if (!result)
                        //{
                        //    MessageBox.Show($"Lỗi khi thanh toán hóa đơn: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                        //if (result)
                        //{
                        //    soHoaDonThanhToan++;
                        //}
                    }
                }

                //if (soHoaDonThanhToan > 0)
                //{
                //    MessageBox.Show($"Đã thanh toán hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    LoadHoaDon();
                //}
                //else
                //{
                //    MessageBox.Show($"Không có hóa đơn nào cần thanh toán hoặc bạn nhập thiếu mã nhân viên ! {dgvHoaDon.Rows.Count}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            else
            {
                MessageBox.Show("Danh sách hóa đơn trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
