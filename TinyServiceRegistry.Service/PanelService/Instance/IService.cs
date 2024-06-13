
using Sardanapal.Interface.IService;
using TinyServiceRegistry.ViewModel.ServiceInstance;

namespace TinyServiceRegistry.Service.PanelService.Instance;

public interface IServiceInstanceService : IPanelService<int, ServiceInstanceSearchVM, ServiceInstanceDto, NewServiceInstanceVM, ServiceInstanceEditableVM>
{

}
