using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace solarLab.Migrations.Migrations
{
    public partial class images_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0f2652f5-20c6-45d1-9d46-c533f9b85576"),
                column: "ConcurrencyStamp",
                value: "aaef8615-8eef-48f6-bc8b-5f37fd309b6f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9161399f-105d-4372-87ae-ca232ba7cf36"),
                column: "ConcurrencyStamp",
                value: "6404895c-68d3-4863-ab0b-5dd413169f55");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0f2652f5-20c6-45d1-9d46-c533f9b85576"),
                column: "ConcurrencyStamp",
                value: "87d49988-2d8f-49ab-8104-3b69dc1848f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9161399f-105d-4372-87ae-ca232ba7cf36"),
                column: "ConcurrencyStamp",
                value: "be4ec665-c892-4778-99d1-76c3c819598b");
        }
    }
}
