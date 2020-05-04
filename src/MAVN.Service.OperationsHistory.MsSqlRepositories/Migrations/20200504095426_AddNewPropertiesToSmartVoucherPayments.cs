using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddNewPropertiesToSmartVoucherPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CampaignId",
                schema: "operation_history",
                table: "smart_voucher_payments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ShortCode",
                schema: "operation_history",
                table: "smart_voucher_payments",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CampaignId",
                schema: "operation_history",
                table: "smart_voucher_payments");

            migrationBuilder.DropColumn(
                name: "ShortCode",
                schema: "operation_history",
                table: "smart_voucher_payments");
        }
    }
}
