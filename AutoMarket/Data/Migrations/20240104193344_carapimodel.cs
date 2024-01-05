using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoMarket.Data.Migrations
{
    public partial class carapimodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carapis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnginePower = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarBrandId = table.Column<int>(type: "int", nullable: false),
                    CarModelId = table.Column<int>(type: "int", nullable: false),
                    CarFuelTypeId = table.Column<int>(type: "int", nullable: false),
                    CarColorId = table.Column<int>(type: "int", nullable: false),
                    CarConditionId = table.Column<int>(type: "int", nullable: false),
                    CarMileageId = table.Column<int>(type: "int", nullable: false),
                    CarSeatsId = table.Column<int>(type: "int", nullable: false),
                    CarTransmissionTypeId = table.Column<int>(type: "int", nullable: false),
                    CarVersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carapis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carapis_Brands_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carapis_Colors_CarColorId",
                        column: x => x.CarColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carapis_Condition_CarConditionId",
                        column: x => x.CarConditionId,
                        principalTable: "Condition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carapis_FuelTypes_CarFuelTypeId",
                        column: x => x.CarFuelTypeId,
                        principalTable: "FuelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carapis_Mileages_CarMileageId",
                        column: x => x.CarMileageId,
                        principalTable: "Mileages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carapis_Models_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carapis_Seats_CarSeatsId",
                        column: x => x.CarSeatsId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carapis_TransmissionTypes_CarTransmissionTypeId",
                        column: x => x.CarTransmissionTypeId,
                        principalTable: "TransmissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carapis_Versions_CarVersionId",
                        column: x => x.CarVersionId,
                        principalTable: "Versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carapis_CarBrandId",
                table: "carapis",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_carapis_CarColorId",
                table: "carapis",
                column: "CarColorId");

            migrationBuilder.CreateIndex(
                name: "IX_carapis_CarConditionId",
                table: "carapis",
                column: "CarConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_carapis_CarFuelTypeId",
                table: "carapis",
                column: "CarFuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_carapis_CarMileageId",
                table: "carapis",
                column: "CarMileageId");

            migrationBuilder.CreateIndex(
                name: "IX_carapis_CarModelId",
                table: "carapis",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_carapis_CarSeatsId",
                table: "carapis",
                column: "CarSeatsId");

            migrationBuilder.CreateIndex(
                name: "IX_carapis_CarTransmissionTypeId",
                table: "carapis",
                column: "CarTransmissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_carapis_CarVersionId",
                table: "carapis",
                column: "CarVersionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carapis");
        }
    }
}
