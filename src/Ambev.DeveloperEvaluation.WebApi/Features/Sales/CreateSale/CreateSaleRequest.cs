namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public string CustomerName { get; set; } = string.Empty;

        public string Branch { get; set; } = string.Empty;

        public ICollection<CreateSaleItemRequest> Items { get; set; } = new List<CreateSaleItemRequest>();
    }
}
