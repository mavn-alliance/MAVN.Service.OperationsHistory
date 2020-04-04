namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Fee collection reason
    /// </summary>
    public enum FeeCollectionReason
    {
        /// <summary>
        /// Fee was collected because of wallet linking
        /// </summary>
        WalletLinking,
        /// <summary>
        /// Fee was collected because of transfer to the public network
        /// </summary>
        TransferToPublic,
    }
}
