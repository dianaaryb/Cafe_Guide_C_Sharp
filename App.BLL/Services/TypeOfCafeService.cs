using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class TypeOfCafeService : 
    BaseEntityService<App.DAL.DTO.TypeOfCafe, App.BLL.DTO.TypeOfCafe, ITypeOfCafeRepository>, ITypeOfCafeService
{
    public TypeOfCafeService(IAppUnitOfWork uoW, ITypeOfCafeRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.TypeOfCafe, App.BLL.DTO.TypeOfCafe>(mapper))
    {
    }
    
    public async Task<IEnumerable<App.BLL.DTO.TypeOfCafe>> GetAllForCafeAsync(Guid id)
    {
        return (await Repository.GetAllForCafeAsync(id)).Select(e => Mapper.Map(e));
    }

    public async Task<bool> DeleteTypeAsync(Guid typeId)
    {
        return (await Repository.DeleteTypeAsync(typeId));
    }
}