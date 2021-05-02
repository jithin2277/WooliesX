using FluentValidation;

namespace WooliesX.Application.UserDetails
{
    public class GetUserDetailsRequestValidator : AbstractValidator<GetUserDetailsRequest>
    {
        public GetUserDetailsRequestValidator()
        {
            RuleFor(v => v.Name).NotEmpty();
        }
    }
}
