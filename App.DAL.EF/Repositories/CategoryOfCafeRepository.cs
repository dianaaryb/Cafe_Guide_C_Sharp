using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CategoryOfCafeRepository: BaseEntityRepository<Guid, APPDomain.CategoryOfCafe, DALDTO.CategoryOfCafe, AppDbContext>, ICategoryOfCafeRepository
{
    public CategoryOfCafeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.CategoryOfCafe, DALDTO.CategoryOfCafe>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.CategoryOfCafe> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (userId != null && !userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.CategoryOfCafe)))
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
    
    public async Task<IEnumerable<App.DAL.DTO.CategoryOfCafe>> GetAllForCafeAsync(Guid cafeId)
    {
        var categories = await (from cc in RepoDbContext.CafeCategories
            join c in RepoDbContext.CategoryOfCafes on cc.CategoryOfCafeId equals c.Id
            where cc.CafeId == cafeId
            select c).ToListAsync();
        var dalEntities = categories.Select(e => Mapper.Map(e));
        return dalEntities;
    }
    
    public async Task<bool> DeleteCategoryAsync(Guid categoryId)
    {
        // Fetch related CafeCategories
        var relatedCafeCategories = await RepoDbContext.CafeCategories
            .Where(cc => cc.CategoryOfCafeId == categoryId)
            .ToListAsync();

        if (relatedCafeCategories.Any())
        {
            // Remove related CafeCategories
            RepoDbContext.CafeCategories.RemoveRange(relatedCafeCategories);
        }

        // Find the CategoryOfCafe entity
        var category = await RepoDbContext.CategoryOfCafes.FindAsync(categoryId);
        if (category == null)
        {
            return false; // Not found
        }

        // Remove the CategoryOfCafe entity
        RepoDbContext.CategoryOfCafes.Remove(category);

        // Save changes to the database
        await RepoDbContext.SaveChangesAsync();
        return true;
    }
    
    
}