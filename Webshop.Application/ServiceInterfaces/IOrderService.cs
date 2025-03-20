using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;

namespace Webshop.Application.ServiceInterfaces;

public interface IOrderService
{
    Task<OrdersResponse?> GetAllAsync();
    Task<OrderResponse?> GetByOrderIdAsync(int orderId);
    Task<OrdersResponse?> GetByCustomerIdAsync(int customerId);
    Task<OrderResponse?> AddAsync(CreateOrderRequest request);
}
