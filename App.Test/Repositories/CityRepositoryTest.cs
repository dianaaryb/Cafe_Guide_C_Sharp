// using App.DAL.EF;
// using App.DAL.EF.Repositories;
// using App.Domain;
// using AutoMapper;
// using Base.Contracts.Domain;
// using Microsoft.EntityFrameworkCore;
// using Moq;
//
// namespace App.Test.Repositories;
//
// public class CityRepositoryTests
// {
//     private readonly DbContextOptions<AppDbContext> _options;
//     private readonly IMapper _mapper;
//     private readonly Mock<IDomainAppUserId<Guid>> _mockUser;
//
//     public CityRepositoryTests()
//     {
//         _options = new DbContextOptionsBuilder<AppDbContext>()
//             .UseInMemoryDatabase(databaseName: "TestDb")
//             .Options;
//
//         var mockMapper = new MapperConfiguration(cfg =>
//         {
//             cfg.CreateMap<City, App.DAL.DTO.City>().ReverseMap();
//         });
//         _mapper = mockMapper.CreateMapper();
//
//         _mockUser = new Mock<IDomainAppUserId<Guid>>();
//     }
//
//     [Fact]
//     public async Task GetAllCities_ReturnsAllCities_WhenNoUserId()
//     {
//         // Arrange
//         using var context = new AppDbContext(_options);
//         var repository = new CityRepository(context, _mapper);
//         context.Cities.AddRange(
//             new City { Id = Guid.NewGuid(), CityName = "City1" },
//             new City { Id = Guid.NewGuid(), CityName = "City2" }
//         );
//         await context.SaveChangesAsync();
//
//         // Act
//         var cities = await repository.GetAllAsync();
//
//         // Assert
//         Assert.Equal(2, cities.Count());
//     }
//
//     // [Fact]
//     // public async Task GetAllCities_ReturnsFilteredCities_WhenUserIdProvided()
//     // {
//     //     // Arrange
//     //     using var context = new AppDbContext(_options);
//     //     var repository = new CityRepository(context, _mapper);
//     //     var userId = Guid.NewGuid();
//     //
//     //     var cityWithUserId = new City { Id = Guid.NewGuid(), CityName = "CityWithUserId", AppUser = userId };
//     //     var cityWithoutUserId = new City { Id = Guid.NewGuid(), CityName = "CityWithoutUserId" };
//     //
//     //     context.Cities.AddRange(cityWithUserId, cityWithoutUserId);
//     //     await context.SaveChangesAsync();
//     //
//     //     // Act
//     //     var cities = await repository.GetAllCities(userId);
//     //
//     //     // Assert
//     //     Assert.Single(cities);
//     //     Assert.Equal(cityWithUserId.Name, cities.First().Name);
//     // }
//
//     [Fact]
//     public async Task GetAllCities_NoTrackingWorks()
//     {
//         // Arrange
//         using var context = new AppDbContext(_options);
//         var repository = new CityRepository(context, _mapper);
//         var city = new City { Id = Guid.NewGuid(), CityName = "City" };
//
//         context.Cities.Add(city);
//         await context.SaveChangesAsync();
//
//         // Act
//         var queriedCity = (await repository.GetAllAsync(noTracking: true)).FirstOrDefault(c => c.Id == city.Id);
//
//         // Modify the entity to ensure it's not being tracked
//         if (queriedCity != null)
//         {
//             queriedCity.CityName = "ModifiedCity";
//         }
//
//         var unchangedCity = await context.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.Id == city.Id);
//
//         // Assert
//         Assert.Equal("City", unchangedCity?.CityName);  // Should not be "ModifiedCity"
//     }
// }