using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic_Web_Api.Migrations
{
    public partial class addCascadeDeleteOnQuiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quiz",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuiz_Quiz",
                table: "UserQuiz");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuiz_User",
                table: "UserQuiz");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quiz",
                table: "Question",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuiz_Quiz",
                table: "UserQuiz",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuiz_User",
                table: "UserQuiz",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quiz",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuiz_Quiz",
                table: "UserQuiz");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuiz_User",
                table: "UserQuiz");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quiz",
                table: "Question",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuiz_Quiz",
                table: "UserQuiz",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuiz_User",
                table: "UserQuiz",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
