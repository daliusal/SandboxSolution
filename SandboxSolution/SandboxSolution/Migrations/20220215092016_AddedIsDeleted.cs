using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SandboxSolution.Migrations
{
    public partial class AddedIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Publisher",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Publisher");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Games");
        }
    }
}
