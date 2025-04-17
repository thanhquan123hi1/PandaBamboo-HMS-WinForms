using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityClass
{
    public class ThuocLoaiDV
    {
        [Key]
        public int MaDV { get; set; }

        public int MaLoaiDV { get; set; }

        public virtual DichVu DichVu { get; set; }
        public virtual LoaiDV LoaiDV { get; set; }
    }
}
