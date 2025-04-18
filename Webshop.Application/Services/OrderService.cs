using Microsoft.Extensions.Logging;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Application.EntityMapping;
using Webshop.Application.ServiceInterfaces;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public OrderService(IUnitOfWork unitOfWork, ILogger<OrderService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<OrderResponse?> AddAsync(CreateOrderRequest request)
    {
        try
        {
            var products = new List<Product>();

            foreach (var item in request.Products)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    _logger.LogError($"Could not find Product with ID {item.ProductId}");
                    return null;
                }

                if (product.StockBalance < item.Quantity)
                {
                    _logger.LogError($"Product with name {product.Name} doesn't have enough in stock");
                    return null;
                }

                product.StockBalance -= item.Quantity;
                _unitOfWork.Products.Update(product);
                products.Add(product);
            }

            var order = request.MapToOrder();

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveAsync();

            return order.MapToResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order");
            return null;
        }
    }

    public async Task<OrdersResponse?> GetAllAsync()
    {
        try
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            var response = orders.MapToResponse();

            return response;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error fetching orders");
            return null;
        }
    }

    public async Task<OrdersResponse?> GetByCustomerIdAsync(int customerId)
    {
        try
        {
            var orders = await _unitOfWork.Orders.GetByCustomerIdAsync(customerId);
            var response = orders.MapToResponse();

            return response;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, $"Error fetching orders with customer id {customerId}");
            return null;
        }
    }

    public async Task<OrderResponse?> GetByOrderIdAsync(int orderId)
    {
        try
        {
            var order = await _unitOfWork.Orders.GetByOrderIdAsync(orderId);
            if(order is null)
            {
                return null;
            }
            var response = order.MapToResponse();

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching order with order id {orderId}");
            return null;
        }
    }
}
