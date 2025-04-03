using System.Runtime.CompilerServices;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Domain.Entities;
using Webshop.Presentation.Models;

namespace Webshop.Presentation.Mapping
{
    public static class ProductMapping
    {
        public static IEnumerable<ProductModel>? MapToModels(this ProductsResponse response)
        {
            List<ProductModel>? models = new();
            if (response is null || response.Items.Count() == 0)
            {
                models = null;
            }
            else
            {
                foreach (var product in response.Items)
                {
                    models.Add(new ProductModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description,
                        StockBalance = product.StockBalance,
                        CategoryId = (int)product.ProductCategoryId,
                        ImageUrl = product.ImageUrl,
                        IsExpanded = false
                    });
                }
            }
            return models;
        }

        public static UpdateProductRequest MapToUpdateRequest(this ProductModel model)
        {
            return new UpdateProductRequest()
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                StockBalance = model.StockBalance,
                ProductCategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                Status = model.Status
            };
        }
    }
}
