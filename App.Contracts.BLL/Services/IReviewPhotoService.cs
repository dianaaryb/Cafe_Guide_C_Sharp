using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IReviewPhotoService: IEntityRepository<App.BLL.DTO.ReviewPhoto>, IReviewPhotoRepositoryCustom<App.BLL.DTO.ReviewPhoto>
{
    
}