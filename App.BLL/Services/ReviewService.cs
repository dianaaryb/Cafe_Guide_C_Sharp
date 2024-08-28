using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class ReviewService : 
    BaseEntityService<App.DAL.DTO.Review, App.BLL.DTO.Review, IReviewRepository>, IReviewService
{
    public ReviewService(IAppUnitOfWork uoW, IReviewRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.Review, App.BLL.DTO.Review>(mapper))
    {
    }

    public async Task<Review?> FirstOrDefaultAsync(Guid id)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id)!);
    }

    public async Task<bool> DeleteReviewAsync(Guid reviewId)
    {

        return (await Repository.DeleteReviewAsync(reviewId));
        
    }

}