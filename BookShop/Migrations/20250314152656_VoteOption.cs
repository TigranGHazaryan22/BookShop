using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Migrations
{
    /// <inheritdoc />
    public partial class VoteOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoteAwardId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VoteAwards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteAwards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteAwards_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoteOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoteAwardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteOption_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteOption_VoteAwards_VoteAwardId",
                        column: x => x.VoteAwardId,
                        principalTable: "VoteAwards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VoteAwardId",
                table: "AspNetUsers",
                column: "VoteAwardId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteAwards_CreatorId",
                table: "VoteAwards",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteOption_AuthorId",
                table: "VoteOption",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteOption_VoteAwardId",
                table: "VoteOption",
                column: "VoteAwardId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_VoteAwards_VoteAwardId",
                table: "AspNetUsers",
                column: "VoteAwardId",
                principalTable: "VoteAwards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_VoteAwards_VoteAwardId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "VoteOption");

            migrationBuilder.DropTable(
                name: "VoteAwards");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VoteAwardId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VoteAwardId",
                table: "AspNetUsers");
        }
    }
}
