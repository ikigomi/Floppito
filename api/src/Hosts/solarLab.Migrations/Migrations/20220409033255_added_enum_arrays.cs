using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace solarLab.Migrations.Migrations
{
    public partial class added_enum_arrays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WorkSchedule",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WorkExperience",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0f2652f5-20c6-45d1-9d46-c533f9b85576"),
                column: "ConcurrencyStamp",
                value: "2232765f-6dcb-4b69-8d57-ed23a08e1976");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9161399f-105d-4372-87ae-ca232ba7cf36"),
                column: "ConcurrencyStamp",
                value: "11e0b3ae-2ad0-49d6-8a99-b6f9195cc1f7");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WorkSchedule",
                table: "Post",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkExperience",
                table: "Post",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0f2652f5-20c6-45d1-9d46-c533f9b85576"),
                column: "ConcurrencyStamp",
                value: "ce9e2f1d-0c5e-463e-869f-104318bce426");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9161399f-105d-4372-87ae-ca232ba7cf36"),
                column: "ConcurrencyStamp",
                value: "6bcc0a4c-4565-435c-8f74-0e04f7ede8ec");
        }
    }
}
