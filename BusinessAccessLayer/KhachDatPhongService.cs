using DataAccessLayer;
using DataAccessLayer.EntityClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
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
        // Lấy danh sách khách đặt phòng theo từ khóa
        public DataTable dsKhachDatPhong(string tuKhoa)
        {
            var danhSach = _context.DatPhongs
                .Where(dp => dp.KhachHang.MaKH.ToString().Contains(tuKhoa)
                          || dp.KhachHang.TenKH.Contains(tuKhoa)
                          || dp.KhachHang.SDT.Contains(tuKhoa)
                          || dp.KhachHang.CCCD_VISA.Contains(tuKhoa)
                          || dp.MaPH.ToString().Contains(tuKhoa)
                          || dp.NgNhanPH.ToString().Contains(tuKhoa))
                .Select(dp => new
                {
                    dp.KhachHang.MaKH,
                    dp.KhachHang.TenKH,
                    dp.KhachHang.QuocTich,
                    dp.KhachHang.CCCD_VISA,
                    dp.KhachHang.SDT,
                    dp.NgNhanPH,
                    dp.NgTraPH,
                    dp.MaPH
                })
                .ToList();

            // Tạo DataTable và thêm các cột
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("MaKH", typeof(int));
            dataTable.Columns.Add("TenKH", typeof(string));
            dataTable.Columns.Add("QuocTich", typeof(string));
            dataTable.Columns.Add("CCCD_VISA", typeof(string));
            dataTable.Columns.Add("SDT", typeof(string));
            dataTable.Columns.Add("NgNhanPH", typeof(DateTime));
            dataTable.Columns.Add("NgTraPH", typeof(DateTime));
            dataTable.Columns.Add("MaPH", typeof(int));

            // Đổ dữ liệu vào DataTable
            foreach (var item in danhSach)
            {
                dataTable.Rows.Add(item.MaKH, item.TenKH, item.QuocTich, item.CCCD_VISA, item.SDT, item.NgNhanPH, item.NgTraPH, item.MaPH);
            }

            return dataTable;
        }


        // Cập nhật thông tin khách đặt phòng
        public bool CapNhatThongTinDatPhong(ref string err, int maKH, string tenKH, string quocTich, string cccdVisa, string sdt, int maPH, DateTime ngNhanPH, DateTime ngTraPH)
        {
            try
            {
                // Tìm khách hàng
                var khachHang = _context.KhachHangs.FirstOrDefault(kh => kh.MaKH == maKH);
                if (khachHang == null || khachHang.MaKH != maKH) 
                {
                    err = "Không được thay đổi MaKH please!!!";
                    return false;
                }
                    

                // Cập nhật thông tin khách hàng
                khachHang.TenKH = tenKH;
                khachHang.QuocTich = quocTich;
                khachHang.CCCD_VISA = cccdVisa;
                khachHang.SDT = sdt;

                // Tìm thông tin đặt phòng
                var datPhong = _context.DatPhongs.FirstOrDefault(dp => dp.MaKH == maKH);
                if (datPhong == null)
                    return false;

                int maPhongCu = datPhong.MaPH; // Lưu phòng cũ

                Console.WriteLine($"{datPhong.MaPH}, {maPH}");
                // Nếu khách đổi phòng
                if (maPhongCu != maPH)
                {
                    var phongMoi = _context.Phongs.FirstOrDefault(p => p.MaPH == maPH);
                    if (phongMoi == null)
                        return false;

                    int soNgay = (ngTraPH - ngNhanPH).Days;
                    if (soNgay <= 0)
                        return false;

                    decimal tongTien = soNgay * phongMoi.GiaPH;

                    var hoaDon = _context.HoaDons.FirstOrDefault(hd => hd.MaKH == maKH);
                    hoaDon.TongTien = tongTien;

                    // Cập nhật trạng thái phòng
                    DatPhongService doiPhong = new DatPhongService();
                    doiPhong.DoiPhong(maPhongCu, maPH);
                }

                // Cập nhật thông tin đặt phòng
                datPhong.MaPH = maPH;
                datPhong.NgNhanPH = ngNhanPH;
                datPhong.NgTraPH = ngTraPH;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }
    }
}
