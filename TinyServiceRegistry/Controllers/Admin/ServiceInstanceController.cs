using Sardanapal.Identity.Authorization.Filters;
using TinyServiceRegistry.Service.PanelService.Instance;
using TinyServiceRegistry.Share.Identity;
using TinyServiceRegistry.ViewModel.ServiceInstance;

namespace TinyServiceRegistry.Controllers.Admin;

[HasRole((byte) TSRRoleType.Admin)]
public class ServiceInstanceController : _AdminControllerBase<IServiceInstanceService, int, ServiceInstanceDto, ServiceInstanceListItemDto, ServiceInstanceSearchVM, NewServiceInstanceVM, ServiceInstanceEditableVM>
{
    public ServiceInstanceController(IServiceInstanceService service)
        : base(service)
    {
    }
}
