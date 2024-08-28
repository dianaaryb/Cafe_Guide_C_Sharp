using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ICategoryOfCafeRepository: IEntityRepository<DALDTO.CategoryOfCafe>, ICategoryOfCafeCustom<DALDTO.CategoryOfCafe>
{
    //
}

public interface ICategoryOfCafeCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllForCafeAsync(Guid id);

    Task<bool> DeleteCategoryAsync(Guid categoryId);
}