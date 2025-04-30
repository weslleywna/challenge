using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleItemCommand> saleItemFaker = new Faker<CreateSaleItemCommand>()
        .RuleFor(i => i.ProductId, f => Guid.NewGuid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 20));

    private static readonly Faker<CreateSaleCommand> saleCommandFaker = new Faker<CreateSaleCommand>()
        .RuleFor(c => c.CustomerName, f => f.Company.CompanyName())
        .RuleFor(c => c.Branch, f => f.Company.CompanySuffix())
        .RuleFor(c => c.Items, f => saleItemFaker.Generate(f.Random.Int(1, 5)));

    public static CreateSaleCommand GenerateValidCommand()
    {
        return saleCommandFaker.Generate();
    }
}
