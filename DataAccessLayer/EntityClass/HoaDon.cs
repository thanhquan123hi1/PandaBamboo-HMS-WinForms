using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityClass
{
    public class HoaDon
    {
        [Key]
        public int MaHD { get; set; }

        [Required(ErrorMessage = "Mã khách hàng là bắt buộc")]
        public int MaKH { get; set; }

        [StringLength(100, ErrorMessage = "Tên hóa đơn không được quá 100 ký tự")]
        public string TenHD { get; set; }

        [Required(ErrorMessage = "Tổng tiền là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải >= 0")]
        public decimal TongTien { get; set; }

        [Required(ErrorMessage = "Tình trạng thanh toán là bắt buộc")]
        [RegularExpression("Chua Thanh Toan|Da Thanh Toan", ErrorMessage = "Tình trạng thanh toán không hợp lệ")]
        public string TinhTrangTT { get; set; }

        [Required(ErrorMessage = "Hình thức thanh toán là bắt buộc")]
        [RegularExpression("Tien Mat|Chuyen Khoan|The Tin Dung|ChuaTT", ErrorMessage = "Hình thức thanh toán không hợp lệ")]
        public string HinhThucTT { get; set; }

        public DateTime? NgayTT { get; set; }

        public virtual KhachHang KhachHang { get; set; }
        public virtual LapHoaDon LapHoaDons { get; set; }
    }
}
