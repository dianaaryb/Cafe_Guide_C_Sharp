using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class OccasionOfCafeRepository: BaseEntityRepository<Guid, APPDomain.OccasionOfCafe, DALDTO.OccasionOfCafe, AppDbContext>, IOccasionOfCafeRepository
{
    public OccasionOfCafeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.OccasionOfCafe, DALDTO.OccasionOfCafe>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.OccasionOfCafe> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (userId != null && !userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.OccasionOfCafe)))
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

    public async Task<IEnumerable<DALDTO.OccasionOfCafe>> GetAllForCafeAsync(Guid cafeId)
    {
        var occasions = await (from co in RepoDbContext.CafeOccasions
            join o in RepoDbContext.OccasionOfCafes on co.OccasionOfCafeId equals o.Id
            where co.CafeId == cafeId
            select o).ToListAsync();
        var dalEntities = occasions.Select(e => Mapper.Map(e));
        return dalEntities;
    }

    public async Task<bool> DeleteOccasionAsync(Guid occasionId)
    {
        // Fetch related CafeCategories
        var relatedOccasions = await RepoDbContext.CafeOccasions
            .Where(cc => cc.OccasionOfCafeId == occasionId)
            .ToListAsync();

        if (relatedOccasions.Any())
        {
            // Remove related CafeCategories
            RepoDbContext.CafeOccasions.RemoveRange(relatedOccasions);
        }

        // Find the CategoryOfCafe entity
        var occasion = await RepoDbContext.OccasionOfCafes.FindAsync(occasionId);
        if (occasion == null)
        {
            return false; // Not found
        }

        // Remove the CategoryOfCafe entity
        RepoDbContext.OccasionOfCafes.Remove(occasion);

        // Save changes to the database
        await RepoDbContext.SaveChangesAsync();
        return true;
    }
}