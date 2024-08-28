using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CityRepository: BaseEntityRepository<Guid, APPDomain.City, DALDTO.City, AppDbContext>, ICityRepository
{
    public CityRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.City, DALDTO.City>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.City> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.City)))
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
}