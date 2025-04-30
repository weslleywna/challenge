using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class DeleteSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DeleteSaleHandler _handler;

        public DeleteSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new DeleteSaleHandler(_saleRepository);
        }

        [Fact]
        public async Task Handle_WhenSaleExists_ReturnsSuccessResponse()
        {
            // Arrange
            var command = DeleteSaleHandlerTestData.GenerateValidCommand();
            var sale = DeleteSaleHandlerTestData.CreateFakeSale(command.Id);
            _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            await _saleRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_WhenSaleDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = DeleteSaleHandlerTestData.GenerateValidCommand();
            _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(false));

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.Id} not found");
        }

        [Fact]
        public async Task Handle_WhenValidationFails_ThrowsValidationException()
        {
            // Arrange
            var command = new DeleteSaleCommand(Guid.Empty );

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
