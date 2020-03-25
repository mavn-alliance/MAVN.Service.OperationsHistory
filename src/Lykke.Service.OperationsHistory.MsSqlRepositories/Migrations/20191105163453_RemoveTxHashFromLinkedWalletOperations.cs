using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class RemoveTxHashFromLinkedWalletOperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tx_hash",
                schema: "operation_history",
                table: "linked_wallet_transfer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tx_hash",
                schema: "operation_history",
                table: "linked_wallet_transfer",
                nullable: true);
        }
    }
}
