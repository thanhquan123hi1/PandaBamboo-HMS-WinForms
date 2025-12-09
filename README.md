# Panda Bamboo (Hotel Management System)

Dự án phần mềm quản lý khách sạn được xây dựng bằng C# WinForms, sử dụng kiến trúc N-Layer và Entity Framework để quản lý dữ liệu. Giao diện được thiết kế hiện đại với sự hỗ trợ của thư viện Guna UI.

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-purple)
![Language](https://img.shields.io/badge/Language-C%23-green)
![App Type](https://img.shields.io/badge/App_Type-WinForms-blue)
![Database](https://img.shields.io/badge/Database-SQL_Server-red)
![ORM](https://img.shields.io/badge/ORM-Entity_Framework_6.4.4-lightblue)
![UI Library](https://img.shields.io/badge/UI_Library-Guna.UI2-orange)
## 🚀 Tính năng chính

*   **Quản lý phòng (Room Management):**
    *   Theo dõi trạng thái phòng (Trống, Có khách).
    *   Quản lý các loại phòng (Phòng đơn, Phòng đôi, Cao cấp).
*   **Check-in / Check-out:** Quy trình nhận và trả phòng nhanh chóng.
*   **Quản lý dịch vụ (Service Management):**
    *   Tích hợp các dịch vụ khách sạn: Nhà hàng, Bể bơi, Xông hơi, Bar, Giặt sấy.
*   **Quản lý khách hàng:** Lưu trữ thông tin cá nhân và lịch sử khách hàng.
*   **Quản lý hóa đơn:** Tính toán tiền phòng và dịch vụ, xuất hóa đơn thanh toán.
*   **Bảo mật:**
    *   Hệ thống đăng nhập an toàn.
    *   Tính năng quên mật khẩu: Xác thực qua Email và mã OTP.

## 🛠 Công nghệ sử dụng

*   **Ngôn ngữ:** C#
*   **Framework:** .NET Framework 4.7.2
*   **Loại ứng dụng:** Windows Forms (WinForms)
*   **UI Library:** Guna.UI2.WinForms
*   **Cơ sở dữ liệu:** SQL Server
*   **ORM:** Entity Framework 6.4.4
*   **Kiến trúc:** N-Layer
    *   `QuanLyKhachSan`: Presentation Layer (Giao diện).
    *   `BusinessAccessLayer`: Business Logic Layer (Nghiệp vụ).
    *   `DataAccessLayer`: Data Access Layer (Truy cập dữ liệu).

## ⚙️ Hướng dẫn Cài đặt

### 1. Yêu cầu môi trường
*   Visual Studio 2019 trở lên.
*   Microsoft SQL Server.
*   .NET Framework 4.7.2.

### 2. Thiết lập cơ sở dữ liệu
1.  Mở SQL Server Management Studio (SSMS).
2.  Mở file `SQL.sql` nằm trong thư mục gốc của repo.
3.  Execute (Chạy) toàn bộ script để tạo database `QuanLyKhachSanWin` và nạp dữ liệu mẫu (Phòng, Dịch vụ, Tài khoản Admin).

### 3. Cấu hình kết nối
1.  Mở file cấu hình `QuanLyKhachSan/App.config`.
2.  Tìm thẻ `<connectionStrings>` và cập nhật `Data Source` thành tên Server của bạn:
    ```xml
    <connectionStrings>
        <add name="HotelDbConnection"
             connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=QuanLyKhachSanWin;Integrated Security=True"
             providerName="System.Data.SqlClient" />
    </connectionStrings>
    ```

### 4. Chạy dự án
1.  Mở file solution `QuanLyKhachSan.sln` hoặc `HotelManagementSystem.sln` trong Visual Studio.
2.  Chuột phải vào Solution Explorer -> **Restore NuGet Packages** để tải các thư viện cần thiết (Entity Framework, Guna UI).
3.  Nhấn **Start** hoặc `F5` để biên dịch và chạy ứng dụng.

## 🔑 Tài khoản Demo
Danh sách tài khoản mặc định (được tạo từ script SQL):

| Username | Password |
|----------|----------|
| minhduc | 123 |
| ducthinh | 123 |
| thanhquan| 123 |

## 📂 Cấu trúc thư mục

*   `/BusinessAccessLayer`: Xử lý logic nghiệp vụ chính.
*   `/DataAccessLayer`: Chứa DBContext và định nghĩa các thực thể (Entities).
*   `/QuanLyKhachSan`:
    *   `fLogin`: Form đăng nhập.
    *   `fPhong`: Form quản lý danh sách phòng.
    *   `fCheckIn`: Form nhận phòng.
    *   `fHoaDon`: Form thanh toán.
    *   ...và các form chức năng khác.
