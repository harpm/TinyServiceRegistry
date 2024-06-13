
using AutoMapper;
using TinyServiceRegistry.Domain.Entities;
using TinyServiceRegistry.ViewModel.ServiceInstance;

namespace TinyServiceRegistry.Service.PanelService.Instance;

public class ServiceInstanceMapper : Profile
{
    public ServiceInstanceMapper()
    {
        CreateMap<ServiceInstance, ServiceInstanceListItemDto>()
            .ForMember(m => m.Address, q => q.MapFrom(w => w.IP + ":" + w.Port));

        CreateMap<ServiceInstance, ServiceInstanceDto>();
        CreateMap<ServiceInstance, ServiceInstanceEditableVM>();
        CreateMap<NewServiceInstanceVM, ServiceInstance>()
            .ForMember(m => m.IsActive, q => q.MapFrom(w => true));
        CreateMap<ServiceInstanceEditableVM, ServiceInstance>()
            .IncludeBase<NewServiceInstanceVM, ServiceInstance>();
    }
}
