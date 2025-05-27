using Webshop.Application.DTOs.Responses;

namespace Webshop.Presentation.Models;

public class OrderModel
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime DateTime { get; set; }
    public List<OrderProductResponse> Products { get; set; } = new();
    public bool ShowDetails = false;
    public bool ProductsLoaded { get; set; }
}
