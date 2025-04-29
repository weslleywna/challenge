using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    /// <summary>
    /// Command for deleting a product.
    /// </summary>
    public class DeleteProductCommand : IRequest<DeleteProductResponse>
    {
        public Guid Id { get; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
