using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class KhachDatPhongService
    {
        private readonly HotelContext _context;

        public KhachDatPhongService()
        {
            _context = new HotelContext();
        }

        public List<dynamic> dsKhachDatPhong()
        {
            var danhSach = _context.DatPhongs
                .Select(dp => new
                {
                    dp.KhachHang.MaKH,
                    dp.KhachHang.TenKH,
                    dp.KhachHang.QuocTich,
                    dp.KhachHang.CCCD_VISA,
                    dp.NgNhanPH,
                    dp.NgTraPH,
                    dp.KhachHang.SDT,
                    dp.MaPH
                })
                .ToList<dynamic>();

            return danhSach;
        }
    }
}
