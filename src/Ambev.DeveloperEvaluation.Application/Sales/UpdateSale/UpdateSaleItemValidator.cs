using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleItemValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        public UpdateSaleItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("The product id is required.");
            RuleFor(x => x.Quantity).GreaterThan(0).LessThanOrEqualTo(20).WithMessage("The quantity must be greater than 0 and lesser or equal to 20.");
        }
    }
}
