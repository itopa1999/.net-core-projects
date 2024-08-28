using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace onboardingAPI.Migrations
{
    /// <inheritdoc />
    public partial class Modelsdata1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoanPayments_LoanId",
                table: "LoanPayments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ec3ea44-3d18-4d49-a9c4-9522a5855545");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42e99bfb-aae9-4ec4-b2c1-133b7ae1a0df");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "122d1923-03a8-44b9-a496-aaced193964d", null, "User", "USER" },
                    { "e58d5cd3-42af-4554-a5c8-bd2a258c525c", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanPayments_LoanId",
                table: "LoanPayments",
                column: "LoanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoanPayments_LoanId",
                table: "LoanPayments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "122d1923-03a8-44b9-a496-aaced193964d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e58d5cd3-42af-4554-a5c8-bd2a258c525c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ec3ea44-3d18-4d49-a9c4-9522a5855545", null, "Admin", "ADMIN" },
                    { "42e99bfb-aae9-4ec4-b2c1-133b7ae1a0df", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanPayments_LoanId",
                table: "LoanPayments",
                column: "LoanId",
                unique: true);
        }
    }
}
