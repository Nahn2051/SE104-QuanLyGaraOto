using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class TraCuuXeViewModel : BaseViewModel
    {
        // =====================================================================
        // Properties
        // =====================================================================

        private string _tuKhoa = string.Empty;
        public string TuKhoa
        {
            get => _tuKhoa;
            set => SetProperty(ref _tuKhoa, value);
        }

        private ObservableCollection<Xe> _danhSachXe = new ObservableCollection<Xe>();
        public ObservableCollection<Xe> DanhSachXe
        {
            get => _danhSachXe;
            set => SetProperty(ref _danhSachXe, value);
        }

        private Xe? _selectedXe;
        public Xe? SelectedXe
        {
            get => _selectedXe;
            set
            {
                SetProperty(ref _selectedXe, value);
                LichSuSuaChua.Clear();
                if (_selectedXe != null)
                {
                    LoadLichSuSuaChua(_selectedXe.MaXe);
                }
            }
        }

        private ObservableCollection<PhieuSuaChua> _lichSuSuaChua = new ObservableCollection<PhieuSuaChua>();
        public ObservableCollection<PhieuSuaChua> LichSuSuaChua
        {
            get => _lichSuSuaChua;
            set => SetProperty(ref _lichSuSuaChua, value);
        }

        private bool _isPopupOpen = false;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        // =====================================================================
        // Commands
        // =====================================================================

        public RelayCommand TimKiemCommand { get; }
        public RelayCommand XuatExcelCommand { get; }
        public RelayCommand XuatExcelChiTietCommand { get; }
        public RelayCommand XemChiTietCommand { get; }
        public RelayCommand DongChiTietCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public TraCuuXeViewModel()
        {
            TimKiemCommand = new RelayCommand(TimKiem);
            XuatExcelCommand = new RelayCommand(XuatExcel, () => DanhSachXe.Any());
            XuatExcelChiTietCommand = new RelayCommand(XuatExcelChiTiet, () => SelectedXe != null);
            XemChiTietCommand = new RelayCommand(XemChiTiet, () => SelectedXe != null);
            DongChiTietCommand = new RelayCommand(DongChiTiet);

            // Tải danh sách toàn bộ xe khi mới mở màn hình
            TimKiem();
        }

        // =====================================================================
        // Methods
        // =====================================================================

        private void TimKiem()
        {
            try
            {
                using var context = new GaraDbContext();

                // Lấy Queryable cơ bản
                var query = context.Xes
                                   .Include(x => x.HieuXe)
                                   .AsQueryable();

                // Nếu có từ khóa thì thêm điều kiện lọc (không phân biệt chữ hoa/thường)
                if (!string.IsNullOrWhiteSpace(TuKhoa))
                {
                    var keyword = TuKhoa.Trim().ToLower();
                    query = query.Where(x => 
                                x.BienSo.ToLower().Contains(keyword) ||
                                x.TenChuXe.ToLower().Contains(keyword) ||
                                (x.HieuXe != null && x.HieuXe.TenHieuXe.ToLower().Contains(keyword))
                            );
                }

                // Thực thi truy vấn
                var ketQua = query.ToList();

                DanhSachXe.Clear();
                foreach (var xe in ketQua)
                {
                    DanhSachXe.Add(xe);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tra cứu xe:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void XemChiTiet()
        {
            if (SelectedXe != null)
            {
                IsPopupOpen = true;
            }
        }

        private void DongChiTiet()
        {
            IsPopupOpen = false;
        }

        private void LoadLichSuSuaChua(int maXe)
        {
            try
            {
                using var context = new GaraDbContext();
                var phieus = context.PhieuSuaChuas
                                    .Include(p => p.DanhSachCTPhieuSuaChua)
                                        .ThenInclude(ct => ct.VatTuPhuTung)
                                    .Include(p => p.DanhSachCTPhieuSuaChua)
                                        .ThenInclude(ct => ct.TienCong)
                                    .Where(p => p.MaXe == maXe)
                                    .OrderByDescending(p => p.NgaySuaChua)
                                    .ToList();

                foreach (var p in phieus)
                {
                    LichSuSuaChua.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải lịch sử:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void XuatExcel()
        {
            var mapping = new System.Collections.Generic.Dictionary<string, string>
            {
                { "BienSo", "Biển số" },
                { "TenChuXe", "Tên chủ xe" },
                { "DienThoai", "Điện thoại" },
                { "Email", "Email" },
                { "DiaChi", "Địa chỉ" },
                { "NgayTiepNhan", "Ngày tiếp nhận" },
                { "TienNo", "Tiền nợ" }
            };

            QuanLyGaraOto.Services.ExcelExportService.ExportListToExcel(DanhSachXe, "TraCuuXe", "Báo cáo Tra Cứu Xe", mapping);
        }

        private void XuatExcelChiTiet()
        {
            if (SelectedXe == null) return;

            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel("ChiTietXe", wb =>
            {
                var ws = wb.Worksheets.Add("ChiTietXe");
                ws.Cell(1, 1).Value = "THÔNG TIN CHI TIẾT XE";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 6).Merge();
                ws.Range(1, 1, 1, 6).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                // Thêm thông tin xe
                ws.Cell(3, 1).Value = "Biển số:"; ws.Cell(3, 1).Style.Font.Bold = true;
                ws.Cell(3, 2).Value = SelectedXe.BienSo;
                
                ws.Cell(4, 1).Value = "Chủ xe:"; ws.Cell(4, 1).Style.Font.Bold = true;
                ws.Cell(4, 2).Value = SelectedXe.TenChuXe;

                ws.Cell(5, 1).Value = "Điện thoại:"; ws.Cell(5, 1).Style.Font.Bold = true;
                ws.Cell(5, 2).Value = SelectedXe.DienThoai;

                ws.Cell(6, 1).Value = "Email:"; ws.Cell(6, 1).Style.Font.Bold = true;
                ws.Cell(6, 2).Value = SelectedXe.Email;

                ws.Cell(7, 1).Value = "Địa chỉ:"; ws.Cell(7, 1).Style.Font.Bold = true;
                ws.Cell(7, 2).Value = SelectedXe.DiaChi;

                ws.Cell(8, 1).Value = "Ngày tiếp nhận:"; ws.Cell(8, 1).Style.Font.Bold = true;
                ws.Cell(8, 2).Value = SelectedXe.NgayTiepNhan?.ToString("dd/MM/yyyy HH:mm");

                ws.Cell(9, 1).Value = "Tiền nợ:"; ws.Cell(9, 1).Style.Font.Bold = true;
                ws.Cell(9, 2).Value = SelectedXe.TienNo;
                ws.Cell(9, 2).Style.NumberFormat.Format = "#,##0";

                // Bảng Lịch sử sửa chữa
                ws.Cell(11, 1).Value = "LỊCH SỬ SỬA CHỮA";
                ws.Cell(11, 1).Style.Font.Bold = true;
                ws.Cell(11, 1).Style.Font.FontSize = 14;

                var headers = new[] { "Mã Phiếu", "Ngày Sửa Chữa", "Tổng Tiền" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(13, i + 1).Value = headers[i];
                    ws.Cell(13, i + 1).Style.Font.Bold = true;
                    ws.Cell(13, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    ws.Cell(13, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                int row = 14;
                foreach (var p in LichSuSuaChua)
                {
                    ws.Cell(row, 1).Value = p.MaPhieuSuaChua;
                    ws.Cell(row, 2).Value = p.NgaySuaChua.ToString("dd/MM/yyyy HH:mm");
                    ws.Cell(row, 3).Value = p.TongTien;
                    ws.Cell(row, 3).Style.NumberFormat.Format = "#,##0";

                    ws.Cell(row, 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    ws.Cell(row, 2).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    ws.Cell(row, 3).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    row++;
                }

                ws.Columns().AdjustToContents();
            });
        }
    }
}
