using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class QuanLyTienCongViewModel : BaseViewModel
    {
        // =====================================================================
        // Properties
        // =====================================================================

        private ObservableCollection<TienCong> _danhSachTienCong = new ObservableCollection<TienCong>();
        public ObservableCollection<TienCong> DanhSachTienCong
        {
            get => _danhSachTienCong;
            set => SetProperty(ref _danhSachTienCong, value);
        }

        private TienCong? _selectedTienCong;
        public TienCong? SelectedTienCong
        {
            get => _selectedTienCong;
            set
            {
                if (SetProperty(ref _selectedTienCong, value))
                {
                    if (_selectedTienCong != null)
                    {
                        TenTienCong = _selectedTienCong.TenTienCong;
                        DonGia = _selectedTienCong.DonGia;
                    }
                    else
                    {
                        TenTienCong = string.Empty;
                        DonGia = 0;
                    }
                }
            }
        }

        private string _tenTienCong = string.Empty;
        public string TenTienCong
        {
            get => _tenTienCong;
            set => SetProperty(ref _tenTienCong, value);
        }

        private decimal _donGia;
        public decimal DonGia
        {
            get => _donGia;
            set => SetProperty(ref _donGia, value);
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

        public QuanLyTienCongViewModel()
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
                var list = context.TienCongs.ToList();
                DanhSachTienCong.Clear();
                foreach (var tc in list)
                {
                    DanhSachTienCong.Add(tc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách tiền công:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanThemSua() => !string.IsNullOrWhiteSpace(TenTienCong) && DonGia > 0;
        private bool CanSuaXoa() => SelectedTienCong != null && !string.IsNullOrWhiteSpace(TenTienCong) && DonGia > 0;

        private void ClearForm()
        {
            SelectedTienCong = null;
            TenTienCong = string.Empty;
            DonGia = 0;
        }

        private void Them()
        {
            try
            {
                using var context = new GaraDbContext();

                // Kiểm tra trùng tên
                var exists = context.TienCongs.Any(x => x.TenTienCong.ToLower() == TenTienCong.Trim().ToLower());
                if (exists)
                {
                    MessageBox.Show("Tên tiền công (loại sửa chữa) này đã tồn tại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newTienCong = new TienCong
                {
                    TenTienCong = TenTienCong.Trim(),
                    DonGia = DonGia
                };

                context.TienCongs.Add(newTienCong);
                context.SaveChanges();

                MessageBox.Show("Thêm loại tiền công thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
            if (SelectedTienCong == null) return;

            try
            {
                using var context = new GaraDbContext();

                // Kiểm tra trùng tên (bỏ qua chính nó)
                var exists = context.TienCongs.Any(x => x.MaTienCong != SelectedTienCong.MaTienCong 
                                                   && x.TenTienCong.ToLower() == TenTienCong.Trim().ToLower());
                if (exists)
                {
                    MessageBox.Show("Tên tiền công này đã tồn tại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var tcToUpdate = context.TienCongs.Find(SelectedTienCong.MaTienCong);
                if (tcToUpdate != null)
                {
                    tcToUpdate.TenTienCong = TenTienCong.Trim();
                    tcToUpdate.DonGia = DonGia;
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
            if (SelectedTienCong == null) return;

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa tiền công '{SelectedTienCong.TenTienCong}'?",
                                         "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                using var context = new GaraDbContext();
                var tcToDelete = context.TienCongs.Find(SelectedTienCong.MaTienCong);
                
                if (tcToDelete != null)
                {
                    context.TienCongs.Remove(tcToDelete);
                    context.SaveChanges();
                    
                    MessageBox.Show("Xóa tiền công thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ClearForm();
                }
            }
            catch (DbUpdateException)
            {
                // Lỗi khóa ngoại
                MessageBox.Show("Không thể xóa loại tiền công này vì đã được sử dụng trong Phiếu Sửa Chữa!\nVui lòng giữ lại để đảm bảo lịch sử dữ liệu.", 
                                "Lỗi ràng buộc dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
