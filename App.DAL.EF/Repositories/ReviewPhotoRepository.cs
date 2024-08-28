using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ReviewPhotoRepository: BaseEntityRepository<Guid, APPDomain.ReviewPhoto, DALDTO.ReviewPhoto, AppDbContext>, IReviewPhotoRepository
{
    public ReviewPhotoRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.ReviewPhoto, DALDTO.ReviewPhoto>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.ReviewPhoto> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.ReviewPhoto)))
        {
            query = query
                .Include(c => c.Review)
                .Where(e => ((IDomainAppUserId<Guid>) e).AppUserId.Equals(userId));
        }
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }

    public async Task<DALDTO.ReviewPhoto?> FirstOrDefaultAsync(Guid id)
    {
        var query = RepoDbSet.Where(c => c.ReviewId == id);
        var domainEntity = await query.FirstOrDefaultAsync();
        return Mapper.Map(domainEntity);
    }
    
    public async Task<IEnumerable<DALDTO.ReviewPhoto>> GetAllAsync()
    {
        var domainEntities = await RepoDbSet
            .ToListAsync();

        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity)).ToList();
        return dalEntities;
    }

    public async Task<IEnumerable<DALDTO.ReviewPhoto>> GetAllAsync(Guid reviewId)
    {
        var domainEntities = await RepoDbSet
            .Where(c => c.ReviewId == reviewId)
            .ToListAsync();

        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity)).ToList();
        return dalEntities;
    }
}