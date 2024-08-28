using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IReviewPhotoRepository: IEntityRepository<DALDTO.ReviewPhoto>, IReviewPhotoRepositoryCustom<DALDTO.ReviewPhoto>
{
    //
}

public interface IReviewPhotoRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task<IEnumerable<TEntity>> GetAllAsync(Guid id);
    
    Task<TEntity?> FirstOrDefaultAsync(Guid id);

}