using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
        private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
        private readonly ISaleItemService _saleItemService = Substitute.For<ISaleItemService>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();

        private readonly UpdateSaleHandler _handler;

        public UpdateSaleHandlerTests()
        {
            _handler = new UpdateSaleHandler(
                _saleRepository,
                _productRepository,
                _saleItemService,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ShouldUpdateSaleSuccessfully()
        {
            // Arrange
            var product = UpdateSaleHandlerTestData.CreateFakeProduct();
            var sale = UpdateSaleHandlerTestData.CreateFakeSale(Guid.NewGuid(), product);
            var command = UpdateSaleHandlerTestData.CreateFakeUpdateCommand(sale.Id, product.Id);

            _saleRepository.GetByIdAsyncWithItems(sale.Id, Arg.Any<CancellationToken>()).Returns(sale);
            _productRepository.GetByIdAsync(product.Id).Returns(product);
            _mapper.Map<UpdateSaleResult>(Arg.Any<Sale>()).Returns(new UpdateSaleResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenInvalidCommand()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.CreateFakeUpdateCommand();
            command.Items.First().ProductId = Guid.Empty;

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowInvalidOperationException_WhenSaleNotFound()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.CreateFakeUpdateCommand();

            _saleRepository.GetByIdAsyncWithItems(command.SaleId, Arg.Any<CancellationToken>())
                .Returns((Sale?)null);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Sale with id {command.SaleId} not exists");
        }

        [Fact]
        public async Task Handle_ShouldThrowKeyNotFoundException_WhenProductNotFound()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.CreateFakeUpdateCommand();
            var sale = UpdateSaleHandlerTestData.CreateFakeSale(command.SaleId);

            _saleRepository.GetByIdAsyncWithItems(command.SaleId, Arg.Any<CancellationToken>())
                .Returns(sale);

            _productRepository.GetByIdAsync(command.Items.First().ProductId)
                .Returns((Product?)null);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Product with ID {command.Items.First().ProductId} not found");
        }
    }
}
