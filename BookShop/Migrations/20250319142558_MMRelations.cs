using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Migrations
{
    /// <inheritdoc />
    public partial class MMRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Award_AwardId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_VoteAwards_VoteAwardId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VoteAwards_AspNetUsers_CreatorId",
                table: "VoteAwards");

            migrationBuilder.DropForeignKey(
                name: "FK_VoteOption_Authors_AuthorId",
                table: "VoteOption");

            migrationBuilder.DropForeignKey(
                name: "FK_VoteOption_VoteAwards_VoteAwardId",
                table: "VoteOption");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AwardId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VoteAwardId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteOption",
                table: "VoteOption");

            migrationBuilder.DropColumn(
                name: "AwardId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VoteAwardId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "VoteOption",
                newName: "Options");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Options",
                newName: "Counts");

            migrationBuilder.RenameIndex(
                name: "IX_VoteOption_VoteAwardId",
                table: "Options",
                newName: "IX_Options_VoteAwardId");

            migrationBuilder.RenameIndex(
                name: "IX_VoteOption_AuthorId",
                table: "Options",
                newName: "IX_Options_AuthorId");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Award",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserFundedAwards",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AwardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFundedAwards", x => new { x.UserId, x.AwardId });
                    table.ForeignKey(
                        name: "FK_UserFundedAwards_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFundedAwards_Award_AwardId",
                        column: x => x.AwardId,
                        principalTable: "Award",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFundedVoteAwards",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoteAwardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFundedVoteAwards", x => new { x.UserId, x.VoteAwardId });
                    table.ForeignKey(
                        name: "FK_UserFundedVoteAwards_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFundedVoteAwards_VoteAwards_VoteAwardId",
                        column: x => x.VoteAwardId,
                        principalTable: "VoteAwards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVoteAwards",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoteAwardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVoteAwards", x => new { x.UserId, x.VoteAwardId });
                    table.ForeignKey(
                        name: "FK_UserVoteAwards_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVoteAwards_VoteAwards_VoteAwardId",
                        column: x => x.VoteAwardId,
                        principalTable: "VoteAwards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Award_CreatorId",
                table: "Award",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFundedAwards_AwardId",
                table: "UserFundedAwards",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFundedVoteAwards_VoteAwardId",
                table: "UserFundedVoteAwards",
                column: "VoteAwardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVoteAwards_VoteAwardId",
                table: "UserVoteAwards",
                column: "VoteAwardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Award_AspNetUsers_CreatorId",
                table: "Award",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Authors_AuthorId",
                table: "Options",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_VoteAwards_VoteAwardId",
                table: "Options",
                column: "VoteAwardId",
                principalTable: "VoteAwards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteAwards_AspNetUsers_CreatorId",
                table: "VoteAwards",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Award_AspNetUsers_CreatorId",
                table: "Award");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Authors_AuthorId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_VoteAwards_VoteAwardId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_VoteAwards_AspNetUsers_CreatorId",
                table: "VoteAwards");

            migrationBuilder.DropTable(
                name: "UserFundedAwards");

            migrationBuilder.DropTable(
                name: "UserFundedVoteAwards");

            migrationBuilder.DropTable(
                name: "UserVoteAwards");

            migrationBuilder.DropIndex(
                name: "IX_Award_CreatorId",
                table: "Award");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Award");

            migrationBuilder.RenameTable(
                name: "Options",
                newName: "VoteOption");

            migrationBuilder.RenameColumn(
                name: "Counts",
                table: "VoteOption",
                newName: "Count");

            migrationBuilder.RenameIndex(
                name: "IX_Options_VoteAwardId",
                table: "VoteOption",
                newName: "IX_VoteOption_VoteAwardId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_AuthorId",
                table: "VoteOption",
                newName: "IX_VoteOption_AuthorId");

            migrationBuilder.AddColumn<int>(
                name: "AwardId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VoteAwardId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteOption",
                table: "VoteOption",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AwardId",
                table: "AspNetUsers",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VoteAwardId",
                table: "AspNetUsers",
                column: "VoteAwardId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Award_AwardId",
                table: "AspNetUsers",
                column: "AwardId",
                principalTable: "Award",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_VoteAwards_VoteAwardId",
                table: "AspNetUsers",
                column: "VoteAwardId",
                principalTable: "VoteAwards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteAwards_AspNetUsers_CreatorId",
                table: "VoteAwards",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoteOption_Authors_AuthorId",
                table: "VoteOption",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoteOption_VoteAwards_VoteAwardId",
                table: "VoteOption",
                column: "VoteAwardId",
                principalTable: "VoteAwards",
                principalColumn: "Id");
        }
    }
}
