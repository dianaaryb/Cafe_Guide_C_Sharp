using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface ICafeService : IEntityRepository<App.BLL.DTO.Cafe>, ICafeRepositoryCustom<App.BLL.DTO.Cafe>
{
    
}