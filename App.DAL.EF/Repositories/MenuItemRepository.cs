using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MenuItemRepository: BaseEntityRepository<Guid, APPDomain.MenuItem, DALDTO.MenuItem, AppDbContext>, IMenuItemRepository
{
    public MenuItemRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.MenuItem, DALDTO.MenuItem>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.MenuItem> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.MenuItem)))
        {
            query = query
                .Include(c => c.Menu)
                .Include(c => c.MenuItemCategory)
                .Where(e => ((IDomainAppUserId<Guid>) e).AppUserId.Equals(userId));
        }
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }

    public async Task<IEnumerable<DALDTO.MenuItem>> GetAllAsync(Guid id)
    {
        var domainEntities = await RepoDbSet
            .Where(c => c.MenuId == id)
            .ToListAsync();

        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity)).ToList();
        return dalEntities;
    }
}