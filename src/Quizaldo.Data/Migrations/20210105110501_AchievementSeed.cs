using Microsoft.EntityFrameworkCore.Migrations;

namespace Quizaldo.Data.Migrations
{
    public partial class AchievementSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "LogoUrl", "Name", "Points", "Requirement" },
                values: new object[,]
                {
                    { 1, "https://i.pinimg.com/originals/a5/69/36/a56936857e3b5ee42eb7f18ceb50adb4.png", "Rookie", 5, "Complete your first quiz." },
                    { 2, "https://webstockreview.net/images/100-clipart-emoji-10.png", "Hundreder", 100, "Get 100 points from quizzes." },
                    { 3, "https://images.vexels.com/media/users/3/207946/isolated/preview/7c504bb059892ea84bd18a76023ba52b-thumb-up-cartoon-hand-by-vexels.png", "Excellent", 75, "Complete a quiz without any mistakes." },
                    { 4, "https://pngimage.net/wp-content/uploads/2018/06/master-ball-png-4.png", "Master", 180, "Earn all previous achievements." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
