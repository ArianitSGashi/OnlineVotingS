using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineVotingS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5431d6b1-1d6b-42db-a330-dda5aad14907");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e509d8c1-26cb-4820-8f63-e4a65ef1afad");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "048e71a0-945c-40f3-a542-edcd1d93b367", "118bdb50-9bfd-4042-bf38-a283814e9dfc", "Voter", "VOTER" },
                    { "363c71fc-d99b-4750-aaa1-1ce3d32c7c7c", "69c95bae-06c8-4050-a1e9-3913f17cd132", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "048e71a0-945c-40f3-a542-edcd1d93b367");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "363c71fc-d99b-4750-aaa1-1ce3d32c7c7c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5431d6b1-1d6b-42db-a330-dda5aad14907", "c00d6eff-59c8-46e3-a11a-4e1e2e66afe6", "Admin", "ADMIN" },
                    { "e509d8c1-26cb-4820-8f63-e4a65ef1afad", "80ab86f2-f82b-4148-b9af-5ad20a2d5be6", "User", "USER" }
                });
        }
    }
}
