using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class BaoCaoTonKhoViewModel : BaseViewModel
    {
        // =====================================================================
        // Properties
        // =====================================================================

        public ObservableCollection<int> DanhSachThang { get; } = new ObservableCollection<int>(Enumerable.Range(1, 12));

        private int _thang = DateTime.Now.Month;
        public int Thang
        {
            get => _thang;
            set => SetProperty(ref _thang, value);
        }

        private int _nam = DateTime.Now.Year;
        public int Nam
        {
            get => _nam;
            set => SetProperty(ref _nam, value);
        }

        public ObservableCollection<ChiTietTonKhoRow> ChiTietBaoCao { get; } = new ObservableCollection<ChiTietTonKhoRow>();

        // =====================================================================
        // Commands
        // =====================================================================

        public RelayCommand LapBaoCaoCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public BaoCaoTonKhoViewModel()
        {
            LapBaoCaoCommand = new RelayCommand(LapBaoCao);
            
            // Note: Không tự động chạy vì có validate tháng chốt sổ
            // Nhưng để UX tốt, ta cứ lùi lại 1 tháng so với hiện tại để làm mặc định
            var lastMonth = DateTime.Now.AddMonths(-1);
            Thang = lastMonth.Month;
            Nam = lastMonth.Year;
        }

        // =====================================================================
        // Methods
        // =====================================================================

        private void LapBaoCao()
        {
            // Kiểm tra: Tháng/Năm lập báo cáo phải nhỏ hơn Tháng/Năm hiện tại (chưa chốt sổ)
            DateTime selectedDate = new DateTime(Nam, Thang, 1);
            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (selectedDate >= currentDate)
            {
                MessageBox.Show("Tháng hiện tại chưa kết thúc (chưa chốt sổ)!\nVui lòng chọn tháng trước đó để lập báo cáo tồn.", 
                                "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using var context = new GaraDbContext();

                // 1. Lấy tất cả chi tiết nhập kho trong tháng
                var phieuNhaps = context.ChiTietPhieuNhaps
                                        .Include(c => c.PhieuNhap)
                                        .Where(c => c.PhieuNhap != null 
                                                 && c.PhieuNhap.NgayNhap.Month == Thang 
                                                 && c.PhieuNhap.NgayNhap.Year == Nam)
                                        .ToList();

                // 2. Lấy tất cả chi tiết sửa chữa (xuất kho) trong tháng
                var phieuXuats = context.ChiTietPhieuSuaChuas
                                        .Include(c => c.PhieuSuaChua)
                                        .Where(c => c.PhieuSuaChua != null 
                                                 && c.MaVTPT != null // Bỏ qua công thợ, chỉ tính vật tư
                                                 && c.PhieuSuaChua.NgaySuaChua.Month == Thang 
                                                 && c.PhieuSuaChua.NgaySuaChua.Year == Nam)
                                        .ToList();

                // Lấy toàn bộ Vật tư để thống kê
                var danhSachVatTu = context.VatTuPhuTungs.ToList();

                ChiTietBaoCao.Clear();

                foreach (var vatTu in danhSachVatTu)
                {
                    // Tổng nhập của vật tư này
                    int tongNhap = phieuNhaps.Where(p => p.MaVTPT == vatTu.MaVTPT).Sum(p => p.SoLuong);
                    
                    // Tổng xuất của vật tư này (giả định 1 dòng CT_PhieuSuaChua dùng 1 vật tư nếu không có cột SoLuong)
                    // Note: Entity CT_PHIEUSUACHUA trong DB thiết kế hiện tại không có cột SoLuong (thường là 1)
                    // Nếu sau này thêm cột SoLuong, đổi lại thành .Sum(p => p.SoLuong)
                    int tongXuat = phieuXuats.Where(p => p.MaVTPT == vatTu.MaVTPT).Count();

                    // Phát sinh = Nhập - Xuất
                    int phatSinh = tongNhap - tongXuat;

                    // Giả định đơn giản cho ViewModel: Lấy số lượng tồn kho hiện tại làm Tồn Cuối
                    // Và suy ngược ra Tồn Đầu = Tồn Cuối - Phát Sinh
                    int tonCuoi = vatTu.SoLuongTon;
                    int tonDau = tonCuoi - phatSinh;

                    // Đưa vào danh sách
                    ChiTietBaoCao.Add(new ChiTietTonKhoRow
                    {
                        TenVatTu = vatTu.TenVTPT,
                        TonDau = tonDau,
                        PhatSinh = phatSinh,
                        TonCuoi = tonCuoi
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lập báo cáo tồn kho:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    // =========================================================================
    // Nested Row ViewModel
    // =========================================================================
    public class ChiTietTonKhoRow
    {
        public string TenVatTu { get; set; } = string.Empty;
        public int TonDau { get; set; }
        public int PhatSinh { get; set; }
        public int TonCuoi { get; set; }
    }
}
