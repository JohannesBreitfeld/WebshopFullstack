using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Domain.Entities;

namespace Webshop.Application.EntityMapping;

public static class OrderMapping
{
    public static Order MapToOrder(this CreateOrderRequest request)
    {
        return new Order
        {
            CustomerId = request.CustomerId,
            DateTime = DateTime.Now,
            OrderProducts = request.Products.Select(p => new OrderProduct
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }).ToList()
        };
    }

    public static OrderResponse MapToResponse(this Order order)
    {
        return new OrderResponse()
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            DateTime = order.DateTime,
            Products = order.OrderProducts.Select(op => new OrderProductResponse
            {
                ProductId = op.ProductId,
                //ProductName = op.Product.Name,
                Quantity = op.Quantity
            }).ToList()
        };
    }

    public static OrdersResponse MapToResponse(this IEnumerable<Order> orders)
    {
        return new OrdersResponse
        {
            Items = orders.Select(p => p.MapToResponse())
        };
    }
}