using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyGaraOto.Migrations
{
    /// <inheritdoc />
    public partial class SeedVaiTroVaNguoiDung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VAITRO",
                columns: new[] { "MaVaiTro", "TenVaiTro" },
                values: new object[,]
                {
                    { 1, "QuanLy" },
                    { 2, "NhanVien" }
                });

            migrationBuilder.InsertData(
                table: "NGUOIDUNG",
                columns: new[] { "MaNguoiDung", "MaVaiTro", "MatKhau", "TenDangNhap", "TenNguoiDung" },
                values: new object[,]
                {
                    { 1, 1, "admin123", "admin", "Quản Lý" },
                    { 2, 2, "nv123", "nhanvien", "Nhân Viên" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NGUOIDUNG",
                keyColumn: "MaNguoiDung",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NGUOIDUNG",
                keyColumn: "MaNguoiDung",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VAITRO",
                keyColumn: "MaVaiTro",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VAITRO",
                keyColumn: "MaVaiTro",
                keyValue: 2);
        }
    }
}
