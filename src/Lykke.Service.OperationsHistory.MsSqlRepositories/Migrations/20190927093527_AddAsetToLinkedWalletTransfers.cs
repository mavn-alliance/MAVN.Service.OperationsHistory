using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddAsetToLinkedWalletTransfers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "asset",
                schema: "operation_history",
                table: "external_wallet_transfer",
                nullable: false,
                defaultValue: "MVN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "asset",
                schema: "operation_history",
                table: "external_wallet_transfer");
        }
    }
}
