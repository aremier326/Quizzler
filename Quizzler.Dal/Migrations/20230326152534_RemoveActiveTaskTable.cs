using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzler.Dal.Migrations
{
    public partial class RemoveActiveTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveTests");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Tests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Result",
                table: "Tests",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_QuizId",
                table: "Tests",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Quizzes_QuizId",
                table: "Tests",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Quizzes_QuizId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_QuizId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Tests");

            migrationBuilder.CreateTable(
                name: "ActiveTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: true),
                    Result = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveTests_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActiveTests_Tests_Id",
                        column: x => x.Id,
                        principalTable: "Tests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTests_QuizId",
                table: "ActiveTests",
                column: "QuizId");
        }
    }
}
