using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityClass
{
    public class NhanVien
    {
        [Key]
        public int MaNV { get; set; }

        [Required(ErrorMessage = "Tên nhân viên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên không được quá 100 ký tự")]
        public string TenNV { get; set; }

        [Required(ErrorMessage = "Chức vụ là bắt buộc")]
        [RegularExpression("TiepTan|QuanLy|NhanVienDonDep|NhanVienDichVu|BaoVe", ErrorMessage = "Chức vụ không hợp lệ")]
        public string ChucVu { get; set; }

        [Required(ErrorMessage = "CCCD là bắt buộc")]
        [StringLength(50, ErrorMessage = "CCCD không được quá 50 ký tự")]
        public string CCCD { get; set; }

        [StringLength(20, ErrorMessage = "SĐT không được quá 20 ký tự")]
        public string SDT { get; set; }

        public virtual ICollection<QuanLy> QuanLys { get; set; }
        public virtual ICollection<LapHoaDon> LapHoaDons { get; set; }
    }
}
