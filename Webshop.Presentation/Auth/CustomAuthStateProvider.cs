using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Webshop.Application.DTOs.Responses;
using Webshop.Domain.Entities;

namespace Webshop.Presentation.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ProtectedLocalStorage _localStorage;

    public CustomAuthStateProvider(HttpClient httpClient, ProtectedLocalStorage localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task MarkUserAsAuthenticated(string token, CustomerResponse? customer)
    {
        Console.WriteLine($"Sparar token: {token}");

        await _localStorage.SetAsync("authToken", token);

        if (customer != null)
        {
            var customerJson = JsonSerializer.Serialize(customer);
            Console.WriteLine($"Sparar customerData: {customerJson}");
            await _localStorage.SetAsync("customerData", customerJson);
        }

        var authState = await GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task Logout()
    {
        await _localStorage.DeleteAsync("authToken");
        await _localStorage.DeleteAsync("customerData");

        var authState = await GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tokenResult = await _localStorage.GetAsync<string>("authToken");
        var token = tokenResult.Success 
            ? tokenResult.Value 
            : null;

        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var customerResult = await _localStorage.GetAsync<string>("customerData");
        var customerJson = customerResult.Success 
            ? customerResult.Value 
            : null;
        CustomerResponse? customer = string.IsNullOrEmpty(customerJson) 
            ? null 
            : JsonSerializer.Deserialize<CustomerResponse>(customerJson);

        var identity = string.IsNullOrEmpty(token)
            ? new ClaimsIdentity()
            : new ClaimsIdentity(new JwtSecurityTokenHandler().ReadJwtToken(token).Claims, "jwt");

        if (customer != null)
        {
            identity.AddClaim(new Claim("firstName", customer.FirstName));
            identity.AddClaim(new Claim("lastName", customer.LastName));
            identity.AddClaim(new Claim("email", customer.Email));
            identity.AddClaim(new Claim("streetAdress", customer.StreetAdress));
            identity.AddClaim(new Claim("postalCode", customer.PostalCode.ToString()));
            identity.AddClaim(new Claim("city", customer.City));
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task<CustomerResponse?> GetCustomerDataAsync()
    {
     
        var customerResult = await _localStorage.GetAsync<string>("customerData");
        if (customerResult.Success && !string.IsNullOrEmpty(customerResult.Value))
        {
            return JsonSerializer.Deserialize<CustomerResponse>(customerResult.Value);
        }

        return null;
    }
}


