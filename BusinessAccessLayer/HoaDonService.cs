using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.EntityClass;
namespace BusinessAccessLayer
{
    public class HoaDonService
    {
        private readonly HotelContext _context;
        public HoaDonService()
        {
            _context = new HotelContext();
        }
        // Kiểm tra khách hàng đã sử dụng dịch vụ chưa
        public int KiemTraKhachDaSDDV(int maKH, string tenDV)
        {
            var hoaDon = _context.HoaDons.FirstOrDefault(hd => hd.MaKH == maKH && hd.TenHD == tenDV);
            if (hoaDon != null)
                return hoaDon.MaHD;

            return -1;
        }
        public bool UpdateHoaDon1(int maHD, decimal tongTienMoi)
        {
            var hoaDon = _context.HoaDons.FirstOrDefault(h => h.MaHD == maHD);

            if (hoaDon == null)
                return false; // Không tìm thấy hóa đơn

            hoaDon.TongTien += tongTienMoi;

            _context.SaveChanges(); // Lưu thay đổi vào CSDL
            return true;
        }

        // Thêm hóa đơn
        public bool InsertHoaDon1(int maKH, string tenHD, decimal tongTien, string tinhTrangTT, string hinhThucTT, DateTime ngayTT)
        {
            var hoaDon = new HoaDon
            {
                MaKH = maKH,
                TenHD = tenHD,
                TongTien = tongTien,
                TinhTrangTT = tinhTrangTT,
                HinhThucTT = hinhThucTT,
                NgayTT = ngayTT
            };
            _context.HoaDons.Add(hoaDon);
            _context.SaveChanges();
            return true;
        }
        // Tìm kiếm hóa đơn theo từ khóa
        public List<dynamic> dsHoaDon(string tuKhoa)
        {
            var danhSach = (from hd in _context.HoaDons
                            join dp in _context.DatPhongs on hd.MaKH equals dp.MaKH
                            where hd.MaHD.ToString().Contains(tuKhoa)
                                  || hd.TenHD.Contains(tuKhoa)
                                  || hd.KhachHang.TenKH.Contains(tuKhoa)
                                  || hd.KhachHang.SDT.Contains(tuKhoa)
                                  || hd.TongTien.ToString().Contains(tuKhoa)
                                  || hd.TinhTrangTT.Contains(tuKhoa)
                                  || (dp.MaPH.ToString().Contains(tuKhoa))
                            select new
                            {
                                hd.MaHD,
                                hd.TenHD,
                                MaKH = hd.MaKH.ToString(),
                                TenKH = hd.KhachHang.TenKH,
                                SDT = hd.KhachHang.SDT,
                                hd.TongTien,
                                hd.TinhTrangTT,
                                MaPH = dp.MaPH.ToString()
                            }).ToList<dynamic>();

            return danhSach;
        }
        // Tính tổng tiền tất cả hóa đơn của 1 khách hàng
        public decimal tongTienHD(string tuKhoa)
        {
            var tongTien = (from hd in _context.HoaDons
                            join dp in _context.DatPhongs on hd.MaKH equals dp.MaKH
                            where hd.MaHD.ToString().Contains(tuKhoa)
                                  || hd.TenHD.Contains(tuKhoa)
                                  || hd.KhachHang.TenKH.Contains(tuKhoa)
                                  || hd.KhachHang.SDT.Contains(tuKhoa)
                                  || hd.TongTien.ToString().Contains(tuKhoa)
                                  || hd.TinhTrangTT.Contains(tuKhoa)
                                  || (dp.MaPH.ToString().Contains(tuKhoa))
                            select hd.TongTien).Sum() ?? 0; // Nếu null thì trả về 0

            return tongTien;
        }
        // Thanh toán hóa đơn
        public bool ThanhToanHoaDon(int maKH, int maHD, string hinhThucTT, DateTime ngayTT, int maPH)
        {
            var hoaDon = _context.HoaDons.FirstOrDefault(h => h.MaHD == maHD && h.TinhTrangTT == "Chua Thanh Toan");
            if (hoaDon == null)
            {
                return false; // Không tìm thấy hóa đơn hoặc đã thanh toán
            }

            // Cập nhật thông tin thanh toán
            hoaDon.NgayTT = ngayTT;
            hoaDon.HinhThucTT = hinhThucTT;
            hoaDon.TinhTrangTT = "Da Thanh Toan";

            // Nếu là hóa đơn dịch vụ
            if (hoaDon.TenHD != "Hóa đơn đặt phòng")
            {
                var dichVus = _context.SuDungDichVus.Where(dv => dv.MaKH == maKH);
                _context.SuDungDichVus.RemoveRange(dichVus);
            }

            // Nếu là hóa đơn đặt phòng
            else
            {
                var datPhong = _context.DatPhongs.FirstOrDefault(dp => dp.MaPH == maPH);
                if (datPhong != null)
                {
                    DatPhongService traPhong = new DatPhongService();
                    traPhong.TraPhong(maPH);
                    _context.DatPhongs.Remove(datPhong);
                }
            }

            // Xóa khách hàng đã thanh toán
            var khachHang = _context.KhachHangs.FirstOrDefault(kh => kh.MaKH == maKH);
                if (khachHang != null)
                {
                    _context.KhachHangs.Remove(khachHang);
                }
             
            _context.SaveChanges();
            return true; // Thanh toán thành công
        }
    }
}
