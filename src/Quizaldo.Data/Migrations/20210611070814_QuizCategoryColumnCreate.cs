using Microsoft.EntityFrameworkCore.Migrations;

namespace Quizaldo.Data.Migrations
{
    public partial class QuizCategoryColumnCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Quizzes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Quizzes");
        }
    }
}
