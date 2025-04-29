namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleResult
    {
        public Guid Id { get; set; }
        public string? SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string? CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Branch { get; set; }
        public ICollection<CreateSaleItemResult>? Items { get; set; } 
    }
}
