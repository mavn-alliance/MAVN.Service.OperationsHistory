using AutoMapper;
using MAVN.Service.OperationsHistory.Client.Models.Responses;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ITransactionHistory, TransactionHistoryResponse>().ReverseMap();
            CreateMap<ITransfer, TransferResponse>()
                .ForMember(t => t.SenderCustomerEmail, opt => opt.Ignore())
                .ForMember(t => t.ReceiverCustomerEmail, opt => opt.Ignore())
                .ForMember(t => t.WalletAddress, o => o.MapFrom(m => m.SenderWalletAddress))
                .ForMember(t => t.OtherSideWalletAddress, o => o.MapFrom(m => m.ReceiverWalletAddress));
            CreateMap<Transfer, TransferResponse>()
                .ForMember(t => t.WalletAddress, o => o.MapFrom(m => m.SenderWalletAddress))
                .ForMember(t => t.OtherSideWalletAddress, o => o.MapFrom(m => m.ReceiverWalletAddress));
            CreateMap<BonusCashInDto, BonusCashInResponse>();
            CreateMap<IBonusCashIn, BonusCashInResponse>()
                .ForMember(i => i.CampaignName, opt => opt.Ignore());

            CreateMap<PaginatedTransactionHistory, PaginatedTransactionHistoryResponse>();
            CreateMap<PaginatedBonusesHistory, PaginatedBonusesHistoryResponse>();
            CreateMap<PaginatedPartnersPaymentsHistory, PaginatedPartnersPaymentsHistoryResponse>();
            CreateMap<PaginatedVoucherPurchasePaymentsHistory, PaginatedVoucherPurchasePaymentsHistoryResponse>();

            CreateMap<PaginatedCustomerOperationsModel, PaginatedCustomerOperationsResponse>();
            CreateMap<BasePagedModel, PaginatedCustomerOperationsResponse>()
                .ForMember(s => s.Transfers, o => o.Ignore())
                .ForMember(s => s.BonusCashIns, o => o.Ignore())
                .ForMember(s => s.PartnersPayments, o => o.Ignore())
                .ForMember(s => s.RefundedPartnersPayments, o => o.Ignore())
                .ForMember(s => s.ReferralStakes, o => o.Ignore())
                .ForMember(s => s.ReleasedReferralStakes, o => o.Ignore())
                .ForMember(s => s.LinkedWalletTransfers, o => o.Ignore())
                .ForMember(s => s.FeeCollectedOperations, o => o.Ignore())
                .ForMember(s => s.VoucherPurchasePayments, o => o.Ignore())
                .ForMember(s => s.SmartVoucherUses, o => o.Ignore())
                .ForMember(s => s.SmartVoucherPayments, o => o.Ignore());

            CreateMap<BasePagedModel, PaginatedTransactionHistoryResponse>()
                .ForMember(s => s.TransactionsHistory, o => o.Ignore());
            CreateMap<TokensAmountResultModel, TokensAmountResponseModel>();
            CreateMap<CustomerStatisticModel, CustomerStatisticResponse>();
            CreateMap<CustomersStatisticListModel, CustomersStatisticListResponse>();
            CreateMap<IPartnersPayment, PartnersPaymentResponse>();
            CreateMap<VoucherPurchasePaymentDto, VoucherPurchasePaymentResponse>(MemberList.Source);
            CreateMap<IVoucherPurchasePayment, VoucherPurchasePaymentResponse>(MemberList.Source);

            CreateMap<ReferralStakeDto, ReferralStakeResponse>();

            CreateMap<LinkedWalletTransferDto, LinkedWalletTransferResponse>()
                .ForMember(t => t.TransactionId, opt => opt.MapFrom(m => m.OperationId))
                .ForMember(t => t.WalletAddress, opt => opt.MapFrom(m => m.PrivateAddress))
                .ForMember(t => t.LinkedWalletAddress, opt => opt.MapFrom(m => m.PublicAddress));

            CreateMap<FeeCollectedOperationDto, FeeCollectedOperationResponse>()
                .ForMember(x => x.Reason, opt => opt.MapFrom(l => l.Reason));

            CreateMap<PaginatedSmartVoucherPaymentsHistory, PaginatedSmartVoucherPaymentsResponse>();
            CreateMap<ISmartVoucherPayment, SmartVoucherPaymentResponse>();
            CreateMap<ISmartVoucherUse, SmartVoucherUseResponse>(MemberList.Source);
        }
    }
}
