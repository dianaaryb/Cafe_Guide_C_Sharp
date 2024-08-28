using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IMenuItemRepository: IEntityRepository<DALDTO.MenuItem>, IMenuItemRepositoryCustom<DALDTO.MenuItem>
{
   // 
}

public interface IMenuItemRepositoryCustom<TEntity>
{
   Task<IEnumerable<TEntity>> GetAllAsync(Guid id);
}