
using System.ComponentModel.DataAnnotations;

namespace TinyServiceRegistry.ViewModel.Service;

public class NewServiceVM
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}

public class ServiceEditableVM : NewServiceVM
{

}