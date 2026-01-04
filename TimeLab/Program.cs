using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeLab.Data;
using TimeLab.Entities;

namespace TimeLab;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                }
            )
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        
        
        // Add services to the container.
        builder.Services.AddRazorPages();
        
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        
        
        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapRazorPages()
            .WithStaticAssets();


        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var task = SeedRolesAndUsers(roleManager, userManager);
            task.GetAwaiter().GetResult();

            
        }

        app.Run();
    }
    private static async Task SeedRolesAndUsers(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        string[] roles = new[] { "User", "Moderator" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        
        
        string modEmail = "moderator@example.com";
        var moderator = await userManager.FindByEmailAsync(modEmail);
        
        if (moderator == null)
        {
            moderator = new ApplicationUser
            {
                UserName = modEmail,
                Email = modEmail,
            };

            var result = await userManager.CreateAsync(moderator, "Moderator123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(moderator, "Moderator");
            }
        }
    }
}