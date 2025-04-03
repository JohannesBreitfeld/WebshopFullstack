using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Domain.Entities;

namespace Webshop.Presentation.Services;

public class CustomerService
{
    private readonly ProtectedLocalStorage _localStorage;
    private readonly IHttpClientFactory _httpClientFactory;

    public CustomerService(ProtectedLocalStorage localStorage, IHttpClientFactory httpClientFactory)
    {
        _localStorage = localStorage;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<CustomerResponse?> GetCustomerAsync()
    {
        try
        {
            var result = await _localStorage.GetAsync<string>("customerData");
            if (result.Success && !string.IsNullOrEmpty(result.Value))
            {
                return JsonSerializer.Deserialize<CustomerResponse>(result.Value);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid hämtning av kunddata: {ex.Message}");
        }

        return null;
    }

    public async Task<CustomerResponse?> UpdateCustomerAsync(int id, UpdateCustomerRequest request)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("API");
            var response = await client.PutAsJsonAsync($"api/customers/{id}", request);

            if (response.IsSuccessStatusCode)
            {
                var updatedCustomer = await response.Content.ReadFromJsonAsync<CustomerResponse>();

                if (updatedCustomer != null)
                {
                    var customerJson = JsonSerializer.Serialize(updatedCustomer);
                    await _localStorage.SetAsync("customerData", customerJson);
                    return updatedCustomer;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid uppdatering av kunddata: {ex.Message}");
        }

        return null;
    }
}
