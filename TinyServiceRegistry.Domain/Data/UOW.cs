
using Microsoft.EntityFrameworkCore;
using Sardanapal.Domain.Model;
using Sardanapal.Identity.Authorization.Data;
using Sardanapal.Identity.Domain.Data;
using TinyServiceRegistry.Domain.Entities;

namespace TinyServiceRegistry.Domain.Data;

public class TSRUnitOfWork : SdIdentityUnitOfWorkBase<long, byte, TSRUser, TSRRole, TSRUserRole>
    , IDisposable
{
    public TSRUnitOfWork(DbContextOptions<TSRUnitOfWork> opt, IIdentityHolder identityHolder)
        : base(opt, identityHolder)
    {
        
    }

    public override Type[] GetDomainModels()
    {
        return (from x in typeof(TSRUser).Assembly.GetTypes()
                where x.IsAssignableTo(typeof(IDomainModel)) && x.IsClass && !x.IsAbstract
                select x).ToArray();
    }
}
