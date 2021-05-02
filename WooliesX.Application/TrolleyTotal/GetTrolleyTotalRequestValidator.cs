using FluentValidation;

namespace WooliesX.Application.TrolleyTotal
{
    public class GetTrolleyTotalRequestValidator : AbstractValidator<GetTrolleyTotalRequest>
    {
        public GetTrolleyTotalRequestValidator()
        {
            RuleFor(v => v.Trolley).NotEmpty();
        }
    }
}
