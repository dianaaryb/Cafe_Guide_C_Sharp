using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class CafeOccasionService : 
    BaseEntityService<App.DAL.DTO.CafeOccasion, App.BLL.DTO.CafeOccasion, ICafeOccasionRepository>, ICafeOccasionService
{
    public CafeOccasionService(IAppUnitOfWork uoW, ICafeOccasionRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.CafeOccasion, App.BLL.DTO.CafeOccasion>(mapper))
    {
    }

}