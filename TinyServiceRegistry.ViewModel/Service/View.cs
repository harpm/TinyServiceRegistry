
using Sardanapal.ViewModel.Models;

namespace TinyServiceRegistry.ViewModel.Service;

public class ServiceDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class ServiceListItemDto : BaseListItem<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
