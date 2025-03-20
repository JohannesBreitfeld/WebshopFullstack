using Microsoft.Extensions.Logging;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Application.EntityMapping;
using Webshop.Application.ServiceInterfaces;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(IUnitOfWork unitOfWork, ILogger<CustomerService> logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerResponse?> CreateAsync(CreateCustomerRequest request)
    {
        try
        {
            var customer = request.MapToCustomer();
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveAsync();
            var response = customer.MapToResponse();

            return response;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error creating customer");
            return null;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);

            if(customer is null)
            {
                return false; 
            }

            _unitOfWork.Customers.Delete(customer);
            await _unitOfWork.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting customer");
            return false;
        }
    }

    public async Task<CustomersResponse?> GetAllAsync()
    {
        try
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            var response = customers.MapToResponse();
 
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting customers");
            return null;
        }
    }

    public async Task<CustomerResponse?> GetByEmailAsync(string email)
    {
        try
        {
            var customer = await _unitOfWork.Customers.GetByEmailAsync(email);
            if(customer is null)
            {
                return null;
            }

            var response = customer.MapToResponse();
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching customer with email {email}");
            return null;
        }
    }

    public async Task<CustomerResponse?> GetByIdAsync(int id)
    {
        try
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer is null)
            {
                return null;
            }
            var response = customer.MapToResponse();
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching customer with id {id}");
            return null;
        }
    }
     
    public async Task<CustomerResponse?> UpdateAsync(int id, UpdateCustomerRequest request)
    {
        try
        {
            var customer = request.MapToCustomer(id);
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.SaveAsync();
            var response = customer.MapToResponse();

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating customer");
            return null;
        }
    }
}


