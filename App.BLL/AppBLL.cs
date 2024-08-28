using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain.Identity;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace App.BLL;

public class AppBLL : BaseBLL<AppDbContext>, IAppBLL
{
    private readonly IMapper _mapper;
    private readonly IAppUnitOfWork _uow;
   
    
    public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
    {
        _uow = uow;
        _mapper = mapper;
    }

    private ICafeService _cafes = null!;
    public ICafeService Cafes => _cafes ?? new CafeService(_uow, _uow.Cafes, _mapper);
    
    
    private ICafeCategoryService _cafeCategories = null!;
    public ICafeCategoryService CafeCategories => _cafeCategories ?? new CafeCategoryService(_uow, _uow.CafeCategories, _mapper);

    
    private ICafeOccasionService _cafeOccasions = null!;
    public ICafeOccasionService CafeOccasions => _cafeOccasions ?? new CafeOccasionService(_uow, _uow.CafeOccasions, _mapper);

    private ICafeTypeService _cafeTypes = null!;
    public ICafeTypeService CafeTypes => _cafeTypes ?? new CafeTypeService(_uow, _uow.CafeTypes, _mapper);


    private ICategoryOfCafeService _categoryOfCafes = null!;
    public ICategoryOfCafeService CategoryOfCafes => _categoryOfCafes ?? new CategoryOfCafeService(_uow, _uow.CategoryOfCafes, _mapper);


    private ICityService _cities = null!;
    public ICityService Cities => _cities ?? new CityService(_uow, _uow.Cities, _mapper);


    private IFavouriteService _favourites = null!;
    public IFavouriteService Favourites => _favourites ?? new FavouriteService(_uow, _uow.Favourites, _mapper);


    private IMenuItemCategoryService _menuItemCategories = null!;
    public IMenuItemCategoryService MenuItemCategories =>_menuItemCategories ?? new MenuItemCategoryService(_uow, _uow.MenuItemCategories, _mapper);

    private IMenuItemService _menuItems = null!;
    public IMenuItemService MenuItems => _menuItems ?? new MenuItemService(_uow, _uow.MenuItems, _mapper);


    private IMenuService _menus = null!;
    public IMenuService Menus => _menus ?? new MenuService(_uow, _uow.Menus, _mapper);


    private IOccasionOfCafeService _occasionOfCafes = null!;
    public IOccasionOfCafeService OccasionOfCafes => _occasionOfCafes ?? new OccasionOfCafeService(_uow, _uow.OccasionOfCafes, _mapper);


    private IReviewPhotoService _reviewPhotos = null!;
    public IReviewPhotoService ReviewPhotos => _reviewPhotos ?? new ReviewPhotoService(_uow, _uow.ReviewPhotos, _mapper);


    private IReviewService _reviews = null!;
    public IReviewService Reviews => _reviews ?? new ReviewService(_uow, _uow.Reviews, _mapper);


    private ITypeOfCafeService _typeOfCafes = null!;
    public ITypeOfCafeService TypeOfCafes => _typeOfCafes ?? new TypeOfCafeService(_uow, _uow.TypeOfCafes, _mapper);


    private IEntityService<AppUser> _appUsers = null!;
    public IEntityService<AppUser> AppUsers => _appUsers ?? new BaseEntityService<AppUser, AppUser, IEntityRepository<AppUser>>(
        _uow, _uow.AppUsers, new BllDalMapper<AppUser, AppUser>(_mapper));
}