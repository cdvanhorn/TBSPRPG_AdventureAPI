using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AdventureApi.Migrations
{
    public partial class AddLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdventureService.Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Initial = table.Column<bool>(type: "boolean", nullable: false),
                    AdventureId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureService.Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdventureService.Location_AdventureService.Adventure_Advent~",
                        column: x => x.AdventureId,
                        principalTable: "AdventureService.Adventure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdventureService.Location_AdventureId",
                table: "AdventureService.Location",
                column: "AdventureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdventureService.Location");
        }
    }
}
