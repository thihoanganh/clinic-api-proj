using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic_Web_Api.Migrations
{
    public partial class dropTblTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Seminar",
                table: "Feedback");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.AlterColumn<int>(
                name: "SeminarId",
                table: "Feedback",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Seminar",
                table: "Feedback",
                column: "SeminarId",
                principalTable: "Seminar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Seminar",
                table: "Feedback");

            migrationBuilder.AlterColumn<int>(
                name: "SeminarId",
                table: "Feedback",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LectureId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Lecture",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_LectureId",
                table: "Tag",
                column: "LectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Seminar",
                table: "Feedback",
                column: "SeminarId",
                principalTable: "Seminar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
