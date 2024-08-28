using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class FavouriteRepository: BaseEntityRepository<Guid, APPDomain.Favourite, DALDTO.Favourite, AppDbContext>, IFavouriteRepository
{
    public FavouriteRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.Favourite, DALDTO.Favourite>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.Favourite> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.Favourite)))
        {
            query = query
                .Include(c => c.Cafe)
                .Include(c => c.AppUser)
                .Where(e => ((IDomainAppUserId<Guid>) e).AppUserId.Equals(userId));
        }
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }
}