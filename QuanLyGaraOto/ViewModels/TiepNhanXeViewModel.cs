using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class TiepNhanXeViewModel : BaseViewModel
    {
        // =====================================================================
        // Backing fields
        // =====================================================================

        private string _bienSo = string.Empty;
        private string _tenChuXe = string.Empty;
        private string _diaChi = string.Empty;
        private string _dienThoai = string.Empty;
        private string _email = string.Empty;
        private HieuXe? _selectedHieuXe;
        private ObservableCollection<HieuXe> _danhSachHieuXe = [];

        // =====================================================================
        // Properties — bind lên UI
        // =====================================================================

        public string BienSo
        {
            get => _bienSo;
            set => SetProperty(ref _bienSo, value);
        }

        public string TenChuXe
        {
            get => _tenChuXe;
            set => SetProperty(ref _tenChuXe, value);
        }

        public string DiaChi
        {
            get => _diaChi;
            set => SetProperty(ref _diaChi, value);
        }

        public string DienThoai
        {
            get => _dienThoai;
            set => SetProperty(ref _dienThoai, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        /// <summary>
        /// Danh sách hiệu xe load từ Database, hiển thị trong ComboBox.
        /// </summary>
        public ObservableCollection<HieuXe> DanhSachHieuXe
        {
            get => _danhSachHieuXe;
            set => SetProperty(ref _danhSachHieuXe, value);
        }

        /// <summary>
        /// Hiệu xe được chọn trên ComboBox.
        /// </summary>
        public HieuXe? SelectedHieuXe
        {
            get => _selectedHieuXe;
            set => SetProperty(ref _selectedHieuXe, value);
        }

        // =====================================================================
        // Commands
        // =====================================================================

        public RelayCommand LuuHoSoCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public TiepNhanXeViewModel()
        {
            LuuHoSoCommand = new RelayCommand(
                execute: LuuHoSo,
                canExecute: () => CanLuuHoSo()
            );

            LoadDanhSachHieuXe();
        }

        // =====================================================================
        // Private Methods
        // =====================================================================

        /// <summary>
        /// Load danh sách hiệu xe từ Database lên ComboBox.
        /// </summary>
        private void LoadDanhSachHieuXe()
        {
            try
            {
                using var context = new GaraDbContext();
                var hieuXes = context.HieuXes.ToList();
                DanhSachHieuXe = new ObservableCollection<HieuXe>(hieuXes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tải danh sách hiệu xe:\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        /// <summary>
        /// Kiểm tra điều kiện hợp lệ trước khi cho phép lưu.
        /// </summary>
        private bool CanLuuHoSo()
        {
            return !string.IsNullOrWhiteSpace(BienSo)
                && !string.IsNullOrWhiteSpace(TenChuXe)
                && SelectedHieuXe is not null;
        }

        /// <summary>
        /// Lưu hồ sơ tiếp nhận xe:
        /// 1. Tạo record XE mới
        /// 2. Tự động tạo PHIEUSUACHUA với NgaySuaChua = ngày hiện tại
        /// </summary>
        private void LuuHoSo()
        {
            try
            {
                using var context = new GaraDbContext();

                var ngayTiepNhan = DateTime.Now;

                // --- Kiểm tra số lượng xe tối đa trong ngày ---
                var thamSoSoXe = context.ThamSos.FirstOrDefault(ts => ts.TenThamSo == "SoXeSuaChuaToiDa");
                if (thamSoSoXe != null && int.TryParse(thamSoSoXe.GiaTri, out int maxSoXe))
                {
                    int soXeDaNhan = context.Xes.Count(x => x.NgayTiepNhan.HasValue && x.NgayTiepNhan.Value.Date == DateTime.Today);
                    if (soXeDaNhan >= maxSoXe)
                    {
                        MessageBox.Show(
                            $"Không thể tiếp nhận thêm xe!\n\nSố xe đã nhận trong ngày hôm nay ({soXeDaNhan}) đã đạt mức tối đa quy định ({maxSoXe}).",
                            "Từ chối tiếp nhận",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning
                        );
                        return;
                    }
                }

                // --- Tạo record Xe mới ---
                var xeMoi = new Xe
                {
                    BienSo = BienSo.Trim(),
                    MaHieuXe = SelectedHieuXe!.MaHieuXe,
                    TenChuXe = TenChuXe.Trim(),
                    DiaChi = string.IsNullOrWhiteSpace(DiaChi) ? null : DiaChi.Trim(),
                    DienThoai = string.IsNullOrWhiteSpace(DienThoai) ? null : DienThoai.Trim(),
                    Email = string.IsNullOrWhiteSpace(Email) ? null : Email.Trim(),
                    NgayTiepNhan = ngayTiepNhan,
                    TienNo = 0
                };

                context.Xes.Add(xeMoi);
                context.SaveChanges(); // Lưu để có MaXe (auto-generated)

                // --- Tự động tạo Phiếu sửa chữa ---
                var phieuSC = new PhieuSuaChua
                {
                    MaXe = xeMoi.MaXe,
                    NgaySuaChua = ngayTiepNhan,
                    TongTien = 0
                };

                context.PhieuSuaChuas.Add(phieuSC);
                context.SaveChanges();

                MessageBox.Show(
                    $"Tiếp nhận xe thành công!\n\n"
                    + $"• Biển số: {xeMoi.BienSo}\n"
                    + $"• Chủ xe: {xeMoi.TenChuXe}\n"
                    + $"• Mã phiếu sửa chữa: {phieuSC.MaPhieuSuaChua}",
                    "Thành công",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                // Reset form sau khi lưu thành công
                ResetForm();
            }
            catch (DbUpdateException ex)
            {
                // Lỗi liên quan đến Database (VD: trùng biển số do UNIQUE index)
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show(
                    $"Lỗi khi lưu vào cơ sở dữ liệu:\n{innerMessage}",
                    "Lỗi Database",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Đã xảy ra lỗi không mong muốn:\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        /// <summary>
        /// Reset toàn bộ form về trạng thái ban đầu.
        /// </summary>
        private void ResetForm()
        {
            BienSo = string.Empty;
            TenChuXe = string.Empty;
            DiaChi = string.Empty;
            DienThoai = string.Empty;
            Email = string.Empty;
            SelectedHieuXe = null;
        }
    }
}
