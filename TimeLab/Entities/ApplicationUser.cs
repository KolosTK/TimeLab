using Microsoft.AspNetCore.Identity;

namespace TimeLab.Entities;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
}