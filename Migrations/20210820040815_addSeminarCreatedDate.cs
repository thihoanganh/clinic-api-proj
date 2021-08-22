using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic_Web_Api.Migrations
{
    public partial class addSeminarCreatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Seminar",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Seminar");
        }
    }
}
