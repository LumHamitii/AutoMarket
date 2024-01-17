using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoMarket.Data.Migrations
{
    public partial class motorlocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_MotorcycleYears_MotorcycleYearId",
                table: "Motorcycles");

            migrationBuilder.DropTable(
                name: "MotorcycleYears");

            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_MotorcycleYearId",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "MotorcycleYearId",
                table: "Motorcycles");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Motorcycles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Motorcycles");

            migrationBuilder.AddColumn<int>(
                name: "MotorcycleYearId",
                table: "Motorcycles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MotorcycleYears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearOfProduction = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleYears", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleYearId",
                table: "Motorcycles",
                column: "MotorcycleYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_MotorcycleYears_MotorcycleYearId",
                table: "Motorcycles",
                column: "MotorcycleYearId",
                principalTable: "MotorcycleYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
