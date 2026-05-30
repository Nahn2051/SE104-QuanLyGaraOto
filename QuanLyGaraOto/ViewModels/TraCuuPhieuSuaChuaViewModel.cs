using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.ViewModels
{
    public class TraCuuPhieuSuaChuaViewModel : BaseViewModel
    {
        private string _tuKhoa = string.Empty;
        public string TuKhoa
        {
            get => _tuKhoa;
            set => SetProperty(ref _tuKhoa, value);
        }

        private ObservableCollection<PhieuSuaChua> _danhSachPhieu = new ObservableCollection<PhieuSuaChua>();
        public ObservableCollection<PhieuSuaChua> DanhSachPhieu
        {
            get => _danhSachPhieu;
            set => SetProperty(ref _danhSachPhieu, value);
        }

        private PhieuSuaChua? _selectedPhieu;
        public PhieuSuaChua? SelectedPhieu
        {
            get => _selectedPhieu;
            set
            {
                SetProperty(ref _selectedPhieu, value);
                ChiTietPhieu.Clear();
                if (_selectedPhieu != null && _selectedPhieu.DanhSachCTPhieuSuaChua != null)
                {
                    foreach (var ct in _selectedPhieu.DanhSachCTPhieuSuaChua)
                    {
                        ChiTietPhieu.Add(ct);
                    }
                }
            }
        }

        private ObservableCollection<ChiTietPhieuSuaChua> _chiTietPhieu = new ObservableCollection<ChiTietPhieuSuaChua>();
        public ObservableCollection<ChiTietPhieuSuaChua> ChiTietPhieu
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

        public TraCuuPhieuSuaChuaViewModel()
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

                var query = context.PhieuSuaChuas
                                   .Include(p => p.Xe)
                                   .Include(p => p.DanhSachCTPhieuSuaChua)
                                       .ThenInclude(ct => ct.VatTuPhuTung)
                                   .Include(p => p.DanhSachCTPhieuSuaChua)
                                       .ThenInclude(ct => ct.TienCong)
                                   .AsQueryable();

                if (!string.IsNullOrWhiteSpace(TuKhoa))
                {
                    var keyword = TuKhoa.Trim().ToLower();
                    query = query.Where(p => 
                                p.MaPhieuSuaChua.ToString() == keyword ||
                                (p.Xe != null && p.Xe.BienSo.ToLower().Contains(keyword))
                            );
                }

                var ketQua = query.OrderByDescending(p => p.NgaySuaChua).ToList();

                DanhSachPhieu.Clear();
                foreach (var p in ketQua)
                {
                    DanhSachPhieu.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tra cứu phiếu sửa chữa:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
