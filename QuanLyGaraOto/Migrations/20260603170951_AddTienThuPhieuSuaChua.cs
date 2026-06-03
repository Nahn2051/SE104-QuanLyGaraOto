using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyGaraOto.Migrations
{
    /// <inheritdoc />
    public partial class AddTienThuPhieuSuaChua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TienThu",
                table: "PHIEUSUACHUA",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TienThu",
                table: "PHIEUSUACHUA");
        }
    }
}
