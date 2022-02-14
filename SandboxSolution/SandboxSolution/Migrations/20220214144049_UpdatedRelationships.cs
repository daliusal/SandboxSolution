using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SandboxSolution.Migrations
{
    public partial class UpdatedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePublisher");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Publisher_PublisherId",
                table: "Games",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Publisher_PublisherId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_PublisherId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GamePublisher",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    PublishersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePublisher", x => new { x.GamesId, x.PublishersId });
                    table.ForeignKey(
                        name: "FK_GamePublisher_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePublisher_Publisher_PublishersId",
                        column: x => x.PublishersId,
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePublisher_PublishersId",
                table: "GamePublisher",
                column: "PublishersId");
        }
    }
}
