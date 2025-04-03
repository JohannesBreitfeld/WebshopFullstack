using Webshop.Application.DTOs.Responses;
using Webshop.Presentation.Mapping;
using Webshop.Presentation.Models;

namespace Webshop.Presentation.Services;

public class OrderService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrderService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<OrderModel>?> GetOrderByCustomerId(int id)
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetFromJsonAsync<OrdersResponse>($"api/orders/by-customer/{id}");

        return response?.MapToModels();
    }


    public async Task<List<OrderModel>?> GetAllOrdersAsync()
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetFromJsonAsync<OrdersResponse>("api/orders");

        return response?.MapToModels();
    }

}
