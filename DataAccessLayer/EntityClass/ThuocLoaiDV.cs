using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityClass
{
    public class ThuocLoaiDV
    {
        [Key]
        [Column(Order = 0)]
        public int MaDV { get; set; }

        [Key]
        [Column(Order = 1)]
        public int MaLoaiDV { get; set; }

        [ForeignKey("MaDV")]
        public virtual DichVu DichVu { get; set; }

        [ForeignKey("MaLoaiDV")]
        public virtual LoaiDV LoaiDV { get; set; }
    }
}
