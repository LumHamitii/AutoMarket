using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoMarket.Data.Migrations
{
    public partial class motorcycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MotorcycleBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleFuelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fuel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleFuelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleMileages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mileage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleMileages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleTransmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Transmission = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleTransmissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleYears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearOfProduction = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnginePower = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotorcycleBrandId = table.Column<int>(type: "int", nullable: false),
                    MotorcycleModelId = table.Column<int>(type: "int", nullable: false),
                    MotorcycleYearId = table.Column<int>(type: "int", nullable: false),
                    MotorcycleTypeId = table.Column<int>(type: "int", nullable: false),
                    MotorcycleColorId = table.Column<int>(type: "int", nullable: false),
                    MotorcycleMileageId = table.Column<int>(type: "int", nullable: false),
                    MotorcycleConditionId = table.Column<int>(type: "int", nullable: false),
                    MotorcycleTransmissionId = table.Column<int>(type: "int", nullable: false),
                    MotorcycleFuelTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motorcycles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Motorcycles_MotorcycleBrands_MotorcycleBrandId",
                        column: x => x.MotorcycleBrandId,
                        principalTable: "MotorcycleBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motorcycles_MotorcycleColors_MotorcycleColorId",
                        column: x => x.MotorcycleColorId,
                        principalTable: "MotorcycleColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motorcycles_MotorcycleConditions_MotorcycleConditionId",
                        column: x => x.MotorcycleConditionId,
                        principalTable: "MotorcycleConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motorcycles_MotorcycleFuelTypes_MotorcycleFuelTypeId",
                        column: x => x.MotorcycleFuelTypeId,
                        principalTable: "MotorcycleFuelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motorcycles_MotorcycleMileages_MotorcycleMileageId",
                        column: x => x.MotorcycleMileageId,
                        principalTable: "MotorcycleMileages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motorcycles_MotorcycleModels_MotorcycleModelId",
                        column: x => x.MotorcycleModelId,
                        principalTable: "MotorcycleModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motorcycles_MotorcycleTransmissions_MotorcycleTransmissionId",
                        column: x => x.MotorcycleTransmissionId,
                        principalTable: "MotorcycleTransmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motorcycles_MotorcycleTypes_MotorcycleTypeId",
                        column: x => x.MotorcycleTypeId,
                        principalTable: "MotorcycleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motorcycles_MotorcycleYears_MotorcycleYearId",
                        column: x => x.MotorcycleYearId,
                        principalTable: "MotorcycleYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleBrandId",
                table: "Motorcycles",
                column: "MotorcycleBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleColorId",
                table: "Motorcycles",
                column: "MotorcycleColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleConditionId",
                table: "Motorcycles",
                column: "MotorcycleConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleFuelTypeId",
                table: "Motorcycles",
                column: "MotorcycleFuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleMileageId",
                table: "Motorcycles",
                column: "MotorcycleMileageId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleModelId",
                table: "Motorcycles",
                column: "MotorcycleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleTransmissionId",
                table: "Motorcycles",
                column: "MotorcycleTransmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleTypeId",
                table: "Motorcycles",
                column: "MotorcycleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleYearId",
                table: "Motorcycles",
                column: "MotorcycleYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_UserId",
                table: "Motorcycles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "MotorcycleBrands");

            migrationBuilder.DropTable(
                name: "MotorcycleColors");

            migrationBuilder.DropTable(
                name: "MotorcycleConditions");

            migrationBuilder.DropTable(
                name: "MotorcycleFuelTypes");

            migrationBuilder.DropTable(
                name: "MotorcycleMileages");

            migrationBuilder.DropTable(
                name: "MotorcycleModels");

            migrationBuilder.DropTable(
                name: "MotorcycleTransmissions");

            migrationBuilder.DropTable(
                name: "MotorcycleTypes");

            migrationBuilder.DropTable(
                name: "MotorcycleYears");
        }
    }
}
