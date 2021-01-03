using Microsoft.EntityFrameworkCore.Migrations;

namespace AdventureApi.Migrations
{
    public partial class AddAdventureId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdventureService.Location_AdventureService.Adventure_Advent~",
                table: "AdventureService.Location");

            migrationBuilder.AlterColumn<int>(
                name: "AdventureId",
                table: "AdventureService.Location",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdventureService.Location_AdventureService.Adventure_Advent~",
                table: "AdventureService.Location",
                column: "AdventureId",
                principalTable: "AdventureService.Adventure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdventureService.Location_AdventureService.Adventure_Advent~",
                table: "AdventureService.Location");

            migrationBuilder.AlterColumn<int>(
                name: "AdventureId",
                table: "AdventureService.Location",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_AdventureService.Location_AdventureService.Adventure_Advent~",
                table: "AdventureService.Location",
                column: "AdventureId",
                principalTable: "AdventureService.Adventure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
