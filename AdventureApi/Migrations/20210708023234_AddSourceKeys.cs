using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdventureApi.Migrations
{
    public partial class AddSourceKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "routes",
                newName: "SourceKey");

            migrationBuilder.AddColumn<Guid>(
                name: "SourceKey",
                table: "locations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SourceKey",
                table: "adventures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceKey",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "SourceKey",
                table: "adventures");

            migrationBuilder.RenameColumn(
                name: "SourceKey",
                table: "routes",
                newName: "SourceId");
        }
    }
}
