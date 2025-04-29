using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public required string SaleNumber { get; set; } 
        public required DateTime SaleDate { get; set; }
        public required string CustomerName { get; set; } 
        public required decimal TotalAmount { get; set; }
        public string? Branch { get; set; }
        public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
        public bool IsCancelled { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Update(DateTime saleDate, string customerName, string branch, List<SaleItem> saleItems)
        {
            SaleDate = saleDate;
            CustomerName = customerName;
            Items = saleItems;
            Branch = branch;
            TotalAmount = Items.Sum(item => item.Quantity * item.UnitPrice * (1 - item.Discount));
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
