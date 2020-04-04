using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddExternalWalletTransfers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "external_wallet_transfer",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    private_address = table.Column<string>(nullable: true),
                    public_address = table.Column<string>(nullable: true),
                    tx_hash = table.Column<string>(nullable: true),
                    timestamp = table.Column<DateTime>(nullable: false),
                    direction = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_external_wallet_transfer", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "external_wallet_transfer",
                schema: "operation_history");
        }
    }
}
