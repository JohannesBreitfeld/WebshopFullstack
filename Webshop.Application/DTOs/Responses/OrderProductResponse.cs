namespace Webshop.Application.DTOs.Responses;

public class OrderProductResponse
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
