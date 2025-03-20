namespace Webshop.Application.DTOs.Requests;

public class CreateOrderRequest
{
    public int CustomerId { get; set; }
    public List<CreateOrderProductRequest> Products { get; set; } = new();
}
