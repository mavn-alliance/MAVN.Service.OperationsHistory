using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class IntroduceMoney18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "amount",
                schema: "operation_history",
                table: "transfer",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "amount",
                schema: "operation_history",
                table: "refunded_payment_transfers",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "amount",
                schema: "operation_history",
                table: "refunded_partners_payments",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "amount",
                schema: "operation_history",
                table: "payment_transfers",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "amount",
                schema: "operation_history",
                table: "partners_payments",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "amount",
                schema: "operation_history",
                table: "bonus_cash_in",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "amount",
                schema: "operation_history",
                table: "transfer",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "amount",
                schema: "operation_history",
                table: "refunded_payment_transfers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "amount",
                schema: "operation_history",
                table: "refunded_partners_payments",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "amount",
                schema: "operation_history",
                table: "payment_transfers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "amount",
                schema: "operation_history",
                table: "partners_payments",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "amount",
                schema: "operation_history",
                table: "bonus_cash_in",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
