using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class OccasionOfCafeService : 
    BaseEntityService<App.DAL.DTO.OccasionOfCafe, App.BLL.DTO.OccasionOfCafe, IOccasionOfCafeRepository>, IOccasionOfCafeService
{
    public OccasionOfCafeService(IAppUnitOfWork uoW, IOccasionOfCafeRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.OccasionOfCafe, App.BLL.DTO.OccasionOfCafe>(mapper))
    {
    }


    public async Task<IEnumerable<OccasionOfCafe>> GetAllForCafeAsync(Guid id)
    {
        return (await Repository.GetAllForCafeAsync(id)).Select(e => Mapper.Map(e));
    }

    public async Task<bool> DeleteOccasionAsync(Guid occasionId)
    {
        return (await Repository.DeleteOccasionAsync(occasionId));
    }
}