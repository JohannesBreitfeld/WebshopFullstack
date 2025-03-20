namespace Webshop.Application.DTOs.Requests;

public class CreateOrderProductRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
