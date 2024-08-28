using App.BLL.DTO;
using App.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace App.Test.Services;

public class CafeServiceTest
{
    public class CafeServiceTests
    {
        private readonly Mock<ICafeRepository> _cafeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAppUnitOfWork> _uoWMock;
        private readonly CafeService _cafeService;

        public CafeServiceTests()
        {
            _cafeRepositoryMock = new Mock<ICafeRepository>();
            _mapperMock = new Mock<IMapper>();
            _uoWMock = new Mock<IAppUnitOfWork>();
            _cafeService = new CafeService(_uoWMock.Object, _cafeRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllSortedAsync_ShouldReturnMappedCafes()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dalCafes = new List<App.DAL.DTO.Cafe>
            {
                new App.DAL.DTO.Cafe { Id = Guid.NewGuid(), CafeName = "Cafe1" },
                new App.DAL.DTO.Cafe { Id = Guid.NewGuid(), CafeName = "Cafe2" }
            };

            var bllCafes = new List<Cafe>
            {
                new Cafe { Id = dalCafes[0].Id, CafeName = "Cafe1" },
                new Cafe { Id = dalCafes[1].Id, CafeName = "Cafe2" }
            };

            _cafeRepositoryMock.Setup(repo => repo.GetAllSortedAsync(userId)).ReturnsAsync(dalCafes);
            _mapperMock.Setup(m => m.Map<Cafe>(It.IsAny<App.DAL.DTO.Cafe>())).Returns((App.DAL.DTO.Cafe dalCafe) =>
            {
                return bllCafes.First(b => b.Id == dalCafe.Id);
            });

            // Act
            var result = await _cafeService.GetAllSortedAsync(userId);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(bllCafes[0].Id, result.First().Id);
            Assert.Equal(bllCafes[1].Id, result.Last().Id);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedCafes()
        {
            // Arrange
            var dalCafes = new List<App.DAL.DTO.Cafe>
            {
                new App.DAL.DTO.Cafe { Id = Guid.NewGuid(), CafeName = "Cafe1" },
                new App.DAL.DTO.Cafe { Id = Guid.NewGuid(), CafeName = "Cafe2" }
            };

            var bllCafes = new List<Cafe>
            {
                new Cafe { Id = dalCafes[0].Id, CafeName = "Cafe1" },
                new Cafe { Id = dalCafes[1].Id, CafeName = "Cafe2" }
            };

            _cafeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(dalCafes);
            _mapperMock.Setup(m => m.Map<Cafe>(It.IsAny<App.DAL.DTO.Cafe>())).Returns((App.DAL.DTO.Cafe dalCafe) =>
            {
                return bllCafes.First(b => b.Id == dalCafe.Id);
            });

            // Act
            var result = await _cafeService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(bllCafes[0].Id, result.First().Id);
            Assert.Equal(bllCafes[1].Id, result.Last().Id);
        }

        [Fact]
        public async Task FirstOrDefaultAsync_ShouldReturnMappedCafe()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dalCafe = new App.DAL.DTO.Cafe { Id = id, CafeName = "Cafe1" };
            var bllCafe = new Cafe { Id = id, CafeName = "Cafe1" };

            _cafeRepositoryMock.Setup(repo => repo.FirstOrDefaultAsync(id)).ReturnsAsync(dalCafe);
            _mapperMock.Setup(m => m.Map<Cafe>(dalCafe)).Returns(bllCafe);

            // Act
            var result = await _cafeService.FirstOrDefaultAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bllCafe.Id, result.Id);
            Assert.Equal(bllCafe.CafeName, result.CafeName);
        }
    }
}