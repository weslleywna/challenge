using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales
{
    public static class UpdateSaleHandlerTestData
    {
        private static readonly Faker Faker = new();

        public static Product CreateFakeProduct(Guid? id = null)
        {
            return new Product
            {
                Id = id ?? Guid.NewGuid(),
                Name = Faker.Commerce.ProductName(),
                Description = Faker.Commerce.ProductDescription(),
                UnitPrice = Faker.Random.Decimal(1, 500),
                CreatedAt = DateTime.UtcNow
            };
        }

        public static SaleItem CreateFakeSaleItem(Product product, int quantity = 1)
        {
            return new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = quantity,
                UnitPrice = product.UnitPrice,
                Discount = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static Sale CreateFakeSale(Guid? id = null, Product? product = null)
        {
            product ??= CreateFakeProduct();

            return new Sale
            {
                Id = id ?? Guid.NewGuid(),
                SaleNumber = Faker.Random.AlphaNumeric(8),
                SaleDate = DateTime.UtcNow.AddDays(-1),
                CustomerName = Faker.Name.FullName(),
                Branch = Faker.Company.CompanyName(),
                TotalAmount = 0,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                Items = new List<SaleItem>
                {
                    CreateFakeSaleItem(product)
                }
            };
        }

        public static UpdateSaleCommand CreateFakeUpdateCommand(Guid? saleId = null, Guid? productId = null)
        {
            return new UpdateSaleCommand
            {
                SaleId = saleId ?? Guid.NewGuid(),
                CustomerName = Faker.Name.FullName(),
                Branch = Faker.Company.CompanyName(),
                Items = new List<UpdateSaleItemCommand>
                {
                    new UpdateSaleItemCommand
                    {
                        ProductId = productId ?? Guid.NewGuid(),
                        Quantity = Faker.Random.Int(1, 10)
                    }
                }
            };
        }
    }
}
