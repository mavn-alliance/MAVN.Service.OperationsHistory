using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class MakePublicAddressNullableForLinkAndUnlinkOperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "public_address",
                schema: "operation_history",
                table: "link_wallet_operations",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "asset_symbol",
                schema: "operation_history",
                table: "link_wallet_operations",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "asset_symbol",
                schema: "operation_history",
                table: "link_wallet_operations");

            migrationBuilder.AlterColumn<string>(
                name: "public_address",
                schema: "operation_history",
                table: "link_wallet_operations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
