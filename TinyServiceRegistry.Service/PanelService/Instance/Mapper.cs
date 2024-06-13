
using AutoMapper;
using Sardanapal.ViewModel.Models;
using System.Xml.Linq;
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

        CreateMap<ServiceInstance, RoundRobinServiceInstanceDto>();

        CreateMap<ServiceInstance, SelectOptionVM<int, object>>()
            .ForMember(x => x.Key, q => q.MapFrom(w => w.Id))
            .ForMember(x => x.Value, q => q.MapFrom(w => new { IP = w.IP, Port = w.Port }));
    }
}
