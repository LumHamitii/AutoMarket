using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoMarket.Data.Migrations
{
    public partial class testim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "YearOfProduction",
                table: "MotorcycleYears",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Mileage",
                table: "MotorcycleMileages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "TruckBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TruckColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TruckConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TruckFuelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckFuelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TruckMileages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mileage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckMileages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TruckModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TruckTransmissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransmissionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckTransmissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TruckVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Truck",
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
                    TruckBrandId = table.Column<int>(type: "int", nullable: false),
                    TruckModelId = table.Column<int>(type: "int", nullable: false),
                    TruckFuelTypeId = table.Column<int>(type: "int", nullable: false),
                    TruckColorId = table.Column<int>(type: "int", nullable: false),
                    TruckConditionId = table.Column<int>(type: "int", nullable: false),
                    TruckMileageId = table.Column<int>(type: "int", nullable: false),
                    TruckLoadCapacity = table.Column<int>(type: "int", nullable: false),
                    TruckTransmissionTypeId = table.Column<int>(type: "int", nullable: false),
                    TruckVersionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Truck_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Truck_TruckBrands_TruckBrandId",
                        column: x => x.TruckBrandId,
                        principalTable: "TruckBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Truck_TruckColors_TruckColorId",
                        column: x => x.TruckColorId,
                        principalTable: "TruckColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Truck_TruckConditions_TruckConditionId",
                        column: x => x.TruckConditionId,
                        principalTable: "TruckConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Truck_TruckFuelTypes_TruckFuelTypeId",
                        column: x => x.TruckFuelTypeId,
                        principalTable: "TruckFuelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Truck_TruckMileages_TruckMileageId",
                        column: x => x.TruckMileageId,
                        principalTable: "TruckMileages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Truck_TruckModels_TruckModelId",
                        column: x => x.TruckModelId,
                        principalTable: "TruckModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Truck_TruckTransmissionTypes_TruckTransmissionTypeId",
                        column: x => x.TruckTransmissionTypeId,
                        principalTable: "TruckTransmissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Truck_TruckVersions_TruckVersionId",
                        column: x => x.TruckVersionId,
                        principalTable: "TruckVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TruckPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruckId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckPhotos_Truck_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Truck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Truck_TruckBrandId",
                table: "Truck",
                column: "TruckBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_TruckColorId",
                table: "Truck",
                column: "TruckColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_TruckConditionId",
                table: "Truck",
                column: "TruckConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_TruckFuelTypeId",
                table: "Truck",
                column: "TruckFuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_TruckMileageId",
                table: "Truck",
                column: "TruckMileageId");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_TruckModelId",
                table: "Truck",
                column: "TruckModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_TruckTransmissionTypeId",
                table: "Truck",
                column: "TruckTransmissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_TruckVersionId",
                table: "Truck",
                column: "TruckVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_UserId",
                table: "Truck",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckPhotos_TruckId",
                table: "TruckPhotos",
                column: "TruckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TruckPhotos");

            migrationBuilder.DropTable(
                name: "Truck");

            migrationBuilder.DropTable(
                name: "TruckBrands");

            migrationBuilder.DropTable(
                name: "TruckColors");

            migrationBuilder.DropTable(
                name: "TruckConditions");

            migrationBuilder.DropTable(
                name: "TruckFuelTypes");

            migrationBuilder.DropTable(
                name: "TruckMileages");

            migrationBuilder.DropTable(
                name: "TruckModels");

            migrationBuilder.DropTable(
                name: "TruckTransmissionTypes");

            migrationBuilder.DropTable(
                name: "TruckVersions");

            migrationBuilder.AlterColumn<int>(
                name: "YearOfProduction",
                table: "MotorcycleYears",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Mileage",
                table: "MotorcycleMileages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
