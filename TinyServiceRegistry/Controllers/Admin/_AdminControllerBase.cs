using Microsoft.AspNetCore.Mvc;
using Sardanapal.Interface.IService;
using Sardanapal.ViewModel.Models;
using Sardanapal.ViewModel.Response;

namespace TinyServiceRegistry.Controllers.Admin;

[Area("Admin")]
[Route("[area]/[controller]")]
public abstract class _AdminControllerBase : ControllerBase
{

}

public abstract class _AdminControllerBase<TService, TKey, TVM, TListItem, TSearchVM, TNewVM, TEditableVM> : _AdminControllerBase
    where TService : class, IPanelService<TKey, TSearchVM, TVM, TNewVM, TEditableVM>
    where TKey : IEquatable<TKey>, IComparable<TKey>
    where TListItem : BaseListItem<TKey>, new()
    where TSearchVM : class, new()
    where TVM : class, new()
    where TNewVM : class, new()
    where TEditableVM : class, new()
{
    protected readonly TService _service;

    public _AdminControllerBase(TService service)
    {

        _service = service;

    }

    [HttpGet("{id}")]
    public async Task<IResponse<TVM>> GetById(TKey id)
    {
        return await _service.Get(id);
    }

    [HttpPost("GetAll")]
    public async Task<IResponse<GridVM<TKey, TListItem, TSearchVM>>> GetAll(GridSearchModelVM<TKey, TSearchVM> searchVM)
    {
        return await _service.GetAll<TListItem>(searchVM);
    }

    [HttpPost]
    public async Task<IResponse<TKey>> Add(TNewVM model)
    {
        return await _service.Add(model);
    }

    [HttpGet("Editable/{id}")]
    public async Task<IResponse<TEditableVM>> GetEditable(TKey id)
    {
        return await _service.GetEditable(id);
    }

    [HttpPut]
    public async Task<IResponse<bool>> Edit(TKey id, TEditableVM model)
    {
        return await _service.Edit(id, model);
    }

    [HttpDelete]
    public async Task<IResponse<bool>> Delete(TKey id)
    {
        return await _service.Delete(id);
    }

    [HttpPost("Dictionary")]
    {
        return await _service.GetDictionary(model);
    }
}
