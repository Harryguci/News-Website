using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsWebsite.Migrations
{
    /// <inheritdoc />
    public partial class Added_Category_Entity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "NewsPost");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "NewsPost",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
