using Webshop.Domain.Enums;

namespace Webshop.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int StockBalance { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Active;
    public string ImageUrl { get; set; } = string.Empty;
    public int? ProductCategoryId { get; set; }
    public ProductCategory ProductCategory { get; set; } = null!;
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
