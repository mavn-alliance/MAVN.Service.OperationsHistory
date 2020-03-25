using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddMoreIndices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "customer_id",
                schema: "operation_history",
                table: "payment_transfers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "asset_symbol",
                schema: "operation_history",
                table: "payment_transfers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_payment_transfers_customer_id",
                schema: "operation_history",
                table: "payment_transfers",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_transfers_timestamp",
                schema: "operation_history",
                table: "payment_transfers",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_bonus_cash_in_timestamp",
                schema: "operation_history",
                table: "bonus_cash_in",
                column: "timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_payment_transfers_customer_id",
                schema: "operation_history",
                table: "payment_transfers");

            migrationBuilder.DropIndex(
                name: "IX_payment_transfers_timestamp",
                schema: "operation_history",
                table: "payment_transfers");

            migrationBuilder.DropIndex(
                name: "IX_bonus_cash_in_timestamp",
                schema: "operation_history",
                table: "bonus_cash_in");

            migrationBuilder.DropColumn(
                name: "asset_symbol",
                schema: "operation_history",
                table: "payment_transfers");

            migrationBuilder.AlterColumn<string>(
                name: "customer_id",
                schema: "operation_history",
                table: "payment_transfers",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
