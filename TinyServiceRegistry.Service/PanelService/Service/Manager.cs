
using AutoMapper;
using Sardanapal.Ef.Services.Services;
using Sardanapal.InterfacePanel.Service;
using TinyServiceRegistry.Domain.Data;
using TinyServiceRegistry.ViewModel.Service;

namespace TinyServiceRegistry.Service.PanelService.Service;

public class ServiceManager : EfPanelService<TSRUnitOfWork, int, Domain.Entities.Service, ServiceListItemDto, ServiceSearchVM, ServiceDto, NewServiceVM, ServiceEditableVM>
    , IServiceManager
{
    public override string ServiceName => "ServiceManager";

    public ServiceManager(TSRUnitOfWork _context, IMapper _mapper, IRequestService _request)
        : base(_context, _mapper, _request)
    {

    }
}