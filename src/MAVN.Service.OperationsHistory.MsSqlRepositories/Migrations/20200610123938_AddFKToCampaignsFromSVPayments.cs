using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddFKToCampaignsFromSVPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "campaign_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_payments_campaign_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                column: "campaign_id");

            migrationBuilder.AddForeignKey(
                name: "FK_smart_voucher_payments_campaigns_campaign_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                column: "campaign_id",
                principalSchema: "operation_history",
                principalTable: "campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_smart_voucher_payments_campaigns_campaign_id",
                schema: "operation_history",
                table: "smart_voucher_payments");

            migrationBuilder.DropIndex(
                name: "IX_smart_voucher_payments_campaign_id",
                schema: "operation_history",
                table: "smart_voucher_payments");

            migrationBuilder.AlterColumn<string>(
                name: "campaign_id",
                schema: "operation_history",
                table: "smart_voucher_payments",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
