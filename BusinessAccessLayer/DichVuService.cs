using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.EntityClass;


namespace BusinessAccessLayer
{
    public class DichVuService
    {
        private readonly HotelContext _context;

        public DichVuService()
        {
            _context = new HotelContext();
        }

        // Lấy danh sách dịch vụ 
        public List<DichVu> GetAllDichVu()
        {
            return _context.DichVus.ToList();
        }
    }
}
