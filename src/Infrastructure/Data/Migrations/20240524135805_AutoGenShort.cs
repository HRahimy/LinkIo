using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkIo.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AutoGenShort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Links_ShortUrlCode",
                table: "Links");

            migrationBuilder.AlterColumn<string>(
                name: "ShortUrlCode",
                table: "Links",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Links_ShortUrlCode",
                table: "Links",
                column: "ShortUrlCode",
                unique: true,
                filter: "[ShortUrlCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Links_ShortUrlCode",
                table: "Links");

            migrationBuilder.AlterColumn<string>(
                name: "ShortUrlCode",
                table: "Links",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Links_ShortUrlCode",
                table: "Links",
                column: "ShortUrlCode",
                unique: true);
        }
    }
}
