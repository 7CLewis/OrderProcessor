using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderProcessor.Producer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PricePerUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadquartersAddress_Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadquartersAddress_Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadquartersAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadquartersAddress_StateOrProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadquartersAddress_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadquartersAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderAddress_Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAddress_Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAddress_StateOrProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAddress_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverAddress_Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverAddress_Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverAddress_StateOrProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverAddress_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_TransportCompanies_TransportCompanyId",
                        column: x => x.TransportCompanyId,
                        principalTable: "TransportCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrderId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductsId",
                table: "OrderProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TransportCompanyId",
                table: "Orders",
                column: "TransportCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TransportCompanies");
        }
    }
}
