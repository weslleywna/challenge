using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(sale => sale.Items).NotEmpty().ForEach(item =>
            {
                item.SetValidator(new UpdateSaleItemValidator());
            });
        }
    }
}
