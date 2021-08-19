using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic_Web_Api.Migrations
{
    public partial class MrDatModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {




            migrationBuilder.DropColumn(
                name: "Expiry",
                table: "Medicine");




            migrationBuilder.AddColumn<DateTime>(
                name: "Expiry",
                table: "ReceiptMedicine",
                type: "datetime2",
                nullable: true);



            migrationBuilder.AddColumn<bool>(
                name: "IsExport",
                table: "DetailOrder",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPayment",
                table: "DetailOrder",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OriginName",
                table: "Attachment",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "Answer",
                type: "bit",
                nullable: false,
                defaultValueSql: "(CONVERT([bit],(0)))",
                oldClrType: typeof(bool),
                oldType: "bit");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarEmail_Seminar",
                table: "SeminarEmail");

            migrationBuilder.DropIndex(
                name: "IX_SeminarEmail_SeminarId",
                table: "SeminarEmail");

            migrationBuilder.DropColumn(
                name: "Expiry",
                table: "ReceiptMedicine");

            migrationBuilder.DropColumn(
                name: "IsExport",
                table: "DetailOrder");

            migrationBuilder.DropColumn(
                name: "IsPayment",
                table: "DetailOrder");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TypeOfMedicine",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Seminar",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Medicine",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expiry",
                table: "Medicine",
                type: "datetime",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OriginName",
                table: "Attachment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "Answer",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "(CONVERT([bit],(0)))");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SeminarEmail_SeminarId",
                table: "SeminarEmail",
                column: "SeminarId");

            migrationBuilder.CreateIndex(
                name: "IX_SeminarEmail",
                table: "SeminarEmail",
                column: "SeminarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Seminar_SeminarEmail1",
                table: "Seminar",
                column: "Id",
                principalTable: "SeminarEmail",
                principalColumn: "SeminarId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
