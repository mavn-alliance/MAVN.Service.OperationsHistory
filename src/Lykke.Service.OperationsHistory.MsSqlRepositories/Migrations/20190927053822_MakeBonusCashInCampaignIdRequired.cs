using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class MakeBonusCashInCampaignIdRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bonus_cash_in_campaigns_campaign_id",
                schema: "operation_history",
                table: "bonus_cash_in");

            migrationBuilder.AlterColumn<string>(
                name: "campaign_id",
                schema: "operation_history",
                table: "bonus_cash_in",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_bonus_cash_in_campaigns_campaign_id",
                schema: "operation_history",
                table: "bonus_cash_in",
                column: "campaign_id",
                principalSchema: "operation_history",
                principalTable: "campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bonus_cash_in_campaigns_campaign_id",
                schema: "operation_history",
                table: "bonus_cash_in");

            migrationBuilder.AlterColumn<string>(
                name: "campaign_id",
                schema: "operation_history",
                table: "bonus_cash_in",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_bonus_cash_in_campaigns_campaign_id",
                schema: "operation_history",
                table: "bonus_cash_in",
                column: "campaign_id",
                principalSchema: "operation_history",
                principalTable: "campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
