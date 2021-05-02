using FluentValidation;

namespace WooliesX.Application.Products
{
    public class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
    {
        public GetProductsRequestValidator()
        {
            RuleFor(v => v.SortOption).NotEmpty();
            RuleFor(v => v.SortOption).IsInEnum();
        }
    }
}
