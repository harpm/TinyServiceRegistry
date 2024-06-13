
using AutoMapper;
using TinyServiceRegistry.ViewModel.Service;

namespace TinyServiceRegistry.Service.PanelService.Service;

public class ServiceMapper : Profile
{
    public ServiceMapper()
    {
        CreateMap<Domain.Entities.Service, ServiceListItemDto>()
            .ForMember(x => x.Name, w => w.MapFrom(z => z.Name))
            .ForMember(x => x.Description, w => w.MapFrom(z => z.Description))
            .ForMember(x => x.Instances, w => w.MapFrom(z => string.Join(',', z.Instances.Select(q => $"{q.IP}:{q.Port}")).Trim(',')))
            ;

        CreateMap<Domain.Entities.Service, ServiceEditableVM>();
        CreateMap<NewServiceVM, Domain.Entities.Service>();
        CreateMap<ServiceEditableVM, Domain.Entities.Service>()
            .IncludeBase<NewServiceVM, Domain.Entities.Service>();
    }
}
