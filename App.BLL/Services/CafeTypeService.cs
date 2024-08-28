using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class CafeTypeService : 
    BaseEntityService<App.DAL.DTO.CafeType, App.BLL.DTO.CafeType, ICafeTypeRepository>, ICafeTypeService
{
    public CafeTypeService(IAppUnitOfWork uoW, ICafeTypeRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.CafeType, App.BLL.DTO.CafeType>(mapper))
    {
    }

}