using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce_DataAccess.Migrations
{
    public partial class newTestDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PricePerUnit",
                table: "OrderDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "OrderDetail");
        }
    }
}
