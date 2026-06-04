using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyGaraOto.Migrations
{
    /// <inheritdoc />
    public partial class AddKhongTinhCong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 1,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 0m, "Không tính công" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 2,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 405000m, "Thay nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 3,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 313000m, "Thay kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 4,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 443000m, "Thay phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 5,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 498000m, "Thay thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 6,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 221000m, "Thay dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 7,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 247000m, "Thay bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 8,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 138000m, "Thay lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 9,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 312000m, "Thay curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 10,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 196000m, "Thay bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 11,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 333000m, "Thay hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 12,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 57000m, "Thay điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 13,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 257000m, "Thay bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 14,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 175000m, "Vệ sinh nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 15,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 237000m, "Vệ sinh kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 16,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 433000m, "Vệ sinh phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 17,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 199000m, "Vệ sinh thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 18,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 344000m, "Vệ sinh dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 19,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 460000m, "Vệ sinh bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 20,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 453000m, "Vệ sinh lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 21,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 472000m, "Vệ sinh curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 22,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 189000m, "Vệ sinh bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 23,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 176000m, "Vệ sinh hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 24,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 51000m, "Vệ sinh điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 25,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 430000m, "Vệ sinh bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 26,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 134000m, "Bảo dưỡng nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 27,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 425000m, "Bảo dưỡng kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 28,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 63000m, "Bảo dưỡng phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 29,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 316000m, "Bảo dưỡng thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 30,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 417000m, "Bảo dưỡng dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 31,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 428000m, "Bảo dưỡng bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 32,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 201000m, "Bảo dưỡng lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 33,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 359000m, "Bảo dưỡng curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 34,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 497000m, "Bảo dưỡng bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 35,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 52000m, "Bảo dưỡng hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 36,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 432000m, "Bảo dưỡng điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 37,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 363000m, "Bảo dưỡng bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 38,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 304000m, "Cân chỉnh nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 39,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 227000m, "Cân chỉnh kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 40,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 90000m, "Cân chỉnh phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 41,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 463000m, "Cân chỉnh thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 42,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 301000m, "Cân chỉnh dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 43,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 58000m, "Cân chỉnh bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 44,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 123000m, "Cân chỉnh lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 45,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 457000m, "Cân chỉnh curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 46,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 491000m, "Cân chỉnh bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 47,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 234000m, "Cân chỉnh hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 48,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 350000m, "Cân chỉnh điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 49,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 310000m, "Cân chỉnh bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 50,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 473000m, "Phục hồi nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 51,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 425000m, "Phục hồi kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 52,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 418000m, "Phục hồi phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 53,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 142000m, "Phục hồi thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 54,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 103000m, "Phục hồi dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 55,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 87000m, "Phục hồi bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 56,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 372000m, "Phục hồi lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 57,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 279000m, "Phục hồi curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 58,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 311000m, "Phục hồi bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 59,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 62000m, "Phục hồi hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 60,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 413000m, "Phục hồi điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 61,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 296000m, "Phục hồi bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 62,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 234000m, "Kiểm tra nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 63,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 321000m, "Kiểm tra kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 64,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 395000m, "Kiểm tra phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 65,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 268000m, "Kiểm tra thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 66,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 181000m, "Kiểm tra dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 67,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 198000m, "Kiểm tra bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 68,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 259000m, "Kiểm tra lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 69,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 173000m, "Kiểm tra curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 70,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 305000m, "Kiểm tra bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 71,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 119000m, "Kiểm tra hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 72,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 394000m, "Kiểm tra điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 73,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 166000m, "Kiểm tra bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 74,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 419000m, "Đánh bóng nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 75,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 490000m, "Đánh bóng kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 76,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 220000m, "Đánh bóng phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 77,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 180000m, "Đánh bóng thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 78,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 102000m, "Đánh bóng dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 79,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 94000m, "Đánh bóng bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 80,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 438000m, "Đánh bóng lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 81,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 400000m, "Đánh bóng curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 82,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 125000m, "Đánh bóng bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 83,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 253000m, "Đánh bóng hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 84,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 343000m, "Đánh bóng điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 85,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 491000m, "Đánh bóng bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 86,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 60000m, "Sơn nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 87,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 109000m, "Sơn kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 88,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 306000m, "Sơn phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 89,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 459000m, "Sơn thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 90,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 180000m, "Sơn dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 91,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 203000m, "Sơn bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 92,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 101000m, "Sơn lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 93,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 292000m, "Sơn curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 94,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 314000m, "Sơn bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 95,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 294000m, "Sơn hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 96,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 279000m, "Sơn điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 97,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 480000m, "Sơn bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 98,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 163000m, "Gò nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 99,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 355000m, "Gò kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 100,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 325000m, "Gò phanh 4 bánh" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 1,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 405000m, "Thay nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 2,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 313000m, "Thay kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 3,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 443000m, "Thay phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 4,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 498000m, "Thay thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 5,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 221000m, "Thay dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 6,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 247000m, "Thay bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 7,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 138000m, "Thay lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 8,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 312000m, "Thay curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 9,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 196000m, "Thay bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 10,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 333000m, "Thay hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 11,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 57000m, "Thay điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 12,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 257000m, "Thay bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 13,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 175000m, "Vệ sinh nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 14,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 237000m, "Vệ sinh kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 15,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 433000m, "Vệ sinh phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 16,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 199000m, "Vệ sinh thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 17,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 344000m, "Vệ sinh dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 18,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 460000m, "Vệ sinh bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 19,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 453000m, "Vệ sinh lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 20,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 472000m, "Vệ sinh curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 21,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 189000m, "Vệ sinh bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 22,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 176000m, "Vệ sinh hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 23,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 51000m, "Vệ sinh điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 24,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 430000m, "Vệ sinh bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 25,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 134000m, "Bảo dưỡng nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 26,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 425000m, "Bảo dưỡng kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 27,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 63000m, "Bảo dưỡng phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 28,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 316000m, "Bảo dưỡng thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 29,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 417000m, "Bảo dưỡng dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 30,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 428000m, "Bảo dưỡng bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 31,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 201000m, "Bảo dưỡng lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 32,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 359000m, "Bảo dưỡng curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 33,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 497000m, "Bảo dưỡng bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 34,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 52000m, "Bảo dưỡng hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 35,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 432000m, "Bảo dưỡng điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 36,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 363000m, "Bảo dưỡng bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 37,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 304000m, "Cân chỉnh nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 38,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 227000m, "Cân chỉnh kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 39,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 90000m, "Cân chỉnh phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 40,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 463000m, "Cân chỉnh thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 41,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 301000m, "Cân chỉnh dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 42,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 58000m, "Cân chỉnh bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 43,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 123000m, "Cân chỉnh lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 44,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 457000m, "Cân chỉnh curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 45,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 491000m, "Cân chỉnh bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 46,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 234000m, "Cân chỉnh hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 47,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 350000m, "Cân chỉnh điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 48,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 310000m, "Cân chỉnh bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 49,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 473000m, "Phục hồi nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 50,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 425000m, "Phục hồi kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 51,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 418000m, "Phục hồi phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 52,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 142000m, "Phục hồi thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 53,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 103000m, "Phục hồi dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 54,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 87000m, "Phục hồi bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 55,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 372000m, "Phục hồi lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 56,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 279000m, "Phục hồi curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 57,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 311000m, "Phục hồi bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 58,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 62000m, "Phục hồi hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 59,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 413000m, "Phục hồi điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 60,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 296000m, "Phục hồi bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 61,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 234000m, "Kiểm tra nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 62,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 321000m, "Kiểm tra kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 63,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 395000m, "Kiểm tra phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 64,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 268000m, "Kiểm tra thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 65,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 181000m, "Kiểm tra dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 66,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 198000m, "Kiểm tra bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 67,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 259000m, "Kiểm tra lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 68,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 173000m, "Kiểm tra curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 69,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 305000m, "Kiểm tra bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 70,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 119000m, "Kiểm tra hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 71,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 394000m, "Kiểm tra điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 72,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 166000m, "Kiểm tra bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 73,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 419000m, "Đánh bóng nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 74,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 490000m, "Đánh bóng kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 75,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 220000m, "Đánh bóng phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 76,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 180000m, "Đánh bóng thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 77,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 102000m, "Đánh bóng dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 78,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 94000m, "Đánh bóng bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 79,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 438000m, "Đánh bóng lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 80,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 400000m, "Đánh bóng curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 81,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 125000m, "Đánh bóng bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 82,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 253000m, "Đánh bóng hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 83,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 343000m, "Đánh bóng điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 84,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 491000m, "Đánh bóng bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 85,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 60000m, "Sơn nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 86,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 109000m, "Sơn kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 87,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 306000m, "Sơn phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 88,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 459000m, "Sơn thước lái" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 89,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 180000m, "Sơn dàn lạnh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 90,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 203000m, "Sơn bugi" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 91,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 101000m, "Sơn lọc gió" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 92,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 292000m, "Sơn curoa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 93,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 314000m, "Sơn bơm nước" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 94,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 294000m, "Sơn hộp số" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 95,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 279000m, "Sơn điều hòa" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 96,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 480000m, "Sơn bơm xăng" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 97,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 163000m, "Gò nhớt máy" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 98,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 355000m, "Gò kim phun" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 99,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 325000m, "Gò phanh 4 bánh" });

            migrationBuilder.UpdateData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 100,
                columns: new[] { "DonGia", "TenTienCong" },
                values: new object[] { 343000m, "Gò thước lái" });
        }
    }
}
