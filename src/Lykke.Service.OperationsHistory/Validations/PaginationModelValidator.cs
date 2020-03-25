using FluentValidation;
using Lykke.Service.OperationsHistory.Client.Models.Requests;

namespace Lykke.Service.OperationsHistory.Validations
{
    public class PaginationModelValidator : AbstractValidator<PaginationModel>
    {
        public PaginationModelValidator()
        {
            RuleFor(x => x.CurrentPage)
                .LessThanOrEqualTo(int.MaxValue)
                .WithMessage($"Current Page value can't be bigger then ${int.MaxValue}")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Current page can't be less than 1")
                .Must((page, list, context) =>
                {
                    context.MessageFormatter.AppendArgument("IntMax", int.MaxValue);
                    context.MessageFormatter.AppendArgument("CurrentPage", nameof(page.CurrentPage));
                    context.MessageFormatter.AppendArgument("PageSize", nameof(page.PageSize));
                    return ((long)page.CurrentPage - 1) * page.PageSize <= int.MaxValue;
                })
                .WithMessage("The requested combination of ({CurrentPage} - 1) * {PageSize} can't be more than {IntMax}"); ;
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page Size can't be less than 1")
                .LessThanOrEqualTo(1000)
                .WithMessage("Page Size cannot exceed more then 1000");
        }
    }
}
