using App.BLL.DTO;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CafeRepository: BaseEntityRepository<APPDomain.Cafe, DALDTO.Cafe, AppDbContext>, ICafeRepository
{
    public CafeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext,new DalDomainMapper<APPDomain.Cafe, DALDTO.Cafe>(mapper))
    {
    }

    protected override IQueryable<APPDomain.Cafe> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.Cafe)))
        {
            query = query
                .Include(c=>c.AppUser)
                .Include(c=> c.City)
                .Where(e => ((IDomainAppUserId<Guid>) e).AppUserId.Equals(userId));
        }
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }

    public async Task<IEnumerable<DALDTO.Cafe>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        // query = query.OrderBy(c => c.CafeName);
        return (await query.ToListAsync()).Select(e => Mapper.Map(e));
    }
    
    public override async Task<IEnumerable<DAL.DTO.Cafe>> GetAllAsync(Guid userId = default, bool noTracking = true)
    {
        var query = CreateQuery(userId, noTracking);
        query = query.Include(c => c.City); // Include City information
    
        return (await query.ToListAsync()).Select(e => Mapper.Map(e));
    }
    
    public async Task<IEnumerable<DALDTO.Cafe>> GetAllAsync()
    {
        var domainEntities = await RepoDbSet
            .Include(m => m.Menus)
            .Include(r => r.Reviews)
            .Include(c=> c.CafeCategories)
            .Include(t=> t.CafeTypes)
            .Include(o=>o.CafeOccasions)
            .Include(f=> f.Favourites)
            .ToListAsync();

        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity)).ToList();
        dalEntities.ForEach(e=> Console.WriteLine(e.Reviews!.Count));
        return dalEntities;
    }

    public async Task<DALDTO.Cafe?> FirstOrDefaultAsync(Guid id)
    {
        var query = RepoDbSet
            .Include(c => c.City) // Include related entities as needed
            .Include(c => c.Menus)
            .Include(c => c.Reviews)
            .Where(c => c.Id == id); // Filter by the cafe's ID

        var domainEntity = await query.FirstOrDefaultAsync();
        return Mapper.Map(domainEntity!);
    }


    // public async Task<DALDTO.Cafe?> FirstOrDefaultAsync()
    // {
    //         var domainEntity = RepoDbSet
    //             .Include(c => c.City) // Include related entities as needed
    //             .Include(c => c.Menus)
    //             .Include(c => c.Reviews);
    //
    //         var dalEntity = await domainEntity.FirstOrDefaultAsync();
    //         return Mapper.Map(dalEntity!);
    //     }

    
}