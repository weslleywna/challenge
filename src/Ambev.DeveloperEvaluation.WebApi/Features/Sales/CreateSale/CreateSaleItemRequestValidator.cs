using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("The product id is required.");

            RuleFor(x => x.ProductName).NotEmpty().WithMessage("The product name is required.");

            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("The unit price must be greater than 0.");

            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("The quantity must be greater than 0.");
        }
    }

}
