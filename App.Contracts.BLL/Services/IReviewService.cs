using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IReviewService: IEntityRepository<App.BLL.DTO.Review>, IReviewRepositoryCustom<App.BLL.DTO.Review>
{
    
}