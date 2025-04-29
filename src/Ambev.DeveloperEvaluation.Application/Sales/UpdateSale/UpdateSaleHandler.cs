using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
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
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
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

            var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);
            if (sale is null)
                throw new InvalidOperationException($"Sale with id {command.SaleId} not exists");

            var saleItems = ManageItems(sale, command.Items.ToList());

            sale.Update(DateTime.UtcNow, command.CustomerName, command.Branch, saleItems);          

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return _mapper.Map<UpdateSaleResult>(sale);
        }

        private static List<SaleItem> ManageItems(Sale sale, List<UpdateSaleItemCommand> items)
        {
            var updatedProductIds = items.Select(i => i.ProductId).ToHashSet();

            sale.Items.ToList().RemoveAll(item => !updatedProductIds.Contains(item.ProductId));

            foreach (var commandItem in items)
            {
                var existingItem = sale.Items.FirstOrDefault(x => x.ProductId == commandItem.ProductId);

                if (existingItem != null)
                {
                    existingItem.ProductName = commandItem.ProductName;
                    existingItem.UnitPrice = commandItem.UnitPrice;
                    existingItem.Quantity = commandItem.Quantity;
                    existingItem.Discount = commandItem.Discount;
                    existingItem.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    sale.Items.Add(new SaleItem
                    {
                        ProductId = commandItem.ProductId,
                        ProductName = commandItem.ProductName,
                        UnitPrice = commandItem.UnitPrice,
                        Quantity = commandItem.Quantity,
                        Discount = commandItem.Discount,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            return sale.Items.ToList();
        }
    }
}
