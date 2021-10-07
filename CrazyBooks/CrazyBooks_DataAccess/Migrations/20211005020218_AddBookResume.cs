using Microsoft.EntityFrameworkCore.Migrations;

namespace CrazyBooks_DataAccess.Migrations
{
    public partial class AddBookResume : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resume",
                table: "Book");
        }
    }
}
