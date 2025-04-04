using Webshop.Application.DTOs.Responses;
using Webshop.Application.DTOs.UserDTOs;
using Webshop.Presentation.Mapping;
using Webshop.Presentation.Models;

namespace Webshop.Presentation.Services;

public class UserService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> RegisterAsync(RegisterModel model)
    {
        var request = model.MapToRequest();

        var client = _httpClientFactory.CreateClient("API");
        var response = await client.PostAsJsonAsync<RegisterUserRequest>("api/auth/register", request);

        return response.IsSuccessStatusCode ? true : false;
    }

}
