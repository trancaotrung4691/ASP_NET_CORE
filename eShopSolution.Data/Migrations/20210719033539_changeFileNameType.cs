using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class changeFileNameType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a7439e9a-70de-4317-aa4e-0e4bcc778e10"),
                column: "ConcurrencyStamp",
                value: "31457005-357a-459b-9a3c-405cc51a1fb9");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("c0a0fade-054e-4616-aa53-377f8ac8de81"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "274b8b79-41a5-4c2b-89bf-f067e7a1613c", "AQAAAAEAACcQAAAAEOOLet3/0qusnmOTJV3KZ9apFUcr5whvJ57ODYEVKcJNj/jsoF6CLNQE45uE4g1JSQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 19, 10, 35, 38, 955, DateTimeKind.Local).AddTicks(4989));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a7439e9a-70de-4317-aa4e-0e4bcc778e10"),
                column: "ConcurrencyStamp",
                value: "09a06590-913b-4c45-800b-ab3d1e1b0ae7");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("c0a0fade-054e-4616-aa53-377f8ac8de81"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "01f2aa72-362c-4b70-9965-c99f48070bbc", "AQAAAAEAACcQAAAAEGmJ2b7d9wdGInYp/Jlkk87r0VzNisxilB7vqqtQ6Aj8erQqWObWpPGU7/rGUWr1Xg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 16, 16, 19, 26, 886, DateTimeKind.Local).AddTicks(6151));
        }
    }
}
