using Microsoft.AspNetCore.Mvc;
using Sardanapal.Identity.Authorization.Filters;
using Sardanapal.ViewModel.Response;
using TinyServiceRegistry.Controllers.Admin;
using TinyServiceRegistry.Service.PanelService.Instance;
using TinyServiceRegistry.ViewModel.ServiceInstance;

namespace TinyServiceRegistry.Controllers.Service;

[Authorize]
public class ClientServiceController : _ServiceControllerBase<IServiceInstanceService, int, ServiceInstanceDto, ServiceInstanceSearchVM, NewServiceInstanceVM, ServiceInstanceEditableVM>
{
    public ClientServiceController(IServiceInstanceService service) : base(service)
    {
        
    }

    [HttpGet("{serviceId}")]
    public async Task<IResponse<RoundRobinServiceInstanceDto>> GetService(int serviceId)
    {
        return await _service.GetRoundRobinInstance(serviceId);
    }
}
