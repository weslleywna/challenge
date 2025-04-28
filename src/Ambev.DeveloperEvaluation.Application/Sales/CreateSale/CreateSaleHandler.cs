using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="productRepository">The product repository</param>
        /// <param name="unitOfWork">Unit of work.</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public CreateSaleHandler(
            ISaleRepository saleRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the CreateProductCommand request
        /// </summary>
        /// <param name="command">The CreateProduct command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created product details</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = _mapper.Map<Sale>(command);

            sale.SaleNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            sale.TotalAmount = sale.Items.Sum(x => x.Quantity * x.UnitPrice);

            await _saleRepository.CreateAsync(sale, cancellationToken);

            return _mapper.Map<CreateSaleResult>(sale);
        }
    }
}
