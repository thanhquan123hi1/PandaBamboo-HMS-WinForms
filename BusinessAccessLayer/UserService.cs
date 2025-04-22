using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.EntityClass;
namespace BusinessAccessLayer
{
    public class UserService
    {
        private readonly HotelContext _context;
        public UserService()
        {
            _context = new HotelContext();
        }
        // Lấy danh sách tất cả người dùng
        public bool KiemTraDangNhap(string username, string password)
        {
            List<User> danhSachTaiKhoan = _context.Users.ToList();

            foreach (var acc in danhSachTaiKhoan)
            {
                if (acc.Username == username && acc.PasswordHash == password)
                {
                    return true; // Đăng nhập thành công
                }
            }
            return false;
        }
        // Kiểm tra địa chỉ email
        public bool KiemTraMail(string gmail)
        {
            List<User> danhSachTaiKhoan = _context.Users.ToList();
            foreach (var acc in danhSachTaiKhoan)
            {
                if (acc.Email == gmail)
                {
                    return true; // Đăng nhập thành công
                }
            }
            return false;
        }
        // Cập nhật mật khẩu
        public bool UpdataPassword(string email, string newPassword)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    user.PasswordHash = newPassword;
                    _context.SaveChanges(); // <--- đây chính là phần "UPDATE"
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần
                return false;
            }
        }

    }
}
