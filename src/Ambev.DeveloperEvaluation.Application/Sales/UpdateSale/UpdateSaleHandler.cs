using Ambev.DeveloperEvaluation.Application.Sales.Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISaleItemService _saleItemService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of UpdateSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="productRepository">The product repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public UpdateSaleHandler(
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
        /// Handles the UpdateSaleCommand request
        /// </summary>
        /// <param name="command">The UpdateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sale details</returns>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsyncWithItems(command.SaleId, cancellationToken);
            if (sale is null)
                throw new InvalidOperationException($"Sale with id {command.SaleId} not exists");

            var saleItems = await ManageItems(sale, command.Items.ToList());

            sale.Update(DateTime.UtcNow, command.CustomerName, command.Branch, saleItems);          

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return _mapper.Map<UpdateSaleResult>(sale);
        }

        private async Task<List<SaleItem>> ManageItems(Sale sale, List<UpdateSaleItemCommand> items)
        {
            var updatedProductIds = items.Select(i => i.ProductId).ToHashSet();

            sale.Items.ToList().RemoveAll(item => !updatedProductIds.Contains(item.ProductId));

            foreach (var commandItem in items)
            {
                var existingItem = sale.Items.FirstOrDefault(x => x.ProductId == commandItem.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity = commandItem.Quantity;
                    existingItem.UpdatedAt = DateTime.UtcNow;
                    _saleItemService.CalculateDiscount(existingItem);
                }
                else
                {
                    var product = await _productRepository.GetByIdAsync(commandItem.ProductId);

                    if (product == null)
                        throw new KeyNotFoundException($"Product with ID {commandItem.ProductId} not found");

                    var newItem = new SaleItem
                    {
                        ProductName = product.Name,
                        UnitPrice = product.UnitPrice,
                        ProductId = commandItem.ProductId,
                        Quantity = commandItem.Quantity,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    _saleItemService.CalculateDiscount(newItem);

                    sale.Items.Add(newItem);
                }
            }

            return sale.Items.ToList();
        }
    }
}
