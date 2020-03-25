using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class VoucherPurchasePayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "voucher_purchase_payments",
                schema: "operation_history",
                columns: table => new
                {
                    transfer_id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    spend_rule_id = table.Column<Guid>(nullable: false),
                    voucher_id = table.Column<Guid>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    asset_symbol = table.Column<string>(type: "varchar(10)", nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voucher_purchase_payments", x => x.transfer_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_voucher_purchase_payments_customer_id",
                schema: "operation_history",
                table: "voucher_purchase_payments",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_voucher_purchase_payments_timestamp",
                schema: "operation_history",
                table: "voucher_purchase_payments",
                column: "timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "voucher_purchase_payments",
                schema: "operation_history");
        }
    }
}
