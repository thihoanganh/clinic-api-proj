using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic_Web_Api.Migrations
{
    public partial class removeCertificateTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Place = table.Column<string>(type: "nchar(300)", fixedLength: true, maxLength: 300, nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificate_Staff",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_StaffId",
                table: "Certificate",
                column: "StaffId");
        }
    }
}
