using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyGaraOto.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HIEUXE",
                columns: table => new
                {
                    MaHieuXe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHieuXe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HIEUXE", x => x.MaHieuXe);
                });

            migrationBuilder.CreateTable(
                name: "PHIEUNHAP",
                columns: table => new
                {
                    MaPhieuNhap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHIEUNHAP", x => x.MaPhieuNhap);
                });

            migrationBuilder.CreateTable(
                name: "THAMSO",
                columns: table => new
                {
                    TenThamSo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GiaTri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THAMSO", x => x.TenThamSo);
                });

            migrationBuilder.CreateTable(
                name: "TIENCONG",
                columns: table => new
                {
                    MaTienCong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTienCong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIENCONG", x => x.MaTienCong);
                });

            migrationBuilder.CreateTable(
                name: "VAITRO",
                columns: table => new
                {
                    MaVaiTro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VAITRO", x => x.MaVaiTro);
                });

            migrationBuilder.CreateTable(
                name: "VATTUPHUTUNG",
                columns: table => new
                {
                    MaVTPT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVTPT = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VATTUPHUTUNG", x => x.MaVTPT);
                });

            migrationBuilder.CreateTable(
                name: "XE",
                columns: table => new
                {
                    MaXe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BienSo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MaHieuXe = table.Column<int>(type: "int", nullable: false),
                    TenChuXe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NgayTiepNhan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TienNo = table.Column<decimal>(type: "decimal(18,0)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XE", x => x.MaXe);
                    table.ForeignKey(
                        name: "FK_XE_HIEUXE_MaHieuXe",
                        column: x => x.MaHieuXe,
                        principalTable: "HIEUXE",
                        principalColumn: "MaHieuXe",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NGUOIDUNG",
                columns: table => new
                {
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TenNguoiDung = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaVaiTro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NGUOIDUNG", x => x.MaNguoiDung);
                    table.ForeignKey(
                        name: "FK_NGUOIDUNG_VAITRO_MaVaiTro",
                        column: x => x.MaVaiTro,
                        principalTable: "VAITRO",
                        principalColumn: "MaVaiTro",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CT_PHIEUNHAP",
                columns: table => new
                {
                    MaCTPN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieuNhap = table.Column<int>(type: "int", nullable: false),
                    MaVTPT = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_PHIEUNHAP", x => x.MaCTPN);
                    table.ForeignKey(
                        name: "FK_CT_PHIEUNHAP_PHIEUNHAP_MaPhieuNhap",
                        column: x => x.MaPhieuNhap,
                        principalTable: "PHIEUNHAP",
                        principalColumn: "MaPhieuNhap",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_PHIEUNHAP_VATTUPHUTUNG_MaVTPT",
                        column: x => x.MaVTPT,
                        principalTable: "VATTUPHUTUNG",
                        principalColumn: "MaVTPT",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PHIEUSUACHUA",
                columns: table => new
                {
                    MaPhieuSuaChua = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgaySuaChua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaXe = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,0)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHIEUSUACHUA", x => x.MaPhieuSuaChua);
                    table.ForeignKey(
                        name: "FK_PHIEUSUACHUA_XE_MaXe",
                        column: x => x.MaXe,
                        principalTable: "XE",
                        principalColumn: "MaXe",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PHIEUTHUTIEN",
                columns: table => new
                {
                    MaPhieuThuTien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaXe = table.Column<int>(type: "int", nullable: false),
                    NgayThuTien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTienThu = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHIEUTHUTIEN", x => x.MaPhieuThuTien);
                    table.ForeignKey(
                        name: "FK_PHIEUTHUTIEN_XE_MaXe",
                        column: x => x.MaXe,
                        principalTable: "XE",
                        principalColumn: "MaXe",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CT_PHIEUSUACHUA",
                columns: table => new
                {
                    MaCTSC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieuSuaChua = table.Column<int>(type: "int", nullable: false),
                    NoiDungSuaChua = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaVTPT = table.Column<int>(type: "int", nullable: true),
                    MaTienCong = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_PHIEUSUACHUA", x => x.MaCTSC);
                    table.ForeignKey(
                        name: "FK_CT_PHIEUSUACHUA_PHIEUSUACHUA_MaPhieuSuaChua",
                        column: x => x.MaPhieuSuaChua,
                        principalTable: "PHIEUSUACHUA",
                        principalColumn: "MaPhieuSuaChua",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_PHIEUSUACHUA_TIENCONG_MaTienCong",
                        column: x => x.MaTienCong,
                        principalTable: "TIENCONG",
                        principalColumn: "MaTienCong",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CT_PHIEUSUACHUA_VATTUPHUTUNG_MaVTPT",
                        column: x => x.MaVTPT,
                        principalTable: "VATTUPHUTUNG",
                        principalColumn: "MaVTPT",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CT_PHIEUNHAP_MaPhieuNhap",
                table: "CT_PHIEUNHAP",
                column: "MaPhieuNhap");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PHIEUNHAP_MaVTPT",
                table: "CT_PHIEUNHAP",
                column: "MaVTPT");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PHIEUSUACHUA_MaPhieuSuaChua",
                table: "CT_PHIEUSUACHUA",
                column: "MaPhieuSuaChua");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PHIEUSUACHUA_MaTienCong",
                table: "CT_PHIEUSUACHUA",
                column: "MaTienCong");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PHIEUSUACHUA_MaVTPT",
                table: "CT_PHIEUSUACHUA",
                column: "MaVTPT");

            migrationBuilder.CreateIndex(
                name: "IX_NGUOIDUNG_MaVaiTro",
                table: "NGUOIDUNG",
                column: "MaVaiTro");

            migrationBuilder.CreateIndex(
                name: "IX_NGUOIDUNG_TenDangNhap",
                table: "NGUOIDUNG",
                column: "TenDangNhap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PHIEUSUACHUA_MaXe",
                table: "PHIEUSUACHUA",
                column: "MaXe");

            migrationBuilder.CreateIndex(
                name: "IX_PHIEUTHUTIEN_MaXe",
                table: "PHIEUTHUTIEN",
                column: "MaXe");

            migrationBuilder.CreateIndex(
                name: "IX_XE_BienSo",
                table: "XE",
                column: "BienSo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XE_MaHieuXe",
                table: "XE",
                column: "MaHieuXe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CT_PHIEUNHAP");

            migrationBuilder.DropTable(
                name: "CT_PHIEUSUACHUA");

            migrationBuilder.DropTable(
                name: "NGUOIDUNG");

            migrationBuilder.DropTable(
                name: "PHIEUTHUTIEN");

            migrationBuilder.DropTable(
                name: "THAMSO");

            migrationBuilder.DropTable(
                name: "PHIEUNHAP");

            migrationBuilder.DropTable(
                name: "PHIEUSUACHUA");

            migrationBuilder.DropTable(
                name: "TIENCONG");

            migrationBuilder.DropTable(
                name: "VATTUPHUTUNG");

            migrationBuilder.DropTable(
                name: "VAITRO");

            migrationBuilder.DropTable(
                name: "XE");

            migrationBuilder.DropTable(
                name: "HIEUXE");
        }
    }
}
