using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ICafeRepository: IEntityRepository<DALDTO.Cafe>, ICafeRepositoryCustom<DALDTO.Cafe>
{
    //define your DAL only custom methods here
}

//define your shared (with bll) custom methods here
public interface ICafeRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity?> FirstOrDefaultAsync(Guid id);

}