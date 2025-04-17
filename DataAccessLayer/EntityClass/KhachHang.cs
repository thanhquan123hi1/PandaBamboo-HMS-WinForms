using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityClass
{
    public class KhachHang
    {
        [Key]
        public int MaKH { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không được quá 100 ký tự")]
        public string TenKH { get; set; }

        [Required(ErrorMessage = "Quốc tịch là bắt buộc")]
        [StringLength(50, ErrorMessage = "Quốc tịch không được quá 50 ký tự")]
        public string QuocTich { get; set; }

        [Required(ErrorMessage = "CCCD/VISA là bắt buộc")]
        [StringLength(50, ErrorMessage = "CCCD/VISA không được quá 50 ký tự")]
        public string CCCD_VISA { get; set; }

        [StringLength(20, ErrorMessage = "SĐT không được quá 20 ký tự")]
        public string SDT { get; set; }

        public virtual ICollection<DatPhong> DatPhongs { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<SuDungDichVu> SuDungDichVus { get; set; }
    }

}
