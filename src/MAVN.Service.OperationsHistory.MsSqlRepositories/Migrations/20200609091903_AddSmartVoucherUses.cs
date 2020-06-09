using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddSmartVoucherUses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "smart_voucher_uses",
                schema: "operation_history",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    linked_customer_id = table.Column<Guid>(nullable: true),
                    partner_id = table.Column<Guid>(nullable: false),
                    location_id = table.Column<Guid>(nullable: true),
                    campaign_id = table.Column<Guid>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    asset_symbol = table.Column<string>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smart_voucher_uses", x => x.id);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "smart_voucher_uses",
                schema: "operation_history");
        }
    }
}
