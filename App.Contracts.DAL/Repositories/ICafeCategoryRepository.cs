using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ICafeCategoryRepository: IEntityRepository<DALDTO.CafeCategory>, ICafeCategoryCustom<DALDTO.CafeCategory>
{
    //define custom methods
}

public interface ICafeCategoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllForCafeAsync(Guid id);
}

