namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        public Guid SaleId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public string Branch { get; set; } = string.Empty;

        public ICollection<UpdateSaleItemRequest> Items { get; set; } = new List<UpdateSaleItemRequest>();
    }
}
