
using Sardanapal.Identity.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyServiceRegistry.Domain.Entities;

public class TSRRole : RoleBase<byte>
{
    [InverseProperty(nameof(TSRUserRole.Role))]
    public ICollection<TSRUserRole> UserRoles { get; set; }
}
