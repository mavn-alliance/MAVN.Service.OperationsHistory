using Falcon.Numerics;
using JetBrains.Annotations;

namespace Lykke.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// The amount of tokens response model
    /// </summary>
    [PublicAPI]
    public class TokensAmountResponseModel
    {
        /// <summary>
        /// The asset id
        /// </summary>
        public string Asset { get; set; }
        
        /// <summary>
        /// Earned amount
        /// </summary>
        public Money18 EarnedAmount { get; set; }
        
        /// <summary>
        /// Burned amount
        /// </summary>
        public Money18 BurnedAmount { get; set; }
    }
}
