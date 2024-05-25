using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkIo.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLinkClickCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClickCount",
                table: "Links");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClickCount",
                table: "Links",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
