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
                // Kiểm tra khách hàng đã tồn tại dựa vào CCCD_VISA và SDT
                var khachHang = _context.KhachHangs
                    .FirstOrDefault(kh => kh.CCCD_VISA == cccdVisa || kh.SDT == sdt);
                // Nếu khách hàng đã tồn tại thì không cần thêm mới
                if (khachHang != null)
                {
                    // Khách hàng đã tồn tại, không cho đặt phòng
                    return false;
                }
                // Nếu chưa tồn tại thì thêm mới
                khachHang = new KhachHang
                {
                    TenKH = tenKH,
                    QuocTich = quocTich,
                    CCCD_VISA = cccdVisa,
                    SDT = sdt
                };
                _context.KhachHangs.Add(khachHang);
                DatPhong(maPH);
                _context.SaveChanges(); // Để lấy MaKH sau khi thêm

                // Lấy thông tin phòng
                var phong = _context.Phongs.FirstOrDefault(p => p.MaPH == maPH);
                if (phong == null)
                    return false;

                // Tính số ngày và tổng tiền
                int soNgay = (ngTraPH - ngNhanPH).Days;
                if (soNgay <= 0)
                    return false;

                decimal tongTien = soNgay * phong.GiaPH;

                // Tạo đặt phòng
                var datPhong = new DatPhong
                {
                    MaPH = maPH,
                    MaKH = khachHang.MaKH,
                    HinhThucDP = hinhThucDP,
                    NgNhanPH = ngNhanPH,
                    NgTraPH = ngTraPH
                };
                _context.DatPhongs.Add(datPhong);

                // Tạo hóa đơn
                var hoaDon = new HoaDon
                {
                    MaKH = khachHang.MaKH,
                    TenHD = "Hóa đơn đặt phòng",
                    TinhTrangTT = "Chua Thanh Toan",
                    HinhThucTT = "ChuaTT",
                    TongTien = tongTien,
                    NgayTT = null
                };
                _context.HoaDons.Add(hoaDon);

                // Lưu tất cả thay đổi vào DB
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
        // Cập nhật trạng thái phòng khi có khách trả phòng
        public void TraPhong(int traPhong)
        {
            var phong = _context.Phongs.FirstOrDefault(p => p.MaPH == traPhong);
            if (phong != null)
            {
                phong.TinhTrangPH = "Dọn Dẹp";
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
