using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineVotingS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedConstraintforParty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidates_FullName_ElectionID",
                table: "Candidates");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_FullName_ElectionID_Party",
                table: "Candidates",
                columns: new[] { "FullName", "ElectionID", "Party" },
                unique: true,
                filter: "[Party] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidates_FullName_ElectionID_Party",
                table: "Candidates");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_FullName_ElectionID",
                table: "Candidates",
                columns: new[] { "FullName", "ElectionID" },
                unique: true);
        }
    }
}
