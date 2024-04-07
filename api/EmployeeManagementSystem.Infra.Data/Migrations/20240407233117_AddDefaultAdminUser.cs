using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2024, 4, 7, 20, 31, 17, 48, DateTimeKind.Local).AddTicks(563), "admin@ems.com", "Admin", "a075d17f3d453073853f813838c15b8023b8c487038436354fe599c3942e1f95", new DateTime(2024, 4, 7, 20, 31, 17, 48, DateTimeKind.Local).AddTicks(572) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
