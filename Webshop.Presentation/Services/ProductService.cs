using Webshop.Application.DTOs.Responses;
using Webshop.Presentation.Mapping;
using Webshop.Presentation.Models;

namespace Webshop.Presentation.Services;

public class ProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<ProductModel>?> GetAllAsync()
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetFromJsonAsync<ProductsResponse>($"api/products");

        return response?.MapToModels();
    }



}
