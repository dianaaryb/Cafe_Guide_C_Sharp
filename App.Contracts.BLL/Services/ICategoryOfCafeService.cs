using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface ICategoryOfCafeService: IEntityRepository<App.BLL.DTO.CategoryOfCafe>, ICategoryOfCafeCustom<App.BLL.DTO.CategoryOfCafe>
{
    
}