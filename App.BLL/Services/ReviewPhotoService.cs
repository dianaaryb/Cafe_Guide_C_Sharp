using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class ReviewPhotoService : 
    BaseEntityService<App.DAL.DTO.ReviewPhoto, App.BLL.DTO.ReviewPhoto, IReviewPhotoRepository>, IReviewPhotoService
{
    public ReviewPhotoService(IAppUnitOfWork uoW, IReviewPhotoRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.ReviewPhoto, App.BLL.DTO.ReviewPhoto>(mapper))
    {
    }

    public async Task<IEnumerable<ReviewPhoto>> GetAllAsync()
    {
        return (await Repository.GetAllAsync()).Select(e=>Mapper.Map(e));
    }

    public async Task<IEnumerable<ReviewPhoto>> GetAllAsync(Guid id)
    {
        return (await Repository.GetAllAsync(id)).Select(e=>Mapper.Map(e));
    }

    public async Task<ReviewPhoto?> FirstOrDefaultAsync(Guid id)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id)!);
    }
}