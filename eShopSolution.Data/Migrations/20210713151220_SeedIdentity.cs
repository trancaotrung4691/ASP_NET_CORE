using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class SeedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("a7439e9a-70de-4317-aa4e-0e4bcc778e10"), "5f05d5da-e9f1-4aac-96dc-b2c9b6e1dff9", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a7439e9a-70de-4317-aa4e-0e4bcc778e10"), new Guid("c0a0fade-054e-4616-aa53-377f8ac8de81") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("c0a0fade-054e-4616-aa53-377f8ac8de81"), 0, "7c60007e-2751-4055-897b-1e13ebe4ab23", new DateTime(1991, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "tctr74691@gmail.com", true, "Trung", "Tran Cao", false, null, "tctr74691@gmail.com", "admin", "AQAAAAEAACcQAAAAEI6q9piq3L628skaWMMQNyVFljAoZ8R+M9odBJX2Xz4XWFZFBLx7Y62zDsmWdDFLow==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 13, 22, 12, 19, 608, DateTimeKind.Local).AddTicks(7426));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a7439e9a-70de-4317-aa4e-0e4bcc778e10"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a7439e9a-70de-4317-aa4e-0e4bcc778e10"), new Guid("c0a0fade-054e-4616-aa53-377f8ac8de81") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("c0a0fade-054e-4616-aa53-377f8ac8de81"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 13, 21, 57, 16, 762, DateTimeKind.Local).AddTicks(7100));
        }
    }
}
