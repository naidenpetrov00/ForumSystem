using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
