using System;
using FluentValidation;
using Lykke.Service.OperationsHistory.Client.Models.Requests;

namespace Lykke.Service.OperationsHistory.Validations
{
    public class PeriodRequestValidator 
        : AbstractValidator<PeriodRequest>
    {
        public PeriodRequestValidator()
        {
            RuleFor(o => o.FromDate.Date)
                .NotEmpty()
                .WithMessage("FromDate is required")
                .LessThanOrEqualTo(x => DateTime.UtcNow.Date)
                .WithMessage("FromDate must be equal or earlier than today.");

            RuleFor(o => o.ToDate.Date)
                .NotEmpty()
                .WithMessage("ToDate is required")
                .GreaterThanOrEqualTo(x => x.FromDate.Date)
                .WithMessage("ToDate must be equal or later than FromDate.")
                .LessThanOrEqualTo(x => DateTime.UtcNow.Date)
                .WithMessage("ToDate must be equal or earlier than today.");
        }
    }
}
