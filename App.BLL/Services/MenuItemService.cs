using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class MenuItemService : 
    BaseEntityService<App.DAL.DTO.MenuItem, App.BLL.DTO.MenuItem, IMenuItemRepository>, IMenuItemService
{
    public MenuItemService(IAppUnitOfWork uoW, IMenuItemRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.MenuItem, App.BLL.DTO.MenuItem>(mapper))
    {
    }
    
    public async Task<IEnumerable<MenuItem>> GetAllAsync(Guid id)
    {
        return (await Repository.GetAllAsync(id)).Select(e=>Mapper.Map(e));
    }

}