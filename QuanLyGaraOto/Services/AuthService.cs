using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.Services
{
    /// <summary>
    /// Singleton quản lý trạng thái đăng nhập toàn cục.
    /// Sử dụng: AuthService.Instance.Login(...), AuthService.Instance.CurrentUser, ...
    /// </summary>
    public class AuthService
    {
        // =================================================================
        // Singleton
        // =================================================================

        private static readonly Lazy<AuthService> _instance = new(() => new AuthService());

        /// <summary>
        /// Truy cập instance duy nhất của AuthService.
        /// </summary>
        public static AuthService Instance => _instance.Value;

        private AuthService() { }

        // =================================================================
        // Properties
        // =================================================================

        /// <summary>
        /// Thông tin người dùng đang đăng nhập. Null nếu chưa đăng nhập.
        /// </summary>
        public NguoiDung? CurrentUser { get; private set; }

        /// <summary>
        /// Kiểm tra nhanh đã đăng nhập hay chưa.
        /// </summary>
        public bool IsLoggedIn => CurrentUser is not null;

        // =================================================================
        // Methods
        // =================================================================

        /// <summary>
        /// Đăng nhập bằng tên đăng nhập và mật khẩu.
        /// Nếu đúng → lưu vào CurrentUser, trả về true.
        /// Nếu sai  → CurrentUser = null, trả về false.
        /// </summary>
        public bool Login(string username, string password)
        {
            try
            {
                using var context = new GaraDbContext();

                var user = context.NguoiDungs
                    .Include(u => u.VaiTro)       // Load luôn navigation property VaiTro
                    .FirstOrDefault(u =>
                        u.TenDangNhap == username
                        && u.MatKhau == password
                    );

                CurrentUser = user;
                return user is not null;
            }
            catch
            {
                CurrentUser = null;
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra CurrentUser có phải Quản Lý (MaVaiTro == 1) không.
        /// </summary>
        public bool IsAdmin()
        {
            return CurrentUser?.MaVaiTro == 1;
        }

        /// <summary>
        /// Đăng xuất — xóa phiên làm việc.
        /// </summary>
        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
