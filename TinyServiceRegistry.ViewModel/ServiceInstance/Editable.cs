
using System.ComponentModel.DataAnnotations;

namespace TinyServiceRegistry.ViewModel.ServiceInstance;

public class NewServiceInstanceVM
{
    [Required]
    public int ServiceId { get; set; }

    [Required]
    public string IP { get; set; }

    [Required]
    public int Port { get; set; }

    public string Description { get; set; }
}

public class ServiceInstanceEditableVM : NewServiceInstanceVM
{

}