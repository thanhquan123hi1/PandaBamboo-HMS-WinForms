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
                            where hd.TinhTrangTT == "Chua Thanh Toan"
                                  && (hd.MaHD.ToString().Contains(tuKhoa)
                                      || hd.KhachHang.TenKH.Contains(tuKhoa)
                                      || hd.KhachHang.SDT.Contains(tuKhoa)
                                      || hd.KhachHang.DatPhongs.Any(dp => dp.MaPH.ToString().Contains(tuKhoa)))
                            select new
                            {
                                hd.MaHD,
                                hd.TenHD,
                                MaKH = hd.MaKH.ToString(),
                                TenKH = hd.KhachHang.TenKH,
                                SDT = hd.KhachHang.SDT,
                                hd.TongTien,
                                hd.TinhTrangTT,
                                MaPH = hd.KhachHang.DatPhongs
                                          .Where(dp => dp.NgTraPH >= DateTime.Now) // nếu muốn lọc đặt phòng còn hiệu lực
                                          .Select(dp => dp.MaPH.ToString())
                                          .FirstOrDefault() ?? ""
                            }).Distinct().ToList<dynamic>();

            return danhSach;
        }
        // Tính tổng tiền tất cả hóa đơn của 1 khách hàng
        public decimal tongTienHD(string tuKhoa)
        {
            var tongTien = (from hd in _context.HoaDons
                            where hd.TinhTrangTT == "Chua Thanh Toan"
                                  && (hd.MaHD.ToString().Contains(tuKhoa)
                                      || hd.KhachHang.TenKH.Contains(tuKhoa)
                                      || hd.KhachHang.SDT.Contains(tuKhoa)
                                      || hd.KhachHang.DatPhongs.Any(dp => dp.MaPH.ToString().Contains(tuKhoa)))
                            select hd.TongTien).Sum() ?? 0; // Nếu null thì trả về 0

            return tongTien;
        }
        // Thanh toán hóa đơn
        public bool ThanhToanHoaDon(int maKH, int maHD, string hinhThucTT, DateTime ngayTT, int maPH)
        {
            var hoaDon = _context.HoaDons.FirstOrDefault(h => h.MaHD == maHD && h.TinhTrangTT == "Chua Thanh Toan");
            if (hoaDon == null)
            {
                return false; // Không tìm thấy hóa đơn cần thanh toán
            }

            // Cập nhật thông tin hóa đơn
            hoaDon.NgayTT = ngayTT;
            hoaDon.HinhThucTT = hinhThucTT;
            hoaDon.TinhTrangTT = "Da Thanh Toan";
            Console.WriteLine("Xong 1");
            // Lấy tên hóa đơn để phân loại
            string tenHD = hoaDon.TenHD;

            if (tenHD != "Hóa đơn đặt phòng")
            {
                // Nếu là hóa đơn dịch vụ -> xóa dịch vụ đã sử dụng
                var dsSuDungDV = _context.SuDungDichVus.Where(dv => dv.MaKH == maKH).ToList();
                if (dsSuDungDV.Any())
                {
                    _context.SuDungDichVus.RemoveRange(dsSuDungDV);
                }
                Console.WriteLine("Xong 2");
            }
            else
            {
                // Nếu là hóa đơn đặt phòng -> xóa thông tin đặt phòng và cập nhật trạng thái phòng
                var dsDatPhong = _context.DatPhongs.Where(dp => dp.MaKH == maKH && dp.MaPH == maPH).ToList();
                foreach (var datPhong in dsDatPhong)
                {
                    var phong = _context.Phongs.FirstOrDefault(p => p.MaPH == datPhong.MaPH);
                    if (phong != null)
                    {
                        phong.TinhTrangPH = "Dọn Dẹp";
                    }

                    _context.DatPhongs.Remove(datPhong);
                }
                Console.WriteLine("Xong 3");
            }

            _context.SaveChanges();
            return true;
        }

    }
}
