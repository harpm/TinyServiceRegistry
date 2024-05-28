
using Sardanapal.ViewModel.Models;

namespace TinyServiceRegistry.ViewModel.ServiceInstance;

public class ServiceInstanceDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class ServiceInstanceListItemDto : BaseListItem<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
