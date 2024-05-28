using Sardanapal.Identity.Services.Services.UserManager;
using TinyServiceRegistry.Domain.Entities;

public static class Seed
{
    public static void SeedUser(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<IUserManagerService<long, TSRUser, TSRRole>>();
        userManager.RegisterUser("admin", "admin");
    }
}
