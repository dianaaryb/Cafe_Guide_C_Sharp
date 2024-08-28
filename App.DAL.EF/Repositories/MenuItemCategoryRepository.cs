using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MenuItemCategoryRepository: BaseEntityRepository<Guid, APPDomain.MenuItemCategory, DALDTO.MenuItemCategory, AppDbContext>, IMenuItemCategoryRepository
{
    public MenuItemCategoryRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.MenuItemCategory, DALDTO.MenuItemCategory>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.MenuItemCategory> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.MenuItemCategory)))
        {
            query = query
                .Include(m=> m.MenuItems)
                .Where(e => ((IDomainAppUserId<Guid>) e).AppUserId.Equals(userId));
        }
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }
}