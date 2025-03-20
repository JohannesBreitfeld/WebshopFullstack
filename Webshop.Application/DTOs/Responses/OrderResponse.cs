namespace Webshop.Application.DTOs.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime DateTime { get; set; }
    public List<OrderProductResponse> Products { get; set; } = new();
}
