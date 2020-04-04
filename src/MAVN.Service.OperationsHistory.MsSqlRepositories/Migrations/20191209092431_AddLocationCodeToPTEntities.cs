using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddLocationCodeToPTEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "location_code",
                schema: "operation_history",
                table: "refunded_payment_transfers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "location_code",
                schema: "operation_history",
                table: "payment_transfers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "location_code",
                schema: "operation_history",
                table: "refunded_payment_transfers");

            migrationBuilder.DropColumn(
                name: "location_code",
                schema: "operation_history",
                table: "payment_transfers");
        }
    }
}
