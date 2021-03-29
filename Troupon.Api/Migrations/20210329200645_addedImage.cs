using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Troupon.Catalog.Service.Api.Migrations
{
    public partial class addedImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("100300d2-8450-4297-84ca-725b9d1a36c5"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("38fed865-a7ba-484b-ad82-7ffa657b6355"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("4edd43f5-9df4-4cf0-8638-e3fb6f959219"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("9141d48a-fa00-4113-8188-ee8435766972"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("9319778a-eb57-4c05-80bf-26765e54f1c2"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("dc336a37-8c38-4f49-89d0-279da2471132"));

            migrationBuilder.DeleteData(
                table: "Deals",
                keyColumn: "Id",
                keyValue: new Guid("dce8acc7-49af-4f89-b35d-29cea76e27ac"));

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

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("042038de-1e60-427d-bdce-7d683ffc8bf5"),
                column: "ImageUri",
                value: "https://picsum.photos/id/1011/200/300");

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"),
                column: "ImageUri",
                value: "https://picsum.photos/id/1003/200/300");

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"),
                column: "ImageUri",
                value: "https://picsum.photos/id/1023/200/300");

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"),
                column: "ImageUri",
                value: "https://picsum.photos/id/1012/200/300");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("4edd43f5-9df4-4cf0-8638-e3fb6f959219"), "1", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("9319778a-eb57-4c05-80bf-26765e54f1c2"), "2", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("100300d2-8450-4297-84ca-725b9d1a36c5"), "3", "0", new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), "1" },
                    { new Guid("38fed865-a7ba-484b-ad82-7ffa657b6355"), "4", "0", new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), "1" },
                    { new Guid("dce8acc7-49af-4f89-b35d-29cea76e27ac"), "5", "0", new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"), "1" },
                    { new Guid("dc336a37-8c38-4f49-89d0-279da2471132"), "6", "0", new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), "1" },
                    { new Guid("9141d48a-fa00-4113-8188-ee8435766972"), "7", "1", new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"), "1" }
                });

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("042038de-1e60-427d-bdce-7d683ffc8bf5"),
                column: "ImageUri",
                value: null);

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("532e8ec2-121d-4a86-bfe2-8812c2c27232"),
                column: "ImageUri",
                value: null);

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("5e448b39-db5b-42a4-bc12-52f34dcd5c14"),
                column: "ImageUri",
                value: null);

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("83c1dce6-97d5-4a35-afb7-4eb86577160c"),
                column: "ImageUri",
                value: null);
        }
    }
}
