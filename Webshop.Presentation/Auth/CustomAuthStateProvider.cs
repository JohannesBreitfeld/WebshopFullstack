using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

    public async Task MarkUserAsAuthenticated(string token)
    {
        await _localStorage.SetAsync("authToken", token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task Logout()
    {
        await _localStorage.DeleteAsync("authToken");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tokenResult = await _localStorage.GetAsync<string>("authToken");
        var token = tokenResult.Success ? tokenResult.Value : null;

        ClaimsIdentity identity;

        if (string.IsNullOrEmpty(token))
        {
            identity = new ClaimsIdentity();
        }
        else
        {
            try
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var claims = jwtToken.Claims.ToList();

                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (!string.IsNullOrEmpty(roleClaim))
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleClaim));
                }

                identity = new ClaimsIdentity(claims, "jwt");
            }
            catch (Exception)
            {
                identity = new ClaimsIdentity();
            }
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }
}


