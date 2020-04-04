using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddPartnerNameToPPsAndConditionToBonuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "partner_name",
                schema: "operation_history",
                table: "refunded_partners_payments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "partner_name",
                schema: "operation_history",
                table: "partners_payments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "condition_name",
                schema: "operation_history",
                table: "bonus_cash_in",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "partner_name",
                schema: "operation_history",
                table: "refunded_partners_payments");

            migrationBuilder.DropColumn(
                name: "partner_name",
                schema: "operation_history",
                table: "partners_payments");

            migrationBuilder.DropColumn(
                name: "condition_name",
                schema: "operation_history",
                table: "bonus_cash_in");
        }
    }
}
