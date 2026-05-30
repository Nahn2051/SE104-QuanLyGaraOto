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
        public RelayCommand XemChiTietCommand { get; }
        public RelayCommand DongChiTietCommand { get; }

        public TraCuuPhieuNhapKhoViewModel()
        {
            TimKiemCommand = new RelayCommand(TimKiem);
            XemChiTietCommand = new RelayCommand(XemChiTiet, () => SelectedPhieu != null);
            DongChiTietCommand = new RelayCommand(DongChiTiet);

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

                if (!string.IsNullOrWhiteSpace(TuKhoa))
                {
                    var keyword = TuKhoa.Trim().ToLower();
                    query = query.Where(p => 
                                p.MaPhieuNhap.ToString() == keyword
                            );
                }

                var ketQua = query.OrderByDescending(p => p.NgayNhap).ToList();

                DanhSachPhieu.Clear();
                foreach (var p in ketQua)
                {
                    DanhSachPhieu.Add(p);
                }
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

        private void DongChiTiet()
        {
            IsPopupOpen = false;
        }
    }
}
