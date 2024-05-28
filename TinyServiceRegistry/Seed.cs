using Sardanapal.Identity.Services.Services.UserManager;
using TinyServiceRegistry.Domain.Entities;

public static class Seed
{
    public static async Task SeedUser(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<IUserManagerService<long, TSRUser, TSRRole>>();
        await userManager.RegisterUser("admin", "admin");
    }
}
