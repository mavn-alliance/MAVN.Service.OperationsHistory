using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddAssetSymbolToPartnersPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "asset_symbol",
                schema: "operation_history",
                table: "refunded_partners_payments",
                nullable: false,
                defaultValue: "MVN");

            migrationBuilder.AddColumn<string>(
                name: "asset_symbol",
                schema: "operation_history",
                table: "partners_payments",
                nullable: false,
                defaultValue: "MVN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "asset_symbol",
                schema: "operation_history",
                table: "refunded_partners_payments");

            migrationBuilder.DropColumn(
                name: "asset_symbol",
                schema: "operation_history",
                table: "partners_payments");
        }
    }
}
