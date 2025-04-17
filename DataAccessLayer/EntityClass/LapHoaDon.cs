using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityClass
{
    public class LapHoaDon
    {
        [Key]
        [Required(ErrorMessage = "Mã nhân viên là bắt buộc")]
        public int MaNV { get; set; }

        [Required(ErrorMessage = "Mã hóa đơn là bắt buộc")]
        public int MaHD { get; set; }

        [Required(ErrorMessage = "Ngày lập hóa đơn là bắt buộc")]
        public DateTime NgayLap { get; set; }

        public virtual NhanVien NhanVien { get; set; }
        public virtual HoaDon HoaDon { get; set; }
    }
}
