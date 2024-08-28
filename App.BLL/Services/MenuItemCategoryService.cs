using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class MenuItemCategoryService: 
    BaseEntityService<App.DAL.DTO.MenuItemCategory, App.BLL.DTO.MenuItemCategory, IMenuItemCategoryRepository>, IMenuItemCategoryService
{
    public MenuItemCategoryService(IAppUnitOfWork uoW, IMenuItemCategoryRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.MenuItemCategory, App.BLL.DTO.MenuItemCategory>(mapper))
    {
    }

}