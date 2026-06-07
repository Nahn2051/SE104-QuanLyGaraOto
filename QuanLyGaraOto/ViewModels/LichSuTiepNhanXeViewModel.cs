using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class LichSuTiepNhanXeViewModel : BaseViewModel
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

        private DateTime? _tuNgay;
        public DateTime? TuNgay
        {
            get => _tuNgay;
            set
            {
                SetProperty(ref _tuNgay, value);
                TimKiem();
            }
        }

        private DateTime? _denNgay;
        public DateTime? DenNgay
        {
            get => _denNgay;
            set
            {
                SetProperty(ref _denNgay, value);
                TimKiem();
            }
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
        public RelayCommand HuyXeCommand { get; }
        public RelayCommand BoLocNgayCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public LichSuTiepNhanXeViewModel()
        {
            TimKiemCommand = new RelayCommand(TimKiem);
            XuatExcelCommand = new RelayCommand(XuatExcel, () => DanhSachXe.Any());
            XuatExcelChiTietCommand = new RelayCommand(XuatExcelChiTiet, () => SelectedXe != null);
            XemChiTietCommand = new RelayCommand(XemChiTiet, () => SelectedXe != null);
            DongChiTietCommand = new RelayCommand(DongChiTiet);
            HuyXeCommand = new RelayCommand(HuyXe, () => SelectedXe != null);
            BoLocNgayCommand = new RelayCommand(BoLocNgay);

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

                // Kiểm tra ràng buộc ngày
                if (TuNgay.HasValue && DenNgay.HasValue && TuNgay.Value.Date > DenNgay.Value.Date)
                {
                    MessageBox.Show("Từ ngày không được lớn hơn Đến ngày!", "Lỗi ngày tháng", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Nếu có từ khóa thì thêm điều kiện lọc (không phân biệt chữ hoa/thường)
                if (!string.IsNullOrWhiteSpace(TuKhoa))
                {
                    var keyword = TuKhoa.Trim().ToLower();
                    // Loại bỏ các ký tự đặc biệt khỏi từ khóa để tìm kiếm biển số cho chính xác (ví dụ khách gõ 50F-123.45 thì sẽ thành 50f12345)
                    var keywordBienSo = keyword.Replace("-", "").Replace(".", "").Replace(" ", "");

                    query = query.Where(x => 
                                x.BienSo.ToLower().Contains(keywordBienSo) ||
                                x.TenChuXe.ToLower().Contains(keyword) ||
                                (x.HieuXe != null && x.HieuXe.TenHieuXe.ToLower().Contains(keyword))
                            );
                }

                // Lọc theo ngày tiếp nhận
                if (TuNgay.HasValue)
                {
                    query = query.Where(x => x.NgayTiepNhan != null && x.NgayTiepNhan.Value.Date >= TuNgay.Value.Date);
                }
                
                if (DenNgay.HasValue)
                {
                    query = query.Where(x => x.NgayTiepNhan != null && x.NgayTiepNhan.Value.Date <= DenNgay.Value.Date);
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

        private void HuyXe()
        {
            if (SelectedXe == null) return;
            
            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa tiếp nhận xe {SelectedXe.BienSo} không?", "Xác nhận xóa xe", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            
            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new GaraDbContext();
                    var xe = context.Xes.Include(x => x.DanhSachPhieuSuaChua).Include(x => x.DanhSachPhieuThuTien).FirstOrDefault(x => x.MaXe == SelectedXe.MaXe);
                    
                    if (xe != null)
                    {
                        if (xe.DanhSachPhieuSuaChua.Any() || xe.DanhSachPhieuThuTien.Any())
                        {
                            MessageBox.Show("Không thể xóa xe này vì đã phát sinh phiếu sửa chữa hoặc phiếu thu tiền.\nVui lòng hủy các phiếu đó trước khi xóa xe.", "Lỗi an toàn dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        context.Xes.Remove(xe);
                        context.SaveChanges();
                        
                        MessageBox.Show("Xóa tiếp nhận xe thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        IsPopupOpen = false;
                        TimKiem();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa xe:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DongChiTiet()
        {
            IsPopupOpen = false;
        }

        private void BoLocNgay()
        {
            _tuNgay = null;
            OnPropertyChanged(nameof(TuNgay));
            _denNgay = null;
            OnPropertyChanged(nameof(DenNgay));
            TimKiem();
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
            if (!DanhSachXe.Any()) return;

            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel("LichSuTiepNhanXe", wb =>
            {
                var ws = wb.Worksheets.Add("LichSuTiepNhanXe");
                ws.Cell(1, 1).Value = "BÁO CÁO LỊCH SỬ TIẾP NHẬN XE";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 9).Merge();
                ws.Range(1, 1, 1, 9).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                var headers = new[] { "STT", "Biển số", "Hiệu xe", "Tên chủ xe", "Điện thoại", "Địa chỉ", "Email", "Ngày tiếp nhận", "Tiền nợ" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(3, i + 1).Value = headers[i];
                    ws.Cell(3, i + 1).Style.Font.Bold = true;
                    ws.Cell(3, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    ws.Cell(3, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                int row = 4;
                int stt = 1;
                foreach (var x in DanhSachXe)
                {
                    ws.Cell(row, 1).Value = stt++;
                    ws.Cell(row, 2).Value = x.BienSo;
                    ws.Cell(row, 3).Value = x.HieuXe?.TenHieuXe;
                    ws.Cell(row, 4).Value = x.TenChuXe;
                    ws.Cell(row, 5).Value = x.DienThoai;
                    ws.Cell(row, 6).Value = x.DiaChi;
                    ws.Cell(row, 7).Value = x.Email;
                    if (x.NgayTiepNhan.HasValue)
                    {
                        ws.Cell(row, 8).Value = x.NgayTiepNhan.Value;
                        ws.Cell(row, 8).Style.NumberFormat.Format = "dd/MM/yyyy";
                    }
                    ws.Cell(row, 9).Value = x.TienNo;
                    ws.Cell(row, 9).Style.NumberFormat.Format = "#,##0";

                    for (int i = 1; i <= 9; i++)
                    {
                        ws.Cell(row, i).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    }
                    row++;
                }

                ws.Columns().AdjustToContents();
            });
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
