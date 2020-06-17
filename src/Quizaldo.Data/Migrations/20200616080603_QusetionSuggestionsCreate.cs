using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quizaldo.Data.Migrations
{
    public partial class QusetionSuggestionsCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionSuggestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionName = table.Column<string>(nullable: true),
                    FirstOption = table.Column<string>(nullable: true),
                    SecondOption = table.Column<string>(nullable: true),
                    ThirdOption = table.Column<string>(nullable: true),
                    FourthOption = table.Column<string>(nullable: true),
                    CorrectAnswer = table.Column<string>(nullable: true),
                    CorrectAnswerPoints = table.Column<int>(nullable: false),
                    SuggestedOn = table.Column<DateTime>(nullable: false),
                    QuizId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSuggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionSuggestions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionSuggestions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSuggestions_QuizId",
                table: "QuestionSuggestions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSuggestions_UserId",
                table: "QuestionSuggestions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionSuggestions");
        }
    }
}
