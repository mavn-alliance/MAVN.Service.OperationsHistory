using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddCustomerWalletAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "receiver_wallet_address",
                schema: "operation_history",
                table: "transfer",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sender_wallet_address",
                schema: "operation_history",
                table: "transfer",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "receiver_wallet_address",
                schema: "operation_history",
                table: "transfer");

            migrationBuilder.DropColumn(
                name: "sender_wallet_address",
                schema: "operation_history",
                table: "transfer");
        }
    }
}
