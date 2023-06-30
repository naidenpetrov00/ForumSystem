using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class GuestId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuestId",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Votes");
        }
    }
}
