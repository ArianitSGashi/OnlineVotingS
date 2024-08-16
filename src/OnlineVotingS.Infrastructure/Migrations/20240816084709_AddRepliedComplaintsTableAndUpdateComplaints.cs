using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineVotingS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRepliedComplaintsTableAndUpdateComplaints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4839a2ff-9d0c-44de-b1f3-9f2ceb675b04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f22441e-7a66-4949-b46d-1052027e6f9f");

            migrationBuilder.CreateTable(
                name: "RepliedComplaints",
                columns: table => new
                {
                    RepliedComplaintID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintID = table.Column<int>(type: "int", nullable: false),
                    ReplyText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ReplyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepliedComplaints", x => x.RepliedComplaintID);
                    table.ForeignKey(
                        name: "FK_RepliedComplaints_Complaints_ComplaintID",
                        column: x => x.ComplaintID,
                        principalTable: "Complaints",
                        principalColumn: "ComplaintID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4892de04-c912-401a-9dcb-943dbf40f2a5", "b6d332dc-f0a8-43b4-b626-64331b733fcd", "User", "USER" },
                    { "bd28e91c-c4df-47b8-8af2-af11e3e65b72", "493e96eb-4354-432f-a1f2-496df533530e", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepliedComplaints_ComplaintID",
                table: "RepliedComplaints",
                column: "ComplaintID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepliedComplaints");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4892de04-c912-401a-9dcb-943dbf40f2a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd28e91c-c4df-47b8-8af2-af11e3e65b72");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4839a2ff-9d0c-44de-b1f3-9f2ceb675b04", "5c6ce15a-b9a2-4e64-be66-8fe0c6d0147b", "Admin", "ADMIN" },
                    { "7f22441e-7a66-4949-b46d-1052027e6f9f", "66654619-1056-40da-804a-5bba15dde883", "User", "USER" }
                });
        }
    }
}
