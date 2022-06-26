using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace solarLab.Migrations.Migrations
{
    public partial class moneyCode_not_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ModeyCode",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0f2652f5-20c6-45d1-9d46-c533f9b85576"),
                column: "ConcurrencyStamp",
                value: "adaddfff-db23-4351-bf22-c3fe3ad808a9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9161399f-105d-4372-87ae-ca232ba7cf36"),
                column: "ConcurrencyStamp",
                value: "faa3e7df-f5ea-4363-9041-785f98381968");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ModeyCode",
                table: "Post",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
