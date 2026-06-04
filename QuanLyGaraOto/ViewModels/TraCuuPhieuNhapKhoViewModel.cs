using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class TraCuuPhieuNhapKhoViewModel : BaseViewModel
    {
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
            set => SetProperty(ref _tuNgay, value);
        }

        private DateTime? _denNgay;
        public DateTime? DenNgay
        {
            get => _denNgay;
            set => SetProperty(ref _denNgay, value);
        }

        private decimal _tongTienHienThi;
        public decimal TongTienHienThi
        {
            get => _tongTienHienThi;
            set => SetProperty(ref _tongTienHienThi, value);
        }

        private ObservableCollection<PhieuNhap> _danhSachPhieu = new ObservableCollection<PhieuNhap>();
        public ObservableCollection<PhieuNhap> DanhSachPhieu
        {
            get => _danhSachPhieu;
            set => SetProperty(ref _danhSachPhieu, value);
        }

        private PhieuNhap? _selectedPhieu;
        public PhieuNhap? SelectedPhieu
        {
            get => _selectedPhieu;
            set
            {
                SetProperty(ref _selectedPhieu, value);
                ChiTietPhieu.Clear();
                if (_selectedPhieu != null && _selectedPhieu.DanhSachCTPhieuNhap != null)
                {
                    foreach (var ct in _selectedPhieu.DanhSachCTPhieuNhap)
                    {
                        ChiTietPhieu.Add(ct);
                    }
                }
            }
        }

        private ObservableCollection<ChiTietPhieuNhap> _chiTietPhieu = new ObservableCollection<ChiTietPhieuNhap>();
        public ObservableCollection<ChiTietPhieuNhap> ChiTietPhieu
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
        public RelayCommand BoLocNgayCommand { get; }
        public RelayCommand XemChiTietCommand { get; }
        public RelayCommand DongChiTietCommand { get; }
        public RelayCommand XuatExcelCommand { get; }
        public RelayCommand XuatExcelChiTietCommand { get; }
        public RelayCommand HuyPhieuCommand { get; }

        public TraCuuPhieuNhapKhoViewModel()
        {
            TimKiemCommand = new RelayCommand(TimKiem);
            BoLocNgayCommand = new RelayCommand(BoLocNgay);
            XemChiTietCommand = new RelayCommand(XemChiTiet, () => SelectedPhieu != null);
            DongChiTietCommand = new RelayCommand(DongChiTiet);
            XuatExcelCommand = new RelayCommand(XuatExcel, () => DanhSachPhieu.Any());
            XuatExcelChiTietCommand = new RelayCommand(XuatExcelChiTiet, () => SelectedPhieu != null && ChiTietPhieu.Any());
            HuyPhieuCommand = new RelayCommand(HuyPhieu, () => SelectedPhieu != null);

            TimKiem();
        }

        private void BoLocNgay()
        {
            TuNgay = null;
            DenNgay = null;
            TimKiem();
        }

        private void TimKiem()
        {
            try
            {
                using var context = new GaraDbContext();

                var query = context.PhieuNhaps
                                   .Include(p => p.DanhSachCTPhieuNhap)
                                       .ThenInclude(ct => ct.VatTuPhuTung)
                                   .AsQueryable();

                if (TuNgay.HasValue && DenNgay.HasValue && TuNgay.Value.Date > DenNgay.Value.Date)
                {
                    MessageBox.Show("Từ ngày không được lớn hơn Đến ngày!", "Lỗi ngày tháng", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(TuKhoa))
                {
                    var keyword = TuKhoa.Trim().ToLower();
                    query = query.Where(p => 
                                p.MaPhieuNhap.ToString() == keyword
                            );
                }

                if (TuNgay.HasValue)
                {
                    query = query.Where(p => p.NgayNhap.Date >= TuNgay.Value.Date);
                }

                if (DenNgay.HasValue)
                {
                    query = query.Where(p => p.NgayNhap.Date <= DenNgay.Value.Date);
                }

                var ketQua = query.OrderByDescending(p => p.NgayNhap).ToList();

                DanhSachPhieu.Clear();
                foreach (var p in ketQua)
                {
                    DanhSachPhieu.Add(p);
                }

                TongTienHienThi = ketQua.Sum(p => p.TongTien);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tra cứu phiếu nhập kho:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void XemChiTiet()
        {
            if (SelectedPhieu != null)
            {
                IsPopupOpen = true;
            }
        }

        private void HuyPhieu()
        {
            if (SelectedPhieu == null) return;

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn hủy phiếu nhập kho #{SelectedPhieu.MaPhieuNhap} không?\nSố lượng vật tư trong phiếu sẽ bị trừ khỏi kho.", "Xác nhận hủy phiếu", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            
            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new GaraDbContext();
                    var p = context.PhieuNhaps.Include(x => x.DanhSachCTPhieuNhap).ThenInclude(x => x.VatTuPhuTung).FirstOrDefault(x => x.MaPhieuNhap == SelectedPhieu.MaPhieuNhap);
                    
                    if (p != null)
                    {
                        // 1. Kiểm tra an toàn: Tồn kho có đủ để trừ hay không?
                        foreach(var ct in p.DanhSachCTPhieuNhap)
                        {
                            if (ct.VatTuPhuTung != null && ct.VatTuPhuTung.SoLuongTon < ct.SoLuong)
                            {
                                MessageBox.Show($"Không thể hủy phiếu nhập này vì vật tư '{ct.VatTuPhuTung.TenVTPT}' đã được xuất sử dụng.\n(Tồn kho hiện tại: {ct.VatTuPhuTung.SoLuongTon}, Cần trừ: {ct.SoLuong})", "Lỗi an toàn dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }

                        // 2. Trừ tồn kho
                        foreach(var ct in p.DanhSachCTPhieuNhap)
                        {
                            if (ct.VatTuPhuTung != null)
                            {
                                ct.VatTuPhuTung.SoLuongTon -= ct.SoLuong;
                            }
                        }

                        context.PhieuNhaps.Remove(p);
                        context.SaveChanges();
                        
                        MessageBox.Show("Hủy phiếu nhập kho thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        IsPopupOpen = false;
                        TimKiem();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi hủy phiếu nhập kho:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DongChiTiet()
        {
            IsPopupOpen = false;
        }

        private void XuatExcel()
        {
            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel("TraCuuPhieuNhapKho", wb =>
            {
                var ws = wb.Worksheets.Add("PhieuNhapKho");
                ws.Cell(1, 1).Value = "BÁO CÁO TRA CỨU PHIẾU NHẬP KHO";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 3).Merge();
                ws.Range(1, 1, 1, 3).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                var headers = new[] { "Mã Phiếu", "Ngày Nhập", "Tổng Tiền" };
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
                    ws.Cell(row, 1).Value = p.MaPhieuNhap;
                    ws.Cell(row, 2).Value = p.NgayNhap.ToString("dd/MM/yyyy HH:mm");
                    ws.Cell(row, 3).Value = p.TongTien;
                    ws.Cell(row, 3).Style.NumberFormat.Format = "#,##0";

                    for (int c = 1; c <= 3; c++)
                        ws.Cell(row, c).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    
                    row++;
                }
                ws.Columns().AdjustToContents();
            });
        }

        private void XuatExcelChiTiet()
        {
            if (SelectedPhieu == null) return;

            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel($"ChiTietPhieuNhap_{SelectedPhieu.MaPhieuNhap}", wb =>
            {
                var ws = wb.Worksheets.Add("ChiTietPhieuNhap");
                ws.Cell(1, 1).Value = $"CHI TIẾT PHIẾU NHẬP KHO #{SelectedPhieu.MaPhieuNhap}";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 5).Merge();
                ws.Range(1, 1, 1, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws.Cell(3, 1).Value = "Ngày nhập:"; ws.Cell(3, 1).Style.Font.Bold = true;
                ws.Cell(3, 2).Value = SelectedPhieu.NgayNhap.ToString("dd/MM/yyyy HH:mm");

                ws.Cell(4, 1).Value = "Tổng tiền:"; ws.Cell(4, 1).Style.Font.Bold = true;
                ws.Cell(4, 2).Value = SelectedPhieu.TongTien;
                ws.Cell(4, 2).Style.NumberFormat.Format = "#,##0";

                var headers = new[] { "Vật Tư Phụ Tùng", "Số Lượng", "Đơn Giá Nhập", "Thành Tiền" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(6, i + 1).Value = headers[i];
                    ws.Cell(6, i + 1).Style.Font.Bold = true;
                    ws.Cell(6, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    ws.Cell(6, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                int row = 7;
                foreach (var ct in ChiTietPhieu)
                {
                    ws.Cell(row, 1).Value = ct.VatTuPhuTung?.TenVTPT ?? "";
                    ws.Cell(row, 2).Value = ct.SoLuong;
                    ws.Cell(row, 3).Value = ct.DonGia;
                    ws.Cell(row, 3).Style.NumberFormat.Format = "#,##0";
                    ws.Cell(row, 4).Value = ct.ThanhTien;
                    ws.Cell(row, 4).Style.NumberFormat.Format = "#,##0";

                    for (int c = 1; c <= 4; c++)
                        ws.Cell(row, c).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    
                    row++;
                }

                ws.Columns().AdjustToContents();
            });
        }
    }
}
