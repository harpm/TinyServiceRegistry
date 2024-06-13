using Microsoft.AspNetCore.Mvc;
using Sardanapal.Interface.IService;

namespace TinyServiceRegistry.Controllers.Admin;

[Area("Service")]
[Route("[area]/[controller]")]
public abstract class _ServiceControllerBase : ControllerBase
{

}

[Area("Service")]
[Route("[area]/[controller]")]
public abstract class _ServiceControllerBase<TService, TKey, TVM, TSearchVM, TNewVM, TEditableVM> : _ServiceControllerBase
    where TService : class, IPanelService<TKey, TSearchVM, TVM, TNewVM, TEditableVM>
    where TKey : IEquatable<TKey>, IComparable<TKey>
    where TSearchVM : class, new()
    where TVM : class, new()
    where TNewVM : class, new()
    where TEditableVM : class, new()
{
    protected readonly TService _service;

    public _ServiceControllerBase(TService service)
    {
        _service = service;
    }
}
