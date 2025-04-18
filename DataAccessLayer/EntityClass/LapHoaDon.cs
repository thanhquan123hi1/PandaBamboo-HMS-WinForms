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
        [Key, ForeignKey("HoaDon")] // vừa là khóa chính vừa là khóa ngoại đến HoaDon
        [Required]
        public int MaHD { get; set; }

        [Required]
        public int MaNV { get; set; }

        [Required]
        public DateTime NgayLap { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual HoaDon HoaDon { get; set; } // navigation đến HoaDon
    }
}
