using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkIo.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShortUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortUrl",
                table: "Link");

            migrationBuilder.AddColumn<string>(
                name: "ShortUrlCode",
                table: "Link",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Link_ShortUrlCode",
                table: "Link",
                column: "ShortUrlCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Link_ShortUrlCode",
                table: "Link");

            migrationBuilder.DropColumn(
                name: "ShortUrlCode",
                table: "Link");

            migrationBuilder.AddColumn<string>(
                name: "ShortUrl",
                table: "Link",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
