using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoMarket.Data.Migrations
{
    public partial class useradminroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "54b3c695-d546-4d1a-a652-450cded64ce6", "69932cf1-04aa-43b3-9458-f85949175a86", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "88908578-a319-4720-95dd-6aed96e47790", "6f6c4235-c91e-4b40-8012-ec11fe39de1c", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54b3c695-d546-4d1a-a652-450cded64ce6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88908578-a319-4720-95dd-6aed96e47790");
        }
    }
}
