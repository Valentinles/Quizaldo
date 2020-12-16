using Microsoft.EntityFrameworkCore.Migrations;

namespace Quizaldo.Data.Migrations
{
    public partial class UserRatedJokesColumnCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuizaldoUserId",
                table: "Jokes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jokes_QuizaldoUserId",
                table: "Jokes",
                column: "QuizaldoUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jokes_AspNetUsers_QuizaldoUserId",
                table: "Jokes",
                column: "QuizaldoUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jokes_AspNetUsers_QuizaldoUserId",
                table: "Jokes");

            migrationBuilder.DropIndex(
                name: "IX_Jokes_QuizaldoUserId",
                table: "Jokes");

            migrationBuilder.DropColumn(
                name: "QuizaldoUserId",
                table: "Jokes");
        }
    }
}
