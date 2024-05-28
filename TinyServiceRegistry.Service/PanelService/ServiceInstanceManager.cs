
using AutoMapper;
using Sardanapal.Ef.Services.Services;
using Sardanapal.InterfacePanel.Service;
using TinyServiceRegistry.Domain.Data;
using TinyServiceRegistry.Domain.Entities;
using TinyServiceRegistry.ViewModel.ServiceInstance;

namespace TinyServiceRegistry.Service.PanelService;

public class ServiceInstanceManager : EfPanelService<TSRUnitOfWork, int, ServiceInstance, ServiceInstanceListItemDto, ServiceInstanceSearchVM, ServiceInstanceDto, NewServiceInstanceVM, ServiceInstanceEditableVM>
{
    public override string ServiceName => "ServiceInstanceManager";

    public ServiceInstanceManager(TSRUnitOfWork _context, IMapper _mapper, IRequestService _request)
        : base(_context, _mapper, _request)
    {

    }
}
