using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class CafeCategoryService:
    BaseEntityService<App.DAL.DTO.CafeCategory, App.BLL.DTO.CafeCategory, ICafeCategoryRepository>, ICafeCategoryService

{
    public CafeCategoryService(IAppUnitOfWork uoW, ICafeCategoryRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.CafeCategory, App.BLL.DTO.CafeCategory>(mapper))
    {
    }


}