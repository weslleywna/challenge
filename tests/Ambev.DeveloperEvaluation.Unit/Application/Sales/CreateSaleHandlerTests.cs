using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISaleItemService _saleItemService;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _saleItemService = Substitute.For<ISaleItemService>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _productRepository, _saleItemService, _mapper);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResult()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();

        foreach (var item in command.Items)
        {
            var product = new Product
            {
                Id = item.ProductId,
                Name = $"product-{item.ProductId}",
                Description = "string",
                UnitPrice = 10m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            _productRepository.GetByIdAsync(item.ProductId).Returns(product);
        }

        var saleEntity = new Sale
        {
            SaleNumber = "12345678",
            SaleDate = new DateTime(2025, 04, 29),
            CustomerName = command.CustomerName,
            Branch = command.Branch,
            TotalAmount = command.Items.Sum(i => i.Quantity * 10m),
            Items = command.Items.Select(item => new SaleItem
            {
                ProductId = item.ProductId,
                ProductName = $"product-{item.ProductId}",
                UnitPrice = 10m,
                Quantity = item.Quantity
            }).ToList()
        };

        _mapper.Map<Sale>(command).Returns(saleEntity);
        _mapper.Map<CreateSaleResult>(Arg.Any<Sale>()).Returns(new CreateSaleResult
        {
            SaleNumber = "12345678",
            SaleDate = new DateTime(2025, 04, 29),
            TotalAmount = saleEntity.TotalAmount
        });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.SaleNumber.Should().NotBeNullOrEmpty();
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        _saleItemService.Received(command.Items.Count).CalculateDiscount(Arg.Any<SaleItem>());
    }

    [Fact]
    public async Task Handle_ProductNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();

        _productRepository.GetByIdAsync(Arg.Any<Guid>()).Returns((Product?)null);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateSaleCommand();

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
