using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.EntityClass;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer
{
    public class Phong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPH { get; set; }

        [Required(ErrorMessage = "Loại phòng là bắt buộc")]
        [StringLength(50, ErrorMessage = "Loại phòng không được quá 50 ký tự")]
        [RegularExpression("PhongDon|PhongDoi|CC1|CC2", ErrorMessage = "Loại phòng không hợp lệ")]
        public string LoaiPH { get; set; }

        [Required(ErrorMessage = "Tình trạng phòng là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tình trạng phòng không được quá 50 ký tự")]
        [RegularExpression("Trống|Đã Đặt|Đã Ở|Dọn Dẹp", ErrorMessage = "Tình trạng phòng không hợp lệ")]
        public string TinhTrangPH { get; set; }

        [Required(ErrorMessage = "Giá phòng là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phòng phải >= 0")]
        public decimal GiaPH { get; set; }
        public virtual ICollection<DatPhong> DatPhongs { get; set; }
        public virtual ICollection<QuanLy> QuanLys { get; set; }
    }
}
