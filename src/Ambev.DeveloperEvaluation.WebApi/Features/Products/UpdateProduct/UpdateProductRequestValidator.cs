using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(product => product.Name).NotEmpty().Length(3, 200);
            RuleFor(product => product.Price).GreaterThan(0);
        }
    }
}
