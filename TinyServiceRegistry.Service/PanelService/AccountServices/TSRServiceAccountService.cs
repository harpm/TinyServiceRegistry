
using Sardanapal.Identity.Dto;
using Sardanapal.Identity.Services.Services.AccountService;
using Sardanapal.Identity.Services.Services.UserManager;
using Sardanapal.Identity.ViewModel.Models.Account;
using TinyServiceRegistry.Domain.Entities;
using TinyServiceRegistry.Share.Identity;

namespace TinyServiceRegistry.Service.PanelService.AccountServices;

public class TSRServiceAccountService : AccountServiceBase<long, TSRUser, TSRRole, TSRUserRole, LoginVM, LoginDto, RegisterVM>
{
    public TSRServiceAccountService(IUserManagerService<long, TSRUser, TSRRole> _userManagerService)
        : base(_userManagerService, (byte)TSRRoleType.Service)
    {
    }
}
