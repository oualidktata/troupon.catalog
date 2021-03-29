using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Troupon.Catalog.Service.Api.Migrations
{
    public partial class addedmerchantname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("189014f9-8e78-474f-96b7-d133ffd86d20"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("4549f44e-18e1-468d-9101-fd17db1f5e8f"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("71c72db9-02ef-4e6b-88ad-88f8295b4eba"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("7677b632-718f-4b82-9a0f-105adebca394"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("b569c247-d272-4a21-b7e7-341049596af5"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("ed66bc3d-0197-43b5-9648-e20af689d12e"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("ef058c71-3f17-4dc2-aa9d-89c7d5b045e1"));

            migrationBuilder.InsertData(
                table: "Deals",
                columns: new[] { "Id", "Description", "Details", "MerchantId", "Name" },
                values: new object[,]
                {
                    { new Guid("f6faaf05-4e4d-4beb-b13d-d3d4d8522c24"), "1", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("1edf029a-49d5-481b-8abc-82fb2984a935"), "2", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("c02fdfbc-5376-4e27-a1e4-5fb41169be80"), "3", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("bf636b10-73ab-4ac9-9b49-3cc7edc32fbf"), "4", "0", new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), "1" },
                    { new Guid("658383fd-fa67-4324-825c-287a754713f6"), "5", "0", new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), "1" },
                    { new Guid("2492f690-1a77-4cc1-8234-827338976189"), "6", "0", new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), "1" },
                    { new Guid("309a8ad5-5d37-4b09-a918-8832ddf20721"), "7", "1", new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), "1" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("1edf029a-49d5-481b-8abc-82fb2984a935"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("2492f690-1a77-4cc1-8234-827338976189"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("309a8ad5-5d37-4b09-a918-8832ddf20721"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("658383fd-fa67-4324-825c-287a754713f6"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("bf636b10-73ab-4ac9-9b49-3cc7edc32fbf"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("c02fdfbc-5376-4e27-a1e4-5fb41169be80"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("f6faaf05-4e4d-4beb-b13d-d3d4d8522c24"));

            migrationBuilder.InsertData(
                table: "Deals",
                columns: new[] { "Id", "Description", "Details", "MerchantId", "Name" },
                values: new object[,]
                {
                    { new Guid("189014f9-8e78-474f-96b7-d133ffd86d20"), "1", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("ef058c71-3f17-4dc2-aa9d-89c7d5b045e1"), "2", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("7677b632-718f-4b82-9a0f-105adebca394"), "3", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("4549f44e-18e1-468d-9101-fd17db1f5e8f"), "4", "0", new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), "1" },
                    { new Guid("71c72db9-02ef-4e6b-88ad-88f8295b4eba"), "5", "0", new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), "1" },
                    { new Guid("b569c247-d272-4a21-b7e7-341049596af5"), "6", "0", new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), "1" },
                    { new Guid("ed66bc3d-0197-43b5-9648-e20af689d12e"), "7", "1", new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), "1" }
                });
        }
    }
}
