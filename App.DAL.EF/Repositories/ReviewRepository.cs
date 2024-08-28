

using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.Contracts.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ReviewRepository: BaseEntityRepository<Guid, APPDomain.Review, DALDTO.Review, AppDbContext>, IReviewRepository
{
    public ReviewRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.Review, DALDTO.Review>(mapper))
    {
    }
    
    protected override IQueryable<APPDomain.Review> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (!userId.Equals(default) &&
            typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.Review)))
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

    public async Task<DALDTO.Review?> FirstOrDefaultAsync(Guid id)
    {
        var query = RepoDbSet
            .Include(c => c.ReviewPhotos)
            .Where(c => c.Id == id);
        var domainEntity = await query.FirstOrDefaultAsync();
        return Mapper.Map(domainEntity!);
    }
    
    public async Task<bool> DeleteReviewAsync(Guid reviewId)
    {
        var reviewPhotos = await RepoDbContext.ReviewPhotos.Where(rp => rp.ReviewId == reviewId).ToListAsync();
        RepoDbContext.ReviewPhotos.RemoveRange(reviewPhotos);

        var review = await RepoDbContext.Reviews.FindAsync(reviewId);
        if (review != null)
        {
            RepoDbContext.Reviews.Remove(review);
        }

        await RepoDbContext.SaveChangesAsync();
        return true;
    }
}



// using App.Contracts.DAL.Repositories;
// using AutoMapper;
// using APPDomain = App.Domain;
// using DALDTO = App.DAL.DTO;
// using Base.Contracts.Domain;
// using Base.DAL.EF;
// using Microsoft.EntityFrameworkCore;
//
// namespace App.DAL.EF.Repositories;
//
// public class ReviewRepository: BaseEntityRepository<Guid, APPDomain.Review, DALDTO.Review, AppDbContext>, IReviewRepository
// {
//     public ReviewRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<APPDomain.Review, DALDTO.Review>(mapper))
//     {
//     }
//     
//     protected override IQueryable<APPDomain.Review> CreateQuery(Guid userId = default, bool noTracking = true)
//     {
//         var query = RepoDbSet.AsQueryable();
//         if (userId != null && !userId.Equals(default) &&
//             typeof(IDomainAppUserId<Guid>).IsAssignableFrom(typeof(APPDomain.Review)))
//         {
//             query = query
//                 .Include(c => c.Cafe)
//                 .Include(c => c.AppUser)
//                 .Where(e => ((IDomainAppUserId<Guid>) e).AppUserId.Equals(userId));
//         }
//         if (noTracking)
//         {
//             query = query.AsNoTracking();
//         }
//         return query;
//     }
//
//     public async Task<DALDTO.Review?> FirstOrDefaultAsyncWithoutUser(Guid id)
//     {
//         var query = RepoDbSet
//             .Include(c => c.ReviewPhotos)
//             .Where(c => c.Id == id);
//         var domainEntity = await query.FirstOrDefaultAsync();
//         return Mapper.Map(domainEntity);
//     }
//     
//     public async Task<DALDTO.Review?> FirstOrDefaultWithUserAsync(Guid id, Guid userId)
//     {
//         var query = RepoDbSet
//             .Include(r => r.ReviewPhotos)
//             .Include(r => r.AppUser) // Include the AppUser entity to fetch user information
//             .Where(r => r.Id == id && r.AppUserId == userId);
//
//         var domainEntity = await query.FirstOrDefaultAsync();
//         return Mapper.Map(domainEntity);
//     }
//
//     
//     public async Task<bool> DeleteReviewAsync(Guid reviewId)
//     {
//         var reviewPhotos = await RepoDbContext.ReviewPhotos.Where(rp => rp.ReviewId == reviewId).ToListAsync();
//         RepoDbContext.ReviewPhotos.RemoveRange(reviewPhotos);
//
//         var review = await RepoDbContext.Reviews.FindAsync(reviewId);
//         if (review != null)
//         {
//             RepoDbContext.Reviews.Remove(review);
//         }
//
//         await RepoDbContext.SaveChangesAsync();
//         return true;
//     }
// }