using Microsoft.EntityFrameworkCore.Migrations;

namespace Quizaldo.Data.Migrations
{
    public partial class JokeTableCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jokes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JokeContent = table.Column<string>(nullable: true),
                    PositiveVotes = table.Column<int>(nullable: false),
                    NegativeVotes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jokes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jokes");
        }
    }
}
