
using Sardanapal.Interface.IService;
using Sardanapal.ViewModel.Response;
using TinyServiceRegistry.ViewModel.ServiceInstance;

namespace TinyServiceRegistry.Service.PanelService.Instance;

public interface IServiceInstanceService : IPanelService<int, ServiceInstanceSearchVM, ServiceInstanceDto, NewServiceInstanceVM, ServiceInstanceEditableVM>
{
    Task<IResponse<RoundRobinServiceInstanceDto>> GetRoundRobinInstance(int serviceId);
}
