using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Migrations
{
    public partial class MigrateOperationTypesInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Update [operation_history].[transaction_history]
                                    SET type = 'PaymentTransferTokensReserved'
                                    WHERE type = 'PaymentTransfer'");

            migrationBuilder.Sql(@"Update [operation_history].[transaction_history]
                                   SET type = 'P2PTransfer'
                                   WHERE type = 'Transfer'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Update [operation_history].[transaction_history]
                                    SET type = 'PaymentTransfer'
                                    WHERE type = 'PaymentTransferTokensReserved'");

            migrationBuilder.Sql(@"Update [operation_history].[transaction_history]
                                   SET type = 'Transfer'
                                   WHERE type = 'P2PTransfer'");
        }
    }
}
