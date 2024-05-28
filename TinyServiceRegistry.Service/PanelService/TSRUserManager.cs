
using Sardanapal.Identity.Domain.Data;
using Sardanapal.Identity.Services.Services;
using Sardanapal.Identity.Services.Services.UserManager;
using TinyServiceRegistry.Domain.Data;
using TinyServiceRegistry.Domain.Entities;

namespace TinyServiceRegistry.Service.PanelService;

public class TSRUserManager : UserManagerService<long, TSRUser, TSRRole, TSRUserRole>
{
    public TSRUserManager(TSRUnitOfWork context, ITokenService tokenService, byte curRole)
        : base(context, tokenService, curRole)
    {
    }
}
