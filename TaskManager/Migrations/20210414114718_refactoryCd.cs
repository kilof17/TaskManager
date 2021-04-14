using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Migrations
{
    public partial class refactoryCd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoneDate",
                table: "FinishedQuests");

            migrationBuilder.DropColumn(
                name: "DoneTime",
                table: "FinishedQuests");

            migrationBuilder.AddColumn<DateTime>(
                name: "Done_ISO8601",
                table: "FinishedQuests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Done_ISO8601",
                table: "FinishedQuests");

            migrationBuilder.AddColumn<string>(
                name: "DoneDate",
                table: "FinishedQuests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoneTime",
                table: "FinishedQuests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
