using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class AddSmartVoucherPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "smart_voucher_payments",
                schema: "operation_history",
                columns: table => new
                {
                    PaymentRequestId = table.Column<string>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    PartnerId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AssetSymbol = table.Column<string>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smart_voucher_payments", x => x.PaymentRequestId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_payments_CustomerId",
                schema: "operation_history",
                table: "smart_voucher_payments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_smart_voucher_payments_Timestamp",
                schema: "operation_history",
                table: "smart_voucher_payments",
                column: "Timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "smart_voucher_payments",
                schema: "operation_history");
        }
    }
}
