namespace Webshop.Application.DTOs.Responses;

public class ProductCategoriesResponse
{
    public required IEnumerable<ProductCategoryResponse> Items { get; init; } = Enumerable.Empty<ProductCategoryResponse>();
}
