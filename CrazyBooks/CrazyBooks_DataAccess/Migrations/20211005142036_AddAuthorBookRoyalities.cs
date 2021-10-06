using Microsoft.EntityFrameworkCore.Migrations;

namespace CrazyBooks_DataAccess.Migrations
{
    public partial class AddAuthorBookRoyalities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PCRoyalties",
                table: "AuthorBook",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PCRoyalties",
                table: "AuthorBook");
        }
    }
}
