using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddReferralIdAndConditionIdInBonuscashin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "condition_id",
                schema: "operation_history",
                table: "bonus_cash_in",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "referral_id",
                schema: "operation_history",
                table: "bonus_cash_in",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "condition_id",
                schema: "operation_history",
                table: "bonus_cash_in");

            migrationBuilder.DropColumn(
                name: "referral_id",
                schema: "operation_history",
                table: "bonus_cash_in");
        }
    }
}
