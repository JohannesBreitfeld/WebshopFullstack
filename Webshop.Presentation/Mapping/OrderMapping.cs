using System.Runtime.CompilerServices;
using Webshop.Application.DTOs.Responses;
using Webshop.Presentation.Models;

namespace Webshop.Presentation.Mapping;

public static class OrderMapping
{
    public static List<OrderModel>? MapToModels(this OrdersResponse orders)
    {
        List<OrderModel>? models = new();
        if (orders is null || orders.Items.Count() == 0)
        {
            models = null;
        }
        else
        {
            foreach (var order in orders.Items)
            {
                models.Add(new OrderModel()
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    DateTime = order.DateTime,
                    Products = order.Products,
                    ShowDetails = false
                });
            }
        }
        return models;
    }
}
