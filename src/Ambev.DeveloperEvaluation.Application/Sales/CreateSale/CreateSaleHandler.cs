using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISaleItemService _saleItemService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="productRepository">The product repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public CreateSaleHandler(
            ISaleRepository saleRepository,
            IProductRepository productRepository,
            ISaleItemService saleItemService,
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _saleItemService = saleItemService;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the CreateSaleCommand request
        /// </summary>
        /// <param name="command">The CreateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale details</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            foreach (var item in command.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {item.ProductId} not found");

                item.UnitPrice = product.UnitPrice;
                item.ProductName = product.Name;
            }

            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = _mapper.Map<Sale>(command);

            foreach (var item in sale.Items)
            {
                _saleItemService.CalculateDiscount(item);
            }

            sale.SaleNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            sale.TotalAmount = sale.Items.Sum(item => item.Quantity * item.UnitPrice * (1 - item.Discount));

            await _saleRepository.CreateAsync(sale, cancellationToken);

            return _mapper.Map<CreateSaleResult>(sale);
        }
    }
}
