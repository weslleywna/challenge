using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(product => product.Name).NotEmpty().Length(3, 200);
            RuleFor(product => product.UnitPrice).GreaterThan(0);
        }
    }
}
