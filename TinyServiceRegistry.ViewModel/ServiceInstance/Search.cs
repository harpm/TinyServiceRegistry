﻿
namespace TinyServiceRegistry.ViewModel.ServiceInstance;

public class ServiceInstanceSearchVM
{
    public string IP { get; set; }
    public string Port { get; set; }
    public int ServiceId { get; set; }
    public bool IsActive { get; set; }
}