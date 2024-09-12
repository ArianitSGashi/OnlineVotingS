using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineVotingS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedElectionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Elections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Elections");
        }
    }
}
