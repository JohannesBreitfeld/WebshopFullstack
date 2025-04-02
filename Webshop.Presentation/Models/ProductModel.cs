﻿namespace Webshop.Presentation.Models;

public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int StockBalance { get; set; } 
    public int CategoryId { get; set; }
    public CategoryModel? CategoryModel { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsExpanded { get; set; } = false;
}