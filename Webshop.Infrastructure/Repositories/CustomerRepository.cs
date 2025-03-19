﻿using Fullstack.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync() => await _context.Customers.ToListAsync();
    
    public async Task<Customer?> GetByIdAsync(int id) => await _context.Customers.FindAsync(id);
    
    public async Task<Customer?> GetByEmailAsync(string email) => await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);

    public async Task AddAsync(Customer customer) => await _context.Customers.AddAsync(customer);
    
    public void Update(Customer customer) => _context.Customers.Update(customer);
    
    public void Delete(Customer customer) => _context.Customers.Remove(customer);
}
