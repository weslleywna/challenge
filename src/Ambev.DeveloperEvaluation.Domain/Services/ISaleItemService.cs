using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleItemService
    {
        void CalculateDiscount(SaleItem item);
    }
}
