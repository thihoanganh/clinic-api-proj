using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic_Web_Api.Migrations
{
    public partial class addCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Lecture",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecture_LectureCategory",
                table: "Lecture");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureComment_Lecture",
                table: "LectureComment");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureComment_User",
                table: "LectureComment");

            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_Lecture",
                table: "Quiz");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Lecture",
                table: "Attachment",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecture_LectureCategory",
                table: "Lecture",
                column: "CateId",
                principalTable: "LectureCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureComment_Lecture",
                table: "LectureComment",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureComment_User",
                table: "LectureComment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Lecture",
                table: "Quiz",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Lecture",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecture_LectureCategory",
                table: "Lecture");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureComment_Lecture",
                table: "LectureComment");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureComment_User",
                table: "LectureComment");

            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_Lecture",
                table: "Quiz");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Lecture",
                table: "Attachment",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecture_LectureCategory",
                table: "Lecture",
                column: "CateId",
                principalTable: "LectureCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureComment_Lecture",
                table: "LectureComment",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureComment_User",
                table: "LectureComment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Lecture",
                table: "Quiz",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
