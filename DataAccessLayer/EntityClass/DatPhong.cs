using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityClass
{
    public class DatPhong
    {
        [Key, Column(Order = 0)]
        public int MaPH { get; set; }
        [Key, Column(Order = 1)]
        public int MaKH { get; set; }

        [Required(ErrorMessage = "Hình thức đặt phòng là bắt buộc")]
        [MaxLength(50, ErrorMessage = "Hình thức đặt phòng không được vượt quá 50 ký tự")]
        [RegularExpression("Truc Tiep|Online", ErrorMessage = "Hình thức đặt phòng không hợp lệ")]
        public string HinhThucDP { get; set; }

        [Required(ErrorMessage = "Ngày nhận phòng là bắt buộc")]
        public DateTime NgNhanPH { get; set; }

        [Required(ErrorMessage = "Ngày trả phòng là bắt buộc")]
        public DateTime NgTraPH { get; set; }

        // Navigation properties
        [ForeignKey("MaPH")]
        public virtual Phong Phong { get; set; }
        [ForeignKey("MaKH")]
        public virtual KhachHang KhachHang { get; set; }
    }
}
