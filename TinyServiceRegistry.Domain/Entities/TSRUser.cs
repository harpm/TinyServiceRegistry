

using Sardanapal.Identity.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyServiceRegistry.Domain.Entities;

public class TSRUser : UserBase<long>
{

    [InverseProperty(nameof(TSRUserRole.User))]
    public ICollection<TSRUserRole> UserRoles { get; set; }
}
