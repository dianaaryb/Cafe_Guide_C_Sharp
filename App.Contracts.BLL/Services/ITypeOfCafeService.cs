using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface ITypeOfCafeService: IEntityRepository<App.BLL.DTO.TypeOfCafe>, ITypeOfCafeCustom<App.BLL.DTO.TypeOfCafe>
{
    
}