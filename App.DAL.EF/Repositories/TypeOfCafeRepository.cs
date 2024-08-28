using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TypeOfCafeRepository: BaseEntityRepository<Guid, APPDomain.TypeOfCafe, DALDTO.TypeOfCafe, AppDbContext>, ITypeOfCafeRepository
{
    public TypeOfCafeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.TypeOfCafe, DALDTO.TypeOfCafe>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.TypeOfCafe> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.TypeOfCafe)))
        {
            query = query
                .Where(e => ((IDomainAppUserId<Guid>) e).AppUserId.Equals(userId));
        }
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }

    public async Task<IEnumerable<DALDTO.TypeOfCafe>> GetAllForCafeAsync(Guid cafeId)
    {
        var types = await (from ct in RepoDbContext.CafeTypes
            join t in RepoDbContext.TypeOfCafes on ct.TypeOfCafeId equals t.Id
            where ct.CafeId == cafeId
            select t).ToListAsync();
        var dalEntities = types.Select(e => Mapper.Map(e));
        return dalEntities;
    }
    
    public async Task<bool> DeleteTypeAsync(Guid typeId)
    {
        // Fetch related CafeCategories
        var relatedCafeTypes = await RepoDbContext.CafeTypes
            .Where(cc => cc.TypeOfCafeId == typeId)
            .ToListAsync();

        if (relatedCafeTypes.Any())
        {
            // Remove related CafeCategories
            RepoDbContext.CafeTypes.RemoveRange(relatedCafeTypes);
        }

        // Find the CategoryOfCafe entity
        var type = await RepoDbContext.TypeOfCafes.FindAsync(typeId);
        if (type == null)
        {
            return false; // Not found
        }

        // Remove the CategoryOfCafe entity
        RepoDbContext.TypeOfCafes.Remove(type);

        // Save changes to the database
        await RepoDbContext.SaveChangesAsync();
        return true;
    }
}