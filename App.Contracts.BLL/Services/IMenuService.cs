using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IMenuService: IEntityRepository<App.BLL.DTO.Menu>, IMenuRepositoryCustom<App.BLL.DTO.Menu>
{
    
}