using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsWebsite.Migrations
{
    /// <inheritdoc />
    public partial class Added_RecentReadPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecentReadPosts",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecentReadPosts", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_RecentReadPosts_NewsPost_PostGuid",
                        column: x => x.PostGuid,
                        principalTable: "NewsPost",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecentReadPosts_Users_UserGuid",
                        column: x => x.UserGuid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecentReadPosts_PostGuid",
                table: "RecentReadPosts",
                column: "PostGuid");

            migrationBuilder.CreateIndex(
                name: "IX_RecentReadPosts_UserGuid",
                table: "RecentReadPosts",
                column: "UserGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecentReadPosts");
        }
    }
}
