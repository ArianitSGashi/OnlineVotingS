using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineVotingS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedtheMobileNumberfromAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c540af6-05fb-46da-a459-26b5b7d8c067");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9c0059a-322a-467f-84e8-ba5281253276");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5431d6b1-1d6b-42db-a330-dda5aad14907", "c00d6eff-59c8-46e3-a11a-4e1e2e66afe6", "Admin", "ADMIN" },
                    { "e509d8c1-26cb-4820-8f63-e4a65ef1afad", "80ab86f2-f82b-4148-b9af-5ad20a2d5be6", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5431d6b1-1d6b-42db-a330-dda5aad14907");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e509d8c1-26cb-4820-8f63-e4a65ef1afad");

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c540af6-05fb-46da-a459-26b5b7d8c067", "05ec46ac-16a2-474b-b94f-a83cd46cbdd7", "User", "USER" },
                    { "b9c0059a-322a-467f-84e8-ba5281253276", "f50b717b-9b45-4528-9027-87729b660bd1", "Admin", "ADMIN" }
                });
        }
    }
}
