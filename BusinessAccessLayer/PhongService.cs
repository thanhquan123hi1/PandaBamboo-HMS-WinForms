using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EntityClass;

namespace BusinessAccessLayer
{
    public class PhongService
    {
        private readonly HotelContext _context;
        public PhongService()
        {
            _context = new HotelContext();
        }

        // Lấy danh sách tất cả các phòng
        public List<Phong> GetAllPhongs()
        {
            return _context.Phongs.ToList();
        }
    }
}
