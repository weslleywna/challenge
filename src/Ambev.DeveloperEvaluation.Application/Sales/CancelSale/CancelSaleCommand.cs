using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Command for cancel a sale.
    /// </summary>
    public class CancelSaleCommand : IRequest<CancelSaleResponse>
    {
        public Guid Id { get; }

        public CancelSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
