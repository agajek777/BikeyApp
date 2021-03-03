using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CordsToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoordinateLon_Value",
                table: "HomeBases",
                newName: "CoordinateLon");

            migrationBuilder.RenameColumn(
                name: "CoordinateLat_Value",
                table: "HomeBases",
                newName: "CoordinateLat");

            migrationBuilder.RenameColumn(
                name: "Capacity_Value",
                table: "HomeBases",
                newName: "Capacity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoordinateLon",
                table: "HomeBases",
                newName: "CoordinateLon_Value");

            migrationBuilder.RenameColumn(
                name: "CoordinateLat",
                table: "HomeBases",
                newName: "CoordinateLat_Value");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "HomeBases",
                newName: "Capacity_Value");
        }
    }
}
