using Falcon.Numerics;

namespace Lykke.Service.OperationsHistory.Domain.Models
{
    public class TokensAmountResultModel
    {
        public string Asset { get; set; }
        public Money18 EarnedAmount { get; set; }
        
        public Money18 BurnedAmount { get; set; }
    }
}
