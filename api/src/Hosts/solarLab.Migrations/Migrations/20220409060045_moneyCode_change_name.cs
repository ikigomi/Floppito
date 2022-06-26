using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace solarLab.Migrations.Migrations
{
    public partial class moneyCode_change_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModeyCode",
                table: "Post",
                newName: "MoneyCode");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0f2652f5-20c6-45d1-9d46-c533f9b85576"),
                column: "ConcurrencyStamp",
                value: "423531d7-10f4-4244-9833-77b0983b7dea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9161399f-105d-4372-87ae-ca232ba7cf36"),
                column: "ConcurrencyStamp",
                value: "3c22c995-821a-4408-b699-c82d6893c73e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MoneyCode",
                table: "Post",
                newName: "ModeyCode");

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
    }
}
