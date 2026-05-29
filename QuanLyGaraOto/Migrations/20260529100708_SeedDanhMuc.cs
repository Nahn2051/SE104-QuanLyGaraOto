using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyGaraOto.Migrations
{
    /// <inheritdoc />
    public partial class SeedDanhMuc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HIEUXE",
                columns: new[] { "MaHieuXe", "TenHieuXe" },
                values: new object[,]
                {
                    { 1, "Toyota" },
                    { 2, "Honda" },
                    { 3, "Ford" },
                    { 4, "Hyundai" },
                    { 5, "Kia" },
                    { 6, "Mazda" },
                    { 7, "Mitsubishi" },
                    { 8, "Nissan" },
                    { 9, "Chevrolet" },
                    { 10, "Suzuki" }
                });

            migrationBuilder.InsertData(
                table: "THAMSO",
                columns: new[] { "TenThamSo", "GiaTri" },
                values: new object[,]
                {
                    { "ApDungQDKiemTraSoTienThu", "1" },
                    { "SoXeSuaChuaToiDa", "30" },
                    { "TiLeDonGiaBan", "1.05" }
                });

            migrationBuilder.InsertData(
                table: "TIENCONG",
                columns: new[] { "MaTienCong", "DonGia", "TenTienCong" },
                values: new object[,]
                {
                    { 1, 405000m, "Thay nhớt máy" },
                    { 2, 313000m, "Thay kim phun" },
                    { 3, 443000m, "Thay phanh 4 bánh" },
                    { 4, 498000m, "Thay thước lái" },
                    { 5, 221000m, "Thay dàn lạnh" },
                    { 6, 247000m, "Thay bugi" },
                    { 7, 138000m, "Thay lọc gió" },
                    { 8, 312000m, "Thay curoa" },
                    { 9, 196000m, "Thay bơm nước" },
                    { 10, 333000m, "Thay hộp số" },
                    { 11, 57000m, "Thay điều hòa" },
                    { 12, 257000m, "Thay bơm xăng" },
                    { 13, 175000m, "Vệ sinh nhớt máy" },
                    { 14, 237000m, "Vệ sinh kim phun" },
                    { 15, 433000m, "Vệ sinh phanh 4 bánh" },
                    { 16, 199000m, "Vệ sinh thước lái" },
                    { 17, 344000m, "Vệ sinh dàn lạnh" },
                    { 18, 460000m, "Vệ sinh bugi" },
                    { 19, 453000m, "Vệ sinh lọc gió" },
                    { 20, 472000m, "Vệ sinh curoa" },
                    { 21, 189000m, "Vệ sinh bơm nước" },
                    { 22, 176000m, "Vệ sinh hộp số" },
                    { 23, 51000m, "Vệ sinh điều hòa" },
                    { 24, 430000m, "Vệ sinh bơm xăng" },
                    { 25, 134000m, "Bảo dưỡng nhớt máy" },
                    { 26, 425000m, "Bảo dưỡng kim phun" },
                    { 27, 63000m, "Bảo dưỡng phanh 4 bánh" },
                    { 28, 316000m, "Bảo dưỡng thước lái" },
                    { 29, 417000m, "Bảo dưỡng dàn lạnh" },
                    { 30, 428000m, "Bảo dưỡng bugi" },
                    { 31, 201000m, "Bảo dưỡng lọc gió" },
                    { 32, 359000m, "Bảo dưỡng curoa" },
                    { 33, 497000m, "Bảo dưỡng bơm nước" },
                    { 34, 52000m, "Bảo dưỡng hộp số" },
                    { 35, 432000m, "Bảo dưỡng điều hòa" },
                    { 36, 363000m, "Bảo dưỡng bơm xăng" },
                    { 37, 304000m, "Cân chỉnh nhớt máy" },
                    { 38, 227000m, "Cân chỉnh kim phun" },
                    { 39, 90000m, "Cân chỉnh phanh 4 bánh" },
                    { 40, 463000m, "Cân chỉnh thước lái" },
                    { 41, 301000m, "Cân chỉnh dàn lạnh" },
                    { 42, 58000m, "Cân chỉnh bugi" },
                    { 43, 123000m, "Cân chỉnh lọc gió" },
                    { 44, 457000m, "Cân chỉnh curoa" },
                    { 45, 491000m, "Cân chỉnh bơm nước" },
                    { 46, 234000m, "Cân chỉnh hộp số" },
                    { 47, 350000m, "Cân chỉnh điều hòa" },
                    { 48, 310000m, "Cân chỉnh bơm xăng" },
                    { 49, 473000m, "Phục hồi nhớt máy" },
                    { 50, 425000m, "Phục hồi kim phun" },
                    { 51, 418000m, "Phục hồi phanh 4 bánh" },
                    { 52, 142000m, "Phục hồi thước lái" },
                    { 53, 103000m, "Phục hồi dàn lạnh" },
                    { 54, 87000m, "Phục hồi bugi" },
                    { 55, 372000m, "Phục hồi lọc gió" },
                    { 56, 279000m, "Phục hồi curoa" },
                    { 57, 311000m, "Phục hồi bơm nước" },
                    { 58, 62000m, "Phục hồi hộp số" },
                    { 59, 413000m, "Phục hồi điều hòa" },
                    { 60, 296000m, "Phục hồi bơm xăng" },
                    { 61, 234000m, "Kiểm tra nhớt máy" },
                    { 62, 321000m, "Kiểm tra kim phun" },
                    { 63, 395000m, "Kiểm tra phanh 4 bánh" },
                    { 64, 268000m, "Kiểm tra thước lái" },
                    { 65, 181000m, "Kiểm tra dàn lạnh" },
                    { 66, 198000m, "Kiểm tra bugi" },
                    { 67, 259000m, "Kiểm tra lọc gió" },
                    { 68, 173000m, "Kiểm tra curoa" },
                    { 69, 305000m, "Kiểm tra bơm nước" },
                    { 70, 119000m, "Kiểm tra hộp số" },
                    { 71, 394000m, "Kiểm tra điều hòa" },
                    { 72, 166000m, "Kiểm tra bơm xăng" },
                    { 73, 419000m, "Đánh bóng nhớt máy" },
                    { 74, 490000m, "Đánh bóng kim phun" },
                    { 75, 220000m, "Đánh bóng phanh 4 bánh" },
                    { 76, 180000m, "Đánh bóng thước lái" },
                    { 77, 102000m, "Đánh bóng dàn lạnh" },
                    { 78, 94000m, "Đánh bóng bugi" },
                    { 79, 438000m, "Đánh bóng lọc gió" },
                    { 80, 400000m, "Đánh bóng curoa" },
                    { 81, 125000m, "Đánh bóng bơm nước" },
                    { 82, 253000m, "Đánh bóng hộp số" },
                    { 83, 343000m, "Đánh bóng điều hòa" },
                    { 84, 491000m, "Đánh bóng bơm xăng" },
                    { 85, 60000m, "Sơn nhớt máy" },
                    { 86, 109000m, "Sơn kim phun" },
                    { 87, 306000m, "Sơn phanh 4 bánh" },
                    { 88, 459000m, "Sơn thước lái" },
                    { 89, 180000m, "Sơn dàn lạnh" },
                    { 90, 203000m, "Sơn bugi" },
                    { 91, 101000m, "Sơn lọc gió" },
                    { 92, 292000m, "Sơn curoa" },
                    { 93, 314000m, "Sơn bơm nước" },
                    { 94, 294000m, "Sơn hộp số" },
                    { 95, 279000m, "Sơn điều hòa" },
                    { 96, 480000m, "Sơn bơm xăng" },
                    { 97, 163000m, "Gò nhớt máy" },
                    { 98, 355000m, "Gò kim phun" },
                    { 99, 325000m, "Gò phanh 4 bánh" },
                    { 100, 343000m, "Gò thước lái" }
                });

            migrationBuilder.InsertData(
                table: "VATTUPHUTUNG",
                columns: new[] { "MaVTPT", "DonGia", "SoLuongTon", "TenVTPT" },
                values: new object[,]
                {
                    { 1, 216000m, 38, "Bugi Michelin Loại 1" },
                    { 2, 803000m, 33, "Bugi Michelin Loại 2" },
                    { 3, 69000m, 23, "Bugi Michelin Cao cấp" },
                    { 4, 1170000m, 44, "Bugi Denso Loại 1" },
                    { 5, 601000m, 35, "Bugi Denso Loại 2" },
                    { 6, 680000m, 35, "Bugi Denso Cao cấp" },
                    { 7, 98000m, 29, "Bugi Bosch Loại 1" },
                    { 8, 1101000m, 12, "Bugi Bosch Loại 2" },
                    { 9, 1020000m, 45, "Bugi Bosch Cao cấp" },
                    { 10, 728000m, 10, "Bugi Brembo Loại 1" },
                    { 11, 1132000m, 26, "Bugi Brembo Loại 2" },
                    { 12, 134000m, 14, "Bugi Brembo Cao cấp" },
                    { 13, 142000m, 23, "Bugi NGK Loại 1" },
                    { 14, 192000m, 41, "Bugi NGK Loại 2" },
                    { 15, 922000m, 15, "Bugi NGK Cao cấp" },
                    { 16, 634000m, 34, "Bugi Yuasa Loại 1" },
                    { 17, 334000m, 39, "Bugi Yuasa Loại 2" },
                    { 18, 601000m, 29, "Bugi Yuasa Cao cấp" },
                    { 19, 933000m, 40, "Bugi Varta Loại 1" },
                    { 20, 1437000m, 17, "Bugi Varta Loại 2" },
                    { 21, 516000m, 11, "Bugi Varta Cao cấp" },
                    { 22, 258000m, 23, "Bugi K&N Loại 1" },
                    { 23, 97000m, 18, "Bugi K&N Loại 2" },
                    { 24, 1420000m, 16, "Bugi K&N Cao cấp" },
                    { 25, 1324000m, 40, "Bugi Mobil Loại 1" },
                    { 26, 857000m, 48, "Bugi Mobil Loại 2" },
                    { 27, 886000m, 44, "Bugi Mobil Cao cấp" },
                    { 28, 716000m, 38, "Bugi Castrol Loại 1" },
                    { 29, 975000m, 26, "Bugi Castrol Loại 2" },
                    { 30, 779000m, 38, "Bugi Castrol Cao cấp" },
                    { 31, 383000m, 50, "Lốp xe Michelin Loại 1" },
                    { 32, 411000m, 20, "Lốp xe Michelin Loại 2" },
                    { 33, 373000m, 44, "Lốp xe Michelin Cao cấp" },
                    { 34, 684000m, 22, "Lốp xe Denso Loại 1" },
                    { 35, 174000m, 20, "Lốp xe Denso Loại 2" },
                    { 36, 704000m, 48, "Lốp xe Denso Cao cấp" },
                    { 37, 1379000m, 21, "Lốp xe Bosch Loại 1" },
                    { 38, 1058000m, 39, "Lốp xe Bosch Loại 2" },
                    { 39, 435000m, 40, "Lốp xe Bosch Cao cấp" },
                    { 40, 160000m, 44, "Lốp xe Brembo Loại 1" },
                    { 41, 606000m, 48, "Lốp xe Brembo Loại 2" },
                    { 42, 1348000m, 45, "Lốp xe Brembo Cao cấp" },
                    { 43, 879000m, 29, "Lốp xe NGK Loại 1" },
                    { 44, 66000m, 24, "Lốp xe NGK Loại 2" },
                    { 45, 444000m, 37, "Lốp xe NGK Cao cấp" },
                    { 46, 1268000m, 18, "Lốp xe Yuasa Loại 1" },
                    { 47, 385000m, 21, "Lốp xe Yuasa Loại 2" },
                    { 48, 1443000m, 23, "Lốp xe Yuasa Cao cấp" },
                    { 49, 1183000m, 46, "Lốp xe Varta Loại 1" },
                    { 50, 186000m, 17, "Lốp xe Varta Loại 2" },
                    { 51, 1170000m, 36, "Lốp xe Varta Cao cấp" },
                    { 52, 164000m, 35, "Lốp xe K&N Loại 1" },
                    { 53, 457000m, 35, "Lốp xe K&N Loại 2" },
                    { 54, 78000m, 22, "Lốp xe K&N Cao cấp" },
                    { 55, 246000m, 40, "Lốp xe Mobil Loại 1" },
                    { 56, 687000m, 46, "Lốp xe Mobil Loại 2" },
                    { 57, 535000m, 36, "Lốp xe Mobil Cao cấp" },
                    { 58, 486000m, 22, "Lốp xe Castrol Loại 1" },
                    { 59, 199000m, 25, "Lốp xe Castrol Loại 2" },
                    { 60, 1035000m, 41, "Lốp xe Castrol Cao cấp" },
                    { 61, 1259000m, 12, "Má phanh Michelin Loại 1" },
                    { 62, 94000m, 26, "Má phanh Michelin Loại 2" },
                    { 63, 638000m, 20, "Má phanh Michelin Cao cấp" },
                    { 64, 1069000m, 36, "Má phanh Denso Loại 1" },
                    { 65, 507000m, 25, "Má phanh Denso Loại 2" },
                    { 66, 1403000m, 25, "Má phanh Denso Cao cấp" },
                    { 67, 987000m, 46, "Má phanh Bosch Loại 1" },
                    { 68, 167000m, 40, "Má phanh Bosch Loại 2" },
                    { 69, 1288000m, 20, "Má phanh Bosch Cao cấp" },
                    { 70, 903000m, 48, "Má phanh Brembo Loại 1" },
                    { 71, 698000m, 39, "Má phanh Brembo Loại 2" },
                    { 72, 382000m, 31, "Má phanh Brembo Cao cấp" },
                    { 73, 390000m, 48, "Má phanh NGK Loại 1" },
                    { 74, 1301000m, 33, "Má phanh NGK Loại 2" },
                    { 75, 1458000m, 37, "Má phanh NGK Cao cấp" },
                    { 76, 368000m, 26, "Má phanh Yuasa Loại 1" },
                    { 77, 359000m, 23, "Má phanh Yuasa Loại 2" },
                    { 78, 561000m, 38, "Má phanh Yuasa Cao cấp" },
                    { 79, 938000m, 37, "Má phanh Varta Loại 1" },
                    { 80, 379000m, 11, "Má phanh Varta Loại 2" },
                    { 81, 1383000m, 25, "Má phanh Varta Cao cấp" },
                    { 82, 1477000m, 41, "Má phanh K&N Loại 1" },
                    { 83, 1225000m, 12, "Má phanh K&N Loại 2" },
                    { 84, 414000m, 28, "Má phanh K&N Cao cấp" },
                    { 85, 874000m, 32, "Má phanh Mobil Loại 1" },
                    { 86, 691000m, 44, "Má phanh Mobil Loại 2" },
                    { 87, 1196000m, 39, "Má phanh Mobil Cao cấp" },
                    { 88, 499000m, 15, "Má phanh Castrol Loại 1" },
                    { 89, 1272000m, 30, "Má phanh Castrol Loại 2" },
                    { 90, 305000m, 28, "Má phanh Castrol Cao cấp" },
                    { 91, 614000m, 14, "Lọc nhớt Michelin Loại 1" },
                    { 92, 1025000m, 36, "Lọc nhớt Michelin Loại 2" },
                    { 93, 271000m, 31, "Lọc nhớt Michelin Cao cấp" },
                    { 94, 281000m, 22, "Lọc nhớt Denso Loại 1" },
                    { 95, 827000m, 25, "Lọc nhớt Denso Loại 2" },
                    { 96, 235000m, 17, "Lọc nhớt Denso Cao cấp" },
                    { 97, 101000m, 33, "Lọc nhớt Bosch Loại 1" },
                    { 98, 93000m, 12, "Lọc nhớt Bosch Loại 2" },
                    { 99, 1103000m, 29, "Lọc nhớt Bosch Cao cấp" },
                    { 100, 1089000m, 16, "Lọc nhớt Brembo Loại 1" },
                    { 101, 1052000m, 27, "Lọc nhớt Brembo Loại 2" },
                    { 102, 68000m, 26, "Lọc nhớt Brembo Cao cấp" },
                    { 103, 372000m, 25, "Lọc nhớt NGK Loại 1" },
                    { 104, 936000m, 29, "Lọc nhớt NGK Loại 2" },
                    { 105, 90000m, 19, "Lọc nhớt NGK Cao cấp" },
                    { 106, 1292000m, 14, "Lọc nhớt Yuasa Loại 1" },
                    { 107, 762000m, 41, "Lọc nhớt Yuasa Loại 2" },
                    { 108, 975000m, 42, "Lọc nhớt Yuasa Cao cấp" },
                    { 109, 1068000m, 23, "Lọc nhớt Varta Loại 1" },
                    { 110, 935000m, 21, "Lọc nhớt Varta Loại 2" },
                    { 111, 1365000m, 48, "Lọc nhớt Varta Cao cấp" },
                    { 112, 1369000m, 17, "Lọc nhớt K&N Loại 1" },
                    { 113, 677000m, 20, "Lọc nhớt K&N Loại 2" },
                    { 114, 1218000m, 19, "Lọc nhớt K&N Cao cấp" },
                    { 115, 1065000m, 19, "Lọc nhớt Mobil Loại 1" },
                    { 116, 624000m, 25, "Lọc nhớt Mobil Loại 2" },
                    { 117, 1190000m, 10, "Lọc nhớt Mobil Cao cấp" },
                    { 118, 1169000m, 49, "Lọc nhớt Castrol Loại 1" },
                    { 119, 206000m, 20, "Lọc nhớt Castrol Loại 2" },
                    { 120, 670000m, 41, "Lọc nhớt Castrol Cao cấp" },
                    { 121, 1383000m, 37, "Lọc gió Michelin Loại 1" },
                    { 122, 456000m, 22, "Lọc gió Michelin Loại 2" },
                    { 123, 809000m, 10, "Lọc gió Michelin Cao cấp" },
                    { 124, 1058000m, 21, "Lọc gió Denso Loại 1" },
                    { 125, 1387000m, 19, "Lọc gió Denso Loại 2" },
                    { 126, 586000m, 26, "Lọc gió Denso Cao cấp" },
                    { 127, 1290000m, 27, "Lọc gió Bosch Loại 1" },
                    { 128, 428000m, 40, "Lọc gió Bosch Loại 2" },
                    { 129, 785000m, 43, "Lọc gió Bosch Cao cấp" },
                    { 130, 1469000m, 49, "Lọc gió Brembo Loại 1" },
                    { 131, 880000m, 25, "Lọc gió Brembo Loại 2" },
                    { 132, 1184000m, 42, "Lọc gió Brembo Cao cấp" },
                    { 133, 1259000m, 29, "Lọc gió NGK Loại 1" },
                    { 134, 518000m, 29, "Lọc gió NGK Loại 2" },
                    { 135, 1489000m, 37, "Lọc gió NGK Cao cấp" },
                    { 136, 1050000m, 28, "Lọc gió Yuasa Loại 1" },
                    { 137, 1363000m, 44, "Lọc gió Yuasa Loại 2" },
                    { 138, 571000m, 19, "Lọc gió Yuasa Cao cấp" },
                    { 139, 1010000m, 34, "Lọc gió Varta Loại 1" },
                    { 140, 1014000m, 26, "Lọc gió Varta Loại 2" },
                    { 141, 862000m, 31, "Lọc gió Varta Cao cấp" },
                    { 142, 495000m, 29, "Lọc gió K&N Loại 1" },
                    { 143, 1308000m, 10, "Lọc gió K&N Loại 2" },
                    { 144, 806000m, 24, "Lọc gió K&N Cao cấp" },
                    { 145, 1134000m, 10, "Lọc gió Mobil Loại 1" },
                    { 146, 724000m, 21, "Lọc gió Mobil Loại 2" },
                    { 147, 460000m, 28, "Lọc gió Mobil Cao cấp" },
                    { 148, 341000m, 32, "Lọc gió Castrol Loại 1" },
                    { 149, 1346000m, 30, "Lọc gió Castrol Loại 2" },
                    { 150, 734000m, 11, "Lọc gió Castrol Cao cấp" },
                    { 151, 1057000m, 18, "Bình ắc quy Michelin Loại 1" },
                    { 152, 453000m, 20, "Bình ắc quy Michelin Loại 2" },
                    { 153, 834000m, 48, "Bình ắc quy Michelin Cao cấp" },
                    { 154, 756000m, 10, "Bình ắc quy Denso Loại 1" },
                    { 155, 162000m, 11, "Bình ắc quy Denso Loại 2" },
                    { 156, 174000m, 47, "Bình ắc quy Denso Cao cấp" },
                    { 157, 281000m, 33, "Bình ắc quy Bosch Loại 1" },
                    { 158, 639000m, 11, "Bình ắc quy Bosch Loại 2" },
                    { 159, 158000m, 22, "Bình ắc quy Bosch Cao cấp" },
                    { 160, 1387000m, 44, "Bình ắc quy Brembo Loại 1" },
                    { 161, 1438000m, 49, "Bình ắc quy Brembo Loại 2" },
                    { 162, 1115000m, 49, "Bình ắc quy Brembo Cao cấp" },
                    { 163, 341000m, 26, "Bình ắc quy NGK Loại 1" },
                    { 164, 278000m, 28, "Bình ắc quy NGK Loại 2" },
                    { 165, 970000m, 43, "Bình ắc quy NGK Cao cấp" },
                    { 166, 537000m, 17, "Bình ắc quy Yuasa Loại 1" },
                    { 167, 241000m, 35, "Bình ắc quy Yuasa Loại 2" },
                    { 168, 1093000m, 24, "Bình ắc quy Yuasa Cao cấp" },
                    { 169, 421000m, 12, "Bình ắc quy Varta Loại 1" },
                    { 170, 1395000m, 48, "Bình ắc quy Varta Loại 2" },
                    { 171, 798000m, 30, "Bình ắc quy Varta Cao cấp" },
                    { 172, 443000m, 39, "Bình ắc quy K&N Loại 1" },
                    { 173, 1410000m, 33, "Bình ắc quy K&N Loại 2" },
                    { 174, 229000m, 39, "Bình ắc quy K&N Cao cấp" },
                    { 175, 129000m, 16, "Bình ắc quy Mobil Loại 1" },
                    { 176, 721000m, 33, "Bình ắc quy Mobil Loại 2" },
                    { 177, 873000m, 36, "Bình ắc quy Mobil Cao cấp" },
                    { 178, 174000m, 39, "Bình ắc quy Castrol Loại 1" },
                    { 179, 742000m, 22, "Bình ắc quy Castrol Loại 2" },
                    { 180, 130000m, 15, "Bình ắc quy Castrol Cao cấp" },
                    { 181, 1168000m, 11, "Phuộc giảm xóc Michelin Loại 1" },
                    { 182, 607000m, 20, "Phuộc giảm xóc Michelin Loại 2" },
                    { 183, 1056000m, 47, "Phuộc giảm xóc Michelin Cao cấp" },
                    { 184, 1168000m, 32, "Phuộc giảm xóc Denso Loại 1" },
                    { 185, 1275000m, 12, "Phuộc giảm xóc Denso Loại 2" },
                    { 186, 1183000m, 10, "Phuộc giảm xóc Denso Cao cấp" },
                    { 187, 608000m, 50, "Phuộc giảm xóc Bosch Loại 1" },
                    { 188, 506000m, 28, "Phuộc giảm xóc Bosch Loại 2" },
                    { 189, 1069000m, 10, "Phuộc giảm xóc Bosch Cao cấp" },
                    { 190, 94000m, 36, "Phuộc giảm xóc Brembo Loại 1" },
                    { 191, 459000m, 28, "Phuộc giảm xóc Brembo Loại 2" },
                    { 192, 614000m, 29, "Phuộc giảm xóc Brembo Cao cấp" },
                    { 193, 569000m, 40, "Phuộc giảm xóc NGK Loại 1" },
                    { 194, 926000m, 30, "Phuộc giảm xóc NGK Loại 2" },
                    { 195, 826000m, 50, "Phuộc giảm xóc NGK Cao cấp" },
                    { 196, 1311000m, 48, "Phuộc giảm xóc Yuasa Loại 1" },
                    { 197, 1492000m, 42, "Phuộc giảm xóc Yuasa Loại 2" },
                    { 198, 292000m, 29, "Phuộc giảm xóc Yuasa Cao cấp" },
                    { 199, 221000m, 10, "Phuộc giảm xóc Varta Loại 1" },
                    { 200, 83000m, 11, "Phuộc giảm xóc Varta Loại 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HIEUXE",
                keyColumn: "MaHieuXe",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HIEUXE",
                keyColumn: "MaHieuXe",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HIEUXE",
                keyColumn: "MaHieuXe",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HIEUXE",
                keyColumn: "MaHieuXe",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HIEUXE",
                keyColumn: "MaHieuXe",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "HIEUXE",
                keyColumn: "MaHieuXe",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "HIEUXE",
                keyColumn: "MaHieuXe",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "HIEUXE",
                keyColumn: "MaHieuXe",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "HIEUXE",
                keyColumn: "MaHieuXe",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "HIEUXE",
                keyColumn: "MaHieuXe",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "THAMSO",
                keyColumn: "TenThamSo",
                keyValue: "ApDungQDKiemTraSoTienThu");

            migrationBuilder.DeleteData(
                table: "THAMSO",
                keyColumn: "TenThamSo",
                keyValue: "SoXeSuaChuaToiDa");

            migrationBuilder.DeleteData(
                table: "THAMSO",
                keyColumn: "TenThamSo",
                keyValue: "TiLeDonGiaBan");

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "TIENCONG",
                keyColumn: "MaTienCong",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "VATTUPHUTUNG",
                keyColumn: "MaVTPT",
                keyValue: 200);
        }
    }
}
