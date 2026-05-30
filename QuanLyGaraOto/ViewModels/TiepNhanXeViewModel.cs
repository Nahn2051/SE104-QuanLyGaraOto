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
        private DateTime _ngayTiepNhan = DateTime.Now;

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

        public DateTime NgayTiepNhan
        {
            get => _ngayTiepNhan;
            set => SetProperty(ref _ngayTiepNhan, value);
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
                if (NgayTiepNhan.Date > DateTime.Now.Date)
                {
                    MessageBox.Show("Ngày tiếp nhận không được lớn hơn ngày hiện tại!", "Lỗi ngày tháng", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using var context = new GaraDbContext();

                // --- Kiểm tra số lượng xe tối đa trong ngày ---
                var thamSoSoXe = context.ThamSos.FirstOrDefault(ts => ts.TenThamSo == "SoXeSuaChuaToiDa");
                if (thamSoSoXe != null && int.TryParse(thamSoSoXe.GiaTri, out int maxSoXe))
                {
                    int soXeDaNhan = context.Xes.Count(x => x.NgayTiepNhan.HasValue && x.NgayTiepNhan.Value.Date == NgayTiepNhan.Date);
                    if (soXeDaNhan >= maxSoXe)
                    {
                        MessageBox.Show(
                            $"Không thể tiếp nhận thêm xe!\n\nSố xe đã nhận trong ngày {NgayTiepNhan:dd/MM/yyyy} ({soXeDaNhan}) đã đạt mức tối đa quy định ({maxSoXe}).",
                            "Từ chối tiếp nhận",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning
                        );
                        return;
                    }
                }

                // --- CHUẨN HÓA VÀ VALIDATION DỮ LIỆU ---

                // 1. Chuẩn hóa Biển Số: Tước bỏ mọi khoảng trắng, dấu chấm, dấu gạch ngang (chỉ giữ chữ và số), sau đó viết hoa.
                // Regex @"[^a-zA-Z0-9]" sẽ tìm và thay thế (loại bỏ) mọi ký tự không phải chữ cái (a-z, A-Z) và không phải số (0-9).
                string bienSoClean = System.Text.RegularExpressions.Regex.Replace(BienSo, @"[^a-zA-Z0-9]", "").ToUpper();
                if (string.IsNullOrEmpty(bienSoClean))
                {
                    MessageBox.Show("Biển số không hợp lệ!", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 2. Validation Email: Bắt buộc phải chứa '@' nếu có nhập
                if (!string.IsNullOrWhiteSpace(Email) && !Email.Contains("@"))
                {
                    MessageBox.Show("Email không hợp lệ (bắt buộc phải chứa ký tự @)!", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 3. Chuẩn hóa & Validation Số điện thoại
                string? dienThoaiClean = null;
                if (!string.IsNullOrWhiteSpace(DienThoai))
                {
                    // Tước bỏ mọi ký tự lạ, khoảng trắng, dấu gạch ngang... chỉ giữ lại số và dấu '+'
                    // Regex @"[^\d+]" loại bỏ tất cả những gì không phải chữ số (\d) và không phải dấu cộng (+).
                    dienThoaiClean = System.Text.RegularExpressions.Regex.Replace(DienThoai, @"[^\d+]", "");

                    // Chuyển +84 hoặc 84 ở đầu thành 0
                    if (dienThoaiClean.StartsWith("+84"))
                    {
                        dienThoaiClean = "0" + dienThoaiClean.Substring(3);
                    }
                    else if (dienThoaiClean.StartsWith("84"))
                    {
                        dienThoaiClean = "0" + dienThoaiClean.Substring(2);
                    }

                    // Validation: Bắt buộc có đúng 10 số và bắt đầu bằng số 0
                    if (dienThoaiClean.Length != 10 || !dienThoaiClean.StartsWith("0"))
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ!\n(Yêu cầu: Bắt đầu bằng 0 hoặc +84 và có đúng 10 chữ số sau khi chuẩn hóa).", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // --- Tạo record Xe mới ---
                var xeMoi = new Xe
                {
                    BienSo = bienSoClean,
                    MaHieuXe = SelectedHieuXe!.MaHieuXe,
                    TenChuXe = TenChuXe.Trim(),
                    DiaChi = string.IsNullOrWhiteSpace(DiaChi) ? null : DiaChi.Trim(),
                    DienThoai = dienThoaiClean,
                    Email = string.IsNullOrWhiteSpace(Email) ? null : Email.Trim(),
                    NgayTiepNhan = this.NgayTiepNhan,
                    TienNo = 0
                };

                context.Xes.Add(xeMoi);
                context.SaveChanges(); // Lưu để có MaXe (auto-generated)

                // --- Tự động tạo Phiếu sửa chữa ---
                var phieuSC = new PhieuSuaChua
                {
                    MaXe = xeMoi.MaXe,
                    NgaySuaChua = this.NgayTiepNhan,
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
            NgayTiepNhan = DateTime.Now;
            SelectedHieuXe = null;
        }
    }
}
