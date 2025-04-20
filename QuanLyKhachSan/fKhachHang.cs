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
    public partial class fKhachHang : Form
    {
        private readonly KhachDatPhongService _khachDP;
        public fKhachHang()
        {
            _khachDP = new KhachDatPhongService();
            InitializeComponent();
        }
        private void fKhachHang_Load(object sender, EventArgs e)
        {
            loadKhachHang();
        }
        private void loadKhachHang()
        {
            string search = txtSearchKH.Text.Trim();
            dgvKhachHang.DataSource = _khachDP.dsKhachDatPhong(search);
            lblSoKH.Text = dgvKhachHang.Rows.Count.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string err = "";
                foreach (DataGridViewRow row in dgvKhachHang.Rows)
                {
                    if (row.Cells["TenKH"].Value != null)
                    {
                        int maKH = Convert.ToInt32(row.Cells["MaKH"].Value);
                        string tenKH = row.Cells["TenKH"].Value.ToString();
                        string quocTich = row.Cells["QuocTich"].Value.ToString();
                        string cccdVisa = row.Cells["CCCD_VISA"].Value.ToString();
                        string sdt = row.Cells["SDT"].Value.ToString();
                        int maPH = Convert.ToInt32(row.Cells["MaPH"].Value);
                        DateTime ngNhanPH = Convert.ToDateTime(row.Cells["NgNhanPH"].Value);
                        DateTime ngTraPH = Convert.ToDateTime(row.Cells["NgTraPH"].Value);

                        bool result = _khachDP.CapNhatThongTinDatPhong(ref err, maKH, tenKH, quocTich, cccdVisa, sdt, maPH, ngNhanPH, ngTraPH);

                        if (!result)
                        {
                            MessageBox.Show($"Cập nhật thất bại cho khách hàng {tenKH} !\n" +
                                $"{err}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                } 

                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Load lại danh sách khách hàng
            loadKhachHang();
        }

        private void txtSearchKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadKhachHang();
                e.SuppressKeyPress = true;
            }
        }
    }
}
