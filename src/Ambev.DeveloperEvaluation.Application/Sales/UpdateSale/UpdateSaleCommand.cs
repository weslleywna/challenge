using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid SaleId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public string Branch { get; set; } = string.Empty;

        public ICollection<UpdateSaleItemCommand> Items { get; set; } = new List<UpdateSaleItemCommand>();

        public ValidationResultDetail Validate()
        {
            var validator = new UpdateSaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
