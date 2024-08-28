using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Repositories;
using App.Domain.Identity;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    private readonly IMapper _mapper;
    public AppUOW(AppDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private ICafeCategoryRepository? _cafeCategoryRepository;
    public ICafeCategoryRepository CafeCategories =>
        _cafeCategoryRepository ?? new CafeCategoryRepository(UowDbContext, _mapper);

    private ICafeOccasionRepository _cafeOccasionRepository = null!;
    public ICafeOccasionRepository CafeOccasions =>
        _cafeOccasionRepository ?? new CafeOccasionRepository(UowDbContext, _mapper);

    private ICafeRepository _cafeRepository = null!;
    public ICafeRepository Cafes => _cafeRepository ?? new CafeRepository(UowDbContext, _mapper);

    private ICafeTypeRepository _cafeTypeRepository = null!;
    public ICafeTypeRepository CafeTypes => _cafeTypeRepository ?? new CafeTypeRepository(UowDbContext, _mapper);

    private ICategoryOfCafeRepository _categoryOfCafeRepository = null!;
    public ICategoryOfCafeRepository CategoryOfCafes =>
        _categoryOfCafeRepository ?? new CategoryOfCafeRepository(UowDbContext, _mapper);

    private ICityRepository _cityRepository = null!;
    public ICityRepository Cities => _cityRepository ?? new CityRepository(UowDbContext, _mapper);

    private IFavouriteRepository _favouriteRepository = null!;
    public IFavouriteRepository Favourites => _favouriteRepository ?? new FavouriteRepository(UowDbContext, _mapper);

    private IMenuItemCategoryRepository _menuItemCategoryRepository = null!;
    public IMenuItemCategoryRepository MenuItemCategories =>
        _menuItemCategoryRepository ?? new MenuItemCategoryRepository(UowDbContext, _mapper);

    private IMenuItemRepository _menuItemRepository = null!;
    public IMenuItemRepository MenuItems => _menuItemRepository ?? new MenuItemRepository(UowDbContext, _mapper);

    private IMenuRepository _menuRepository = null!;
    public IMenuRepository Menus => _menuRepository ?? new MenuRepository(UowDbContext, _mapper);

    private IOccasionOfCafeRepository _occasionOfCafeRepository = null!;
    public IOccasionOfCafeRepository OccasionOfCafes =>
        _occasionOfCafeRepository ?? new OccasionOfCafeRepository(UowDbContext, _mapper);

    private IReviewPhotoRepository _reviewPhotoRepository = null!;
    public IReviewPhotoRepository ReviewPhotos =>
        _reviewPhotoRepository ?? new ReviewPhotoRepository(UowDbContext, _mapper);

    private IReviewRepository _reviewRepository = null!;
    public IReviewRepository Reviews => _reviewRepository ?? new ReviewRepository(UowDbContext, _mapper);

    private ITypeOfCafeRepository _typeOfCafeRepository = null!;

    public ITypeOfCafeRepository TypeOfCafes =>
        _typeOfCafeRepository ?? new TypeOfCafeRepository(UowDbContext, _mapper);

    private IEntityRepository<AppUser> _appUserRepository = null!;
    public IEntityRepository<AppUser> AppUsers => _appUserRepository ??
                                                           new BaseEntityRepository<AppUser, AppUser, AppDbContext>(
                                                               UowDbContext,
                                                               new DalDomainMapper<AppUser, AppUser>(_mapper));
}