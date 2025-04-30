using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public static class CancelSaleHandlerTestData
    {
        private static readonly Faker<CancelSaleCommand> cancelSaleCommandFaker = new Faker<CancelSaleCommand>()
            .CustomInstantiator(f => new CancelSaleCommand(f.Random.Guid()));

        public static CancelSaleCommand GenerateValidCommand()
        {
            return cancelSaleCommandFaker.Generate();
        }

        public static Sale? CreateFakeSale(Guid saleId)
        {
            return new Sale
            {
                Id = saleId,
                SaleNumber = "S123",
                SaleDate = DateTime.UtcNow.AddDays(-1),
                CustomerName = "Client Test",
                Branch = "Branch Test",
                TotalAmount = 100,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow,
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product Test",
                        Quantity = 2,
                        UnitPrice = 50,
                        Discount = 0.1m,
                        CreatedAt = DateTime.UtcNow.AddDays(-1),
                        UpdatedAt = DateTime.UtcNow
                    }
                }
            };
        }
    }
}
