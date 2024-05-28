
using Sardanapal.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyServiceRegistry.Domain.Entities;

public class Service : BaseEntityModel<int>
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

    [InverseProperty(nameof(ServiceInstance.RelatedService))]
    public ICollection<ServiceInstance> Instances { get; set; }
        = new HashSet<ServiceInstance>();
}
