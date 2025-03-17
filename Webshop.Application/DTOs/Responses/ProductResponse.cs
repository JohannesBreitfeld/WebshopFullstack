using System.Text.Json.Serialization;
using Webshop.Domain.Enums;

namespace Webshop.Application.DTOs.Responses;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = String.Empty;
    public int StockBalance { get; set; }
    public int ProductCategoryId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public ProductStatus Status { get; set; }
}
