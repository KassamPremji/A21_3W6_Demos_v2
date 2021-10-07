using Microsoft.EntityFrameworkCore.Migrations;

namespace CrazyBooks_DataAccess.Migrations
{
    public partial class updateDiff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Promo",
                table: "Book",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Promo",
                table: "Book");
        }
    }
}
