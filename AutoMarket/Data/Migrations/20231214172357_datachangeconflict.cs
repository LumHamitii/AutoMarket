using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoMarket.Data.Migrations
{
    public partial class datachangeconflict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add a new temporary column to store the converted years
            migrationBuilder.AddColumn<int>(
                name: "TempYear",
                table: "MotorcycleYears",
                type: "int",
                nullable: true);

            // Update the temporary column with the year part of the datetime
            migrationBuilder.Sql("UPDATE MotorcycleYears SET TempYear = YEAR(YearOfProduction)");

            // Remove the original datetime column
            migrationBuilder.DropColumn(
                name: "YearOfProduction",
                table: "MotorcycleYears");

            // Rename the temporary column to the original column name
            migrationBuilder.RenameColumn(
                name: "TempYear",
                table: "MotorcycleYears",
                newName: "YearOfProduction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add a new temporary column to store the converted years
            migrationBuilder.AddColumn<DateTime>(
                name: "TempYear",
                table: "MotorcycleYears",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.MinValue);

            // Update the temporary column with the datetime values
            migrationBuilder.Sql("UPDATE MotorcycleYears SET TempYear = CAST(CAST(YearOfProduction AS NVARCHAR(4)) + '-01-01' AS DATETIME)");

            // Remove the original int column
            migrationBuilder.DropColumn(
                name: "YearOfProduction",
                table: "MotorcycleYears");

            // Rename the temporary column to the original column name
            migrationBuilder.RenameColumn(
                name: "TempYear",
                table: "MotorcycleYears",
                newName: "YearOfProduction");
        }
    }
}
