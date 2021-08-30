using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class addProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    MyProperty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a7439e9a-70de-4317-aa4e-0e4bcc778e10"),
                column: "ConcurrencyStamp",
                value: "5f05d5da-e9f1-4aac-96dc-b2c9b6e1dff9");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("c0a0fade-054e-4616-aa53-377f8ac8de81"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7c60007e-2751-4055-897b-1e13ebe4ab23", "AQAAAAEAACcQAAAAEI6q9piq3L628skaWMMQNyVFljAoZ8R+M9odBJX2Xz4XWFZFBLx7Y62zDsmWdDFLow==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 13, 22, 12, 19, 608, DateTimeKind.Local).AddTicks(7426));
        }
    }
}
