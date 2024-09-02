using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineVotingS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27ec9183-68eb-4401-a747-b110be03df38");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d484e72-3c96-4c2c-8f07-0eba30babf4a");

            migrationBuilder.DropColumn(
                name: "VoterId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c540af6-05fb-46da-a459-26b5b7d8c067", "05ec46ac-16a2-474b-b94f-a83cd46cbdd7", "User", "USER" },
                    { "b9c0059a-322a-467f-84e8-ba5281253276", "f50b717b-9b45-4528-9027-87729b660bd1", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c540af6-05fb-46da-a459-26b5b7d8c067");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9c0059a-322a-467f-84e8-ba5281253276");

            migrationBuilder.AddColumn<string>(
                name: "VoterId",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27ec9183-68eb-4401-a747-b110be03df38", "71406e7c-9a21-4c08-803a-b976662107ae", "User", "USER" },
                    { "9d484e72-3c96-4c2c-8f07-0eba30babf4a", "ac1e4f16-07a0-4b8f-b46b-f6f212b71c9e", "Admin", "ADMIN" }
                });
        }
    }
}
