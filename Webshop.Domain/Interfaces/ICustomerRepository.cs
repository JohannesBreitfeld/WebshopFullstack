using Webshop.Domain.Entities;

namespace Webshop.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer?> GetByEmailAsync(string email);
    Task AddAsync(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
}

