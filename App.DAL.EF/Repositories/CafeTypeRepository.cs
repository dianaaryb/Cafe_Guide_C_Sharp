using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CafeTypeRepository: BaseEntityRepository<Guid, APPDomain.CafeType, DALDTO.CafeType, AppDbContext>, ICafeTypeRepository
{
    public CafeTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.CafeType, DALDTO.CafeType>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.CafeType> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.CafeType)))
        {
            query = query
                .Include(c => c.Cafe)
                .Include(c => c.TypeOfCafe)
                .Where(e => ((IDomainAppUserId<Guid>) e).AppUserId.Equals(userId));
        }
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }
}