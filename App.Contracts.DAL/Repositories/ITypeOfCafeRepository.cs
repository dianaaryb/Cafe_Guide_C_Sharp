using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ITypeOfCafeRepository: IEntityRepository<DALDTO.TypeOfCafe>, ITypeOfCafeCustom<DALDTO.TypeOfCafe>
{
    //
}


public interface ITypeOfCafeCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllForCafeAsync(Guid id);

    Task<bool> DeleteTypeAsync(Guid typeId);
}