using Sardanapal.Identity.Authorization.Filters;
using TinyServiceRegistry.Service.PanelService.Service;
using TinyServiceRegistry.Share.Identity;
using TinyServiceRegistry.ViewModel.Service;

namespace TinyServiceRegistry.Controllers.Admin;

[HasRole((byte) TSRRoleType.Admin)]
public class ServiceController : _AdminControllerBase<IServiceManager, int, ServiceDto, ServiceListItemDto, ServiceSearchVM, NewServiceVM, ServiceEditableVM>
{
    public ServiceController(IServiceManager service) : base(service)
    {
    }
}
