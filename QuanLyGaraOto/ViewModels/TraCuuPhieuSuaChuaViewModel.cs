using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class TraCuuPhieuSuaChuaViewModel : BaseViewModel
    {
        private string _tuKhoa = string.Empty;
        public string TuKhoa
        {
            get => _tuKhoa;
            set => SetProperty(ref _tuKhoa, value);
        }

        private ObservableCollection<PhieuSuaChua> _danhSachPhieu = new ObservableCollection<PhieuSuaChua>();
        public ObservableCollection<PhieuSuaChua> DanhSachPhieu
        {
            get => _danhSachPhieu;
            set => SetProperty(ref _danhSachPhieu, value);
        }

        private PhieuSuaChua? _selectedPhieu;
        public PhieuSuaChua? SelectedPhieu
        {
            get => _selectedPhieu;
            set
            {
                SetProperty(ref _selectedPhieu, value);
                ChiTietPhieu.Clear();
                if (_selectedPhieu != null && _selectedPhieu.DanhSachCTPhieuSuaChua != null)
                {
                    foreach (var ct in _selectedPhieu.DanhSachCTPhieuSuaChua)
                    {
                        ChiTietPhieu.Add(ct);
                    }
                }
            }
        }

        private ObservableCollection<ChiTietPhieuSuaChua> _chiTietPhieu = new ObservableCollection<ChiTietPhieuSuaChua>();
        public ObservableCollection<ChiTietPhieuSuaChua> ChiTietPhieu
        {
            get => _chiTietPhieu;
            set => SetProperty(ref _chiTietPhieu, value);
        }

        private bool _isPopupOpen = false;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        public RelayCommand TimKiemCommand { get; }
        public RelayCommand XemChiTietCommand { get; }
        public RelayCommand DongChiTietCommand { get; }
        public RelayCommand XuatExcelCommand { get; }
        public RelayCommand XuatExcelChiTietCommand { get; }

        private decimal _soTienDaTra;
        public decimal SoTienDaTra
        {
            get => _soTienDaTra;
            set => SetProperty(ref _soTienDaTra, value);
        }

        private decimal _soTienNoConLai;
        public decimal SoTienNoConLai
        {
            get => _soTienNoConLai;
            set => SetProperty(ref _soTienNoConLai, value);
        }

        public TraCuuPhieuSuaChuaViewModel()
        {
            TimKiemCommand = new RelayCommand(TimKiem);
            XemChiTietCommand = new RelayCommand(XemChiTiet, () => SelectedPhieu != null);
            DongChiTietCommand = new RelayCommand(DongChiTiet);
            XuatExcelCommand = new RelayCommand(XuatExcel, () => DanhSachPhieu.Any());
            XuatExcelChiTietCommand = new RelayCommand(XuatExcelChiTiet, () => SelectedPhieu != null && ChiTietPhieu.Any());

            TimKiem();
        }

        private void TimKiem()
        {
            try
            {
                using var context = new GaraDbContext();

                var query = context.PhieuSuaChuas
                                   .Include(p => p.Xe)
                                   .Include(p => p.DanhSachCTPhieuSuaChua)
                                       .ThenInclude(ct => ct.VatTuPhuTung)
                                   .Include(p => p.DanhSachCTPhieuSuaChua)
                                       .ThenInclude(ct => ct.TienCong)
                                   .AsQueryable();

                if (!string.IsNullOrWhiteSpace(TuKhoa))
                {
                    var keyword = TuKhoa.Trim().ToLower();
                    query = query.Where(p => 
                                p.MaPhieuSuaChua.ToString() == keyword ||
                                (p.Xe != null && p.Xe.BienSo.ToLower().Contains(keyword))
                            );
                }

                var ketQua = query.OrderByDescending(p => p.NgaySuaChua).ToList();

                DanhSachPhieu.Clear();
                foreach (var p in ketQua)
                {
                    DanhSachPhieu.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tra cứu phiếu sửa chữa:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void XemChiTiet()
        {
            if (SelectedPhieu != null)
            {
                // Tính toán tiền đã trả (dựa trên phiếu thu tiền được tạo cùng lúc)
                try
                {
                    using var context = new GaraDbContext();
                    var phieuThu = context.PhieuThuTiens.FirstOrDefault(pt => pt.MaXe == SelectedPhieu.MaXe && pt.NgayThuTien == SelectedPhieu.NgaySuaChua);
                    if (phieuThu != null)
                    {
                        SoTienDaTra = phieuThu.SoTienThu;
                    }
                    else
                    {
                        SoTienDaTra = 0;
                    }

                    SoTienNoConLai = SelectedPhieu.TongTien - SoTienDaTra;
                }
                catch
                {
                    SoTienDaTra = 0;
                    SoTienNoConLai = SelectedPhieu.TongTien;
                }

                IsPopupOpen = true;
            }
        }

        private void DongChiTiet()
        {
            IsPopupOpen = false;
        }

        private void XuatExcel()
        {
            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel("TraCuuPhieuSuaChua", wb =>
            {
                var ws = wb.Worksheets.Add("PhieuSuaChua");
                ws.Cell(1, 1).Value = "BÁO CÁO TRA CỨU PHIẾU SỬA CHỮA";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 5).Merge();
                ws.Range(1, 1, 1, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                var headers = new[] { "Mã Phiếu", "Biển Số Xe", "Ngày Sửa Chữa", "Tổng Tiền" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(3, i + 1).Value = headers[i];
                    ws.Cell(3, i + 1).Style.Font.Bold = true;
                    ws.Cell(3, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    ws.Cell(3, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                int row = 4;
                foreach (var p in DanhSachPhieu)
                {
                    ws.Cell(row, 1).Value = p.MaPhieuSuaChua;
                    ws.Cell(row, 2).Value = p.Xe?.BienSo;
                    ws.Cell(row, 3).Value = p.NgaySuaChua.ToString("dd/MM/yyyy HH:mm");
                    ws.Cell(row, 4).Value = p.TongTien;
                    ws.Cell(row, 4).Style.NumberFormat.Format = "#,##0";

                    for (int c = 1; c <= 4; c++)
                        ws.Cell(row, c).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    
                    row++;
                }
                ws.Columns().AdjustToContents();
            });
        }

        private void XuatExcelChiTiet()
        {
            if (SelectedPhieu == null) return;

            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel($"ChiTietPhieu_{SelectedPhieu.MaPhieuSuaChua}", wb =>
            {
                var ws = wb.Worksheets.Add("ChiTietPhieu");
                ws.Cell(1, 1).Value = $"CHI TIẾT PHIẾU SỬA CHỮA #{SelectedPhieu.MaPhieuSuaChua}";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 6).Merge();
                ws.Range(1, 1, 1, 6).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws.Cell(3, 1).Value = "Biển số xe:"; ws.Cell(3, 1).Style.Font.Bold = true;
                ws.Cell(3, 2).Value = SelectedPhieu.Xe?.BienSo;

                ws.Cell(4, 1).Value = "Ngày sửa chữa:"; ws.Cell(4, 1).Style.Font.Bold = true;
                ws.Cell(4, 2).Value = SelectedPhieu.NgaySuaChua.ToString("dd/MM/yyyy HH:mm");

                ws.Cell(5, 1).Value = "Tổng tiền:"; ws.Cell(5, 1).Style.Font.Bold = true;
                ws.Cell(5, 2).Value = SelectedPhieu.TongTien;
                ws.Cell(5, 2).Style.NumberFormat.Format = "#,##0";

                var headers = new[] { "Nội Dung", "Vật Tư Phụ Tùng", "Số Lượng", "Đơn Giá", "Tiền Công", "Thành Tiền" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(7, i + 1).Value = headers[i];
                    ws.Cell(7, i + 1).Style.Font.Bold = true;
                    ws.Cell(7, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    ws.Cell(7, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                int row = 8;
                foreach (var ct in ChiTietPhieu)
                {
                    ws.Cell(row, 1).Value = ct.NoiDungSuaChua;
                    ws.Cell(row, 2).Value = ct.VatTuPhuTung?.TenVTPT ?? "";
                    ws.Cell(row, 3).Value = ct.SoLuong;
                    ws.Cell(row, 4).Value = ct.DonGia;
                    ws.Cell(row, 4).Style.NumberFormat.Format = "#,##0";
                    ws.Cell(row, 5).Value = ct.TienCong?.TenTienCong ?? "";
                    ws.Cell(row, 6).Value = ct.ThanhTien;
                    ws.Cell(row, 6).Style.NumberFormat.Format = "#,##0";

                    for (int c = 1; c <= 6; c++)
                        ws.Cell(row, c).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    
                    row++;
                }

                ws.Columns().AdjustToContents();
            });
        }
    }
}
