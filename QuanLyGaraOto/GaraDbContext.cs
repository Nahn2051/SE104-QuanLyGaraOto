using Microsoft.EntityFrameworkCore;

namespace QuanLyGaraOto.Models
{
    public class GaraDbContext : DbContext
    {
        // =====================================================================
        // DbSets
        // =====================================================================

        public DbSet<HieuXe> HieuXes { get; set; }
        public DbSet<Xe> Xes { get; set; }
        public DbSet<VatTuPhuTung> VatTuPhuTungs { get; set; }
        public DbSet<TienCong> TienCongs { get; set; }
        public DbSet<PhieuSuaChua> PhieuSuaChuas { get; set; }
        public DbSet<ChiTietPhieuSuaChua> ChiTietPhieuSuaChuas { get; set; }
        public DbSet<PhieuThuTien> PhieuThuTiens { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public DbSet<ThamSo> ThamSos { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }

        // =====================================================================
        // Constructors
        // =====================================================================

        public GaraDbContext() { }

        public GaraDbContext(DbContextOptions<GaraDbContext> options) : base(options) { }

        // =====================================================================
        // Connection String
        // =====================================================================

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=MSI;Database=GaraOtoDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
                );
            }
        }

        // =====================================================================
        // Fluent API Configuration
        // =====================================================================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------------------------------------------
            // HIEUXE
            // -----------------------------------------------------------------
            modelBuilder.Entity<HieuXe>(entity =>
            {
                entity.HasKey(e => e.MaHieuXe);

                entity.Property(e => e.TenHieuXe)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // -----------------------------------------------------------------
            // XE
            // -----------------------------------------------------------------
            modelBuilder.Entity<Xe>(entity =>
            {
                entity.HasKey(e => e.MaXe);

                entity.Property(e => e.BienSo)
                      .IsRequired()
                      .HasMaxLength(15);

                entity.HasIndex(e => e.BienSo)
                      .IsUnique();

                entity.Property(e => e.TenChuXe)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.TienNo)
                      .HasColumnType("decimal(18,0)")
                      .HasDefaultValue(0);

                // FK: Xe -> HieuXe (n - 1)
                entity.HasOne(e => e.HieuXe)
                      .WithMany(h => h.DanhSachXe)
                      .HasForeignKey(e => e.MaHieuXe)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -----------------------------------------------------------------
            // VATTUPHUTUNG
            // -----------------------------------------------------------------
            modelBuilder.Entity<VatTuPhuTung>(entity =>
            {
                entity.HasKey(e => e.MaVTPT);

                entity.Property(e => e.TenVTPT)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.DonGia)
                      .HasColumnType("decimal(18,0)");

                entity.Property(e => e.SoLuongTon)
                      .HasDefaultValue(0);
            });

            // -----------------------------------------------------------------
            // TIENCONG
            // -----------------------------------------------------------------
            modelBuilder.Entity<TienCong>(entity =>
            {
                entity.HasKey(e => e.MaTienCong);

                entity.Property(e => e.TenTienCong)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.DonGia)
                      .HasColumnType("decimal(18,0)");
            });

            // -----------------------------------------------------------------
            // PHIEUSUACHUA
            // -----------------------------------------------------------------
            modelBuilder.Entity<PhieuSuaChua>(entity =>
            {
                entity.HasKey(e => e.MaPhieuSuaChua);

                entity.Property(e => e.NgaySuaChua)
                      .IsRequired();

                entity.Property(e => e.TongTien)
                      .HasColumnType("decimal(18,0)")
                      .HasDefaultValue(0);

                entity.Property(e => e.TienGiamGia)
                      .HasColumnType("decimal(18,0)")
                      .HasDefaultValue(0);

                // FK: PhieuSuaChua -> Xe (n - 1)
                entity.HasOne(e => e.Xe)
                      .WithMany(x => x.DanhSachPhieuSuaChua)
                      .HasForeignKey(e => e.MaXe)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -----------------------------------------------------------------
            // CT_PHIEUSUACHUA
            // -----------------------------------------------------------------
            modelBuilder.Entity<ChiTietPhieuSuaChua>(entity =>
            {
                entity.HasKey(e => e.MaCTSC);

                entity.Property(e => e.NoiDungSuaChua)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.DonGia)
                      .HasColumnType("decimal(18,0)");

                entity.Property(e => e.ThanhTien)
                      .HasColumnType("decimal(18,0)");

                // FK: CT_PhieuSuaChua -> PhieuSuaChua (n - 1)
                entity.HasOne(e => e.PhieuSuaChua)
                      .WithMany(p => p.DanhSachCTPhieuSuaChua)
                      .HasForeignKey(e => e.MaPhieuSuaChua)
                      .OnDelete(DeleteBehavior.Cascade);

                // FK: CT_PhieuSuaChua -> VatTuPhuTung (n - 1, nullable)
                entity.HasOne(e => e.VatTuPhuTung)
                      .WithMany(v => v.DanhSachCTPhieuSuaChua)
                      .HasForeignKey(e => e.MaVTPT)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);

                // FK: CT_PhieuSuaChua -> TienCong (n - 1, nullable)
                entity.HasOne(e => e.TienCong)
                      .WithMany(t => t.DanhSachCTPhieuSuaChua)
                      .HasForeignKey(e => e.MaTienCong)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -----------------------------------------------------------------
            // PHIEUTHUTIEN
            // -----------------------------------------------------------------
            modelBuilder.Entity<PhieuThuTien>(entity =>
            {
                entity.HasKey(e => e.MaPhieuThuTien);

                entity.Property(e => e.NgayThuTien)
                      .IsRequired();

                entity.Property(e => e.SoTienThu)
                      .HasColumnType("decimal(18,0)");

                // FK: PhieuThuTien -> Xe (n - 1)
                entity.HasOne(e => e.Xe)
                      .WithMany(x => x.DanhSachPhieuThuTien)
                      .HasForeignKey(e => e.MaXe)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -----------------------------------------------------------------
            // PHIEUNHAP
            // -----------------------------------------------------------------
            modelBuilder.Entity<PhieuNhap>(entity =>
            {
                entity.HasKey(e => e.MaPhieuNhap);

                entity.Property(e => e.NgayNhap)
                      .IsRequired();
            });

            // -----------------------------------------------------------------
            // CT_PHIEUNHAP
            // -----------------------------------------------------------------
            modelBuilder.Entity<ChiTietPhieuNhap>(entity =>
            {
                entity.HasKey(e => e.MaCTPN);

                entity.Property(e => e.DonGia)
                      .HasColumnType("decimal(18,0)");

                entity.Property(e => e.ThanhTien)
                      .HasColumnType("decimal(18,0)");

                // FK: CT_PhieuNhap -> PhieuNhap (n - 1)
                entity.HasOne(e => e.PhieuNhap)
                      .WithMany(p => p.DanhSachCTPhieuNhap)
                      .HasForeignKey(e => e.MaPhieuNhap)
                      .OnDelete(DeleteBehavior.Cascade);

                // FK: CT_PhieuNhap -> VatTuPhuTung (n - 1)
                entity.HasOne(e => e.VatTuPhuTung)
                      .WithMany(v => v.DanhSachCTPhieuNhap)
                      .HasForeignKey(e => e.MaVTPT)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -----------------------------------------------------------------
            // THAMSO
            // -----------------------------------------------------------------
            modelBuilder.Entity<ThamSo>(entity =>
            {
                entity.HasKey(e => e.TenThamSo);

                entity.Property(e => e.TenThamSo)
                      .HasMaxLength(100);

                entity.Property(e => e.GiaTri)
                      .IsRequired()
                      .HasMaxLength(200);
            });

            // -----------------------------------------------------------------
            // VAITRO
            // -----------------------------------------------------------------
            modelBuilder.Entity<VaiTro>(entity =>
            {
                entity.HasKey(e => e.MaVaiTro);

                entity.Property(e => e.TenVaiTro)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            // -----------------------------------------------------------------
            // NGUOIDUNG
            // -----------------------------------------------------------------
            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNguoiDung);

                entity.Property(e => e.TenDangNhap)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasIndex(e => e.TenDangNhap)
                      .IsUnique();

                entity.Property(e => e.MatKhau)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.TenNguoiDung)
                      .IsRequired()
                      .HasMaxLength(100);

                // FK: NguoiDung -> VaiTro (n - 1)
                entity.HasOne(e => e.VaiTro)
                      .WithMany(v => v.DanhSachNguoiDung)
                      .HasForeignKey(e => e.MaVaiTro)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // =================================================================
            // SEED DATA — Dữ liệu mẫu
            // =================================================================

            // --- Vai trò ---
            modelBuilder.Entity<VaiTro>().HasData(
                new VaiTro { MaVaiTro = 1, TenVaiTro = "QuanLy" },
                new VaiTro { MaVaiTro = 2, TenVaiTro = "NhanVien" }
            );

            // --- Tài khoản mẫu ---
            modelBuilder.Entity<NguoiDung>().HasData(
                new NguoiDung
                {
                    MaNguoiDung = 1,
                    TenDangNhap = "admin",
                    MatKhau = "admin123",
                    TenNguoiDung = "Quản Lý",
                    MaVaiTro = 1  // QuanLy
                },
                new NguoiDung
                {
                    MaNguoiDung = 2,
                    TenDangNhap = "nhanvien",
                    MatKhau = "nv123",
                    TenNguoiDung = "Nhân Viên",
                    MaVaiTro = 2  // NhanVien
                }
            );

            // --- Tham số hệ thống ---
            modelBuilder.Entity<ThamSo>().HasData(
                new ThamSo
                {
                    TenThamSo = "SoXeSuaChuaToiDa",
                    GiaTri = "30"
                },
                new ThamSo
                {
                    TenThamSo = "TiLeDonGiaBan",
                    GiaTri = "1.05"
                },
                new ThamSo
                {
                    TenThamSo = "ApDungQDKiemTraSoTienThu",
                    GiaTri = "1"
                }
            );

            // --- Danh mục Hiệu Xe ---
            var tenHieuXes = new[] { "Toyota", "Honda", "Ford", "Hyundai", "Kia", "Mazda", "Mitsubishi", "Nissan", "Chevrolet", "Suzuki" };
            var hieuXes = tenHieuXes.Select((name, index) => new HieuXe { MaHieuXe = index + 1, TenHieuXe = name }).ToArray();
            modelBuilder.Entity<HieuXe>().HasData(hieuXes);

            // Dùng Random có seed cố định để tránh EF Core báo lỗi tạo lại data mỗi lần Migration
            var random = new Random(2024);

            // --- Danh mục Vật Tư Phụ Tùng (Tổ hợp 200 record) ---
            var loaiVatTus = new[] { "Bugi", "Lốp xe", "Má phanh", "Lọc nhớt", "Lọc gió", "Bình ắc quy", "Phuộc giảm xóc", "Gạt mưa", "Bóng đèn", "Bơm xăng", "Dây curoa", "Rotuyn", "Kính chiếu hậu", "Mâm đúc", "Mobin" };
            var thuongHieus = new[] { "Michelin", "Denso", "Bosch", "Brembo", "NGK", "Yuasa", "Varta", "K&N", "Mobil", "Castrol" };
            var kichCos = new[] { "Loại 1", "Loại 2", "Cao cấp" }; 

            var vatTus = loaiVatTus
                .SelectMany(l => thuongHieus, (l, t) => $"{l} {t}")
                .SelectMany(name => kichCos, (name, k) => $"{name} {k}")
                .Take(200) // Cắt đúng 200 dòng theo yêu cầu
                .Select((ten, index) => new VatTuPhuTung
                {
                    MaVTPT = index + 1,
                    TenVTPT = ten,
                    // Random từ 50,000 đến 1,500,000 (làm tròn hàng ngàn)
                    DonGia = random.Next(50, 1500) * 1000m,
                    SoLuongTon = random.Next(10, 51)
                }).ToArray();
            
            modelBuilder.Entity<VatTuPhuTung>().HasData(vatTus);

            // --- Danh mục Tiền Công (Tổ hợp 100 record) ---
            var hanhDongs = new[] { "Thay", "Vệ sinh", "Bảo dưỡng", "Cân chỉnh", "Phục hồi", "Kiểm tra", "Đánh bóng", "Sơn", "Gò", "Tháo lắp" };
            var boPhans = new[] { "nhớt máy", "kim phun", "phanh 4 bánh", "thước lái", "dàn lạnh", "bugi", "lọc gió", "curoa", "bơm nước", "hộp số", "điều hòa", "bơm xăng" };

            var tienCongs = hanhDongs
                .SelectMany(h => boPhans, (h, b) => $"{h} {b}")
                .Take(98) // Lấy 98 dòng
                .Select((ten, index) => new TienCong
                {
                    MaTienCong = index + 3, // Bắt đầu từ ID = 3
                    TenTienCong = ten,
                    // Random từ 50,000 đến 500,000 (làm tròn hàng ngàn)
                    DonGia = random.Next(50, 500) * 1000m
                }).ToList();

            // Chèn "Không tính công" và "Bảo hành" vào đầu danh sách
            tienCongs.Insert(0, new TienCong
            {
                MaTienCong = 2,
                TenTienCong = "Bảo hành",
                DonGia = 0
            });
            tienCongs.Insert(0, new TienCong
            {
                MaTienCong = 1,
                TenTienCong = "Không tính công",
                DonGia = 0
            });

            modelBuilder.Entity<TienCong>().HasData(tienCongs);
        }
    }
}
