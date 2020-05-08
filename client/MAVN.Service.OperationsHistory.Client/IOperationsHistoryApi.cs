using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.OperationsHistory.Client.Models.Requests;
using MAVN.Service.OperationsHistory.Client.Models.Responses;
using Refit;

namespace MAVN.Service.OperationsHistory.Client
{
    /// <summary>
    /// OperationsHistory client API interface.
    /// </summary>
    [PublicAPI]
    public interface IOperationsHistoryApi
    {
        /// <summary>
        /// Gets paged transactions history by customer id
        /// </summary>
        /// <param name="paginationModel">Information of which page you want the data for</param>
        /// <param name="customerId">Id of the customer</param>
        [Get("/api/transactions/{customerId}")]
        Task<PaginatedCustomerOperationsResponse> GetByCustomerIdAsync(string customerId,
            PaginationModel paginationModel);

        /// <summary>
        /// Gets paged transactions history between two dates
        /// </summary>
        /// <param name="paginationModel">Information of which page you want the data for</param>
        [Get("/api/transactions")]
        Task<PaginatedTransactionHistoryResponse> GetByDateAsync(PaginationModelWithDatesRange paginationModel);

        /// <summary>
        /// Gets paged bonus rewards history between two dates
        /// </summary>
        /// <param name="paginationModel">Information of which page you want the data for</param>
        [Get("/api/bonuses")]
        Task<PaginatedBonusesHistoryResponse> GetBonusesByDatesAsync(PaginationModelWithDatesRange paginationModel);

        /// <summary>
        /// Gets paged partners payments history between two dates
        /// </summary>
        /// <param name="paginationModel">Information of which page you want the data for</param>
        [Get("/api/partners-payments")]
        Task<PaginatedPartnersPaymentsHistoryResponse> GetPartnersPaymentsByDatesAsync(PaginationModelWithDatesRange paginationModel);

        /// <summary>
        /// Gets paged voucher purchase payments history between two dates
        /// </summary>
        /// <param name="paginationModel">Information of which page you want the data for</param>
        [Get("/api/voucher-purchases")]
        Task<PaginatedVoucherPurchasePaymentsHistoryResponse> GetVoucherPurchasePaymentsByDatesAsync(PaginationModelWithDatesRange paginationModel);

        /// <summary>
        /// Gets paged smart voucher payments history between two dates
        /// </summary>
        /// <returns><see cref="PaginatedSmartVoucherPaymentsResponse"/></returns>
        [Get("/api/smart-voucher-payments")]
        Task<PaginatedSmartVoucherPaymentsResponse> GetSmartVoucherPaymentsByDatesAsync(
            PaginationModelWithDatesRange paginationModel);

        /// <summary>
        /// Gets bonus cash ins transfers for customer per campaign
        /// </summary>
        /// <returns><see cref="BonusCashInResponse"/></returns>
        [Get("/api/bonuses/customerId/{customerId}/campaignId/{campaignId}")]
        Task<IEnumerable<BonusCashInResponse>> GetBonusCashInsAsync(
            string customerId,
            string campaignId);

        /// <summary>
        /// Gets bonus cash ins transfers for customer per referral
        /// </summary>
        /// <returns><see cref="BonusCashInResponse"/></returns>
        [Get("/api/bonuses/customerId/{customerId}/referralId/{referralId}")]
        Task<IEnumerable<BonusCashInResponse>> GetBonusCashInsByReferralAsync(
            string customerId,
            string referralId);
    }
}
