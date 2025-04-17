using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityClass
{
    public class DichVu
    {
        [Key]
        public int MaDV { get; set; }

        [Required(ErrorMessage = "Tên dịch vụ là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên dịch vụ không được quá 100 ký tự")]
        public string TenDV { get; set; }

        [Required(ErrorMessage = "Giá dịch vụ là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá dịch vụ phải >= 0")]
        public decimal GiaDV { get; set; }
        public virtual ICollection<SuDungDichVu> SuDungDichVus { get; set; }
        public virtual ICollection<ThuocLoaiDV> ThuocLoaiDVs { get; set; }
    }
}
