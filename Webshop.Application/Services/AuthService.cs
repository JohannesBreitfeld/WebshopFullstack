using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Webshop.Application.DTOs.UserDTOs;
using Webshop.Application.EntityMapping;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;



public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponse?> RegisterAsync(RegisterUserRequest request)
    {
        var customer = request.Customer.MapToCustomer();

        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveAsync();

        var user = new ApplicationUser
        {
            UserName = customer.Email,
            Email = customer.Email,
            CustomerId = customer.Id
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return null;
        }

        await _userManager.AddToRoleAsync(user, "Customer");

        return new AuthResponse { Token = GenerateJwtToken(user, "Customer"), Customer = customer.MapToResponse() };
    }

    public async Task<AuthResponse?> LoginAsync(LoginUserRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        { 
            return null; 
        }

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "Customer";

        if(user.CustomerId is not null)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync((int)user.CustomerId);
            return customer is not null
                ? new AuthResponse { Token = GenerateJwtToken(user, role), Customer = customer.MapToResponse() }
                : new AuthResponse { Token = GenerateJwtToken(user, role) };
        }

        return new AuthResponse { Token = GenerateJwtToken(user, role)};
    }

    private string GenerateJwtToken(ApplicationUser user, string role)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!);
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email!),
        new Claim(ClaimTypes.Role, role)
    };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );
        Console.WriteLine($"Generated JWT Token: {new JwtSecurityTokenHandler().WriteToken(token)}");
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

