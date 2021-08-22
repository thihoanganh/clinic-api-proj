using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic_Web_Api.Migrations
{
    public partial class addMailSupportTblUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "MailSupport",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "MailSupport");
        }
    }
}
