
using Sardanapal.Identity.Dto;
using Sardanapal.Identity.Services.Services.AccountService;
using Sardanapal.Identity.Services.Services.UserManager;
using Sardanapal.Identity.ViewModel.Models.Account;
using TinyServiceRegistry.Domain.Entities;

namespace TinyServiceRegistry.Service.PanelService;

public class TSRAccountService : AccountServiceBase<long, TSRUser, TSRRole, TSRUserRole, LoginVM, LoginDto, RegisterVM>
{
    public TSRAccountService(IUserManagerService<long, TSRUser, TSRRole> _userManagerService, byte _roleId)
        : base(_userManagerService, _roleId)
    {
    }
}
