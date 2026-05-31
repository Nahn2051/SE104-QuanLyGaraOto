using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class PhieuSuaChuaViewModel : BaseViewModel
    {
        // =====================================================================
        // Backing fields
        // =====================================================================

        private Xe? _selectedXe;
        private DateTime _ngaySuaChua = DateTime.Now;
        private decimal _tongTien;
        private decimal _soTienTra;
        private ChiTietPhieuSuaChuaRow? _selectedChiTiet;
        private decimal _tiLeDonGiaBan = 1.0m; // Lưu trữ tỷ lệ lấy từ DB

        // =====================================================================
        // Danh sách nguồn (load từ DB)
        // =====================================================================

        public ObservableCollection<Xe> DanhSachXe { get; set; } = [];
        public ObservableCollection<VatTuPhuTung> DanhSachVatTu { get; set; } = [];
        public ObservableCollection<TienCong> DanhSachTienCong { get; set; } = [];

        // =====================================================================
        // Danh sách chi tiết (hiển thị trên DataGrid)
        // =====================================================================

        public ObservableCollection<ChiTietPhieuSuaChuaRow> ChiTietSuaChuas { get; set; } = [];

        // =====================================================================
        // Properties — bind lên UI
        // =====================================================================

        public Xe? SelectedXe
        {
            get => _selectedXe;
            set => SetProperty(ref _selectedXe, value);
        }

        public DateTime NgaySuaChua
        {
            get => _ngaySuaChua;
            set => SetProperty(ref _ngaySuaChua, value);
        }

        public decimal TongTien
        {
            get => _tongTien;
            private set => SetProperty(ref _tongTien, value);
        }

        public decimal SoTienTra
        {
            get => _soTienTra;
            set
            {
                if (SetProperty(ref _soTienTra, value))
                    OnPropertyChanged(nameof(TienConLai));
            }
        }

        /// <summary>
        /// Tiền Còn Lại = Tổng Tiền - Số Tiền Trả. Tự tính toán.
        /// </summary>
        public decimal TienConLai => TongTien - SoTienTra;

        /// <summary>
        /// Dòng chi tiết đang được chọn trên DataGrid.
        /// </summary>
        public ChiTietPhieuSuaChuaRow? SelectedChiTiet
        {
            get => _selectedChiTiet;
            set => SetProperty(ref _selectedChiTiet, value);
        }

        // =====================================================================
        // Commands
        // =====================================================================

        public RelayCommand ThemChiTietCommand { get; }
        public RelayCommand<System.Collections.IList> XoaChiTietCommand { get; }
        public RelayCommand LuuPhieuCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public PhieuSuaChuaViewModel()
        {
            ThemChiTietCommand = new RelayCommand(ThemChiTiet);

            XoaChiTietCommand = new RelayCommand<System.Collections.IList>(
                execute: XoaChiTiet,
                canExecute: (items) => items != null && items.Count > 0
            );

            LuuPhieuCommand = new RelayCommand(
                execute: LuuPhieu,
                canExecute: () => SelectedXe is not null && ChiTietSuaChuas.Count > 0
            );

            LoadDuLieu();
        }

        // =====================================================================
        // Load dữ liệu từ Database
        // =====================================================================

        private void LoadDuLieu()
        {
            try
            {
                using var context = new GaraDbContext();

                DanhSachXe = new ObservableCollection<Xe>(context.Xes.ToList());
                DanhSachVatTu = new ObservableCollection<VatTuPhuTung>(context.VatTuPhuTungs.ToList());
                DanhSachTienCong = new ObservableCollection<TienCong>(context.TienCongs.ToList());

                // Lấy tỷ lệ đơn giá bán từ bảng THAMSO (chỉ query 1 lần)
                var thamSo = context.ThamSos.FirstOrDefault(ts => ts.TenThamSo == "TiLeDonGiaBan");
                if (thamSo != null && decimal.TryParse(thamSo.GiaTri, out decimal tiLe))
                {
                    _tiLeDonGiaBan = tiLe;
                }

                OnPropertyChanged(nameof(DanhSachXe));
                OnPropertyChanged(nameof(DanhSachVatTu));
                OnPropertyChanged(nameof(DanhSachTienCong));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tải dữ liệu:\n{ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error
                );
            }
        }

        // =====================================================================
        // Command: Thêm 1 dòng trống vào danh sách chi tiết
        // =====================================================================

        private void ThemChiTiet()
        {
            // Truyền tỷ lệ vào để dòng chi tiết tự tính toán khi đổi vật tư
            var row = new ChiTietPhieuSuaChuaRow(_tiLeDonGiaBan);
            row.PropertyChanged += (_, _) => TinhTongTien();
            ChiTietSuaChuas.Add(row);
        }

        // =====================================================================
        // Command: Xóa dòng chi tiết đang chọn
        // =====================================================================

        private void XoaChiTiet(System.Collections.IList? items)
        {
            if (items == null || items.Count == 0) return;

            // Chuyển sang list để tránh lỗi CollectionModifiedException khi xóa
            var list = items.Cast<ChiTietPhieuSuaChuaRow>().ToList();
            foreach (var item in list)
            {
                ChiTietSuaChuas.Remove(item);
            }
            
            SelectedChiTiet = null;
            TinhTongTien();
        }

        // =====================================================================
        // Tính tổng tiền từ tất cả dòng chi tiết
        // =====================================================================

        private void TinhTongTien()
        {
            TongTien = ChiTietSuaChuas.Sum(ct => ct.ThanhTien);
            OnPropertyChanged(nameof(TienConLai));
        }

        // =====================================================================
        // Command: Lưu phiếu sửa chữa (Transaction)
        // =====================================================================

        private void LuuPhieu()
        {
            // --- Validation ---
            if (SelectedXe is null)
            {
                MessageBox.Show("Vui lòng chọn xe!", "Cảnh báo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (NgaySuaChua.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Ngày sửa chữa không được lớn hơn ngày hiện tại!", "Lỗi ngày tháng",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedXe.NgayTiepNhan.HasValue && NgaySuaChua.Date < SelectedXe.NgayTiepNhan.Value.Date)
            {
                MessageBox.Show($"Ngày sửa chữa không được nhỏ hơn ngày tiếp nhận xe ({SelectedXe.NgayTiepNhan.Value:dd/MM/yyyy})!", "Lỗi ngày tháng",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ChiTietSuaChuas.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất 1 chi tiết sửa chữa!", "Cảnh báo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Kiểm tra mỗi dòng chi tiết
            foreach (var ct in ChiTietSuaChuas)
            {
                if (string.IsNullOrWhiteSpace(ct.NoiDungSuaChua))
                {
                    MessageBox.Show("Mỗi dòng chi tiết phải có nội dung sửa chữa!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                
                if (ct.SelectedVatTu == null && ct.SelectedTienCong == null)
                {
                    MessageBox.Show("Mỗi dòng chi tiết phải chọn ít nhất một Vật tư hoặc một Loại tiền công!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            if (SoTienTra > TongTien)
            {
                MessageBox.Show("Số tiền trả không được vượt quá Tổng tiền của phiếu sửa chữa!",
                    "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using var context = new GaraDbContext();
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    // --- 1. Tạo PhieuSuaChua ---
                    var phieu = new PhieuSuaChua
                    {
                        MaXe = SelectedXe.MaXe,
                        NgaySuaChua = NgaySuaChua,
                        TongTien = TongTien
                    };

                    context.PhieuSuaChuas.Add(phieu);
                    context.SaveChanges(); // Để có MaPhieuSuaChua

                    // --- 2. Lưu danh sách CT_PHIEUSUACHUA ---
                    foreach (var row in ChiTietSuaChuas)
                    {
                        var chiTiet = new ChiTietPhieuSuaChua
                        {
                            MaPhieuSuaChua = phieu.MaPhieuSuaChua,
                            NoiDungSuaChua = row.NoiDungSuaChua,
                            MaVTPT = row.SelectedVatTu?.MaVTPT,
                            MaTienCong = row.SelectedTienCong?.MaTienCong,
                            SoLuong = row.SoLuong,
                            DonGia = row.DonGia,
                            ThanhTien = row.ThanhTien
                        };

                        context.ChiTietPhieuSuaChuas.Add(chiTiet);

                        // --- 3. Trừ SoLuongTon của vật tư ---
                        if (row.SelectedVatTu is not null)
                        {
                            var vatTu = context.VatTuPhuTungs
                                .FirstOrDefault(v => v.MaVTPT == row.SelectedVatTu.MaVTPT);

                            if (vatTu is not null)
                            {
                                if (vatTu.SoLuongTon < row.SoLuong)
                                {
                                    throw new InvalidOperationException(
                                        $"Vật tư '{vatTu.TenVTPT}' không đủ số lượng tồn kho! " +
                                        $"(Tồn: {vatTu.SoLuongTon}, Yêu cầu: {row.SoLuong})");
                                }

                                vatTu.SoLuongTon -= row.SoLuong;
                            }
                        }
                    }

                    // --- 4. Cộng TienConLai vào TienNo của xe ---
                    var xe = context.Xes.FirstOrDefault(x => x.MaXe == SelectedXe.MaXe);
                    if (xe is not null)
                    {
                        xe.TienNo += TienConLai;
                    }

                    // --- 5. Lưu Phiếu Thu Tiền nếu có trả trước ---
                    if (SoTienTra > 0)
                    {
                        var phieuThu = new PhieuThuTien
                        {
                            MaXe = SelectedXe.MaXe,
                            NgayThuTien = NgaySuaChua, // Lưu cùng thời gian với Phiếu Sửa Chữa
                            SoTienThu = SoTienTra
                        };
                        context.PhieuThuTiens.Add(phieuThu);
                    }

                    context.SaveChanges();
                    transaction.Commit();

                    MessageBox.Show(
                        $"Lưu phiếu sửa chữa thành công!\n\n" +
                        $"• Mã phiếu: {phieu.MaPhieuSuaChua}\n" +
                        $"• Xe: {SelectedXe.BienSo}\n" +
                        $"• Tổng tiền: {TongTien:N0} VNĐ\n" +
                        $"• Tiền nợ cộng thêm: {TienConLai:N0} VNĐ",
                        "Thành công", MessageBoxButton.OK, MessageBoxImage.Information
                    );

                    ResetForm();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Giao dịch bị rollback: " + ex.Message, ex);
                }
            }
            catch (DbUpdateException ex)
            {
                var innerMsg = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show(
                    $"Lỗi Database:\n{innerMsg}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi:\n{ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error
                );
            }
        }

        // =====================================================================
        // Reset form
        // =====================================================================

        private void ResetForm()
        {
            SelectedXe = null;
            NgaySuaChua = DateTime.Now;
            SoTienTra = 0;
            ChiTietSuaChuas.Clear();
            TinhTongTien();

            // Reload dữ liệu (cập nhật SoLuongTon)
            LoadDuLieu();
        }
    }

    // =========================================================================
    // Row ViewModel — đại diện 1 dòng trên DataGrid
    // =========================================================================

    /// <summary>
    /// ViewModel cho mỗi dòng chi tiết trên DataGrid.
    /// Kế thừa BaseViewModel để hỗ trợ binding 2 chiều trên từng ô.
    /// </summary>
    public class ChiTietPhieuSuaChuaRow : BaseViewModel
    {
        private string _noiDungSuaChua = string.Empty;
        private VatTuPhuTung? _selectedVatTu;
        private TienCong? _selectedTienCong;
        private int _soLuong = 1;
        private decimal _donGia;
        private decimal _thanhTien;
        private readonly decimal _tiLeDonGiaBan;

        public ChiTietPhieuSuaChuaRow(decimal tiLeDonGiaBan = 1.0m)
        {
            _tiLeDonGiaBan = tiLeDonGiaBan;
        }

        public string NoiDungSuaChua
        {
            get => _noiDungSuaChua;
            set => SetProperty(ref _noiDungSuaChua, value);
        }

        /// <summary>
        /// Vật tư được chọn trên ComboBox trong DataGrid.
        /// Khi chọn → tự động cập nhật DonGia.
        /// </summary>
        public VatTuPhuTung? SelectedVatTu
        {
            get => _selectedVatTu;
            set
            {
                if (SetProperty(ref _selectedVatTu, value) && value is not null)
                {
                    // DonGiaBan = DonGiaNhap * TiLeDonGiaBan
                    DonGia = value.DonGia * _tiLeDonGiaBan;
                }
            }
        }

        /// <summary>
        /// Tiền công được chọn trên ComboBox trong DataGrid.
        /// Khi chọn → cộng thêm DonGia tiền công.
        /// </summary>
        public TienCong? SelectedTienCong
        {
            get => _selectedTienCong;
            set
            {
                if (SetProperty(ref _selectedTienCong, value))
                {
                    TinhThanhTien();
                }
            }
        }

        public int SoLuong
        {
            get => _soLuong;
            set
            {
                if (SetProperty(ref _soLuong, value))
                    TinhThanhTien();
            }
        }

        public decimal DonGia
        {
            get => _donGia;
            set
            {
                if (SetProperty(ref _donGia, value))
                    TinhThanhTien();
            }
        }

        public decimal ThanhTien
        {
            get => _thanhTien;
            private set => SetProperty(ref _thanhTien, value);
        }

        /// <summary>
        /// ThanhTien = (DonGia vật tư × SoLuong) + DonGia tiền công
        /// </summary>
        private void TinhThanhTien()
        {
            decimal tienVatTu = DonGia * SoLuong;
            decimal tienCong = SelectedTienCong?.DonGia ?? 0;
            ThanhTien = tienVatTu + tienCong;
        }
    }
}
