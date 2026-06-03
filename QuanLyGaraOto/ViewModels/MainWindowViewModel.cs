using System.Windows;
using QuanLyGaraOto.Services;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        // =================================================================
        // Properties — Thông tin người dùng
        // =================================================================

        /// <summary>
        /// Tên người dùng đang đăng nhập, hiển thị trên sidebar.
        /// </summary>
        public string TenNguoiDung => AuthService.Instance.CurrentUser?.TenNguoiDung ?? "Người dùng";

        /// <summary>
        /// Tên vai trò hiển thị trên sidebar.
        /// </summary>
        public string TenVaiTro => AuthService.Instance.CurrentUser?.VaiTro?.TenVaiTro ?? "";

        // =================================================================
        // Properties — Phân quyền
        // =================================================================

        /// <summary>
        /// true nếu người dùng hiện tại là Quản lý (MaVaiTro == 1).
        /// Dùng kết hợp với BooleanToVisibilityConverter trong XAML.
        /// </summary>
        public bool IsAdmin => AuthService.Instance.IsAdmin();

        // =================================================================
        // Properties — Điều hướng (Navigation)
        // =================================================================

        private BaseViewModel _currentView;

        /// <summary>
        /// Màn hình hiện tại đang được hiển thị ở phần Main Content.
        /// </summary>
        public BaseViewModel CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        // =================================================================
        // Commands
        // =================================================================

        public RelayCommand DangXuatCommand { get; }

        public RelayCommand ShowTiepNhanXeCommand { get; }
        public RelayCommand ShowPhieuSuaChuaCommand { get; }
        public RelayCommand ShowPhieuThuTienCommand { get; }
        public RelayCommand ShowQuanLyVatTuCommand { get; }
        public RelayCommand ShowPhieuNhapKhoCommand { get; }
        public RelayCommand ShowTraCuuXeCommand { get; }
        public RelayCommand ShowLichSuTiepNhanXeCommand { get; }
        public RelayCommand ShowTraCuuPhieuSuaChuaCommand { get; }
        public RelayCommand ShowTraCuuPhieuNhapKhoCommand { get; }
        public RelayCommand ShowTraCuuPhieuThuTienCommand { get; }
        public RelayCommand ShowBaoCaoDoanhSoCommand { get; }
        public RelayCommand ShowBaoCaoTonKhoCommand { get; }
        public RelayCommand ShowQuanLyHieuXeCommand { get; }
        public RelayCommand ShowQuanLyTienCongCommand { get; }
        public RelayCommand ShowThayDoiQuyDinhCommand { get; }
        public RelayCommand ShowQuanLyNguoiDungCommand { get; }

        // =================================================================
        // Constructor
        // =================================================================

        public MainWindowViewModel()
        {
            DangXuatCommand = new RelayCommand(DangXuat);
            
            ShowTiepNhanXeCommand = new RelayCommand(() => CurrentView = new TiepNhanXeViewModel());
            ShowPhieuSuaChuaCommand = new RelayCommand(() => CurrentView = new PhieuSuaChuaViewModel());
            ShowPhieuThuTienCommand = new RelayCommand(() => CurrentView = new PhieuThuTienViewModel());
            ShowQuanLyVatTuCommand = new RelayCommand(() => CurrentView = new QuanLyVatTuViewModel());
            ShowPhieuNhapKhoCommand = new RelayCommand(() => CurrentView = new PhieuNhapKhoViewModel());
            ShowTraCuuXeCommand = new RelayCommand(() => CurrentView = new TraCuuXeViewModel());
            ShowLichSuTiepNhanXeCommand = new RelayCommand(() => CurrentView = new LichSuTiepNhanXeViewModel());
            ShowTraCuuPhieuSuaChuaCommand = new RelayCommand(() => CurrentView = new TraCuuPhieuSuaChuaViewModel());
            ShowTraCuuPhieuNhapKhoCommand = new RelayCommand(() => CurrentView = new TraCuuPhieuNhapKhoViewModel());
            ShowTraCuuPhieuThuTienCommand = new RelayCommand(() => CurrentView = new TraCuuPhieuThuTienViewModel());
            ShowBaoCaoDoanhSoCommand = new RelayCommand(() => CurrentView = new BaoCaoDoanhSoViewModel());
            ShowBaoCaoTonKhoCommand = new RelayCommand(() => CurrentView = new BaoCaoTonKhoViewModel());
            ShowQuanLyHieuXeCommand = new RelayCommand(() => CurrentView = new QuanLyHieuXeViewModel());
            ShowQuanLyTienCongCommand = new RelayCommand(() => CurrentView = new QuanLyTienCongViewModel());
            ShowThayDoiQuyDinhCommand = new RelayCommand(() => CurrentView = new ThayDoiQuyDinhViewModel());
            ShowQuanLyNguoiDungCommand = new RelayCommand(() => CurrentView = new QuanLyNguoiDungViewModel());

            // Mặc định hiển thị màn hình Tiếp nhận xe khi mới mở
            _currentView = new TiepNhanXeViewModel();
        }

        // =================================================================
        // Private Methods
        // =================================================================

        private void DangXuat()
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                AuthService.Instance.Logout();

                // Mở lại màn hình đăng nhập
                var loginView = new Views.LoginView();
                loginView.Show();

                // Đóng MainWindow
                CloseCurrentWindow();
            }
        }

        private void CloseCurrentWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
