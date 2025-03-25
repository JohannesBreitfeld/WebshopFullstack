using System.Text.Json.Serialization;
using Webshop.Domain.Enums;

namespace Webshop.Application.DTOs.Requests;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int StockBalance { get; set; }
    public int? ProductCategoryId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public ProductStatus Status { get; set; } = ProductStatus.Active;
}
