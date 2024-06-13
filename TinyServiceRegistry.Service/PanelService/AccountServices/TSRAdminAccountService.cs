
using Sardanapal.Identity.Dto;
using Sardanapal.Identity.Services.Services.AccountService;
using Sardanapal.Identity.Services.Services.UserManager;
using Sardanapal.Identity.ViewModel.Models.Account;
using TinyServiceRegistry.Domain.Entities;
using TinyServiceRegistry.Share.Identity;

namespace TinyServiceRegistry.Service.PanelService.AccountServices;

public class TSRAdminAccountService : AccountServiceBase<long, TSRUser, TSRRole, TSRUserRole, LoginVM, LoginDto, RegisterVM>
{
    public TSRAdminAccountService(IUserManagerService<long, TSRUser, TSRRole> _userManagerService)
        : base(_userManagerService, (byte)TSRRoleType.Admin)
    {
    }
}
