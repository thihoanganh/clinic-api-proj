using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic_Web_Api.Migrations
{
    public partial class mergedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailOrder_DiscountEvent",
                table: "DetailOrder");

            migrationBuilder.RenameColumn(
                name: "Priceid",
                table: "Medicine",
                newName: "PriceId");

            migrationBuilder.RenameIndex(
                name: "IX_Medicine_Priceid",
                table: "Medicine",
                newName: "IX_Medicine_PriceId");

            migrationBuilder.CreateTable(
                name: "PriceMedicine",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    MedicineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceMedicine", x => x.id);
                    table.ForeignKey(
                        name: "FK_PriceMedicine_Medicine",
                        column: x => x.MedicineId,
                        principalTable: "Medicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceScientificEquipment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ScientificEquipmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceScientificEquipment", x => x.id);
                    table.ForeignKey(
                        name: "FK_PriceScientificEquipment_ScientificEquipment",
                        column: x => x.ScientificEquipmentId,
                        principalTable: "ScientificEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceMedicine_MedicineId",
                table: "PriceMedicine",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceScientificEquipment_ScientificEquipmentId",
                table: "PriceScientificEquipment",
                column: "ScientificEquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailOrder_DiscountEvent_DiscountEventId",
                table: "DetailOrder",
                column: "DiscountEventId",
                principalTable: "DiscountEvent",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailOrder_DiscountEvent_DiscountEventId",
                table: "DetailOrder");

            migrationBuilder.DropTable(
                name: "PriceMedicine");

            migrationBuilder.DropTable(
                name: "PriceScientificEquipment");

            migrationBuilder.RenameColumn(
                name: "PriceId",
                table: "Medicine",
                newName: "Priceid");

            migrationBuilder.RenameIndex(
                name: "IX_Medicine_PriceId",
                table: "Medicine",
                newName: "IX_Medicine_Priceid");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailOrder_DiscountEvent",
                table: "DetailOrder",
                column: "DiscountEventId",
                principalTable: "DiscountEvent",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
