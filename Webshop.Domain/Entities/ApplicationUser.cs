using Microsoft.AspNetCore.Identity;

namespace Webshop.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
