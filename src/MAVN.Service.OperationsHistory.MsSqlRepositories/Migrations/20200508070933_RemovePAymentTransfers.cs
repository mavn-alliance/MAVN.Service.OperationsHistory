using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class RemovePAymentTransfers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_transfers",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "refunded_payment_transfers",
                schema: "operation_history");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payment_transfers",
                schema: "operation_history",
                columns: table => new
                {
                    transfer_id = table.Column<string>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    asset_symbol = table.Column<string>(nullable: false),
                    burn_rule_id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    instalment_name = table.Column<string>(nullable: false),
                    invoice_id = table.Column<string>(nullable: false),
                    location_code = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_transfers", x => x.transfer_id);
                    table.ForeignKey(
                        name: "FK_payment_transfers_burn_rules_burn_rule_id",
                        column: x => x.burn_rule_id,
                        principalSchema: "operation_history",
                        principalTable: "burn_rules",
                        principalColumn: "burn_rule_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refunded_payment_transfers",
                schema: "operation_history",
                columns: table => new
                {
                    transfer_id = table.Column<string>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    asset_symbol = table.Column<string>(nullable: false),
                    burn_rule_id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    instalment_name = table.Column<string>(nullable: false),
                    invoice_id = table.Column<string>(nullable: false),
                    location_code = table.Column<string>(nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_payment_transfers_burn_rule_id",
                schema: "operation_history",
                table: "payment_transfers",
                column: "burn_rule_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_transfers_customer_id",
                schema: "operation_history",
                table: "payment_transfers",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_transfers_timestamp",
                schema: "operation_history",
                table: "payment_transfers",
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
    }
}
