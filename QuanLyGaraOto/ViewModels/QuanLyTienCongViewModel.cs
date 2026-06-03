using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class QuanLyTienCongViewModel : BaseViewModel
    {
        // =====================================================================
        // Properties
        // =====================================================================

        private ObservableCollection<TienCong> _danhSachTienCong = new ObservableCollection<TienCong>();
        public ObservableCollection<TienCong> DanhSachTienCong
        {
            get => _danhSachTienCong;
            set => SetProperty(ref _danhSachTienCong, value);
        }

        private TienCong? _selectedTienCong;
        public TienCong? SelectedTienCong
        {
            get => _selectedTienCong;
            set
            {
                if (SetProperty(ref _selectedTienCong, value))
                {
                    if (_selectedTienCong != null)
                    {
                        TenTienCong = _selectedTienCong.TenTienCong;
                        DonGia = _selectedTienCong.DonGia;
                    }
                    else
                    {
                        TenTienCong = string.Empty;
                        DonGia = 0;
                    }
                }
            }
        }

        private string _tenTienCong = string.Empty;
        public string TenTienCong
        {
            get => _tenTienCong;
            set => SetProperty(ref _tenTienCong, value);
        }

        private decimal _donGia;
        public decimal DonGia
        {
            get => _donGia;
            set => SetProperty(ref _donGia, value);
        }

        private string _tuKhoa = string.Empty;
        public string TuKhoa
        {
            get => _tuKhoa;
            set => SetProperty(ref _tuKhoa, value);
        }

        // =====================================================================
        // Commands
        // =====================================================================

        public RelayCommand ThemCommand { get; }
        public RelayCommand SuaCommand { get; }
        public RelayCommand XoaCommand { get; }
        public RelayCommand ClearFormCommand { get; }
        public RelayCommand XuatExcelCommand { get; }
        public RelayCommand TimKiemCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public QuanLyTienCongViewModel()
        {
            ThemCommand = new RelayCommand(Them, CanThemSua);
            SuaCommand = new RelayCommand(Sua, CanSuaXoa);
            XoaCommand = new RelayCommand(Xoa, CanSuaXoa);
            ClearFormCommand = new RelayCommand(ClearForm);
            XuatExcelCommand = new RelayCommand(XuatExcel, () => DanhSachTienCong.Any());
            TimKiemCommand = new RelayCommand(LoadData);

            LoadData();
        }

        // =====================================================================
        // Methods
        // =====================================================================

        private void LoadData()
        {
            try
            {
                using var context = new GaraDbContext();
                var query = context.TienCongs.AsQueryable();

                if (!string.IsNullOrWhiteSpace(TuKhoa))
                {
                    var keyword = TuKhoa.Trim().ToLower();
                    query = query.Where(t => t.TenTienCong.ToLower().Contains(keyword));
                }

                var list = query.ToList();
                DanhSachTienCong.Clear();
                foreach (var tc in list)
                {
                    DanhSachTienCong.Add(tc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách tiền công:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanThemSua() => !string.IsNullOrWhiteSpace(TenTienCong) && DonGia >= 0;
        private bool CanSuaXoa() => SelectedTienCong != null && !string.IsNullOrWhiteSpace(TenTienCong) && DonGia >= 0;

        private void ClearForm()
        {
            SelectedTienCong = null;
            TenTienCong = string.Empty;
            DonGia = 0;
        }

        private void XuatExcel()
        {
            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel("DanhSachTienCong", wb =>
            {
                var ws = wb.Worksheets.Add("TienCong");
                ws.Cell(1, 1).Value = "DANH SÁCH TIỀN CÔNG";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 3).Merge();
                ws.Range(1, 1, 1, 3).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                var headers = new[] { "Mã Tiền Công", "Nội Dung Tiền Công", "Đơn Giá" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(3, i + 1).Value = headers[i];
                    ws.Cell(3, i + 1).Style.Font.Bold = true;
                    ws.Cell(3, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    ws.Cell(3, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                int row = 4;
                foreach (var tc in DanhSachTienCong)
                {
                    ws.Cell(row, 1).Value = tc.MaTienCong;
                    ws.Cell(row, 2).Value = tc.TenTienCong;
                    ws.Cell(row, 3).Value = tc.DonGia;
                    ws.Cell(row, 3).Style.NumberFormat.Format = "#,##0";

                    for (int c = 1; c <= 3; c++)
                        ws.Cell(row, c).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    
                    row++;
                }
                ws.Columns().AdjustToContents();
            });
        }

        private void Them()
        {
            try
            {
                using var context = new GaraDbContext();

                // Kiểm tra trùng tên
                var exists = context.TienCongs.Any(x => x.TenTienCong.ToLower() == TenTienCong.Trim().ToLower());
                if (exists)
                {
                    MessageBox.Show("Tên tiền công (loại sửa chữa) này đã tồn tại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newTienCong = new TienCong
                {
                    TenTienCong = TenTienCong.Trim(),
                    DonGia = DonGia
                };

                context.TienCongs.Add(newTienCong);
                context.SaveChanges();

                MessageBox.Show("Thêm loại tiền công thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Sua()
        {
            if (SelectedTienCong == null) return;

            try
            {
                using var context = new GaraDbContext();

                // Kiểm tra trùng tên (bỏ qua chính nó)
                var exists = context.TienCongs.Any(x => x.MaTienCong != SelectedTienCong.MaTienCong 
                                                   && x.TenTienCong.ToLower() == TenTienCong.Trim().ToLower());
                if (exists)
                {
                    MessageBox.Show("Tên tiền công này đã tồn tại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var tcToUpdate = context.TienCongs.Find(SelectedTienCong.MaTienCong);
                if (tcToUpdate != null)
                {
                    tcToUpdate.TenTienCong = TenTienCong.Trim();
                    tcToUpdate.DonGia = DonGia;
                    context.SaveChanges();

                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Xoa()
        {
            if (SelectedTienCong == null) return;

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa tiền công '{SelectedTienCong.TenTienCong}'?",
                                         "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                using var context = new GaraDbContext();
                var tcToDelete = context.TienCongs.Find(SelectedTienCong.MaTienCong);
                
                if (tcToDelete != null)
                {
                    context.TienCongs.Remove(tcToDelete);
                    context.SaveChanges();
                    
                    MessageBox.Show("Xóa tiền công thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ClearForm();
                }
            }
            catch (DbUpdateException)
            {
                // Lỗi khóa ngoại
                MessageBox.Show("Không thể xóa loại tiền công này vì đã được sử dụng trong Phiếu Sửa Chữa!\nVui lòng giữ lại để đảm bảo lịch sử dữ liệu.", 
                                "Lỗi ràng buộc dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
