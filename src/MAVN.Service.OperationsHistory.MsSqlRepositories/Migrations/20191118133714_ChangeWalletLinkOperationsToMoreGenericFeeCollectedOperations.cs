using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class ChangeWalletLinkOperationsToMoreGenericFeeCollectedOperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "fee_collected_operations",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    reason = table.Column<int>(nullable: false),
                    fee = table.Column<string>(nullable: false),
                    asset_symbol = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fee_collected_operations", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fee_collected_operations_customer_id",
                schema: "operation_history",
                table: "fee_collected_operations",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_fee_collected_operations_timestamp",
                schema: "operation_history",
                table: "fee_collected_operations",
                column: "timestamp");

            migrationBuilder.Sql(
                @"INSERT INTO [operation_history].[fee_collected_operations] (id, customer_id, timestamp, reason, fee, asset_symbol)
                    SELECT id, customer_id, timestamp, 0 as reason, fee, asset_symbol FROM [operation_history].[link_wallet_operations]");

            migrationBuilder.DropTable(
                name: "link_wallet_operations",
                schema: "operation_history");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fee_collected_operations",
                schema: "operation_history");

            migrationBuilder.CreateTable(
                name: "link_wallet_operations",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    asset_symbol = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    direction = table.Column<string>(nullable: false),
                    fee = table.Column<string>(nullable: false),
                    private_address = table.Column<string>(nullable: false),
                    public_address = table.Column<string>(nullable: true),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_link_wallet_operations", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_link_wallet_operations_customer_id",
                schema: "operation_history",
                table: "link_wallet_operations",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_link_wallet_operations_timestamp",
                schema: "operation_history",
                table: "link_wallet_operations",
                column: "timestamp");
        }
    }
}
