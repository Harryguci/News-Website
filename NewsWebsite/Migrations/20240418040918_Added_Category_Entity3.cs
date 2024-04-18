using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsWebsite.Migrations
{
    /// <inheritdoc />
    public partial class Added_Category_Entity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_NewsPost_NewsPostGuid",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_NewsPostGuid",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "NewsPostGuid",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryNewsPost",
                columns: table => new
                {
                    CategoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsPostsGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryNewsPost", x => new { x.CategoryGuid, x.NewsPostsGuid });
                    table.ForeignKey(
                        name: "FK_CategoryNewsPost_Categories_CategoryGuid",
                        column: x => x.CategoryGuid,
                        principalTable: "Categories",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryNewsPost_NewsPost_NewsPostsGuid",
                        column: x => x.NewsPostsGuid,
                        principalTable: "NewsPost",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryNewsPost_NewsPostsGuid",
                table: "CategoryNewsPost",
                column: "NewsPostsGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryNewsPost");

            migrationBuilder.AddColumn<Guid>(
                name: "NewsPostGuid",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_NewsPostGuid",
                table: "Categories",
                column: "NewsPostGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_NewsPost_NewsPostGuid",
                table: "Categories",
                column: "NewsPostGuid",
                principalTable: "NewsPost",
                principalColumn: "Guid");
        }
    }
}
