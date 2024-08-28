using App.Contracts.DAL.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork: IUnitOfWork
{
    //list your repos here
    ICafeCategoryRepository CafeCategories { get; }
    ICafeOccasionRepository CafeOccasions { get; }
    ICafeRepository Cafes { get; }
    ICafeTypeRepository CafeTypes { get; }
    ICategoryOfCafeRepository CategoryOfCafes { get; }
    ICityRepository Cities { get; }
    IFavouriteRepository Favourites { get; }
    IMenuItemCategoryRepository MenuItemCategories { get; }
    IMenuItemRepository MenuItems { get; }
    IMenuRepository Menus { get; }
    IOccasionOfCafeRepository OccasionOfCafes { get; }
    IReviewPhotoRepository ReviewPhotos { get; }
    IReviewRepository Reviews { get; }
    ITypeOfCafeRepository TypeOfCafes { get; }
    IEntityRepository<AppUser> AppUsers { get; }
}