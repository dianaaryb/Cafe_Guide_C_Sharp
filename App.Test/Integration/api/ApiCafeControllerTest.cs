// using App.BLL.DTO;
// using App.Contracts.BLL;
// using App.DAL.EF;
// using App.Domain.Identity;
// using AutoMapper;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Moq;
// using WebApp.ApiControllers;
//
// namespace App.Test.Integration.api;
//
// public class CafesControllerTests
// {
//     private readonly Mock<IAppBLL> _bllMock;
//     private readonly Mock<UserManager<AppUser>> _userManagerMock;
//     private readonly IMapper _mapper;
//     private readonly CafesController _controller;
//
//     public CafesControllerTests()
//     {
//         _bllMock = new Mock<IAppBLL>();
//         _userManagerMock = MockUserManager<AppUser>();
//         var mapperConfig = new MapperConfiguration(cfg =>
//         {
//             cfg.CreateMap<Cafe, App.DTO.v1_0.Cafe>().ReverseMap();
//         });
//         _mapper = mapperConfig.CreateMapper();
//
//         _controller = new CafesController(
//             new AppDbContext(new DbContextOptions<AppDbContext>()),
//             _bllMock.Object,
//             _userManagerMock.Object,
//             _mapper);
//     }
//
//     // Utility to mock UserManager
//     private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
//     {
//         var store = new Mock<IUserStore<TUser>>();
//         return new Mock<UserManager<TUser>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
//     }
//
//     [Fact]
//     public async Task GetCafes_ReturnsOkResult_WithListOfCafes()
//     {
//         // Arrange
//         var userId = Guid.NewGuid();
//         _userManagerMock.Setup(um => um.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).Returns(userId.ToString());
//
//         var cafes = new List<Cafe> { new Cafe { Id = Guid.NewGuid(), CafeName = "Test Cafe" } };
//         _bllMock.Setup(b => b.Cafes.GetAllSortedAsync(userId)).ReturnsAsync(cafes);
//
//         // Act
//         var result = await _controller.GetCafes();
//
//         // Assert
//         var okResult = Assert.IsType<OkObjectResult>(result.Result);
//         var returnCafes = Assert.IsAssignableFrom<IEnumerable<App.DTO.v1_0.Cafe>>(okResult.Value);
//         Assert.Single(returnCafes);
//     }
//
//
//
//
//     // [Fact]
//     // public async Task GetCafe_ReturnsNotFound_WhenCafeDoesNotExist()
//     // {
//     //     // Arrange
//     //     var cafeId = Guid.NewGuid();
//     //     _bllMock.Setup(b => b.Cafes.FirstOrDefaultAsync(cafeId, It.IsAny<Guid>())).ReturnsAsync((Cafe)null!);
//     //
//     //     // Act
//     //     var result = await _controller.GetCafe(cafeId);
//     //
//     //     // Assert
//     //     Assert.IsType<NotFoundResult>(result.Result);
//     // }
//
//     // [Fact]
//     // public async Task PostCafe_ReturnsCreatedAtAction_WithNewCafe()
//     // {
//     //     // Arrange
//     //     var cafe = new App.DTO.v1_0.Cafe() { CafeName = "New Cafe" };
//     //     var newCafe = new Cafe { Id = Guid.NewGuid(), CafeName = "New Cafe" };
//     //     _bllMock.Setup(b => b.Cafes.Add(It.IsAny<Cafe>())).Returns(newCafe);
//     //
//     //     // Act
//     //     var result = await _controller.PostCafe(cafe);
//     //
//     //     // Assert
//     //     var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
//     //     var returnCafe = Assert.IsType<App.DTO.v1_0.Cafe>(createdAtActionResult.Value);
//     //     Assert.Equal("New Cafe", returnCafe.CafeName);
//     // }
//
//     // [Fact]
//     // public async Task DeleteCafe_ReturnsNoContent_WhenCafeIsDeleted()
//     // {
//     //     // Arrange
//     //     var cafeId = Guid.NewGuid();
//     //     _bllMock.Setup(b => b.Cafes.RemoveAsync(cafeId)).Returns(Task.CompletedTask);
//     //
//     //     // Act
//     //     var result = await _controller.DeleteCafe(cafeId);
//     //
//     //     // Assert
//     //     Assert.IsType<NoContentResult>(result);
//     // }
// }