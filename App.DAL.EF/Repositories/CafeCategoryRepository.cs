using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CafeCategoryRepository: BaseEntityRepository<Guid, APPDomain.CafeCategory, DALDTO.CafeCategory, AppDbContext>, ICafeCategoryRepository
{
    private ICafeCategoryRepository _cafeCategoryRepositoryImplementation = null!;

    public CafeCategoryRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.CafeCategory, DALDTO.CafeCategory>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.CafeCategory> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.CafeCategory)))
        {
            query = query
                .Include(c => c.Cafe)
                .Include(c => c.CategoryOfCafe)
                .Where(e => ((IDomainAppUserId<Guid>) e).AppUserId.Equals(userId));
        }
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }


}