using System.Collections.ObjectModel;
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
            ObservableCollection<ProductModel>? models = new();
            if (response is null || response.Items.Count() == 0)
            {
                models = null;
            }
            else
            {
                foreach (var product in response.Items)
                {
                    models.Add(product.MapToModel());
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

        public static CreateProductRequest MapToCreateRequest(this ProductModel model)
        {
            return new CreateProductRequest()
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


        public static ProductModel MapToModel(this ProductResponse response)
        {
            return new ProductModel()
            {
                Id = response.Id,
                Name = response.Name,
                Price = response.Price,
                Description = response.Description,
                StockBalance = response.StockBalance,
                CategoryId = (int)response.ProductCategoryId,
                ImageUrl = response.ImageUrl,
                IsExpanded = false
            };
        }
    }
}
