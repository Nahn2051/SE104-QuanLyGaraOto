using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class PhieuNhapKhoViewModel : BaseViewModel
    {
        // =====================================================================
        // Backing fields
        // =====================================================================

        private DateTime _ngayNhap = DateTime.Now;
        private decimal _tongTien;
        private ChiTietPhieuNhapRow? _selectedChiTiet;

        // =====================================================================
        // Properties
        // =====================================================================

        public DateTime NgayNhap
        {
            get => _ngayNhap;
            set => SetProperty(ref _ngayNhap, value);
        }

        public decimal TongTien
        {
            get => _tongTien;
            private set => SetProperty(ref _tongTien, value);
        }

        public ObservableCollection<VatTuPhuTung> DanhSachVatTu { get; set; } = new ObservableCollection<VatTuPhuTung>();
        public ObservableCollection<ChiTietPhieuNhapRow> ChiTietNhapKhos { get; set; } = new ObservableCollection<ChiTietPhieuNhapRow>();

        public ChiTietPhieuNhapRow? SelectedChiTiet
        {
            get => _selectedChiTiet;
            set => SetProperty(ref _selectedChiTiet, value);
        }

        // =====================================================================
        // Commands
        // =====================================================================

        public RelayCommand ThemChiTietCommand { get; }
        public RelayCommand XoaChiTietCommand { get; }
        public RelayCommand LuuPhieuNhapCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public PhieuNhapKhoViewModel()
        {
            ThemChiTietCommand = new RelayCommand(ThemChiTiet);
            XoaChiTietCommand = new RelayCommand(XoaChiTiet, () => SelectedChiTiet != null);
            LuuPhieuNhapCommand = new RelayCommand(LuuPhieuNhap, CanLuuPhieu);

            LoadDuLieu();
        }

        // =====================================================================
        // Methods
        // =====================================================================

        private void LoadDuLieu()
        {
            try
            {
                using var context = new GaraDbContext();
                var ds = context.VatTuPhuTungs.ToList();
                DanhSachVatTu.Clear();
                foreach (var item in ds)
                {
                    DanhSachVatTu.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách vật tư:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ThemChiTiet()
        {
            var row = new ChiTietPhieuNhapRow();
            row.PropertyChanged += (_, _) => TinhTongTien();
            ChiTietNhapKhos.Add(row);
        }

        private void XoaChiTiet()
        {
            if (SelectedChiTiet != null)
            {
                ChiTietNhapKhos.Remove(SelectedChiTiet);
                SelectedChiTiet = null;
                TinhTongTien();
            }
        }

        private void TinhTongTien()
        {
            TongTien = ChiTietNhapKhos.Sum(x => x.ThanhTien);
        }

        private bool CanLuuPhieu()
        {
            return ChiTietNhapKhos.Count > 0;
        }

        private void LuuPhieuNhap()
        {
            // Validation
            foreach (var row in ChiTietNhapKhos)
            {
                if (row.SelectedVatTu == null)
                {
                    MessageBox.Show("Vui lòng chọn vật tư cho tất cả các dòng!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (row.SoLuong <= 0)
                {
                    MessageBox.Show("Số lượng nhập phải lớn hơn 0!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (row.DonGia <= 0)
                {
                    MessageBox.Show("Đơn giá nhập phải lớn hơn 0!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                using var context = new GaraDbContext();
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    // 1. Tạo Phiếu Nhập
                    var phieuNhap = new PhieuNhap
                    {
                        NgayNhap = NgayNhap
                    };
                    context.PhieuNhaps.Add(phieuNhap);
                    context.SaveChanges(); // Lấy MaPhieuNhap

                    // 2. Lưu Chi Tiết và Cập nhật Kho
                    foreach (var row in ChiTietNhapKhos)
                    {
                        // Thêm chi tiết phiếu nhập
                        var chiTiet = new ChiTietPhieuNhap
                        {
                            MaPhieuNhap = phieuNhap.MaPhieuNhap,
                            MaVTPT = row.SelectedVatTu!.MaVTPT,
                            SoLuong = row.SoLuong,
                            DonGia = row.DonGia,
                            ThanhTien = row.ThanhTien
                        };
                        context.ChiTietPhieuNhaps.Add(chiTiet);

                        // Cập nhật lại tồn kho và đơn giá nhập vào bảng VATTUPHUTUNG
                        var vatTuDb = context.VatTuPhuTungs.FirstOrDefault(v => v.MaVTPT == row.SelectedVatTu.MaVTPT);
                        if (vatTuDb != null)
                        {
                            vatTuDb.SoLuongTon += row.SoLuong; // Cộng dồn số lượng
                            vatTuDb.DonGia = row.DonGia;       // Cập nhật giá nhập mới nhất
                        }
                    }

                    context.SaveChanges();
                    transaction.Commit();

                    MessageBox.Show($"Nhập kho thành công!\nMã phiếu nhập: {phieuNhap.MaPhieuNhap}\nTổng tiền: {TongTien:N0} VNĐ", 
                                    "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                    ResetForm();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Lỗi Transaction: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu phiếu nhập:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetForm()
        {
            NgayNhap = DateTime.Now;
            ChiTietNhapKhos.Clear();
            SelectedChiTiet = null;
            TinhTongTien();

            // Tải lại danh sách vật tư để lấy giá và số lượng mới nhất
            LoadDuLieu();
        }
    }

    // =========================================================================
    // Row ViewModel
    // =========================================================================
    public class ChiTietPhieuNhapRow : BaseViewModel
    {
        private VatTuPhuTung? _selectedVatTu;
        public VatTuPhuTung? SelectedVatTu
        {
            get => _selectedVatTu;
            set
            {
                if (SetProperty(ref _selectedVatTu, value))
                {
                    if (value != null)
                    {
                        // Lấy đơn giá cũ làm gợi ý (nhưng người dùng có thể gõ sửa lại)
                        DonGia = value.DonGia;
                    }
                }
            }
        }

        private int _soLuong = 1;
        public int SoLuong
        {
            get => _soLuong;
            set
            {
                if (SetProperty(ref _soLuong, value))
                    TinhThanhTien();
            }
        }

        private decimal _donGia;
        public decimal DonGia
        {
            get => _donGia;
            set
            {
                if (SetProperty(ref _donGia, value))
                    TinhThanhTien();
            }
        }

        private decimal _thanhTien;
        public decimal ThanhTien
        {
            get => _thanhTien;
            private set => SetProperty(ref _thanhTien, value);
        }

        private void TinhThanhTien()
        {
            ThanhTien = SoLuong * DonGia;
        }
    }
}
