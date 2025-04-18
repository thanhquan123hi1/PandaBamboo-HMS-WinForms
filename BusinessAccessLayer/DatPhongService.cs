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

        public bool InsertKhachHangDatPhong(string tenKH, string quocTich, string cccdVisa, string sdt,
                                 int maPH, string hinhThucDP, DateTime ngNhanPH, DateTime ngTraPH)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Kiểm tra khách hàng đã tồn tại hay chưa
                    var khachHang = _context.KhachHangs.FirstOrDefault(kh => kh.CCCD_VISA == cccdVisa || kh.SDT == sdt);

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
                        _context.SaveChanges(); // Để lấy MaKH sau khi thêm
                    }

                    // Lấy giá phòng
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

                    _context.SaveChanges();
                    transaction.Commit();

                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

    }
}
