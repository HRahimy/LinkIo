using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkIo.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContextLinkProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkReferrer_Link_LinkId",
                table: "LinkReferrer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkReferrer",
                table: "LinkReferrer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Link",
                table: "Link");

            migrationBuilder.RenameTable(
                name: "LinkReferrer",
                newName: "LinkReferrers");

            migrationBuilder.RenameTable(
                name: "Link",
                newName: "Links");

            migrationBuilder.RenameIndex(
                name: "IX_LinkReferrer_LinkId",
                table: "LinkReferrers",
                newName: "IX_LinkReferrers_LinkId");

            migrationBuilder.RenameIndex(
                name: "IX_Link_ShortUrlCode",
                table: "Links",
                newName: "IX_Links_ShortUrlCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LinkReferrers",
                table: "LinkReferrers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Links",
                table: "Links",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkReferrers_Links_LinkId",
                table: "LinkReferrers",
                column: "LinkId",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkReferrers_Links_LinkId",
                table: "LinkReferrers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Links",
                table: "Links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkReferrers",
                table: "LinkReferrers");

            migrationBuilder.RenameTable(
                name: "Links",
                newName: "Link");

            migrationBuilder.RenameTable(
                name: "LinkReferrers",
                newName: "LinkReferrer");

            migrationBuilder.RenameIndex(
                name: "IX_Links_ShortUrlCode",
                table: "Link",
                newName: "IX_Link_ShortUrlCode");

            migrationBuilder.RenameIndex(
                name: "IX_LinkReferrers_LinkId",
                table: "LinkReferrer",
                newName: "IX_LinkReferrer_LinkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Link",
                table: "Link",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LinkReferrer",
                table: "LinkReferrer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkReferrer_Link_LinkId",
                table: "LinkReferrer",
                column: "LinkId",
                principalTable: "Link",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
