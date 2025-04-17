using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityClass
{
    public class QuanLy
    {
        [Required(ErrorMessage = "Mã phòng là bắt buộc")]
        public int MaPH { get; set; }

        [Required(ErrorMessage = "Mã nhân viên là bắt buộc")]
        public int MaNV { get; set; }

        public virtual Phong Phong { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
