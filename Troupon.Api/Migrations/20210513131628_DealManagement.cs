using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Troupon.Catalog.Service.Api.Migrations
{
    public partial class DealManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Troupon");

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateProvince = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditCard",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cvv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MerchantEntity",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillingInfo",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingInfo_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "Troupon",
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillingInfo_CreditCard_CreditCardId",
                        column: x => x.CreditCardId,
                        principalSchema: "Troupon",
                        principalTable: "CreditCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Price_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Troupon",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DealEntity",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealEntity_MerchantEntity_MerchantId",
                        column: x => x.MerchantId,
                        principalSchema: "Troupon",
                        principalTable: "MerchantEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "Troupon",
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_Position_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "Troupon",
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_BillingInfo_BillingInfoId",
                        column: x => x.BillingInfoId,
                        principalSchema: "Troupon",
                        principalTable: "BillingInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Troupon",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalSchema: "Troupon",
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deals",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Limitation = table.Column<int>(type: "int", nullable: false),
                    OtherConditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deals_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Troupon",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DealCategories",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealCategories_Deals_DealId",
                        column: x => x.DealId,
                        principalSchema: "Troupon",
                        principalTable: "Deals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DealOption",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealOption_Deals_DealId",
                        column: x => x.DealId,
                        principalSchema: "Troupon",
                        principalTable: "Deals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DealPrice",
                schema: "Troupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalPriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DealOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealPrice_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Troupon",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DealPrice_DealOption_DealOptionId",
                        column: x => x.DealOptionId,
                        principalSchema: "Troupon",
                        principalTable: "DealOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DealPrice_Price_CurrentPriceId",
                        column: x => x.CurrentPriceId,
                        principalSchema: "Troupon",
                        principalTable: "Price",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DealPrice_Price_OriginalPriceId",
                        column: x => x.OriginalPriceId,
                        principalSchema: "Troupon",
                        principalTable: "Price",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Troupon",
                table: "MerchantEntity",
                columns: new[] { "Id", "ImageUri", "Name" },
                values: new object[,]
                {
                    { new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "https://picsum.photos/id/1023/200/300", "Awsome Goods Plus" },
                    { new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), "https://picsum.photos/id/1003/200/300", "Masso Relax Inc" },
                    { new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), "https://picsum.photos/id/1012/200/300", "Antirouille la magouille" },
                    { new Guid("042038de-1e60-427d-bdce-7d683ffc8bf5"), "https://picsum.photos/id/1011/200/300", "Bronsage & Debosselage Reuni" }
                });

            migrationBuilder.InsertData(
                schema: "Troupon",
                table: "DealEntity",
                columns: new[] { "Id", "Description", "Details", "MerchantId", "Name" },
                values: new object[,]
                {
                    { new Guid("0d53c6ce-6181-42a8-8616-03f86f883112"), "0", "1", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "0" },
                    { new Guid("060fad6c-112c-49b5-91eb-490a700098a5"), "1", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("effb7842-5cf8-4daa-86c9-4b253e5749c1"), "2", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("565fa793-22f9-4ca3-936b-cfeb5da17453"), "3", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("1dba892b-24fe-4007-8cb8-245f5afbee10"), "4", "0", new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), "1" },
                    { new Guid("87c9ab80-0c13-49ba-8235-e23a7e4df525"), "5", "0", new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), "1" },
                    { new Guid("2fe4efb6-1e2f-4b54-94c4-0c8b210198c9"), "6", "0", new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), "1" },
                    { new Guid("8aeb6e23-8099-47e4-9e34-2e22df9ee79c"), "7", "1", new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), "1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BillingInfoId",
                schema: "Troupon",
                table: "Accounts",
                column: "BillingInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LocationId",
                schema: "Troupon",
                table: "Accounts",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_MerchantId",
                schema: "Troupon",
                table: "Accounts",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingInfo_AddressId",
                schema: "Troupon",
                table: "BillingInfo",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingInfo_CreditCardId",
                schema: "Troupon",
                table: "BillingInfo",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_DealCategories_DealId",
                schema: "Troupon",
                table: "DealCategories",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_DealEntity_MerchantId",
                schema: "Troupon",
                table: "DealEntity",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_DealOption_DealId",
                schema: "Troupon",
                table: "DealOption",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_DealPrice_CurrencyId",
                schema: "Troupon",
                table: "DealPrice",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DealPrice_CurrentPriceId",
                schema: "Troupon",
                table: "DealPrice",
                column: "CurrentPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_DealPrice_DealOptionId",
                schema: "Troupon",
                table: "DealPrice",
                column: "DealOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DealPrice_OriginalPriceId",
                schema: "Troupon",
                table: "DealPrice",
                column: "OriginalPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_AccountId",
                schema: "Troupon",
                table: "Deals",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_AddressId",
                schema: "Troupon",
                table: "Location",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_PositionId",
                schema: "Troupon",
                table: "Location",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Price_CurrencyId",
                schema: "Troupon",
                table: "Price",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealCategories",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "DealEntity",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "DealPrice",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "MerchantEntity",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "DealOption",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "Price",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "Deals",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "BillingInfo",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "Merchants",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "CreditCard",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "Troupon");

            migrationBuilder.DropTable(
                name: "Position",
                schema: "Troupon");
        }
    }
}
