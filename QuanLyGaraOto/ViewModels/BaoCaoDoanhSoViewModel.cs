using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class BaoCaoDoanhSoViewModel : BaseViewModel
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

        private decimal _tongDoanhThu;
        public decimal TongDoanhThu
        {
            get => _tongDoanhThu;
            private set => SetProperty(ref _tongDoanhThu, value);
        }

        private decimal _doanhThuKhac;
        public decimal DoanhThuKhac
        {
            get => _doanhThuKhac;
            private set => SetProperty(ref _doanhThuKhac, value);
        }

        private decimal _tongDoanhThuThucTe;
        public decimal TongDoanhThuThucTe
        {
            get => _tongDoanhThuThucTe;
            private set => SetProperty(ref _tongDoanhThuThucTe, value);
        }

        public ObservableCollection<ChiTietDoanhSoRow> ChiTietBaoCao { get; } = new ObservableCollection<ChiTietDoanhSoRow>();

        private ChiTietDoanhSoRow? _selectedRow;
        public ChiTietDoanhSoRow? SelectedRow
        {
            get => _selectedRow;
            set => SetProperty(ref _selectedRow, value);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<PhieuSuaChua> _danhSachPhieuChiTiet = new ObservableCollection<PhieuSuaChua>();
        public ObservableCollection<PhieuSuaChua> DanhSachPhieuChiTiet
        {
            get => _danhSachPhieuChiTiet;
            set => SetProperty(ref _danhSachPhieuChiTiet, value);
        }

        // =====================================================================
        // Commands
        // =====================================================================

        public RelayCommand LapBaoCaoCommand { get; }
        public RelayCommand XuatExcelCommand { get; }
        public RelayCommand XuatExcelChiTietCommand { get; }
        public RelayCommand XemChiTietCommand { get; }
        public RelayCommand DongChiTietCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public BaoCaoDoanhSoViewModel()
        {
            LapBaoCaoCommand = new RelayCommand(LapBaoCao);
            XuatExcelCommand = new RelayCommand(XuatExcel, () => ChiTietBaoCao.Any());
            XuatExcelChiTietCommand = new RelayCommand(XuatExcelChiTiet, () => DanhSachPhieuChiTiet.Any());
            XemChiTietCommand = new RelayCommand(XemChiTiet, () => SelectedRow != null);
            DongChiTietCommand = new RelayCommand(DongChiTiet);
            
            // Tự động lập báo cáo cho tháng hiện tại khi mở màn hình
            LapBaoCao();
        }

        // =====================================================================
        // Methods
        // =====================================================================

        private void LapBaoCao()
        {
            if (Nam < 2000 || Nam > 2100)
            {
                MessageBox.Show("Năm không hợp lệ! Vui lòng nhập đúng số năm (vd: 2024).", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using var context = new GaraDbContext();

                // 1. Lấy danh sách phiếu sửa chữa trong Tháng/Năm
                // Cần Include Xe và HieuXe để có thông tin gom nhóm
                var dsPhieu = context.PhieuSuaChuas
                                     .Include(p => p.Xe)
                                     .ThenInclude(x => x.HieuXe)
                                     .Where(p => p.NgaySuaChua.Month == Thang && p.NgaySuaChua.Year == Nam)
                                     .ToList();

                // 2. Tính Tổng doanh thu
                TongDoanhThu = dsPhieu.Sum(p => p.TongTien);

                // 3. Tính Doanh Thu Khác (Tiền phạt/dư từ phiếu thu)
                var cacPhieuThu = context.PhieuThuTiens
                                         .Where(p => p.NgayThuTien.Month == Thang && p.NgayThuTien.Year == Nam)
                                         .ToList();
                DoanhThuKhac = cacPhieuThu.Where(p => p.SoTienThu > p.TienNoTruocThu)
                                          .Sum(p => p.SoTienThu - p.TienNoTruocThu);

                // 4. Tổng Doanh Thu Thực Tế
                TongDoanhThuThucTe = TongDoanhThu + DoanhThuKhac;

                // 5. Group By theo Mã Hiệu Xe để tính chi tiết doanh thu cốt lõi
                var thongKeHieuXe = dsPhieu
                    .Where(p => p.Xe?.HieuXe != null)
                    .GroupBy(p => p.Xe!.HieuXe)
                    .Select(g => new ChiTietDoanhSoRow
                    {
                        TenHieuXe = g.Key!.TenHieuXe,
                        SoLuotSua = g.Count(), // Đếm số lượng phiếu sửa chữa của hiệu xe đó
                        ThanhTien = g.Sum(p => p.TongTien), // Tính tổng tiền
                    })
                    .ToList();

                // Tính Tỉ lệ % và đổ vào ObservableCollection
                ChiTietBaoCao.Clear();
                foreach (var row in thongKeHieuXe)
                {
                    row.TiLe = TongDoanhThu > 0 ? (double)(row.ThanhTien / TongDoanhThu * 100) : 0;
                    ChiTietBaoCao.Add(row);
                }
                XuatExcelCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lập báo cáo doanh số:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void XemChiTiet()
        {
            if (SelectedRow == null) return;

            try
            {
                using var context = new GaraDbContext();
                var phieus = context.PhieuSuaChuas
                                    .Include(p => p.Xe)
                                    .ThenInclude(x => x.HieuXe)
                                    .Where(p => p.NgaySuaChua.Month == Thang && 
                                                p.NgaySuaChua.Year == Nam && 
                                                p.Xe!.HieuXe!.TenHieuXe == SelectedRow.TenHieuXe)
                                    .OrderByDescending(p => p.NgaySuaChua)
                                    .ToList();

                DanhSachPhieuChiTiet.Clear();
                foreach (var p in phieus)
                {
                    DanhSachPhieuChiTiet.Add(p);
                }

                IsPopupOpen = true;
                XuatExcelChiTietCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DongChiTiet()
        {
            IsPopupOpen = false;
        }

        private void XuatExcel()
        {
            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel($"BaoCaoDoanhSo_{Thang}_{Nam}", wb =>
            {
                var ws = wb.Worksheets.Add("BaoCaoDoanhSo");
                ws.Cell(1, 1).Value = $"BÁO CÁO DOANH SỐ - THÁNG {Thang}/{Nam}";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 4).Merge();
                ws.Range(1, 1, 1, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws.Cell(3, 1).Value = "Doanh Thu Sửa Chữa:"; ws.Cell(3, 1).Style.Font.Bold = true;
                ws.Cell(3, 2).Value = TongDoanhThu;
                ws.Cell(3, 2).Style.NumberFormat.Format = "#,##0";

                ws.Cell(4, 1).Value = "Doanh Thu Khác (Tiền phạt):"; ws.Cell(4, 1).Style.Font.Bold = true;
                ws.Cell(4, 2).Value = DoanhThuKhac;
                ws.Cell(4, 2).Style.NumberFormat.Format = "#,##0";

                ws.Cell(5, 1).Value = "Tổng Doanh Thu Thực Tế:"; ws.Cell(5, 1).Style.Font.Bold = true;
                ws.Cell(5, 2).Value = TongDoanhThuThucTe;
                ws.Cell(5, 2).Style.NumberFormat.Format = "#,##0";

                var headers = new[] { "Hiệu Xe", "Số Lượt Sửa", "Thành Tiền", "Tỉ Lệ (%)" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(7, i + 1).Value = headers[i];
                    ws.Cell(7, i + 1).Style.Font.Bold = true;
                    ws.Cell(7, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    ws.Cell(7, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                int row = 8;
                foreach (var item in ChiTietBaoCao)
                {
                    ws.Cell(row, 1).Value = item.TenHieuXe;
                    ws.Cell(row, 2).Value = item.SoLuotSua;
                    ws.Cell(row, 3).Value = item.ThanhTien;
                    ws.Cell(row, 3).Style.NumberFormat.Format = "#,##0";
                    ws.Cell(row, 4).Value = item.TiLe;
                    ws.Cell(row, 4).Style.NumberFormat.Format = "0.00";

                    for (int c = 1; c <= 4; c++)
                        ws.Cell(row, c).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    
                    row++;
                }
                ws.Columns().AdjustToContents();
            });
        }

        private void XuatExcelChiTiet()
        {
            if (SelectedRow == null) return;
            string fileName = $"ChiTietDoanhSo_{SelectedRow.TenHieuXe}_{Thang}_{Nam}";
            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel(fileName, wb =>
            {
                var ws = wb.Worksheets.Add("ChiTiet");
                ws.Cell(1, 1).Value = $"CHI TIẾT DOANH SỐ - {SelectedRow.TenHieuXe.ToUpper()} - THÁNG {Thang}/{Nam}";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 5).Merge();
                ws.Range(1, 1, 1, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                var headers = new[] { "Mã Phiếu", "Ngày Sửa Chữa", "Biển Số", "Tổng Tiền" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(3, i + 1).Value = headers[i];
                    ws.Cell(3, i + 1).Style.Font.Bold = true;
                    ws.Cell(3, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    ws.Cell(3, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                int row = 4;
                foreach (var item in DanhSachPhieuChiTiet)
                {
                    ws.Cell(row, 1).Value = item.MaPhieuSuaChua;
                    ws.Cell(row, 2).Value = item.NgaySuaChua.ToString("dd/MM/yyyy");
                    ws.Cell(row, 3).Value = item.Xe?.BienSo;
                    ws.Cell(row, 4).Value = item.TongTien;
                    ws.Cell(row, 4).Style.NumberFormat.Format = "#,##0";

                    for (int c = 1; c <= 4; c++)
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
    public class ChiTietDoanhSoRow
    {
        public string TenHieuXe { get; set; } = string.Empty;
        public int SoLuotSua { get; set; }
        public decimal ThanhTien { get; set; }
        public double TiLe { get; set; }
    }
}
