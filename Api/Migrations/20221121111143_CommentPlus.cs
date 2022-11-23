using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class CommentPlus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostContent_Users_AuthorContentId",
                table: "PostContent");

            migrationBuilder.RenameColumn(
                name: "AuthorContentId",
                table: "PostContent",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PostContent_AuthorContentId",
                table: "PostContent",
                newName: "IX_PostContent_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostContent_Users_UserId",
                table: "PostContent",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostContent_Users_UserId",
                table: "PostContent");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PostContent",
                newName: "AuthorContentId");

            migrationBuilder.RenameIndex(
                name: "IX_PostContent_UserId",
                table: "PostContent",
                newName: "IX_PostContent_AuthorContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostContent_Users_AuthorContentId",
                table: "PostContent",
                column: "AuthorContentId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
