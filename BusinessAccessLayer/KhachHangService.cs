using DataAccessLayer.EntityClass;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessAccessLayer
{
    public class KhachHangService
    {
        private readonly HotelContext _context;

        public KhachHangService()
        {
            _context = new HotelContext();
        }

        // Lấy danh sách khách hàng
        public List<KhachHang> GetAllKhachHang()
        {
            return _context.KhachHangs.ToList();
        }

        // Kiểm tra khách hàng dựa vào Họ tên và SĐT
        public int KiemTraHTAndSDT(string tenKH, string sdt)
        {
            var kh = _context.KhachHangs
                .FirstOrDefault(k => k.TenKH == tenKH && k.SDT == sdt);
            return kh != null ? kh.MaKH : -1;
        }
    }
}
