
using System.ComponentModel.DataAnnotations;

namespace TinyServiceRegistry.ViewModel.ServiceInstance;

public class NewServiceInstanceVM
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}

public class ServiceInstanceEditableVM : NewServiceInstanceVM
{

}