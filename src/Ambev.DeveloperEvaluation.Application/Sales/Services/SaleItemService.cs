using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Sales.Services
{
    public class SaleItemService : ISaleItemService
    {
        public void CalculateDiscount(SaleItem item)
        {
            if (item.Quantity < 4)
            {
                item.Discount = 0;
            }
            else if (item.Quantity >= 4 && item.Quantity < 10)
            {
                item.Discount = 0.10m;
            }
            else if (item.Quantity >= 10 && item.Quantity <= 20)
            {
                item.Discount = 0.20m;
            }
        }
    }
}
