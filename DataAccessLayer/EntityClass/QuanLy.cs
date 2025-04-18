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
        [Key, Column(Order = 0)]
        public int MaPH { get; set; }

        [Key, Column(Order = 1)]
        public int MaNV { get; set; }

        [ForeignKey("MaPH")]
        public virtual Phong Phong { get; set; }
        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }
    }
}
