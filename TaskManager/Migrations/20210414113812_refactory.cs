using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Migrations
{
    public partial class refactory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "AddTime",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "ExpiryTime",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "FinishedQuests");

            migrationBuilder.DropColumn(
                name: "AddTime",
                table: "FinishedQuests");

            migrationBuilder.AddColumn<DateTime>(
                name: "Added_ISO8601",
                table: "Quests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Expiry_ISO8601",
                table: "Quests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Added_ISO8601",
                table: "FinishedQuests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Added_ISO8601",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "Expiry_ISO8601",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "Added_ISO8601",
                table: "FinishedQuests");

            migrationBuilder.AddColumn<string>(
                name: "AddDate",
                table: "Quests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddTime",
                table: "Quests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpiryDate",
                table: "Quests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpiryTime",
                table: "Quests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddDate",
                table: "FinishedQuests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddTime",
                table: "FinishedQuests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
