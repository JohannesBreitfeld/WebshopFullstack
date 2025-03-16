namespace Webshop.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = String.Empty;
    public int StockBalance { get; set; }
    public bool Status { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
