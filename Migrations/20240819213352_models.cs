using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace onboardingAPI.Migrations
{
    /// <inheritdoc />
    public partial class models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cce71d3-aef5-4a7a-b2a0-8c55d6249c1d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7548a09b-9ef4-4bda-80c8-da84ebbf1c3c");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Otps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4957b5a4-80ca-4bd9-b5d3-bf19df3e1115", null, "User", "USER" },
                    { "8f26df3d-aca6-4a58-b004-2e0cae61d49f", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4957b5a4-80ca-4bd9-b5d3-bf19df3e1115");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f26df3d-aca6-4a58-b004-2e0cae61d49f");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Otps");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6cce71d3-aef5-4a7a-b2a0-8c55d6249c1d", null, "Admin", "ADMIN" },
                    { "7548a09b-9ef4-4bda-80c8-da84ebbf1c3c", null, "User", "USER" }
                });
        }
    }
}
