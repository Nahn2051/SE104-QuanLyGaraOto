using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class TraCuuPhieuThuTienViewModel : BaseViewModel
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

        private ObservableCollection<PhieuThuTien> _danhSachPhieu = new ObservableCollection<PhieuThuTien>();
        public ObservableCollection<PhieuThuTien> DanhSachPhieu
        {
            get => _danhSachPhieu;
            set => SetProperty(ref _danhSachPhieu, value);
        }

        private PhieuThuTien? _selectedPhieu;
        public PhieuThuTien? SelectedPhieu
        {
            get => _selectedPhieu;
            set
            {
                SetProperty(ref _selectedPhieu, value);
                XuatExcelChiTietCommand.RaiseCanExecuteChanged();
            }
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

        public TraCuuPhieuThuTienViewModel()
        {
            TimKiemCommand = new RelayCommand(TimKiem);
            BoLocNgayCommand = new RelayCommand(BoLocNgay);
            XemChiTietCommand = new RelayCommand(XemChiTiet, () => SelectedPhieu != null);
            DongChiTietCommand = new RelayCommand(DongChiTiet);
            XuatExcelCommand = new RelayCommand(XuatExcel, () => DanhSachPhieu.Any());
            XuatExcelChiTietCommand = new RelayCommand(XuatExcelChiTiet, () => SelectedPhieu != null);
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

                var query = context.PhieuThuTiens
                                   .Include(p => p.Xe)
                                   .AsQueryable();

                if (!string.IsNullOrWhiteSpace(TuKhoa))
                {
                    var keyword = TuKhoa.Trim().ToLower();
                    query = query.Where(p => 
                                p.MaPhieuThuTien.ToString() == keyword ||
                                (p.Xe != null && p.Xe.BienSo.ToLower().Contains(keyword))
                            );
                }

                if (TuNgay.HasValue)
                {
                    query = query.Where(p => p.NgayThuTien.Date >= TuNgay.Value.Date);
                }

                if (DenNgay.HasValue)
                {
                    query = query.Where(p => p.NgayThuTien.Date <= DenNgay.Value.Date);
                }

                var ketQua = query.OrderByDescending(p => p.NgayThuTien).ToList();

                DanhSachPhieu.Clear();
                foreach (var p in ketQua)
                {
                    DanhSachPhieu.Add(p);
                }

                TongTienHienThi = ketQua.Sum(p => p.SoTienThu);
                
                XuatExcelCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tra cứu phiếu thu tiền:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
            
            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn hủy phiếu thu tiền #{SelectedPhieu.MaPhieuThuTien} không?\nSố tiền {SelectedPhieu.SoTienThu:N0} VNĐ sẽ được cộng trở lại vào Tiền Nợ của xe.", "Xác nhận hủy phiếu", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            
            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new GaraDbContext();
                    var p = context.PhieuThuTiens.Include(x => x.Xe).FirstOrDefault(x => x.MaPhieuThuTien == SelectedPhieu.MaPhieuThuTien);
                    if (p != null)
                    {
                        if (p.Xe != null)
                        {
                            // Chỉ rollback phần tiền thực sự đã trừ vào nợ (không rollback phần tiền dôi ra được tính là phạt)
                            decimal tienDaTru = Math.Min(p.SoTienThu, p.TienNoTruocThu);
                            p.Xe.TienNo += tienDaTru; 
                        }
                        context.PhieuThuTiens.Remove(p);
                        context.SaveChanges();
                        
                        MessageBox.Show("Hủy phiếu thu tiền thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        IsPopupOpen = false;
                        TimKiem();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi hủy phiếu thu tiền:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DongChiTiet()
        {
            IsPopupOpen = false;
        }

        private void XuatExcel()
        {
            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel("TraCuuPhieuThuTien", wb =>
            {
                var ws = wb.Worksheets.Add("PhieuThuTien");
                ws.Cell(1, 1).Value = "BÁO CÁO TRA CỨU PHIẾU THU TIỀN";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 5).Merge();
                ws.Range(1, 1, 1, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                var headers = new[] { "Mã Phiếu", "Biển Số Xe", "Ngày Thu", "Số Tiền Thu", "Tiền Phạt/Dư" };
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
                    ws.Cell(row, 1).Value = p.MaPhieuThuTien;
                    ws.Cell(row, 2).Value = p.Xe?.BienSo;
                    ws.Cell(row, 3).Value = p.NgayThuTien.ToString("dd/MM/yyyy HH:mm");
                    ws.Cell(row, 4).Value = p.SoTienThu;
                    ws.Cell(row, 4).Style.NumberFormat.Format = "#,##0";
                    ws.Cell(row, 5).Value = p.TienPhat;
                    ws.Cell(row, 5).Style.NumberFormat.Format = "#,##0";

                    for (int c = 1; c <= 5; c++)
                        ws.Cell(row, c).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    
                    row++;
                }
                ws.Columns().AdjustToContents();
            });
        }

        private void XuatExcelChiTiet()
        {
            if (SelectedPhieu == null) return;

            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel($"BienLaiThuTien_{SelectedPhieu.MaPhieuThuTien}", wb =>
            {
                var ws = wb.Worksheets.Add("BienLaiThuTien");
                ws.Cell(1, 1).Value = $"BIÊN LAI THU TIỀN #{SelectedPhieu.MaPhieuThuTien}";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 2).Merge();
                ws.Range(1, 1, 1, 2).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws.Cell(3, 1).Value = "Biển số xe:"; ws.Cell(3, 1).Style.Font.Bold = true;
                ws.Cell(3, 2).Value = SelectedPhieu.Xe?.BienSo;

                ws.Cell(4, 1).Value = "Ngày thu tiền:"; ws.Cell(4, 1).Style.Font.Bold = true;
                ws.Cell(4, 2).Value = SelectedPhieu.NgayThuTien.ToString("dd/MM/yyyy HH:mm");

                ws.Cell(5, 1).Value = "Số tiền thu:"; ws.Cell(5, 1).Style.Font.Bold = true;
                ws.Cell(5, 2).Value = SelectedPhieu.SoTienThu;
                ws.Cell(5, 2).Style.NumberFormat.Format = "#,##0";

                ws.Columns().AdjustToContents();
            });
        }
    }
}
