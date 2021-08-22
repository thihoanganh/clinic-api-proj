using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic_Web_Api.Migrations
{
    public partial class addExamatedDateField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExaminatedDate",
                table: "UserQuiz",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExaminatedDate",
                table: "UserQuiz");
        }
    }
}
