
using Microsoft.EntityFrameworkCore;
using Sardanapal.Identity.Authorization.Data;
using Sardanapal.Identity.Domain.Data;
using TinyServiceRegistry.Domain.Entities;

namespace TinyServiceRegistry.Domain.Data;

public class TSRUnitOfWork : SdIdentityUnitOfWorkBase<long, byte, TSRUser, TSRRole, TSRUserRole>
{
    public TSRUnitOfWork(DbContextOptions<TSRUnitOfWork> opt, IIdentityHolder identityHolder)
        : base(opt, identityHolder)
    {
        
    }
}
