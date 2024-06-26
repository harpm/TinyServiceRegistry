﻿
using Sardanapal.ViewModel.Models;

namespace TinyServiceRegistry.ViewModel.ServiceInstance;

public class ServiceInstanceDto
{
    public string IP { get; set; }
    public int Port { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
}

public class ServiceInstanceListItemDto : BaseListItem<int>
{
    public string Address { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
}

public class RoundRobinServiceInstanceDto
{
    public string IP { get; set; }
    public int Port { get; set; }
}