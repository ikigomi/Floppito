using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace solarLab.Migrations.Migrations
{
    public partial class addedMoneyCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Post");

            migrationBuilder.AddColumn<int>(
                name: "ModeyCode",
                table: "Post",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryFrom",
                table: "Post",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryTo",
                table: "Post",
                type: "decimal(18,2)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModeyCode",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "SalaryFrom",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "SalaryTo",
                table: "Post");

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Post",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0f2652f5-20c6-45d1-9d46-c533f9b85576"),
                column: "ConcurrencyStamp",
                value: "b17f2f5e-ab36-4944-af72-aa0134e3478c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9161399f-105d-4372-87ae-ca232ba7cf36"),
                column: "ConcurrencyStamp",
                value: "064e6481-aba3-478f-845c-de21eff0241d");
        }
    }
}
