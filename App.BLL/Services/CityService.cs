using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class CityService : 
    BaseEntityService<App.DAL.DTO.City, App.BLL.DTO.City, ICityRepository>, ICityService
{
    public CityService(IAppUnitOfWork uoW, ICityRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.City, App.BLL.DTO.City>(mapper))
    {
    }

}