using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineVotingS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatefeedbackentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_UserID",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Elections_ElectionID",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ElectionID",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_UserID",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "ElectionID",
                table: "Feedbacks",
                newName: "FeedbackCategory");

            migrationBuilder.AddColumn<int>(
                name: "ElectionsElectionID",
                table: "Feedbacks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ElectionsElectionID",
                table: "Feedbacks",
                column: "ElectionsElectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Elections_ElectionsElectionID",
                table: "Feedbacks",
                column: "ElectionsElectionID",
                principalTable: "Elections",
                principalColumn: "ElectionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Elections_ElectionsElectionID",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ElectionsElectionID",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "ElectionsElectionID",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "FeedbackCategory",
                table: "Feedbacks",
                newName: "ElectionID");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Feedbacks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ElectionID",
                table: "Feedbacks",
                column: "ElectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserID",
                table: "Feedbacks",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_UserID",
                table: "Feedbacks",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Elections_ElectionID",
                table: "Feedbacks",
                column: "ElectionID",
                principalTable: "Elections",
                principalColumn: "ElectionID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
