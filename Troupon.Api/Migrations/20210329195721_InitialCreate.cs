using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Troupon.Catalog.Service.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deals",
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
                    table.PrimaryKey("PK_Deals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deals_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "ImageUri", "Name" },
                values: new object[,]
                {
                    { new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), null, "Awsome Goods Plus" },
                    { new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), null, "Masso Relax Inc" },
                    { new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), null, "Antirouille la magouille" },
                    { new Guid("042038de-1e60-427d-bdce-7d683ffc8bf5"), null, "Bronsage & Debosselage Reuni" }
                });

            migrationBuilder.InsertData(
                table: "Deals",
                columns: new[] { "Id", "Description", "Details", "MerchantId", "Name" },
                values: new object[,]
                {
                    { new Guid("0d53c6ce-6181-42a8-8616-03f86f883112"), "0", "1", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "0" },
                    { new Guid("4edd43f5-9df4-4cf0-8638-e3fb6f959219"), "1", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("9319778a-eb57-4c05-80bf-26765e54f1c2"), "2", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("100300d2-8450-4297-84ca-725b9d1a36c5"), "3", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("38fed865-a7ba-484b-ad82-7ffa657b6355"), "4", "0", new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), "1" },
                    { new Guid("dce8acc7-49af-4f89-b35d-29cea76e27ac"), "5", "0", new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), "1" },
                    { new Guid("dc336a37-8c38-4f49-89d0-279da2471132"), "6", "0", new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), "1" },
                    { new Guid("9141d48a-fa00-4113-8188-ee8435766972"), "7", "1", new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), "1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deals_MerchantId",
                table: "Deals",
                column: "MerchantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.DropTable(
                name: "Merchants");
        }
    }
}
