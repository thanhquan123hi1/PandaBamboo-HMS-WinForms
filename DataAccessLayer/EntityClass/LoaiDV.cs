using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityClass
{
    public class LoaiDV
    {
        [Key]
        public int MaLoaiDV { get; set; }

        [Required(ErrorMessage = "Tên loại dịch vụ là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên loại dịch vụ không được quá 50 ký tự")]
        [RegularExpression("GiaiTri|AnUong|DiChuyen|CaNhan", ErrorMessage = "Tên loại dịch vụ không hợp lệ")]
        public string TenLoaiDV { get; set; }

        public virtual ICollection<ThuocLoaiDV> ThuocLoaiDVs { get; set; }
    }
}
