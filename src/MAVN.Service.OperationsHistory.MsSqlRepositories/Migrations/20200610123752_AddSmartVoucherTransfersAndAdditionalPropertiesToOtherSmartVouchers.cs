using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddSmartVoucherTransfersAndAdditionalPropertiesToOtherSmartVouchers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "ShortCode",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "short_code");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "partner_id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "CampaignId",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "campaign_id");

            migrationBuilder.RenameColumn(
                name: "AssetSymbol",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "asset_symbol");

            migrationBuilder.RenameColumn(
                name: "PaymentRequestId",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "payment_request_id");

            migrationBuilder.RenameIndex(
                name: "IX_smart_voucher_payments_Timestamp",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "IX_smart_voucher_payments_timestamp");

            migrationBuilder.RenameIndex(
                name: "IX_smart_voucher_payments_CustomerId",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "IX_smart_voucher_payments_customer_id");

            migrationBuilder.AlterColumn<string>(
                name: "campaign_id",
                schema: "operation_history",
                table: "smart_voucher_uses",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "partner_name",
                schema: "operation_history",
                table: "smart_voucher_uses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "vertical",
                schema: "operation_history",
                table: "smart_voucher_uses",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "campaign_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "partner_name",
                schema: "operation_history",
                table: "smart_voucher_payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "vertical",
                schema: "operation_history",
                table: "smart_voucher_payments",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_uses_campaign_id",
                schema: "operation_history",
                table: "smart_voucher_uses",
                column: "campaign_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_smart_voucher_uses_campaigns_campaign_id",
                schema: "operation_history",
                table: "smart_voucher_uses",
                column: "campaign_id",
                principalSchema: "operation_history",
                principalTable: "campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_smart_voucher_uses_campaigns_campaign_id",
                schema: "operation_history",
                table: "smart_voucher_uses");

            migrationBuilder.DropTable(
                name: "smart_voucher_transfers",
                schema: "operation_history");

            migrationBuilder.DropIndex(
                name: "IX_smart_voucher_uses_campaign_id",
                schema: "operation_history",
                table: "smart_voucher_uses");

            migrationBuilder.DropColumn(
                name: "partner_name",
                schema: "operation_history",
                table: "smart_voucher_uses");

            migrationBuilder.DropColumn(
                name: "vertical",
                schema: "operation_history",
                table: "smart_voucher_uses");

            migrationBuilder.DropColumn(
                name: "partner_name",
                schema: "operation_history",
                table: "smart_voucher_payments");

            migrationBuilder.DropColumn(
                name: "vertical",
                schema: "operation_history",
                table: "smart_voucher_payments");

            migrationBuilder.RenameColumn(
                name: "timestamp",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "amount",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "short_code",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "ShortCode");

            migrationBuilder.RenameColumn(
                name: "partner_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "PartnerId");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "campaign_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "CampaignId");

            migrationBuilder.RenameColumn(
                name: "asset_symbol",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "AssetSymbol");

            migrationBuilder.RenameColumn(
                name: "payment_request_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "PaymentRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_smart_voucher_payments_timestamp",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "IX_smart_voucher_payments_Timestamp");

            migrationBuilder.RenameIndex(
                name: "IX_smart_voucher_payments_customer_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                newName: "IX_smart_voucher_payments_CustomerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "campaign_id",
                schema: "operation_history",
                table: "smart_voucher_uses",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "CampaignId",
                schema: "operation_history",
                table: "smart_voucher_payments",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
