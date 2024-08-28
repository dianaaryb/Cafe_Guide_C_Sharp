using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IReviewRepository: IEntityRepository<DALDTO.Review>, IReviewRepositoryCustom<DALDTO.Review>
{
    
}

public interface IReviewRepositoryCustom<TEntity>
{


        Task<TEntity?> FirstOrDefaultAsync(Guid id);
    
        Task<bool> DeleteReviewAsync(Guid reviewId);
    
}