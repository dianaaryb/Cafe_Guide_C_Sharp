using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class FavouriteService : 
    BaseEntityService<App.DAL.DTO.Favourite, App.BLL.DTO.Favourite, IFavouriteRepository>, IFavouriteService
{
    public FavouriteService(IAppUnitOfWork uoW, IFavouriteRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.Favourite, App.BLL.DTO.Favourite>(mapper))
    {
    }

}