using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;

public static class GetSaleHandlerTestData
{
    public static Sale CreateFakeSale(Guid saleId)
    {
        var faker = new Faker();

        return new Sale
        {
            Id = saleId,
            SaleNumber = faker.Random.AlphaNumeric(10),
            SaleDate = faker.Date.Recent(),
            CustomerName = faker.Person.FullName,
            Branch = faker.Company.CompanyName(),
            TotalAmount = faker.Finance.Amount(100, 500),
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UpdatedAt = DateTime.UtcNow,
            Items = new List<SaleItem>
            {
                new SaleItem
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = faker.Commerce.ProductName(),
                    Quantity = faker.Random.Int(1, 10),
                    UnitPrice = faker.Random.Decimal(10, 100),
                    Discount = faker.Random.Decimal(0, 0.3m),
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    UpdatedAt = DateTime.UtcNow
                }
            }
        };
    }
}