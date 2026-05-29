using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class QuanLyHieuXeViewModel : BaseViewModel
    {
        // =====================================================================
        // Properties
        // =====================================================================

        private ObservableCollection<HieuXe> _danhSachHieuXe = new ObservableCollection<HieuXe>();
        public ObservableCollection<HieuXe> DanhSachHieuXe
        {
            get => _danhSachHieuXe;
            set => SetProperty(ref _danhSachHieuXe, value);
        }

        private HieuXe? _selectedHieuXe;
        public HieuXe? SelectedHieuXe
        {
            get => _selectedHieuXe;
            set
            {
                if (SetProperty(ref _selectedHieuXe, value))
                {
                    if (_selectedHieuXe != null)
                    {
                        TenHieuXe = _selectedHieuXe.TenHieuXe;
                    }
                    else
                    {
                        TenHieuXe = string.Empty;
                    }
                }
            }
        }

        private string _tenHieuXe = string.Empty;
        public string TenHieuXe
        {
            get => _tenHieuXe;
            set => SetProperty(ref _tenHieuXe, value);
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

        public QuanLyHieuXeViewModel()
        {
            ThemCommand = new RelayCommand(Them, CanThemSua);
            SuaCommand = new RelayCommand(Sua, CanSuaXoa);
            XoaCommand = new RelayCommand(Xoa, CanSuaXoa);
            ClearFormCommand = new RelayCommand(ClearForm);

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
                var list = context.HieuXes.ToList();
                DanhSachHieuXe.Clear();
                foreach (var hx in list)
                {
                    DanhSachHieuXe.Add(hx);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách hiệu xe:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanThemSua() => !string.IsNullOrWhiteSpace(TenHieuXe);
        private bool CanSuaXoa() => SelectedHieuXe != null && !string.IsNullOrWhiteSpace(TenHieuXe);

        private void ClearForm()
        {
            SelectedHieuXe = null;
            TenHieuXe = string.Empty;
        }

        private void Them()
        {
            try
            {
                using var context = new GaraDbContext();

                // Kiểm tra trùng tên hiệu xe
                var exists = context.HieuXes.Any(x => x.TenHieuXe.ToLower() == TenHieuXe.Trim().ToLower());
                if (exists)
                {
                    MessageBox.Show("Tên hiệu xe này đã tồn tại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newHieuXe = new HieuXe
                {
                    TenHieuXe = TenHieuXe.Trim()
                };

                context.HieuXes.Add(newHieuXe);
                context.SaveChanges();

                MessageBox.Show("Thêm hiệu xe thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Sua()
        {
            if (SelectedHieuXe == null) return;

            try
            {
                using var context = new GaraDbContext();

                // Kiểm tra trùng tên hiệu xe (bỏ qua chính nó)
                var exists = context.HieuXes.Any(x => x.MaHieuXe != SelectedHieuXe.MaHieuXe 
                                                   && x.TenHieuXe.ToLower() == TenHieuXe.Trim().ToLower());
                if (exists)
                {
                    MessageBox.Show("Tên hiệu xe này đã tồn tại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var hxToUpdate = context.HieuXes.Find(SelectedHieuXe.MaHieuXe);
                if (hxToUpdate != null)
                {
                    hxToUpdate.TenHieuXe = TenHieuXe.Trim();
                    context.SaveChanges();

                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Xoa()
        {
            if (SelectedHieuXe == null) return;

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa hiệu xe '{SelectedHieuXe.TenHieuXe}'?",
                                         "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                using var context = new GaraDbContext();
                var hxToDelete = context.HieuXes.Find(SelectedHieuXe.MaHieuXe);
                
                if (hxToDelete != null)
                {
                    context.HieuXes.Remove(hxToDelete);
                    context.SaveChanges();
                    
                    MessageBox.Show("Xóa hiệu xe thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ClearForm();
                }
            }
            catch (DbUpdateException)
            {
                // Đây là lỗi do dính khóa ngoại (Foreign Key Constraint)
                MessageBox.Show("Không thể xóa hiệu xe này vì đã có Xe sử dụng!\nVui lòng kiểm tra lại danh sách Xe.", 
                                "Lỗi ràng buộc dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
