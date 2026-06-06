using System;
using System.Linq;
using System.Windows;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class ThayDoiQuyDinhViewModel : BaseViewModel
    {
        // =====================================================================
        // Backing fields
        // =====================================================================

        private int _soXeToiDa;
        private double _tiLeDonGiaBan;
        private bool _kiemTraTienThu;

        // =====================================================================
        // Properties
        // =====================================================================

        public int SoXeToiDa
        {
            get => _soXeToiDa;
            set => SetProperty(ref _soXeToiDa, value);
        }

        public double TiLeDonGiaBan
        {
            get => _tiLeDonGiaBan;
            set => SetProperty(ref _tiLeDonGiaBan, value);
        }

        public bool KiemTraTienThu
        {
            get => _kiemTraTienThu;
            set => SetProperty(ref _kiemTraTienThu, value);
        }

        // =====================================================================
        // Commands
        // =====================================================================

        public RelayCommand LuuQuyDinhCommand { get; }

        // =====================================================================
        // Constructor
        // =====================================================================

        public ThayDoiQuyDinhViewModel()
        {
            LuuQuyDinhCommand = new RelayCommand(LuuQuyDinh);

            LoadDuLieu();
        }

        // =====================================================================
        // Methods
        // =====================================================================

        /// <summary>
        /// Đọc các tham số quy định từ Database lên giao diện
        /// </summary>
        private void LoadDuLieu()
        {
            try
            {
                using var context = new GaraDbContext();

                // 1. Số xe sửa chữa tối đa trong ngày
                var qd1 = context.ThamSos.FirstOrDefault(ts => ts.TenThamSo == "SoXeSuaChuaToiDa");
                if (qd1 != null && int.TryParse(qd1.GiaTri, out int soXe))
                {
                    SoXeToiDa = soXe;
                }

                // 2. Tỷ lệ đơn giá bán (lợi nhuận)
                var qd2 = context.ThamSos.FirstOrDefault(ts => ts.TenThamSo == "TiLeDonGiaBan");
                if (qd2 != null && double.TryParse(qd2.GiaTri, out double tiLe))
                {
                    TiLeDonGiaBan = tiLe;
                }

                // 3. Quy định kiểm tra tiền thu <= tiền nợ
                var qd4 = context.ThamSos.FirstOrDefault(ts => ts.TenThamSo == "ApDungQDKiemTraSoTienThu");
                if (qd4 != null)
                {
                    KiemTraTienThu = qd4.GiaTri == "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải tham số quy định:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Cập nhật lại các tham số quy định mới xuống Database
        /// </summary>
        private void LuuQuyDinh()
        {
            if (SoXeToiDa <= 0)
            {
                MessageBox.Show("Số xe sửa chữa tối đa phải lớn hơn 0!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (TiLeDonGiaBan < 1.0)
            {
                MessageBox.Show("Tỷ lệ đơn giá bán không được nhỏ hơn 1.0 (tránh bán lỗ)!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using var context = new GaraDbContext();

                // Kiểm tra số xe tiếp nhận trong ngày
                int soXeHomNay = context.Xes.Count(x => x.NgayTiepNhan.HasValue && x.NgayTiepNhan.Value.Date == DateTime.Today.Date);
                if (SoXeToiDa < soXeHomNay)
                {
                    MessageBox.Show($"Số xe tiếp nhận trong ngày ({soXeHomNay} xe) đã lớn hơn số muốn đổi ({SoXeToiDa} xe).\nVui lòng thực hiện lại vào ngày mai hoặc nhập số lớn hơn!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 1. Update Số xe tối đa
                var qd1 = context.ThamSos.FirstOrDefault(ts => ts.TenThamSo == "SoXeSuaChuaToiDa");
                if (qd1 != null)
                {
                    qd1.GiaTri = SoXeToiDa.ToString();
                }

                // 2. Update Tỷ lệ đơn giá bán
                var qd2 = context.ThamSos.FirstOrDefault(ts => ts.TenThamSo == "TiLeDonGiaBan");
                if (qd2 != null)
                {
                    double oldRatio = 1.05;
                    if (double.TryParse(qd2.GiaTri, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double parsedOldRatio))
                    {
                        oldRatio = parsedOldRatio;
                    }

                    // Cập nhật lại giá bán của tất cả vật tư phụ tùng dựa trên tỉ lệ mới
                    // Công thức: Giá_Mới = (Giá_Cũ / Tỉ_Lệ_Cũ) * Tỉ_Lệ_Mới
                    if (oldRatio > 0 && Math.Abs(oldRatio - TiLeDonGiaBan) > 0.0001)
                    {
                        var tatCaVatTu = context.VatTuPhuTungs.ToList();
                        foreach (var vt in tatCaVatTu)
                        {
                            decimal newPrice = (vt.DonGia / (decimal)oldRatio) * (decimal)TiLeDonGiaBan;
                            // Phải làm tròn về 0 chữ số thập phân vì database cấu hình cột DonGia là decimal(18,0)
                            vt.DonGia = Math.Round(newPrice, 0);
                        }
                    }

                    // Chuyển sang string, giữ định dạng số thập phân chuẩn (tránh lỗi văn hóa dấu phẩy)
                    qd2.GiaTri = TiLeDonGiaBan.ToString(System.Globalization.CultureInfo.InvariantCulture);
                }

                // 3. Update Quy định kiểm tra tiền thu
                var qd4 = context.ThamSos.FirstOrDefault(ts => ts.TenThamSo == "ApDungQDKiemTraSoTienThu");
                if (qd4 != null)
                {
                    qd4.GiaTri = KiemTraTienThu ? "1" : "0";
                }

                context.SaveChanges();

                MessageBox.Show("Thay đổi quy định thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu tham số quy định:\n{ex.Message}", "Lỗi Database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
