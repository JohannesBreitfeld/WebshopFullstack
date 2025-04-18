using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Webshop.Application.DTOs.Requests;
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

    public async Task<bool> UpdateAsync(ProductModel model, string token)
    {
        var id = model.Id;
        var updateRequest = model.MapToUpdateRequest();

        var request = new HttpRequestMessage(HttpMethod.Put, $"api/products/{id}")
        {
            Content = new StringContent(
                JsonSerializer.Serialize(updateRequest),
                Encoding.UTF8,
                "application/json"
            )
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var client = _httpClientFactory.CreateClient("API");
        var response = await client.SendAsync(request);

        return response.IsSuccessStatusCode;
    }

    public async Task<ProductModel?> AddProductAsync(ProductModel model, string token)
    {
        var createRequest = model.MapToCreateRequest();
        var request = new HttpRequestMessage(HttpMethod.Post, "api/products")

        {
            Content = new StringContent(
            JsonSerializer.Serialize(createRequest),
             Encoding.UTF8,
            "application/json"
        )};
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var client = _httpClientFactory.CreateClient("API");
        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var productResponse = await response.Content.ReadFromJsonAsync<ProductResponse>();
            return productResponse?.MapToModel();
        }

        return null;
    }

    public async Task<bool> SoftDeleteAsync(int id, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"api/products/soft-delete/{id}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var client = _httpClientFactory.CreateClient("API");
        var response = await client.SendAsync(request);

        return response.IsSuccessStatusCode;
    }



}
