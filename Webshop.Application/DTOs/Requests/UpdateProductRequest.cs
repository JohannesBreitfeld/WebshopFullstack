using System.Text.Json.Serialization;
using Webshop.Domain.Enums;

namespace Webshop.Application.DTOs.Requests;

public class UpdateProductRequest
{
    public string Name { get; set; } = String.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = String.Empty;
    public int StockBalance { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductStatus Status { get; set; } = ProductStatus.Active;
}
