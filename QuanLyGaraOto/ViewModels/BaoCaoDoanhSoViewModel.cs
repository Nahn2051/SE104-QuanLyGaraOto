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

        public ObservableCollection<ChiTietDoanhSoRow> ChiTietBaoCao { get; } = new ObservableCollection<ChiTietDoanhSoRow>();

        // =====================================================================
        // Commands
        // =====================================================================

        public RelayCommand LapBaoCaoCommand { get; }
        public RelayCommand XuatExcelCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public BaoCaoDoanhSoViewModel()
        {
            LapBaoCaoCommand = new RelayCommand(LapBaoCao);
            XuatExcelCommand = new RelayCommand(XuatExcel, () => ChiTietBaoCao.Any());
            
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

                // 3. Group By theo Mã Hiệu Xe để tính chi tiết
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

                ws.Cell(3, 1).Value = "Tổng Doanh Thu:"; ws.Cell(3, 1).Style.Font.Bold = true;
                ws.Cell(3, 2).Value = TongDoanhThu;
                ws.Cell(3, 2).Style.NumberFormat.Format = "#,##0";

                var headers = new[] { "Hiệu Xe", "Số Lượt Sửa", "Thành Tiền", "Tỉ Lệ (%)" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(5, i + 1).Value = headers[i];
                    ws.Cell(5, i + 1).Style.Font.Bold = true;
                    ws.Cell(5, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    ws.Cell(5, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                int row = 6;
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
