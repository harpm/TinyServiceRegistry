using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using Sardanapal.Identity.Services.Statics;
using TinyServiceRegistry.Domain.Data;
using TinyServiceRegistry.Domain.Entities;
using TinyServiceRegistry.Share.Identity;

public static class Seed
{
    public static async Task SeedRoles(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();

        var uow = scope.ServiceProvider.GetRequiredService<TSRUnitOfWork>();

        var roles = await uow.Roles.AsNoTracking()
            .ToListAsync();

        var staticRoles = Enum.GetValues<TSRRoleType>();
        foreach (var role in staticRoles)
        {
            if (!roles.Where(r => r.Id == (byte)role).Any())
            {
                await uow.AddAsync(new TSRRole()
                {
                    Id = (byte)role,
                    Title = Enum.GetName(role)
                });
            }
        }

        await uow.SaveChangesAsync();
    }

    public static async Task SeedUser(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();

        var uow = scope.ServiceProvider.GetRequiredService<TSRUnitOfWork>();

        var admin = await uow.Users.AsNoTracking()
            .Where(x => EF.Functions.Like(x.Username, "%admin%"))
            .FirstOrDefaultAsync();

        if (admin == null)
        {
            admin = new TSRUser()
            {
                Username = "admin",
                HashedPassword = await Utilities.EncryptToMd5("admin"),
                VerifiedEmail = true,
                VerifiedPhoneNumber = true
            };

            await uow.AddAsync(admin);
            await uow.SaveChangesAsync();
        }

        var tur = await uow.UserRoles.AsNoTracking()
            .Where(x => x.UserId == admin.Id && x.RoleId == (byte)TSRRoleType.Admin)
            .FirstOrDefaultAsync();

        if (tur == null)
        {
            tur = new TSRUserRole()
            {
                UserId = admin.Id,
                RoleId = (byte)TSRRoleType.Admin
            };

            await uow.AddAsync(tur);
            await uow.SaveChangesAsync();
        }
    }
}
