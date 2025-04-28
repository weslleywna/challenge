using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public required string SaleNumber { get; set; } 
        public DateTime SaleDate { get; set; }
        public required string CustomerName { get; set; } 
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; } = string.Empty;
        public List<SaleItem>? Items { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
