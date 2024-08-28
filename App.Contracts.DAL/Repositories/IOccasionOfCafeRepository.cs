using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IOccasionOfCafeRepository: IEntityRepository<DALDTO.OccasionOfCafe>, IOccasionOfCafeCustom<DALDTO.OccasionOfCafe>
{
    
}

public interface IOccasionOfCafeCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllForCafeAsync(Guid id);

    Task<bool> DeleteOccasionAsync(Guid occasionId);
}