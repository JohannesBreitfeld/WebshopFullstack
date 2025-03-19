using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;

namespace Webshop.Application.ServiceInterfaces;

public interface ICustomerService
{
    Task<CustomersResponse?> GetAllAsync();
    Task<CustomerResponse?> GetByIdAsync(int id);
    Task<CustomerResponse?> GetByEmailAsync(string email);
    Task<CustomerResponse?> CreateAsync(CreateCustomerRequest request);
    Task<CustomerResponse?> UpdateAsync(int id, UpdateCustomerRequest request);
    Task<bool> DeleteAsync(int id);
}

