
using Sardanapal.Interface.IService;
using TinyServiceRegistry.ViewModel.Service;

namespace TinyServiceRegistry.Service.PanelService.Service;

public interface IServiceManager : IPanelService<int, ServiceSearchVM, ServiceDto, NewServiceVM, ServiceEditableVM>
{
}