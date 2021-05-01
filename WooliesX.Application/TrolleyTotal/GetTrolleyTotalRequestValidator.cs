using FluentValidation;

namespace WooliesX.Application.TrolleyTotal
{
    public class GetTrolleyTotalRequestValidator : AbstractValidator<GetTrolleyTotalRequest>
    {
        public GetTrolleyTotalRequestValidator()
        {
            RuleFor(v => v.Trolley).NotEmpty();
            RuleFor(v => v.Trolley.Products).NotEmpty();
            RuleFor(v => v.Trolley.Quantities).NotEmpty();
            RuleFor(v => v.Trolley.Specials).NotEmpty();
        }
    }
}
