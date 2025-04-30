using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class CancelSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly CancelSaleHandler _handler;

        public CancelSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new CancelSaleHandler(_saleRepository);
        }

        [Fact]
        public async Task Handle_WhenSaleExists_ReturnsSuccessResponse()
        {
            // Arrange
            var command = CancelSaleHandlerTestData.GenerateValidCommand();
            var sale = CancelSaleHandlerTestData.CreateFakeSale(command.Id);
            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_WhenSaleDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            var command = CancelSaleHandlerTestData.GenerateValidCommand();
            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Sale>(null));

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Sale with id {command.Id} not exists");
        }

        [Fact]
        public async Task Handle_WhenValidationFails_ThrowsValidationException()
        {
            // Arrange
            var command = new CancelSaleCommand(Guid.Empty);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
