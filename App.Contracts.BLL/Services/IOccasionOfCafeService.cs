using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IOccasionOfCafeService: IEntityRepository<App.BLL.DTO.OccasionOfCafe>, IOccasionOfCafeCustom<App.BLL.DTO.OccasionOfCafe>
{
    
}