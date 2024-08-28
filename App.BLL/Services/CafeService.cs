using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class CafeService : 
    BaseEntityService<App.DAL.DTO.Cafe, App.BLL.DTO.Cafe, ICafeRepository>, ICafeService
{
    public CafeService(IAppUnitOfWork uoW, ICafeRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.Cafe, App.BLL.DTO.Cafe>(mapper))
    {
    }

    public async Task<IEnumerable<Cafe>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
    }

    public async Task<IEnumerable<Cafe>> GetAllAsync()
    {
        return (await Repository.GetAllAsync()).Select(e=>Mapper.Map(e));
    }

    public async Task<Cafe?> FirstOrDefaultAsync(Guid id)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id)!);
    }

    // public async Task<Cafe?> FirstOrDefaultAsync()
    // {
    //     return Mapper.Map(await Repository.FirstOrDefaultAsync());
    // }
}
