using App.BLL.DTO;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace App.Test.Repositories;

    public class CafeRepositoryTests
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AppDbContext _dbContext;
        private readonly CafeRepository _repository;

        public CafeRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbContext = new AppDbContext(_dbContextOptions);
            _mapperMock = new Mock<IMapper>();

            SeedDatabase();

            _repository = new CafeRepository(_dbContext, _mapperMock.Object);
        }


        private void SeedDatabase()
        {
            var userId = Guid.NewGuid();
            var appUser = new App.Domain.Identity.AppUser { Id = userId };

            var cafes = new List<App.Domain.Cafe>
            {
                new App.Domain.Cafe { CafeName = "A", AppUserId = userId},
                new App.Domain.Cafe { CafeName = "B", AppUserId = userId },
                new App.Domain.Cafe { CafeName = "C", AppUserId = userId }
            };

            _dbContext.Cafes.AddRange(cafes);
            _dbContext.SaveChanges();
        }



        [Fact]
        public async Task GetAllSortedAsync_ShouldReturnMappedCafes()
        {
            // Arrange
            var userId = _dbContext.Cafes.First().AppUserId;
            _mapperMock.Setup(m => m.Map<App.DAL.DTO.Cafe>(It.IsAny<App.Domain.Cafe>()))
                .Returns((App.Domain.Cafe domainCafe) => new App.DAL.DTO.Cafe { Id = domainCafe.Id, CafeName = domainCafe.CafeName, AppUserId = domainCafe.AppUserId });

            // Act
            var result = await _repository.GetAllSortedAsync(userId);

            // Assert
            var cafeList = result.ToList();
            Assert.Equal(3, cafeList.Count());
            Assert.Equal("A", cafeList.ElementAt(0).CafeName);
            Assert.Equal("B", cafeList.ElementAt(1).CafeName);
            Assert.Equal("C", cafeList.ElementAt(2).CafeName);
        }
        
        [Fact]
public async Task GetAllAsync_ShouldReturnMappedCafes()
{
    // Arrange
    var userId = Guid.NewGuid();
    var cafes = new List<App.Domain.Cafe>
    {
        new App.Domain.Cafe 
        { 
            Id = Guid.NewGuid(), 
            CafeName = "C", 
            AppUserId = userId,
            Reviews = new List<App.Domain.Review>(),
            Menus = new List<App.Domain.Menu>(),
            CafeCategories = new List<App.Domain.CafeCategory>(),
            CafeTypes = new List<App.Domain.CafeType>(),
            CafeOccasions = new List<App.Domain.CafeOccasion>(),
            Favourites = new List<App.Domain.Favourite>()
        },
        new App.Domain.Cafe 
        { 
            Id = Guid.NewGuid(), 
            CafeName = "A", 
            AppUserId = userId,
            Reviews = new List<App.Domain.Review>(),
            Menus = new List<App.Domain.Menu>(),
            CafeCategories = new List<App.Domain.CafeCategory>(),
            CafeTypes = new List<App.Domain.CafeType>(),
            CafeOccasions = new List<App.Domain.CafeOccasion>(),
            Favourites = new List<App.Domain.Favourite>()
        },
        new App.Domain.Cafe 
        { 
            Id = Guid.NewGuid(), 
            CafeName = "B", 
            AppUserId = userId,
            Reviews = new List<App.Domain.Review>(),
            Menus = new List<App.Domain.Menu>(),
            CafeCategories = new List<App.Domain.CafeCategory>(),
            CafeTypes = new List<App.Domain.CafeType>(),
            CafeOccasions = new List<App.Domain.CafeOccasion>(),
            Favourites = new List<App.Domain.Favourite>()
        }
    };

    await _dbContext.Cafes.AddRangeAsync(cafes);
    await _dbContext.SaveChangesAsync();

    _mapperMock.Setup(m => m.Map<App.DAL.DTO.Cafe>(It.IsAny<App.Domain.Cafe>()))
        .Returns((App.Domain.Cafe domainCafe) => new App.DAL.DTO.Cafe 
        { 
            Id = domainCafe.Id, 
            CafeName = domainCafe.CafeName, 
            AppUserId = domainCafe.AppUserId,
            Reviews = domainCafe.Reviews?.Select(r => new App.DAL.DTO.Review()).ToList(),
            Menus = domainCafe.Menus?.Select(m => new App.DAL.DTO.Menu()).ToList(),
            CafeCategories = domainCafe.CafeCategories?.Select(cc => new App.DAL.DTO.CafeCategory()).ToList(),
            CafeTypes = domainCafe.CafeTypes?.Select(ct => new App.DAL.DTO.CafeType()).ToList(),
            CafeOccasions = domainCafe.CafeOccasions?.Select(co => new App.DAL.DTO.CafeOccasion()).ToList(),
            Favourites = domainCafe.Favourites?.Select(f => new App.DAL.DTO.Favourite()).ToList()
        });

    // Act
    var result = await _repository.GetAllAsync();

    // Assert
    Assert.Equal(3, result.Count());
    Assert.Contains(result, r => r.CafeName == "A");
    Assert.Contains(result, r => r.CafeName == "B");
    Assert.Contains(result, r => r.CafeName == "C");
}


        
        // [Fact]
        // public async Task GetAllAsync_ShouldReturnMappedBrands()
        // {
        //     // Arrange
        //     _mapperMock.Setup(m => m.Map<App.DAL.DTO.Cafe>(It.IsAny<App.Domain.Cafe>()))
        //         .Returns((App.Domain.Cafe domainBrand) => new App.DAL.DTO.Cafe { Id = domainBrand.Id, CafeName = domainBrand.CafeName, AppUserId = domainBrand.AppUserId });
        //
        //     // Act
        //     var result = await _repository.GetAllAsync();
        //
        //     // Assert
        //     Assert.Equal(3, result.Count());
        //     Assert.Contains(result, r => r.CafeName == "A");
        //     Assert.Contains(result, r => r.CafeName == "B");
        //     Assert.Contains(result, r => r.CafeName == "C");
        // }

        //
        // [Fact]
        // public async Task FirstOrDefaultAsync_ShouldReturnMappedCafe()
        // {
        //     // Arrange
        //     var id = _dbContext.Cafes.First().Id;
        //     var repository = new CafeRepository(_dbContext, _mapperMock.Object);
        //
        //     _mapperMock.Setup(m => m.Map<Cafe>(It.IsAny<Cafe>())).Returns((Cafe domainCafe) =>
        //     {
        //         return new Cafe { Id = domainCafe.Id, CafeName = domainCafe.CafeName };
        //     });
        //
        //     // Act
        //     var result = await repository.FirstOrDefaultAsync(id);
        //
        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.Equal(id, result.Id);
        // }
    }

       
    
