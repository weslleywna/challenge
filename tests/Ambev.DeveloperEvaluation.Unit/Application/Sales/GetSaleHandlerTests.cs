using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetSaleHandlerTest
    {
        private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly GetSaleHandler _handler;

        public GetSaleHandlerTest()
        {
            _handler = new GetSaleHandler(_saleRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedResult_WhenSaleExists()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var fakeSale = GetSaleHandlerTestData.CreateFakeSale(saleId);
            var expectedResult = new GetSaleResult();

            _saleRepository.GetByIdAsyncWithItems(saleId, Arg.Any<CancellationToken>())
                .Returns(fakeSale);
            _mapper.Map<GetSaleResult>(fakeSale).Returns(expectedResult);

            var command = new GetSaleCommand(saleId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new GetSaleCommand(Guid.Empty);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowKeyNotFoundException_WhenSaleDoesNotExist()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            _saleRepository.GetByIdAsyncWithItems(saleId, Arg.Any<CancellationToken>())
                .Returns((Sale)null);

            var command = new GetSaleCommand(saleId);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {saleId} not found");
        }
    }
}