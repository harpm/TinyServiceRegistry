

using Sardanapal.Identity.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyServiceRegistry.Domain.Entities;

public class TSRUser : UserBase<long>
{
    public override string? FirstName { get; set; }
    public override string? LastName { get; set; }

    [InverseProperty(nameof(TSRUserRole.User))]
    public ICollection<TSRUserRole> UserRoles { get; set; }
}
