using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class PhieuThuTienViewModel : BaseViewModel
    {
        // =================================================================
        // Properties
        // =================================================================

        private ObservableCollection<Xe> _danhSachXeNo = new ObservableCollection<Xe>();
        public ObservableCollection<Xe> DanhSachXeNo
        {
            get => _danhSachXeNo;
            set => SetProperty(ref _danhSachXeNo, value);
        }

        private Xe? _selectedXe;
        public Xe? SelectedXe
        {
            get => _selectedXe;
            set => SetProperty(ref _selectedXe, value);
        }

        private DateTime _ngayThuTien = DateTime.Now;
        public DateTime NgayThuTien
        {
            get => _ngayThuTien;
            set => SetProperty(ref _ngayThuTien, value);
        }

        private decimal _soTienThu;
        public decimal SoTienThu
        {
            get => _soTienThu;
            set => SetProperty(ref _soTienThu, value);
        }

        // =================================================================
        // Commands
        // =================================================================

        public RelayCommand LuuPhieuThuCommand { get; }

        // =================================================================
        // Constructor
        // =================================================================

        public PhieuThuTienViewModel()
        {
            DanhSachXeNo = new ObservableCollection<Xe>();
            LoadData();

            LuuPhieuThuCommand = new RelayCommand(LuuPhieuThu, CanLuuPhieuThu);
        }

        // =================================================================
        // Private Methods
        // =================================================================

        private void LoadData()
        {
            try
            {
                using var context = new GaraDbContext();
                // Chỉ lấy những xe đang có tiền nợ > 0
                var xeList = context.Xes.Where(x => x.TienNo > 0).ToList();
                DanhSachXeNo.Clear();
                foreach (var xe in xeList)
                {
                    DanhSachXeNo.Add(xe);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách xe: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanLuuPhieuThu()
        {
            return SelectedXe != null && SoTienThu > 0;
        }

        private void LuuPhieuThu()
        {
            try
            {
                using var context = new GaraDbContext();
                
                // 1. Đọc tham số ApDungQDKiemTraSoTienThu
                var thamSo = context.ThamSos.FirstOrDefault(ts => ts.TenThamSo == "ApDungQDKiemTraSoTienThu");
                bool apDungKiemTra = thamSo != null && thamSo.GiaTri == "1";

                // 2. Lấy chiếc xe hiện tại từ database để đảm bảo dữ liệu mới nhất
                var xe = context.Xes.FirstOrDefault(x => x.MaXe == SelectedXe!.MaXe);
                if (xe == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin xe trong cơ sở dữ liệu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Kiểm tra quy định số tiền thu <= tiền nợ
                if (apDungKiemTra && SoTienThu > xe.TienNo)
                {
                    MessageBox.Show($"Số tiền thu ({SoTienThu:N0} VNĐ) không được vượt quá số tiền nợ ({xe.TienNo:N0} VNĐ).", 
                                    "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 3. Lưu 1 record mới vào bảng PHIEUTHUTIEN
                var phieuThu = new PhieuThuTien
                {
                    MaXe = xe.MaXe,
                    NgayThuTien = NgayThuTien,
                    SoTienThu = SoTienThu
                };
                context.PhieuThuTiens.Add(phieuThu);

                // 4. Trừ số tiền thu vào TienNo của chiếc xe
                xe.TienNo -= SoTienThu;

                // Lưu thay đổi
                context.SaveChanges();

                MessageBox.Show("Đã lưu phiếu thu tiền thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // Reset form và load lại danh sách xe (những xe trả hết nợ sẽ biến mất khỏi ComboBox)
                SoTienThu = 0;
                LoadData();
                SelectedXe = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu phiếu thu tiền: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
