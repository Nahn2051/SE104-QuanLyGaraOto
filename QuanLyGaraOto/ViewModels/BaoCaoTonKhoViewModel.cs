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
        public RelayCommand XuatExcelCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public BaoCaoTonKhoViewModel()
        {
            LapBaoCaoCommand = new RelayCommand(LapBaoCao);
            XuatExcelCommand = new RelayCommand(XuatExcel, () => ChiTietBaoCao.Any());
            
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

                var listResult = danhSachVatTu.Select(vatTu =>
                {
                    // Tổng nhập của vật tư này trong tháng
                    int tongNhap = phieuNhaps.Where(p => p.MaVTPT == vatTu.MaVTPT).Sum(p => p.SoLuong);
                    
                    // Tổng xuất của vật tư này trong tháng
                    int tongXuat = phieuXuats.Where(p => p.MaVTPT == vatTu.MaVTPT).Sum(p => p.SoLuong);

                    // Phát sinh = Nhập - Xuất
                    int phatSinh = tongNhap - tongXuat;

                    // Tính Tồn Cuối của tháng được chọn bằng cách đi ngược từ Tồn kho hiện tại:
                    // TonCuoiThang = TonHienTai - (Tổng Nhập Tương Lai) + (Tổng Xuất Tương Lai)
                    DateTime startOfNextMonth = new DateTime(Nam, Thang, 1).AddMonths(1);
                    
                    int nhapTuongLai = context.ChiTietPhieuNhaps
                        .Where(c => c.PhieuNhap != null && c.PhieuNhap.NgayNhap >= startOfNextMonth)
                        .Where(c => c.MaVTPT == vatTu.MaVTPT)
                        .Sum(c => (int?)c.SoLuong) ?? 0;

                    int xuatTuongLai = context.ChiTietPhieuSuaChuas
                        .Where(c => c.PhieuSuaChua != null && c.PhieuSuaChua.NgaySuaChua >= startOfNextMonth)
                        .Where(c => c.MaVTPT == vatTu.MaVTPT)
                        .Sum(c => (int?)c.SoLuong) ?? 0;

                    int tonCuoi = vatTu.SoLuongTon - nhapTuongLai + xuatTuongLai;
                    int tonDau = tonCuoi - phatSinh;

                    return new ChiTietTonKhoRow
                    {
                        TenVatTu = vatTu.TenVTPT,
                        TonDau = tonDau,
                        PhatSinhNhap = tongNhap,
                        PhatSinhXuat = tongXuat,
                        TonCuoi = tonCuoi
                    };
                }).ToList();

                ChiTietBaoCao.Clear();
                foreach (var r in listResult)
                {
                    ChiTietBaoCao.Add(r);
                }
                XuatExcelCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lập báo cáo tồn kho:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void XuatExcel()
        {
            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel($"BaoCaoTonKho_{Thang}_{Nam}", wb =>
            {
                var ws = wb.Worksheets.Add("BaoCaoTonKho");
                ws.Cell(1, 1).Value = $"BÁO CÁO TỒN KHO - THÁNG {Thang}/{Nam}";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 5).Merge();
                ws.Range(1, 1, 1, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                var headers = new[] { "Vật Tư Phụ Tùng", "Tồn Đầu", "Phát Sinh Nhập", "Phát Sinh Xuất", "Tồn Cuối" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(3, i + 1).Value = headers[i];
                    ws.Cell(3, i + 1).Style.Font.Bold = true;
                    ws.Cell(3, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    ws.Cell(3, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                int row = 4;
                foreach (var item in ChiTietBaoCao)
                {
                    ws.Cell(row, 1).Value = item.TenVatTu;
                    ws.Cell(row, 2).Value = item.TonDau;
                    ws.Cell(row, 3).Value = item.PhatSinhNhap;
                    ws.Cell(row, 4).Value = item.PhatSinhXuat;
                    ws.Cell(row, 5).Value = item.TonCuoi;

                    for (int c = 1; c <= 5; c++)
                        ws.Cell(row, c).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    
                    row++;
                }
                ws.Columns().AdjustToContents();
            });
        }
    }

    // =========================================================================
    // Nested Row ViewModel
    // =========================================================================
    public class ChiTietTonKhoRow
    {
        public string TenVatTu { get; set; } = string.Empty;
        public int TonDau { get; set; }
        public int PhatSinhNhap { get; set; }
        public int PhatSinhXuat { get; set; }
        public int TonCuoi { get; set; }
    }
}
