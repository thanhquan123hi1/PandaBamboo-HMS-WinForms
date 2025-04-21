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
            string tuKhoa = txtSearchHD.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox
            dgvHoaDon.DataSource = dbHoaDon.TimKiemHoaDon(tuKhoa);
        }
    }
}
