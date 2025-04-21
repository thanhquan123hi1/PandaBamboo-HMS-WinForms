using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.EntityClass;
namespace BusinessAccessLayer
{
    public class SuDungDVService
    {
        private readonly HotelContext _context;
        public SuDungDVService()
        {
            _context = new HotelContext();
        }
        // Kiểm tra khách hàng đã sử dụng dịch vụ chưa
        public bool KiemTraSuDungDichVu(int id, int maDV)
        {
            var kh = _context.SuDungDichVus.FirstOrDefault(d => d.MaKH == id && d.MaDV == maDV);
            if(kh != null) return true;
            return false;
        }
        // Thêm mới sử dụng dịch vụ
        public bool ThemSuDungDichVu(int maKH, int maDV, int soLuong, DateTime thoiGian)
        {
            if (soLuong <= 0)
                return false;
            var suDungDV = new SuDungDichVu
            {
                MaKH = maKH,
                MaDV = maDV,
                SoLuong = soLuong,
                ThoiGian = thoiGian
            };
            _context.SuDungDichVus.Add(suDungDV);
            _context.SaveChanges();
            return true;
         }

        // Cập nhật sử dụng dịch vụ
        public bool UpdateSuDungDichVu(ref string err, int maKH, int maDV, int soLuong, DateTime thoiGian)
        {
            err = "";

            if (soLuong <= 0)
            {
                err = "Số lượng dịch vụ phải lớn hơn 0!";
                return false;
            }

            try
            {
                var suDungDV = _context.SuDungDichVus.FirstOrDefault(s => s.MaKH == maKH && s.MaDV == maDV);

                if (suDungDV != null)
                {
                    // Nếu đã tồn tại thì cập nhật số lượng
                    suDungDV.SoLuong += soLuong;
                    suDungDV.ThoiGian = thoiGian;
                }
                else
                {
                    // Nếu chưa có thì thêm mới
                    suDungDV = new SuDungDichVu
                    {
                        MaKH = maKH,
                        MaDV = maDV,
                        SoLuong = soLuong,
                        ThoiGian = thoiGian
                    };
                    _context.SuDungDichVus.Add(suDungDV);
                }

                _context.SaveChanges();
                err = "Cập nhật sử dụng dịch vụ thành công.";
                return true;
            }
            catch (Exception ex)
            {
                err = "Lỗi khi cập nhật dịch vụ: " + ex.Message;
                return false;
            }
        }
    }
}
