using App.Contracts.BLL.Services;
using App.Domain.Identity;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    ICafeService Cafes { get;}
    ICafeCategoryService CafeCategories {get;}
    ICafeOccasionService CafeOccasions { get; }
    ICafeTypeService CafeTypes { get; }
    ICategoryOfCafeService CategoryOfCafes { get; }
    ICityService Cities { get; }
    IFavouriteService Favourites { get; }
    IMenuItemCategoryService MenuItemCategories { get; }
    IMenuItemService MenuItems { get; }
    IMenuService Menus { get; }
    IOccasionOfCafeService OccasionOfCafes { get; }
    IReviewPhotoService ReviewPhotos { get; }
    IReviewService Reviews { get; }
    ITypeOfCafeService TypeOfCafes { get; }
    IEntityService<AppUser> AppUsers { get; }
}