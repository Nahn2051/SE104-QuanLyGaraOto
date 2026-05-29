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

        // =====================================================================
        // Commands
        // =====================================================================

        public RelayCommand TimKiemCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public TraCuuXeViewModel()
        {
            TimKiemCommand = new RelayCommand(TimKiem);

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
    }
}
