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

    public async Task<bool> UpdateAsync(ProductModel model)
    {
        var id = model.Id;
        var request = model.MapToUpdateRequest();
        
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.PutAsJsonAsync<UpdateProductRequest>($"api/products/{id}", request);

        return response.IsSuccessStatusCode ? true : false;
    }

    public async Task<ProductModel?> AddProductAsync(ProductModel model)
    {
        var request = model.MapToCreateRequest();
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.PostAsJsonAsync<CreateProductRequest>($"api/products", request);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var productResponse = await response.Content.ReadFromJsonAsync<ProductResponse>();

        return productResponse?.MapToModel();
    }




}
