using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentScoreRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScoreId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ScoreId",
                table: "Comments",
                column: "ScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Scores_ScoreId",
                table: "Comments",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Scores_ScoreId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ScoreId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ScoreId",
                table: "Comments");
        }
    }
}
