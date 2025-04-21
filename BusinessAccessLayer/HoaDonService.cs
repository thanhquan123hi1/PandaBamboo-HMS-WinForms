using System;
using System.Collections.Generic;
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
        //// Tìm kiếm hóa đơn theo từ khóa
        //public List<HoaDon> TimKiemHoaDon(string tuKhoa)
        //{
        //    var ketQua = _context.HoaDons
        //        .Join(_context.DatPhongs
        //            dp => dp.MaPH,
        //            (temp, dp) => new { HoaDon = temp, DatPhong = dp })
        //        .Where(x => x.HoaDon.KhachHang.TenKH.Contains(tuKhoa)
        //                 || x.HoaDon.KhachHang.SDT.Contains(tuKhoa)
        //                 || x.DatPhong.MaPH.ToString().Contains(tuKhoa)) // Nếu MaPH là số
        //        .Select(x => x.HoaDon)
        //        .ToList();
        //}
    }
}
