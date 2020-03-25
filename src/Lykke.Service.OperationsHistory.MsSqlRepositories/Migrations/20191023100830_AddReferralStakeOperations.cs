using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddReferralStakeOperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "referral_stakes",
                schema: "operation_history");

            migrationBuilder.DropTable(
                name: "released_referral_stakes",
                schema: "operation_history");
        }
    }
}
