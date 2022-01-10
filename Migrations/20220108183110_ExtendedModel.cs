using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moldovan_Andrea_SADE_Proiect.Migrations
{
    public partial class ExtendedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coffee",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taste = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coffee", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    CoffeeID = table.Column<int>(type: "int", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK_Invoice_Coffee_CoffeeID",
                        column: x => x.CoffeeID,
                        principalTable: "Coffee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuppliedCoffee",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    CoffeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuppliedCoffee", x => new { x.CoffeeID, x.SupplierID });
                    table.ForeignKey(
                        name: "FK_SuppliedCoffee_Coffee_CoffeeID",
                        column: x => x.CoffeeID,
                        principalTable: "Coffee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuppliedCoffee_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CoffeeID",
                table: "Invoice",
                column: "CoffeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerID",
                table: "Invoice",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_SuppliedCoffee_SupplierID",
                table: "SuppliedCoffee",
                column: "SupplierID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "SuppliedCoffee");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Coffee");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
