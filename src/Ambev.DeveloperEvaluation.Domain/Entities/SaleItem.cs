using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public required string ProductName { get; set; }
        public required decimal UnitPrice { get; set; }
        public required int Quantity { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Update(Guid productId, int quantity, decimal discount)
        {
            ProductId = productId;
            Quantity = quantity;
            Discount = discount;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
