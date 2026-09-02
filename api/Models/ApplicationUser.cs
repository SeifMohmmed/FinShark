using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = [];
    }
}