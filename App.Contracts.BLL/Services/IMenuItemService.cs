using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IMenuItemService: IEntityRepository<App.BLL.DTO.MenuItem>, IMenuItemRepositoryCustom<App.BLL.DTO.MenuItem>
{
    
}