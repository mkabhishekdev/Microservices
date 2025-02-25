using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InventoryManagementAPI.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "category_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "product_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "InventoryCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    invCategory = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    category = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    imageName = table.Column<string>(nullable: true),
                    imageURL = table.Column<string>(nullable: true),
                    inventoryCategoryId = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    productName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Product_InventoryCategory_inventoryCategoryId",
                        column: x => x.inventoryCategoryId,
                        principalTable: "InventoryCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_inventoryCategoryId",
                table: "Product",
                column: "inventoryCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "InventoryCategory");

            migrationBuilder.DropSequence(
                name: "category_hilo");

            migrationBuilder.DropSequence(
                name: "product_hilo");
        }
    }
}
