using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyGaraOto.Migrations
{
    /// <inheritdoc />
    public partial class AddTienNoTruocThu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TienNoTruocThu",
                table: "PHIEUTHUTIEN",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TienNoTruocThu",
                table: "PHIEUTHUTIEN");
        }
    }
}
