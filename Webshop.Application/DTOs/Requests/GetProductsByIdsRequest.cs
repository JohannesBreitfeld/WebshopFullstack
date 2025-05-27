namespace Webshop.Application.DTOs.Requests;

public class GetProductsByIdsRequest
{
    public IEnumerable<int> ProductsIds { get; set; } = [];
}
