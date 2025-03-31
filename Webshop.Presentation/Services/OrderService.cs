using Webshop.Application.DTOs.Responses;

namespace Webshop.Presentation.Services;

public class OrderService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrderService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<OrdersResponse?> GetOrderByCustomerId(int id)
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetFromJsonAsync<OrdersResponse>($"api/orders/by-customer/{id}");

        return response;
    }

}
