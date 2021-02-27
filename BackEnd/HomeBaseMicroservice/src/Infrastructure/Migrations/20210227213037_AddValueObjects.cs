using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddValueObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "HomeBases");

            migrationBuilder.DropColumn(
                name: "CoordinateLat",
                table: "HomeBases");

            migrationBuilder.DropColumn(
                name: "CoordinateLon",
                table: "HomeBases");

            migrationBuilder.AddColumn<int>(
                name: "Capacity_Value",
                table: "HomeBases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CoordinateLat_Value",
                table: "HomeBases",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CoordinateLon_Value",
                table: "HomeBases",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity_Value",
                table: "HomeBases");

            migrationBuilder.DropColumn(
                name: "CoordinateLat_Value",
                table: "HomeBases");

            migrationBuilder.DropColumn(
                name: "CoordinateLon_Value",
                table: "HomeBases");

            migrationBuilder.AddColumn<double>(
                name: "Capacity",
                table: "HomeBases",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CoordinateLat",
                table: "HomeBases",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CoordinateLon",
                table: "HomeBases",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
