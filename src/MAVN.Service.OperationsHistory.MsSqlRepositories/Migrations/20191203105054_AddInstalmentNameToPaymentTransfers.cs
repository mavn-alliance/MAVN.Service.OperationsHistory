using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddInstalmentNameToPaymentTransfers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "instalment_name",
                schema: "operation_history",
                table: "refunded_payment_transfers",
                nullable: false,
                defaultValue: "DefaultInstalmentName");

            migrationBuilder.AddColumn<string>(
                name: "instalment_name",
                schema: "operation_history",
                table: "payment_transfers",
                nullable: false,
                defaultValue: "DefaultInstalmentName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "instalment_name",
                schema: "operation_history",
                table: "refunded_payment_transfers");

            migrationBuilder.DropColumn(
                name: "instalment_name",
                schema: "operation_history",
                table: "payment_transfers");
        }
    }
}
