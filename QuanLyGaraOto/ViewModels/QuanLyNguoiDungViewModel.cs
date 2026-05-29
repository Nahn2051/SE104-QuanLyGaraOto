using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;
using QuanLyGaraOto.Services;

namespace QuanLyGaraOto.ViewModels
{
    public class QuanLyNguoiDungViewModel : BaseViewModel
    {
        // =====================================================================
        // Properties
        // =====================================================================

        private ObservableCollection<NguoiDung> _danhSachNguoiDung = new ObservableCollection<NguoiDung>();
        public ObservableCollection<NguoiDung> DanhSachNguoiDung
        {
            get => _danhSachNguoiDung;
            set => SetProperty(ref _danhSachNguoiDung, value);
        }

        private ObservableCollection<VaiTro> _danhSachVaiTro = new ObservableCollection<VaiTro>();
        public ObservableCollection<VaiTro> DanhSachVaiTro
        {
            get => _danhSachVaiTro;
            set => SetProperty(ref _danhSachVaiTro, value);
        }

        private NguoiDung? _selectedNguoiDung;
        public NguoiDung? SelectedNguoiDung
        {
            get => _selectedNguoiDung;
            set
            {
                if (SetProperty(ref _selectedNguoiDung, value))
                {
                    if (_selectedNguoiDung != null)
                    {
                        TenDangNhap = _selectedNguoiDung.TenDangNhap;
                        HoTen = _selectedNguoiDung.TenNguoiDung;
                        MatKhau = string.Empty; // Tránh hiển thị pass cũ
                        SelectedVaiTro = DanhSachVaiTro.FirstOrDefault(v => v.MaVaiTro == _selectedNguoiDung.MaVaiTro);
                    }
                    else
                    {
                        ResetForm();
                    }
                }
            }
        }

        // --- Form Fields ---
        private string _tenDangNhap = string.Empty;
        public string TenDangNhap
        {
            get => _tenDangNhap;
            set => SetProperty(ref _tenDangNhap, value);
        }

        private string _matKhau = string.Empty;
        public string MatKhau
        {
            get => _matKhau;
            set => SetProperty(ref _matKhau, value);
        }

        private string _hoTen = string.Empty;
        public string HoTen
        {
            get => _hoTen;
            set => SetProperty(ref _hoTen, value);
        }

        private VaiTro? _selectedVaiTro;
        public VaiTro? SelectedVaiTro
        {
            get => _selectedVaiTro;
            set => SetProperty(ref _selectedVaiTro, value);
        }

        // =====================================================================
        // Commands
        // =====================================================================

        public RelayCommand ThemCommand { get; }
        public RelayCommand SuaCommand { get; }
        public RelayCommand XoaCommand { get; }
        public RelayCommand ClearFormCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public QuanLyNguoiDungViewModel()
        {
            ThemCommand = new RelayCommand(Them, CanThem);
            SuaCommand = new RelayCommand(Sua, CanSuaXoa);
            XoaCommand = new RelayCommand(Xoa, CanSuaXoa);
            ClearFormCommand = new RelayCommand(ResetForm);

            LoadData();
        }

        // =====================================================================
        // Methods
        // =====================================================================

        private void LoadData()
        {
            try
            {
                using var context = new GaraDbContext();
                
                // Load Vai trò
                var vaiTros = context.VaiTros.ToList();
                DanhSachVaiTro.Clear();
                foreach (var vt in vaiTros) DanhSachVaiTro.Add(vt);

                // Load Người dùng (Include VaiTro để hiển thị Tên vai trò trên DataGrid)
                var users = context.NguoiDungs.Include(u => u.VaiTro).ToList();
                DanhSachNguoiDung.Clear();
                foreach (var u in users) DanhSachNguoiDung.Add(u);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách người dùng:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetForm()
        {
            SelectedNguoiDung = null;
            TenDangNhap = string.Empty;
            MatKhau = string.Empty;
            HoTen = string.Empty;
            SelectedVaiTro = null;
        }

        // --- Logic THÊM ---
        private bool CanThem()
        {
            return SelectedNguoiDung == null && !string.IsNullOrWhiteSpace(TenDangNhap) 
                   && !string.IsNullOrWhiteSpace(MatKhau) && !string.IsNullOrWhiteSpace(HoTen) 
                   && SelectedVaiTro != null;
        }

        private void Them()
        {
            try
            {
                using var context = new GaraDbContext();

                // Kiểm tra trùng TenDangNhap
                var isExist = context.NguoiDungs.Any(u => u.TenDangNhap == TenDangNhap.Trim());
                if (isExist)
                {
                    MessageBox.Show("Tên đăng nhập này đã tồn tại! Vui lòng chọn tên khác.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var userMoi = new NguoiDung
                {
                    TenDangNhap = TenDangNhap.Trim(),
                    MatKhau = MatKhau, // Trong thực tế nên mã hóa (Hash) mật khẩu ở đây
                    TenNguoiDung = HoTen.Trim(),
                    MaVaiTro = SelectedVaiTro!.MaVaiTro
                };

                context.NguoiDungs.Add(userMoi);
                context.SaveChanges();

                MessageBox.Show("Tạo người dùng mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                
                LoadData();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm người dùng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // --- Logic SỬA ---
        private bool CanSuaXoa()
        {
            return SelectedNguoiDung != null;
        }

        private void Sua()
        {
            if (string.IsNullOrWhiteSpace(TenDangNhap) || string.IsNullOrWhiteSpace(HoTen) || SelectedVaiTro == null)
            {
                MessageBox.Show("Vui lòng điền đủ Tên đăng nhập, Họ tên và Chọn vai trò!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using var context = new GaraDbContext();
                
                // Kiểm tra trùng lặp nếu sửa Tên đăng nhập thành tên của người khác
                var isExist = context.NguoiDungs.Any(u => u.TenDangNhap == TenDangNhap.Trim() && u.MaNguoiDung != SelectedNguoiDung!.MaNguoiDung);
                if (isExist)
                {
                    MessageBox.Show("Tên đăng nhập này đang được sử dụng bởi người khác!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var userDb = context.NguoiDungs.FirstOrDefault(u => u.MaNguoiDung == SelectedNguoiDung!.MaNguoiDung);
                if (userDb != null)
                {
                    userDb.TenDangNhap = TenDangNhap.Trim();
                    userDb.TenNguoiDung = HoTen.Trim();
                    userDb.MaVaiTro = SelectedVaiTro.MaVaiTro;

                    // Nếu có nhập mật khẩu mới thì mới cập nhật
                    if (!string.IsNullOrWhiteSpace(MatKhau))
                    {
                        userDb.MatKhau = MatKhau;
                    }

                    context.SaveChanges();

                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // --- Logic XÓA ---
        private void Xoa()
        {
            // Kiểm tra không được tự xóa chính mình
            if (AuthService.Instance.CurrentUser != null && SelectedNguoiDung!.MaNguoiDung == AuthService.Instance.CurrentUser.MaNguoiDung)
            {
                MessageBox.Show("Bạn không thể tự xóa tài khoản của chính mình khi đang đăng nhập!", "Từ chối", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng '{SelectedNguoiDung!.TenDangNhap}' không?", 
                                         "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                using var context = new GaraDbContext();
                var userDb = context.NguoiDungs.FirstOrDefault(u => u.MaNguoiDung == SelectedNguoiDung.MaNguoiDung);
                
                if (userDb != null)
                {
                    context.NguoiDungs.Remove(userDb);
                    context.SaveChanges();
                    
                    MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không thể xóa tài khoản:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
