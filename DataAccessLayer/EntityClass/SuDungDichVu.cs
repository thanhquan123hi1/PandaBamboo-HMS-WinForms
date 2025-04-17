using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityClass
{
    public class SuDungDichVu
    {
        [Required(ErrorMessage = "Mã khách hàng là bắt buộc")]
        public int MaKH { get; set; }

        [Required(ErrorMessage = "Mã dịch vụ là bắt buộc")]
        public int MaDV { get; set; }

        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Thời gian là bắt buộc")]
        public DateTime ThoiGian { get; set; }

        public virtual KhachHang KhachHang { get; set; }
        public virtual DichVu DichVu { get; set; }
    }
}
