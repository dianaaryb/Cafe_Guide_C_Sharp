using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IMenuRepository: IEntityRepository<DALDTO.Menu>, IMenuRepositoryCustom<DALDTO.Menu>
{
   
}

public interface IMenuRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task<IEnumerable<TEntity>> GetAllAsync(Guid id);
    
    Task<TEntity?> FirstOrDefaultAsync(Guid id);

}