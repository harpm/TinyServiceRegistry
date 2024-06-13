using Microsoft.AspNetCore.Mvc;
using Sardanapal.Identity.Authorization.Filters;
using Sardanapal.Identity.Dto;
using Sardanapal.Identity.ViewModel.Models.Account;
using Sardanapal.ViewModel.Response;
using TinyServiceRegistry.Service.PanelService.AccountServices;
using TinyServiceRegistry.Share.Identity;

namespace TinyServiceRegistry.Controllers.Admin;

public class AdminAccountController : _AdminControllerBase
{
    private TSRAdminAccountService accountService { get; set; }
    public AdminAccountController(TSRAdminAccountService _accountService)
    {
        accountService = _accountService;
    }

    [HttpPost("Login")]
    public async Task<IResponse<LoginDto>> Login([FromBody] LoginVM model)
    {
        return await accountService.Login(model);
    }

    [HasRole((byte) TSRRoleType.Admin)]
    [HttpPost("Register")]
    public async Task<IResponse<long>> RegisterNewUser([FromBody] RegisterVM model)
    {
        return await accountService.Register(model);
    }
}
