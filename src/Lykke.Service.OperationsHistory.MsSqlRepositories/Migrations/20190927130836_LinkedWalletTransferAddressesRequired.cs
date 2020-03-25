using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class LinkedWalletTransferAddressesRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "public_address",
                schema: "operation_history",
                table: "linked_wallet_transfer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "private_address",
                schema: "operation_history",
                table: "linked_wallet_transfer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "public_address",
                schema: "operation_history",
                table: "linked_wallet_transfer",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "private_address",
                schema: "operation_history",
                table: "linked_wallet_transfer",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
