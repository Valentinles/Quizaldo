using Microsoft.EntityFrameworkCore.Migrations;

namespace Quizaldo.Data.Migrations
{
    public partial class NotificationSchemaUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_QuizaldoUserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_QuizaldoUserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "QuizaldoUserId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "QuizaldoUserId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_QuizaldoUserId",
                table: "Notifications",
                column: "QuizaldoUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_QuizaldoUserId",
                table: "Notifications",
                column: "QuizaldoUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
