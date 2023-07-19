#nullable disable

namespace ForumSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

	/// <inheritdoc />
    public partial class AddName : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Name",
				table: "Posts",
				type: "nvarchar(max)",
				nullable: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Name",
				table: "Posts");
		}
	}
}
