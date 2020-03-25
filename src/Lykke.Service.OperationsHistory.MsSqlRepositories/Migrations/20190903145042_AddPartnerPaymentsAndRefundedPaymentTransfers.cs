using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddPartnerPaymentsAndRefundedPaymentTransfers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "partners_payments",
                schema: "operation_history",
                columns: table => new
                {
                    payment_request_id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    partner_id = table.Column<string>(nullable: false),
                    location_id = table.Column<string>(nullable: true),
                    amount = table.Column<long>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partners_payments", x => x.payment_request_id);
                });

            migrationBuilder.CreateTable(
                name: "refunded_partners_payments",
                schema: "operation_history",
                columns: table => new
                {
                    payment_request_id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    partner_id = table.Column<string>(nullable: false),
                    location_id = table.Column<string>(nullable: true),
                    amount = table.Column<long>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refunded_partners_payments", x => x.payment_request_id);
                });

            migrationBuilder.CreateTable(
                name: "refunded_payment_transfers",
                schema: "operation_history",
                columns: table => new
                {
                    transfer_id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    burn_rule_id = table.Column<string>(nullable: false),
                    invoice_id = table.Column<string>(nullable: false),
                    asset_symbol = table.Column<string>(nullable: false),
                    amount = table.Column<long>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refunded_payment_transfers", x => x.transfer_id);
                    table.ForeignKey(
                        name: "FK_refunded_payment_transfers_burn_rules_burn_rule_id",
                        column: x => x.burn_rule_id,
                        principalSchema: "operation_history",
                        principalTable: "burn_rules",
                        principalColumn: "burn_rule_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tiers_updates",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(type: "char(36)", nullable: false),
                    tier_id = table.Column<string>(type: "char(36)", nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tiers_updates", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_partners_payments_customer_id",
                schema: "operation_history",
                table: "partners_payments",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_partners_payments_timestamp",
                schema: "operation_history",
                table: "partners_payments",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_refunded_partners_payments_customer_id",
                schema: "operation_history",
                table: "refunded_partners_payments",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_refunded_partners_payments_timestamp",
                schema: "operation_history",
                table: "refunded_partners_payments",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_refunded_payment_transfers_burn_rule_id",
                schema: "operation_history",
                table: "refunded_payment_transfers",
                column: "burn_rule_id");

            migrationBuilder.CreateIndex(
                name: "IX_refunded_payment_transfers_customer_id",
                schema: "operation_history",
                table: "refunded_payment_transfers",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_refunded_payment_transfers_timestamp",
                schema: "operation_history",
                table: "refunded_payment_transfers",
                column: "timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "partners_payments",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "refunded_partners_payments",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "refunded_payment_transfers",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "tiers_updates",
                schema: "operation_history");
        }
    }
}
