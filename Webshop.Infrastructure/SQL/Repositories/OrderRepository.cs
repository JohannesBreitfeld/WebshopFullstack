using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;
using Webshop.Infrastructure.SQL.Data;

namespace Webshop.Infrastructure.SQL.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return  await _context.Orders
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ToListAsync();
    }

    public async Task<Order?> GetByOrderIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId) 
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task AddAsync(Order order) => await _context.Orders.AddAsync(order);
}
