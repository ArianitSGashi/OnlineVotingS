using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineVotingS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesHardCoded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "037629ae-362f-4bb2-9393-331f3c4ce43c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91200ead-291b-40f8-914a-19765c3b9d46");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5be9341f-b95e-4155-a0f2-ca8f188a1d7b", "7d5cf4ed-83e2-4c71-a8e8-03c2e6a572aa", "Admin", "ADMIN" },
                    { "6cd1fa1e-c4c6-4a16-a8b2-3f4c031ec0d3", "9d5bf4ce-7a3e-9c15-b2e9-04d3e7a683bb", "Voter", "VOTER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5be9341f-b95e-4155-a0f2-ca8f188a1d7b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cd1fa1e-c4c6-4a16-a8b2-3f4c031ec0d3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "037629ae-362f-4bb2-9393-331f3c4ce43c", "62367227-2410-42d5-9313-8b8da0461936", "Voter", "VOTER" },
                    { "91200ead-291b-40f8-914a-19765c3b9d46", "57055b53-18cd-48d0-ab2f-ca0bca8bf618", "Admin", "ADMIN" }
                });
        }
    }
}
