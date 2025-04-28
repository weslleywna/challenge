using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(sale => sale.Items).NotEmpty().ForEach(item =>
            {
                item.SetValidator(new UpdateSaleItemRequestValidator());
            });
        }
    }
}
