using Webshop.Application.DTOs.Responses;
using Webshop.Presentation.Models;

namespace Webshop.Presentation.Services;

public class CategoryService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CategoryService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<CategoryModel>?> GetAllAsync()
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetFromJsonAsync<ProductCategoriesResponse>("api/productcategories");

        if(response is null)
        {
            return null;
        }

        var categories = new List<CategoryModel>();
        foreach (var category in response.Items)
        {
            categories.Add(new CategoryModel() { Id = category.Id, Name = category.Name });
        }
        return categories;
    }
}
