using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyGaraOto.Migrations
{
    /// <inheritdoc />
    public partial class AddGiamGiaPhieuSuaChua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TienGiamGia",
                table: "PHIEUSUACHUA",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TienGiamGia",
                table: "PHIEUSUACHUA");
        }
    }
}
