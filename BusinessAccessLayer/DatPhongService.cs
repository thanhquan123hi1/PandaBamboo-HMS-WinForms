using DataAccessLayer.EntityClass;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class DatPhongService
    {
        private HotelContext _context = new HotelContext();
        // Thêm khách hàng đặt phòng
        public bool InsertKhachHangDatPhong(ref string err, string tenKH, string quocTich, string cccdVisa, string sdt,
                                            int maPH, string hinhThucDP, DateTime ngNhanPH, DateTime ngTraPH)
        {
            try
            {
                int maKH;
                // Tìm khách hàng theo CCCD hoặc SDT
                var khachHang = _context.KhachHangs
                    .FirstOrDefault(kh => kh.CCCD_VISA == cccdVisa || kh.SDT == sdt);

                // Nếu chưa tồn tại, thêm mới
                if (khachHang == null)
                {
                    khachHang = new KhachHang
                    {
                        TenKH = tenKH,
                        QuocTich = quocTich,
                        CCCD_VISA = cccdVisa,
                        SDT = sdt
                    };
                    _context.KhachHangs.Add(khachHang);
                    _context.SaveChanges(); // Để lấy MaKH sau khi insert
                }

                maKH = khachHang.MaKH;

                // Lấy giá phòng
                var phong = _context.Phongs.FirstOrDefault(p => p.MaPH == maPH);
                if (phong == null)
                {
                    err = "Không tìm thấy phòng.";
                    return false;
                }

                // Tính tổng tiền
                int soNgay = (ngTraPH - ngNhanPH).Days;
                if (soNgay <= 0)
                {
                    err = "Ngày nhận phòng không hợp lệ.";
                    return false;
                }

                decimal tongTien = soNgay * phong.GiaPH;

                // Thêm đặt phòng
                var datPhong = new DatPhong
                {
                    MaPH = maPH,
                    MaKH = maKH,
                    HinhThucDP = hinhThucDP,
                    NgNhanPH = ngNhanPH,
                    NgTraPH = ngTraPH
                };
                _context.DatPhongs.Add(datPhong);

                // Thêm hóa đơn
                var hoaDon = new HoaDon
                {
                    MaKH = maKH,
                    TenHD = "Hóa đơn đặt phòng",
                    TinhTrangTT = "Chua Thanh Toan",
                    HinhThucTT = "ChuaTT",
                    TongTien = tongTien,
                    NgayTT = null
                };
                _context.HoaDons.Add(hoaDon);

                DatPhongService dp = new DatPhongService();
                dp.DatPhong(maPH); // Cập nhật trạng thái phòng thành "Đã Đặt"
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        // Cập nhật trạng thái phòng khi có khách đặt phòng
        public void DatPhong(int datPhong)
        {
            var phong = _context.Phongs.FirstOrDefault(p => p.MaPH == datPhong);
            if (phong != null)
            {
                phong.TinhTrangPH = "Đã Đặt";
            }

            _context.SaveChanges();
        }

        // Cập nhật trạng thái phòng khi đã dọn dẹp xog
        public void PhongTrong(int maPhong)
        {
            var phong = _context.Phongs.FirstOrDefault(p => p.MaPH == maPhong);
            if (phong != null)
            {
                phong.TinhTrangPH = "Trống";
            }

            _context.SaveChanges();
        }

        // Cập nhật trạng thái phòng khi có khách đổi phòng 
        public void DoiPhong(int maPhongCu, int maPhongMoi)
        {
            var datPhong = _context.DatPhongs.FirstOrDefault(dp => dp.MaPH == maPhongCu);
            if (datPhong != null)
            {
                var phongCu = _context.Phongs.FirstOrDefault(p => p.MaPH == datPhong.MaPH);
                if (phongCu != null)
                    phongCu.TinhTrangPH = "Trống";

                var phongMoi = _context.Phongs.FirstOrDefault(p => p.MaPH == maPhongMoi);
                if (phongMoi != null)
                    phongMoi.TinhTrangPH = "Đã Đặt";

                datPhong.MaPH = maPhongMoi;

                _context.SaveChanges();
            }
        }
    }
}
