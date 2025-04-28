using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
    {
        public GetProductRequestValidator()
        {
            RuleFor(product => product.Name).NotEmpty().Length(3, 200);
            RuleFor(product => product.Price).GreaterThan(0);
        }
    }
}
