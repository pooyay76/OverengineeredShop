using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Media_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "Medias",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section",
                table: "Medias");
        }
    }
}
