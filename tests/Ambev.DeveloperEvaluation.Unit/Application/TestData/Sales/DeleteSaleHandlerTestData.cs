using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public static class DeleteSaleHandlerTestData
    {
        private static readonly Faker<DeleteSaleCommand> deleteSaleCommandFaker = new Faker<DeleteSaleCommand>()
            .CustomInstantiator(f => new DeleteSaleCommand(f.Random.Guid()));

        public static DeleteSaleCommand GenerateValidCommand()
        {
            return deleteSaleCommandFaker.Generate();
        }

        public static Sale CreateFakeSale(Guid saleId)
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
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
