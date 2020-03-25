using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "operation_history");

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
                name: "transaction_history",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    receiver_id = table.Column<string>(nullable: false),
                    asset = table.Column<string>(nullable: false),
                    amount = table.Column<long>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfer", x => x.transaction_id);
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
                    amount = table.Column<long>(nullable: false),
                    bonus_type = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    partner_id = table.Column<string>(nullable: true),
                    campaign_id = table.Column<string>(nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bonus_cash_in",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "transaction_history",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "transfer",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "campaigns",
                schema: "operation_history");
        }
    }
}
