
using Sardanapal.Identity.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyServiceRegistry.Domain.Entities;

public class TSRUserRole : UserRoleBase<long, byte>
{
    [ForeignKey(nameof(UserId))]
    public TSRUser User { get; set; }

    [ForeignKey(nameof(RoleId))]
    public TSRRole Role { get; set; }
}
