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
using DataAccessLayer;
using Guna.UI2.WinForms;
namespace QuanLyKhachSan
{
    public partial class fPhong : Form
    {
        public fPhong()
        {
            InitializeComponent();
            loadListPhong();
        }
        void loadListPhong()
        {
            flpPhong.Controls.Clear();
            PhongService phongService = new PhongService();
            List<Phong> listPhong = phongService.GetAllPhongs();
            foreach (Phong item in listPhong)
            {
                string status = "";
                if (item.TinhTrangPH == "Trống") status = "Trống";
                else if (item.TinhTrangPH == "Đã Đặt") status = "Đã đặt";
                else if (item.TinhTrangPH == "Đã Ở") status = "Đã ở";
                else status = "Dọn dẹp";

                string loai = "";
                if (item.LoaiPH == "PhongDon") loai = "Đơn";
                else if (item.LoaiPH == "PhongDoi") loai = "Đôi";
                else if (item.LoaiPH == "CC1") loai = "VIP 1";
                else loai = "VIP 2";

                Guna2Button btn = new Guna.UI2.WinForms.Guna2Button() { Width = 140, Height = 160 };
                btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                btn.BorderRadius = 25;
                btn.Text = "Phòng: " + item.MaPH + Environment.NewLine + "LoạiP: " + loai + Environment.NewLine + "Status: " + status;
                btn.BorderThickness = 2;  // Độ dày viền (px)
                btn.BorderColor = Color.FromArgb(198, 142, 253);  // Màu viền

                if (item.TinhTrangPH == "Trống")
                {
                    btn.ForeColor = Color.FromArgb(143, 135, 241);
                    btn.FillColor = Color.FromArgb(255, 255, 255);
                }
                else
                {
                    btn.ForeColor = Color.FromArgb(255, 255, 255);
                    btn.FillColor = Color.FromArgb(143, 135, 241);
                }
                btn.Tag = item.MaPH;
                btn.Click += new EventHandler(BtnPhong_Click);
                flpPhong.Controls.Add(btn);
            }
        }
        private void BtnPhong_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            int maPH = Convert.ToInt32(btn.Tag);

            if (btn.FillColor == Color.FromArgb(255, 255, 255))  // Nếu phòng trống
            {
                fCheckIn fCheckIn = new fCheckIn(maPH);
                if (fCheckIn.ShowDialog() == DialogResult.OK)
                {
                    loadListPhong();
                }
            }
        }
    }
}
