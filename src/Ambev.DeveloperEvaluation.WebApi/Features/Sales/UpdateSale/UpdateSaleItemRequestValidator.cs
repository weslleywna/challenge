using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
    {
        public UpdateSaleItemRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("The product id is required.");
            RuleFor(x => x.Quantity).GreaterThan(0).LessThanOrEqualTo(20).WithMessage("The quantity must be greater than 0 and lesser or equal to 20.");
        }
    }
}
