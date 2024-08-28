using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CafeOccasionRepository: BaseEntityRepository<Guid, APPDomain.CafeOccasion, DALDTO.CafeOccasion, AppDbContext>, ICafeOccasionRepository
{
    public CafeOccasionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.CafeOccasion, DALDTO.CafeOccasion>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.CafeOccasion> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.CafeOccasion)))
        {
            query = query
                .Include(c => c.Cafe)
                .Include(c => c.OccasionOfCafe)
                .Where(e => ((IDomainAppUserId<Guid>) e).AppUserId.Equals(userId));
        }
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }
}