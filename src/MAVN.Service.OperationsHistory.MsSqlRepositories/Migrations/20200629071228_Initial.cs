using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "operation_history");

            migrationBuilder.CreateTable(
                name: "burn_rules",
                schema: "operation_history",
                columns: table => new
                {
                    burn_rule_id = table.Column<string>(nullable: false),
                    burn_rule_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_burn_rules", x => x.burn_rule_id);
                });

            migrationBuilder.CreateTable(
                name: "campaigns",
                schema: "operation_history",
                columns: table => new
                {
                    CampaignId = table.Column<string>(nullable: false),
                    CampaignName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaigns", x => x.CampaignId);
                });

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

            migrationBuilder.CreateTable(
                name: "link_wallet_operations",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    private_address = table.Column<string>(nullable: false),
                    public_address = table.Column<string>(nullable: true),
                    timestamp = table.Column<DateTime>(nullable: false),
                    direction = table.Column<string>(nullable: false),
                    fee = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_link_wallet_operations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "linked_wallet_transfer",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    private_address = table.Column<string>(nullable: false),
                    public_address = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    direction = table.Column<int>(nullable: false),
                    asset = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_linked_wallet_transfer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "partners_payments",
                schema: "operation_history",
                columns: table => new
                {
                    payment_request_id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    partner_id = table.Column<string>(nullable: false),
                    partner_name = table.Column<string>(nullable: false),
                    location_id = table.Column<string>(nullable: true),
                    amount = table.Column<string>(nullable: false),
                    asset_symbol = table.Column<string>(nullable: false),
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
                    partner_name = table.Column<string>(nullable: false),
                    location_id = table.Column<string>(nullable: true),
                    amount = table.Column<string>(nullable: false),
                    asset_symbol = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refunded_partners_payments", x => x.payment_request_id);
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

            migrationBuilder.CreateTable(
                name: "transaction_history",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_id = table.Column<string>(nullable: false),
                    transaction_id = table.Column<string>(nullable: false),
                    asset = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transfer",
                schema: "operation_history",
                columns: table => new
                {
                    transaction_id = table.Column<string>(nullable: false),
                    external_operation_id = table.Column<string>(nullable: true),
                    sender_id = table.Column<string>(nullable: false),
                    sender_wallet_address = table.Column<string>(nullable: false),
                    receiver_id = table.Column<string>(nullable: false),
                    receiver_wallet_address = table.Column<string>(nullable: false),
                    asset = table.Column<string>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfer", x => x.transaction_id);
                });

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

            migrationBuilder.CreateTable(
                name: "bonus_cash_in",
                schema: "operation_history",
                columns: table => new
                {
                    transaction_id = table.Column<string>(nullable: false),
                    external_operation_id = table.Column<string>(nullable: true),
                    customer_id = table.Column<string>(nullable: false),
                    asset = table.Column<string>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    location_code = table.Column<string>(nullable: true),
                    bonus_type = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    partner_id = table.Column<string>(nullable: true),
                    location_id = table.Column<string>(nullable: true),
                    campaign_id = table.Column<string>(nullable: false),
                    condition_name = table.Column<string>(nullable: true),
                    condition_id = table.Column<string>(nullable: true),
                    referral_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bonus_cash_in", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_bonus_cash_in_campaigns_campaign_id",
                        column: x => x.campaign_id,
                        principalSchema: "operation_history",
                        principalTable: "campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "referral_stakes",
                schema: "operation_history",
                columns: table => new
                {
                    referral_id = table.Column<string>(nullable: false),
                    campaign_id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    asset = table.Column<string>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_stakes", x => x.referral_id);
                    table.ForeignKey(
                        name: "FK_referral_stakes_campaigns_campaign_id",
                        column: x => x.campaign_id,
                        principalSchema: "operation_history",
                        principalTable: "campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "released_referral_stakes",
                schema: "operation_history",
                columns: table => new
                {
                    referral_id = table.Column<string>(nullable: false),
                    campaign_id = table.Column<string>(nullable: false),
                    customer_id = table.Column<string>(nullable: false),
                    asset = table.Column<string>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_released_referral_stakes", x => x.referral_id);
                    table.ForeignKey(
                        name: "FK_released_referral_stakes_campaigns_campaign_id",
                        column: x => x.campaign_id,
                        principalSchema: "operation_history",
                        principalTable: "campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "smart_voucher_payments",
                schema: "operation_history",
                columns: table => new
                {
                    payment_request_id = table.Column<string>(nullable: false),
                    short_code = table.Column<string>(nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    partner_id = table.Column<Guid>(nullable: false),
                    partner_name = table.Column<string>(nullable: true),
                    vertical = table.Column<string>(nullable: true),
                    campaign_id = table.Column<string>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    asset_symbol = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smart_voucher_payments", x => x.payment_request_id);
                    table.ForeignKey(
                        name: "FK_smart_voucher_payments_campaigns_campaign_id",
                        column: x => x.campaign_id,
                        principalSchema: "operation_history",
                        principalTable: "campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "smart_voucher_transfers",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    old_customer_id = table.Column<Guid>(nullable: false),
                    new_customer_id = table.Column<Guid>(nullable: false),
                    partner_id = table.Column<Guid>(nullable: false),
                    partner_name = table.Column<string>(nullable: true),
                    vertical = table.Column<string>(nullable: true),
                    campaign_id = table.Column<string>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    asset_symbol = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    short_code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smart_voucher_transfers", x => x.id);
                    table.ForeignKey(
                        name: "FK_smart_voucher_transfers_campaigns_campaign_id",
                        column: x => x.campaign_id,
                        principalSchema: "operation_history",
                        principalTable: "campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "smart_voucher_uses",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    linked_customer_id = table.Column<Guid>(nullable: true),
                    partner_id = table.Column<Guid>(nullable: false),
                    partner_name = table.Column<string>(nullable: true),
                    vertical = table.Column<string>(nullable: true),
                    location_id = table.Column<Guid>(nullable: true),
                    campaign_id = table.Column<string>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    asset_symbol = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smart_voucher_uses", x => x.id);
                    table.ForeignKey(
                        name: "FK_smart_voucher_uses_campaigns_campaign_id",
                        column: x => x.campaign_id,
                        principalSchema: "operation_history",
                        principalTable: "campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bonus_cash_in_campaign_id",
                schema: "operation_history",
                table: "bonus_cash_in",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_bonus_cash_in_customer_id",
                schema: "operation_history",
                table: "bonus_cash_in",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_bonus_cash_in_timestamp",
                schema: "operation_history",
                table: "bonus_cash_in",
                column: "timestamp");

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
                name: "IX_referral_stakes_campaign_id",
                schema: "operation_history",
                table: "referral_stakes",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_referral_stakes_customer_id",
                schema: "operation_history",
                table: "referral_stakes",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_referral_stakes_timestamp",
                schema: "operation_history",
                table: "referral_stakes",
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
                name: "IX_released_referral_stakes_campaign_id",
                schema: "operation_history",
                table: "released_referral_stakes",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_released_referral_stakes_customer_id",
                schema: "operation_history",
                table: "released_referral_stakes",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_released_referral_stakes_timestamp",
                schema: "operation_history",
                table: "released_referral_stakes",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_payments_campaign_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_payments_customer_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_payments_timestamp",
                schema: "operation_history",
                table: "smart_voucher_payments",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_transfers_campaign_id",
                schema: "operation_history",
                table: "smart_voucher_transfers",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_transfers_new_customer_id",
                schema: "operation_history",
                table: "smart_voucher_transfers",
                column: "new_customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_transfers_old_customer_id",
                schema: "operation_history",
                table: "smart_voucher_transfers",
                column: "old_customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_transfers_timestamp",
                schema: "operation_history",
                table: "smart_voucher_transfers",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_uses_campaign_id",
                schema: "operation_history",
                table: "smart_voucher_uses",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_uses_customer_id",
                schema: "operation_history",
                table: "smart_voucher_uses",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_uses_timestamp",
                schema: "operation_history",
                table: "smart_voucher_uses",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_history_customer_id",
                schema: "operation_history",
                table: "transaction_history",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_history_timestamp",
                schema: "operation_history",
                table: "transaction_history",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_history_transaction_id",
                schema: "operation_history",
                table: "transaction_history",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_transfer_receiver_id",
                schema: "operation_history",
                table: "transfer",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "IX_transfer_sender_id",
                schema: "operation_history",
                table: "transfer",
                column: "sender_id");

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
                name: "bonus_cash_in",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "burn_rules",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "fee_collected_operations",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "link_wallet_operations",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "linked_wallet_transfer",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "partners_payments",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "referral_stakes",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "refunded_partners_payments",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "released_referral_stakes",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "smart_voucher_payments",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "smart_voucher_transfers",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "smart_voucher_uses",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "tiers_updates",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "transaction_history",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "transfer",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "voucher_purchase_payments",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "campaigns",
                schema: "operation_history");
        }
    }
}
