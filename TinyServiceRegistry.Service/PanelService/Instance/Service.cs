
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sardanapal.Ef.Services.Services;
using Sardanapal.InterfacePanel.Service;
using Sardanapal.ViewModel.Response;
using TinyServiceRegistry.Domain.Data;
using TinyServiceRegistry.Domain.Entities;
using TinyServiceRegistry.ViewModel.ServiceInstance;

namespace TinyServiceRegistry.Service.PanelService.Instance;

public class ServiceInstanceService : EfPanelService<TSRUnitOfWork, int, ServiceInstance, ServiceInstanceListItemDto, ServiceInstanceSearchVM, ServiceInstanceDto, NewServiceInstanceVM, ServiceInstanceEditableVM>
    , IServiceInstanceService
{
    public override string ServiceName => "ServiceInstanceManager";

    public ServiceInstanceService(TSRUnitOfWork _context, IMapper _mapper, IRequestService _request)
        : base(_context, _mapper, _request)
    {

    }

    protected override IQueryable<ServiceInstance> GetCurrentService()
    {
        return base.GetCurrentService().Where(x => x.IsActive);
    }

    public async Task<IResponse<RoundRobinServiceInstanceDto>> GetRoundRobinInstance(int serviceId)
    {
        IResponse<RoundRobinServiceInstanceDto> response = new Response<RoundRobinServiceInstanceDto>(ServiceName);

        await response.FillAsync(async () =>
        {
            var count = await GetCurrentService().Where(x => x.ServiceId == serviceId)
                .CountAsync();

            if (count > 0)
            {
                if (count > 1)
                {
                    var lastUsed = await GetCurrentService()
                        .Where(s => s.ServiceId == serviceId && s.LastUse)
                        .FirstOrDefaultAsync();


                    if (lastUsed != null)
                    {
                        lastUsed.LastUse = false;

                        var next = await GetCurrentService()
                            .Where(x => x.ServiceId == serviceId && x.Id > lastUsed.Id)
                            .FirstOrDefaultAsync();

                        if (next == null)
                        {
                            next = await GetCurrentService()
                                .Where(x => x.ServiceId == serviceId)
                                .FirstOrDefaultAsync();
                        }

                        if (next != null)
                        {
                            next.LastUse = true;
                            await UnitOfWork.SaveChangesAsync();

                            var result = Mapper.Map<RoundRobinServiceInstanceDto>(next);

                            response.Set(StatusCode.Succeeded, result);
                        }
                        else
                        {
                            response.Set(StatusCode.NotExists);
                        }
                    }
                    else
                    {
                        var next = await GetCurrentService()
                            .Where(x => x.ServiceId == serviceId)
                            .FirstOrDefaultAsync();

                        if (next != null)
                        {
                            var result = Mapper.Map<RoundRobinServiceInstanceDto>(next);

                            next.LastUse = true;
                            await UnitOfWork.SaveChangesAsync();

                            response.Set(StatusCode.Succeeded, result);
                        }
                        else
                        {
                            response.Set(StatusCode.NotExists);
                        }
                    }
                }
                else
                {
                    var next = await GetCurrentService()
                        .Where(s => s.ServiceId == serviceId)
                        .ProjectTo<RoundRobinServiceInstanceDto>(Mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

                    if (next != null)
                    {
                        response.Set(StatusCode.Succeeded, next);
                    }
                    else
                    {
                        response.Set(StatusCode.NotExists);
                    }
                }
            }
            else
            {
                response.Set(StatusCode.NotExists);
            }
        });

        return response;
    }
}
