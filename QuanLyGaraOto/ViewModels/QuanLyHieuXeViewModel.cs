using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class QuanLyHieuXeViewModel : BaseViewModel
    {
        // =====================================================================
        // Properties
        // =====================================================================

        private ObservableCollection<HieuXe> _danhSachHieuXe = new ObservableCollection<HieuXe>();
        public ObservableCollection<HieuXe> DanhSachHieuXe
        {
            get => _danhSachHieuXe;
            set => SetProperty(ref _danhSachHieuXe, value);
        }

        private HieuXe? _selectedHieuXe;
        public HieuXe? SelectedHieuXe
        {
            get => _selectedHieuXe;
            set
            {
                if (SetProperty(ref _selectedHieuXe, value))
                {
                    if (_selectedHieuXe != null)
                    {
                        TenHieuXe = _selectedHieuXe.TenHieuXe;
                    }
                    else
                    {
                        TenHieuXe = string.Empty;
                    }
                }
            }
        }

        private string _tenHieuXe = string.Empty;
        public string TenHieuXe
        {
            get => _tenHieuXe;
            set => SetProperty(ref _tenHieuXe, value);
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
        public RelayCommand<System.Collections.IList> XoaCommand { get; }
        public RelayCommand ClearFormCommand { get; }
        public RelayCommand XuatExcelCommand { get; }
        public RelayCommand TimKiemCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public QuanLyHieuXeViewModel()
        {
            ThemCommand = new RelayCommand(ThemHieuXe, () => !string.IsNullOrWhiteSpace(TenHieuXe));
            SuaCommand = new RelayCommand(SuaHieuXe, () => SelectedHieuXe != null && !string.IsNullOrWhiteSpace(TenHieuXe));
            XoaCommand = new RelayCommand<System.Collections.IList>(XoaHieuXe, (items) => items != null && items.Count > 0);
            ClearFormCommand = new RelayCommand(ClearForm);
            XuatExcelCommand = new RelayCommand(XuatExcel, () => DanhSachHieuXe.Any());
            TimKiemCommand = new RelayCommand(LoadDanhSachHieuXe);

            LoadDanhSachHieuXe();
        }

        // =====================================================================
        // Methods
        // =====================================================================

        private void LoadDanhSachHieuXe()
        {
            try
            {
                using var context = new GaraDbContext();
                var query = context.HieuXes.AsQueryable();
                
                if (!string.IsNullOrWhiteSpace(TuKhoa))
                {
                    var keyword = TuKhoa.Trim().ToLower();
                    query = query.Where(h => h.TenHieuXe.ToLower().Contains(keyword));
                }
                
                var list = query.ToList();
                DanhSachHieuXe.Clear();
                foreach (var hx in list)
                {
                    DanhSachHieuXe.Add(hx);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách hiệu xe:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            SelectedHieuXe = null;
            TenHieuXe = string.Empty;
        }

        private void XuatExcel()
        {
            QuanLyGaraOto.Services.ExcelExportService.ExportCustomExcel("DanhSachHieuXe", wb =>
            {
                var ws = wb.Worksheets.Add("HieuXe");
                ws.Cell(1, 1).Value = "DANH SÁCH HIỆU XE HIỆN CÓ";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Range(1, 1, 1, 2).Merge();
                ws.Range(1, 1, 1, 2).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                var headers = new[] { "Mã Hiệu Xe", "Tên Hiệu Xe" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(3, i + 1).Value = headers[i];
                    ws.Cell(3, i + 1).Style.Font.Bold = true;
                    ws.Cell(3, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    ws.Cell(3, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                int row = 4;
                foreach (var hx in DanhSachHieuXe)
                {
                    ws.Cell(row, 1).Value = hx.MaHieuXe;
                    ws.Cell(row, 2).Value = hx.TenHieuXe;

                    for (int c = 1; c <= 2; c++)
                        ws.Cell(row, c).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    
                    row++;
                }
                ws.Columns().AdjustToContents();
            });
        }

        private void ThemHieuXe()
        {
            try
            {
                using var context = new GaraDbContext();

                // Kiểm tra trùng tên hiệu xe
                var exists = context.HieuXes.Any(x => x.TenHieuXe.ToLower() == TenHieuXe.Trim().ToLower());
                if (exists)
                {
                    MessageBox.Show("Tên hiệu xe này đã tồn tại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newHieuXe = new HieuXe
                {
                    TenHieuXe = TenHieuXe.Trim()
                };

                context.HieuXes.Add(newHieuXe);
                context.SaveChanges();

                MessageBox.Show("Thêm hiệu xe thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadDanhSachHieuXe();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SuaHieuXe()
        {
            if (SelectedHieuXe == null) return;

            try
            {
                using var context = new GaraDbContext();

                // Kiểm tra trùng tên hiệu xe (bỏ qua chính nó)
                var exists = context.HieuXes.Any(x => x.MaHieuXe != SelectedHieuXe.MaHieuXe 
                                                   && x.TenHieuXe.ToLower() == TenHieuXe.Trim().ToLower());
                if (exists)
                {
                    MessageBox.Show("Tên hiệu xe này đã tồn tại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var hxToUpdate = context.HieuXes.Find(SelectedHieuXe.MaHieuXe);
                if (hxToUpdate != null)
                {
                    hxToUpdate.TenHieuXe = TenHieuXe.Trim();
                    context.SaveChanges();

                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadDanhSachHieuXe();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void XoaHieuXe(System.Collections.IList? items)
        {
            if (items == null || items.Count == 0) return;

            var list = items.Cast<HieuXe>().ToList();
            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {list.Count} hiệu xe đã chọn?",
                                         "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                using var context = new GaraDbContext();
                int successCount = 0;
                
                foreach (var hx in list)
                {
                    var hxToDelete = context.HieuXes.Find(hx.MaHieuXe);
                    if (hxToDelete != null)
                    {
                        context.HieuXes.Remove(hxToDelete);
                        successCount++;
                    }
                }
                
                context.SaveChanges();
                
                MessageBox.Show($"Đã xóa thành công {successCount} hiệu xe!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadDanhSachHieuXe();
                ClearForm();
            }
            catch (DbUpdateException)
            {
                // Lỗi do dính khóa ngoại (Foreign Key Constraint)
                MessageBox.Show("Không thể xóa một số hiệu xe vì đã có Xe sử dụng!\nVui lòng kiểm tra lại danh sách Xe.", 
                                "Lỗi ràng buộc dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
