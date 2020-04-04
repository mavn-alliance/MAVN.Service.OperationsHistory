using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddedLinkWalletOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "link_wallet_operations",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    private_address = table.Column<string>(nullable: false),
                    public_address = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    direction = table.Column<string>(nullable: false),
                    fee = table.Column<string>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "link_wallet_operations",
                schema: "operation_history");
        }
    }
}
