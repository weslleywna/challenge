namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleResult
    {
        public Guid Id { get; set; }
        public string? SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string? CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Branch { get; set; }
        public ICollection<UpdateSaleItemResult>? Items { get; set; }
        public bool IsCancelled { get; set; } = false;
    }
}
