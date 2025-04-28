using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(sale => sale.Items).NotEmpty().ForEach(item =>
            {
                item.SetValidator(new CreateSaleItemValidator());
            });
        }
    }
}
