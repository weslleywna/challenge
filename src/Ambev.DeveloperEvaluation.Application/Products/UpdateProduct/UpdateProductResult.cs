namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    /// <summary>
    /// Response model for UpdateProduct operation
    /// </summary>
    public class UpdateProductResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the newly updated product.
        /// </summary>
        /// <value>A GUID that uniquely identifies the updated product in the system.</value>
        public Guid Id { get; set; }
    }
}
