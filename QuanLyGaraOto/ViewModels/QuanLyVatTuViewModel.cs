using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class QuanLyVatTuViewModel : BaseViewModel
    {
        // =====================================================================
        // Properties
        // =====================================================================

        private ObservableCollection<VatTuPhuTung> _danhSachVatTu = new ObservableCollection<VatTuPhuTung>();
        public ObservableCollection<VatTuPhuTung> DanhSachVatTu
        {
            get => _danhSachVatTu;
            set => SetProperty(ref _danhSachVatTu, value);
        }

        private VatTuPhuTung? _selectedVatTu;
        public VatTuPhuTung? SelectedVatTu
        {
            get => _selectedVatTu;
            set
            {
                if (SetProperty(ref _selectedVatTu, value))
                {
                    // Khi chọn trên DataGrid, tự động điền dữ liệu lên form
                    if (_selectedVatTu != null)
                    {
                        TenVatTu = _selectedVatTu.TenVTPT;
                        DonGiaNhap = _selectedVatTu.DonGia;
                        SoLuongTon = _selectedVatTu.SoLuongTon;
                    }
                    else
                    {
                        ResetForm();
                    }
                }
            }
        }

        // Form Fields
        private string _tenVatTu = string.Empty;
        public string TenVatTu
        {
            get => _tenVatTu;
            set => SetProperty(ref _tenVatTu, value);
        }

        private decimal _donGiaNhap;
        public decimal DonGiaNhap
        {
            get => _donGiaNhap;
            set => SetProperty(ref _donGiaNhap, value);
        }

        private int _soLuongTon;
        public int SoLuongTon
        {
            get => _soLuongTon;
            set => SetProperty(ref _soLuongTon, value);
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

        public QuanLyVatTuViewModel()
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
                var ds = context.VatTuPhuTungs.ToList();
                DanhSachVatTu.Clear();
                foreach (var item in ds)
                {
                    DanhSachVatTu.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetForm()
        {
            SelectedVatTu = null;
            TenVatTu = string.Empty;
            DonGiaNhap = 0;
            SoLuongTon = 0;
        }

        // --- Logic THÊM ---
        private bool CanThem()
        {
            // Chỉ cần Tên vật tư không rỗng. (Đơn giá và số lượng mặc định = 0).
            return !string.IsNullOrWhiteSpace(TenVatTu) && SelectedVatTu == null;
        }

        private void Them()
        {
            try
            {
                using var context = new GaraDbContext();
                
                var vMoi = new VatTuPhuTung
                {
                    TenVTPT = TenVatTu.Trim(),
                    DonGia = 0, // Nhập kho mới có đơn giá nhập
                    SoLuongTon = 0 // Nhập kho mới có số lượng
                };

                context.VatTuPhuTungs.Add(vMoi);
                context.SaveChanges();

                MessageBox.Show("Thêm vật tư mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                
                LoadData();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm vật tư: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // --- Logic SỬA ---
        private bool CanSuaXoa()
        {
            return SelectedVatTu != null;
        }

        private void Sua()
        {
            if (string.IsNullOrWhiteSpace(TenVatTu))
            {
                MessageBox.Show("Tên vật tư không được để trống!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using var context = new GaraDbContext();
                var v = context.VatTuPhuTungs.FirstOrDefault(x => x.MaVTPT == SelectedVatTu!.MaVTPT);
                if (v != null)
                {
                    // Quản lý chỉ được sửa tên, Đơn giá và Số lượng được chốt từ việc Lập Phiếu Nhập
                    v.TenVTPT = TenVatTu.Trim();
                    context.SaveChanges();

                    MessageBox.Show("Sửa tên vật tư thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa vật tư '{SelectedVatTu!.TenVTPT}' không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                using var context = new GaraDbContext();
                var v = context.VatTuPhuTungs.FirstOrDefault(x => x.MaVTPT == SelectedVatTu.MaVTPT);
                if (v != null)
                {
                    context.VatTuPhuTungs.Remove(v);
                    context.SaveChanges();
                    
                    MessageBox.Show("Xóa vật tư thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ResetForm();
                }
            }
            catch (DbUpdateException)
            {
                // Bắt lỗi ràng buộc khóa ngoại (Foreign Key constraint)
                MessageBox.Show("Không thể xóa vật tư này vì đã được sử dụng trong Phiếu Sửa Chữa hoặc Phiếu Nhập Kho!", 
                                "Lỗi ràng buộc dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không mong muốn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
