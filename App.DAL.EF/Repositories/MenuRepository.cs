using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MenuRepository: BaseEntityRepository<APPDomain.Menu, DALDTO.Menu, AppDbContext>, IMenuRepository
{
    public MenuRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.Menu, DALDTO.Menu>(mapper))
    {
        Console.WriteLine("MenuRepository instantiated"); 
    }
    
    protected override IQueryable<APPDomain.Menu> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.Menu)))
        {
            query = query
                .Include(c => c.Cafe)
                .Where(e => ((IDomainAppUserId<Guid>) e).AppUserId.Equals(userId));
        }
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }
    
    public async Task<IEnumerable<DALDTO.Menu>> GetAllAsync()
    {
        var domainEntities = await RepoDbSet
            .Include(m => m.MenuItems)
            .ToListAsync();

        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity)).ToList();
        return dalEntities;
    }
    
    public async Task<IEnumerable<DALDTO.Menu>> GetAllAsync(Guid cafeId)
    {
        try
        {
            var domainEntities = await RepoDbSet
                .Where(c => c.CafeId == cafeId)
                .Include(m => m.MenuItems)
                .ToListAsync();

            var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity)).ToList();
            return dalEntities;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Repository: Exception occurred - {ex.Message}");
            throw;
        }
        // Console.WriteLine($"Repository: Filtering by cafeId: {cafeId}");
        // var domainEntities = await RepoDbSet
        //     .Where(c=> c.CafeId == cafeId)
        //     .Include(m => m.MenuItems)
        //     .ToListAsync();
        //
        // Console.WriteLine($"Repository: Found {domainEntities.Count} menus for cafeId: {cafeId}");  // Logging
        //
        // var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity)).ToList();
        // return dalEntities;
    }
    
    public async Task<DALDTO.Menu?> FirstOrDefaultAsync(Guid id)
    {
        var query = RepoDbSet
            .Include(c => c.MenuItems) 
            .Where(c => c.CafeId == id); 

        var domainEntity = await query.FirstOrDefaultAsync();
        return Mapper.Map(domainEntity!);
    }
}