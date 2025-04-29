using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    /// <summary>
    /// Command for retrieving a product by their ID
    /// </summary>
    public class GetProductCommand : IRequest<GetProductResult>
    {
        public Guid Id { get; }

        public GetProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
