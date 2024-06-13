using Sardanapal.Identity.Services.Services;
using Sardanapal.Identity.Services.Services.UserManager;
using Sardanapal.Identity.Services.Statics;
using TinyServiceRegistry.Domain.Data;
using TinyServiceRegistry.Domain.Entities;
using TinyServiceRegistry.Share.Identity;

namespace TinyServiceRegistry.Service.PanelService;

public class TSRAdminUserManager : UserManagerService<long, TSRUser, TSRRole, TSRUserRole>
{
    public TSRAdminUserManager(TSRUnitOfWork context, ITokenService tokenService)
        : base(context, tokenService, (byte) TSRRoleType.Admin)
    {
    }

    public override async Task<long> RegisterUser(string username, string password)
    {
        string hashedPass = await Utilities.EncryptToMd5(password);
        TSRUser newUser = await GetUser(username);
        if (newUser == null)
        {
            newUser = new TSRUser
            {
                Username = username,
                HashedPassword = hashedPass,
                VerifiedEmail = true,
                VerifiedPhoneNumber = true
            };
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        if (await HasRole(_currentRole, newUser.Id))
        {
            TSRUserRole entity = new TSRUserRole
            {
                RoleId = _currentRole,
                UserId = newUser.Id
            };
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        return newUser.Id;
    }
}
