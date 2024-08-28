using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Base.Test.DAL;
using Base.Test.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Base.Test.ServiceTest;

public class BaseServiceTest
{
    private readonly TestDbContext _ctx;
    private readonly TestEntityRepository _testRepo;
    private readonly TestEntityService _testService;
    private readonly IMapper _mapper;
    
    public BaseServiceTest()
    {
        // Set up in-memory database
        var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new TestDbContext(optionsBuilder.Options);

        // Reset database
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        // Configure AutoMapper
        var config = new MapperConfiguration(cfg => cfg.CreateMap<TestEntity, TestEntity>().ReverseMap());
        _mapper = config.CreateMapper();

        // Set up repository
        _testRepo = new TestEntityRepository(_ctx, new TestMapper(_mapper));

        // Mock IUnitOfWork
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Set up service
        _testService = new TestEntityService(unitOfWorkMock.Object, _testRepo, new TestMapper(_mapper));
    }
    
    [Fact]
    public void Add_ShouldReturnMappedEntity()
    {
        // Arrange
        var bllEntity = new TestEntity { Id = Guid.NewGuid(), Value = "Test Value" };

        // Act
        var result = _testService.Add(bllEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bllEntity.Id, result.Id);
        Assert.Equal(bllEntity.Value, result.Value);
    }
    
    [Fact]
    public void Update_ShouldReturnUpdatedEntity()
    {
        // Arrange
        var dalEntity = new TestEntity { Id = Guid.NewGuid(), Value = "Original Value" };
        _ctx.TestEntities.Add(dalEntity);
        _ctx.SaveChanges();
        _ctx.Entry(dalEntity).State = EntityState.Detached; // Detach the original entity
        var bllEntity = new TestEntity { Id = dalEntity.Id, Value = "Updated Value" };

        // Act
        var result = _testService.Update(bllEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bllEntity.Id, result.Id);
        Assert.Equal(bllEntity.Value, result.Value);
    }
    

    
  
    
    [Fact]
    public void FirstOrDefault_ShouldReturnCorrectEntity()
    {
        // Arrange
        var dalEntity = new TestEntity { Id = Guid.NewGuid(), Value = "Test Value" };
        _ctx.TestEntities.Add(dalEntity);
        _ctx.SaveChanges();

        // Act
        var result = _testService.FirstOrDefault(dalEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dalEntity.Id, result.Id);
        Assert.Equal(dalEntity.Value, result.Value);
    }
    
    [Fact]
    public void Exists_ShouldReturnTrue()
    {
        // Arrange
        var dalEntity = new TestEntity { Id = Guid.NewGuid(), Value = "Test Value" };
        _ctx.TestEntities.Add(dalEntity);
        _ctx.SaveChanges();

        // Act
        var result = _testService.Exists(dalEntity.Id);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task FirstOrDefaultAsync_ShouldReturnCorrectEntity()
    {
        // Arrange
        var dalEntity = new TestEntity { Id = Guid.NewGuid(), Value = "Test Value" };
        await _ctx.TestEntities.AddAsync(dalEntity);
        await _ctx.SaveChangesAsync();

        // Act
        var result = await _testService.FirstOrDefaultAsync(dalEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dalEntity.Id, result.Id);
        Assert.Equal(dalEntity.Value, result.Value);
    }
    
    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue()
    {
        // Arrange
        var dalEntity = new TestEntity { Id = Guid.NewGuid(), Value = "Test Value" };
        await _ctx.TestEntities.AddAsync(dalEntity);
        await _ctx.SaveChangesAsync();

        // Act
        var result = await _testService.ExistsAsync(dalEntity.Id);

        // Assert
        Assert.True(result);
    }
    
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedEntities()
    {
        // Arrange
        var dalEntities = new List<TestEntity>
        {
            new TestEntity { Id = Guid.NewGuid(), Value = "Value1" },
            new TestEntity { Id = Guid.NewGuid(), Value = "Value2" }
        };
        await _ctx.TestEntities.AddRangeAsync(dalEntities);
        await _ctx.SaveChangesAsync();

        // Act
        var result = await _testService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.Equal(dalEntities[0].Id, resultList[0].Id);
        Assert.Equal(dalEntities[0].Value, resultList[0].Value);
        Assert.Equal(dalEntities[1].Id, resultList[1].Id);
        Assert.Equal(dalEntities[1].Value, resultList[1].Value);
    }
    
    [Fact]
    public void GetAll_ShouldReturnMappedEntities()
    {
        // Arrange
        var dalEntities = new List<TestEntity>
        {
            new TestEntity { Id = Guid.NewGuid(), Value = "Value1" },
            new TestEntity { Id = Guid.NewGuid(), Value = "Value2" }
        };
        _ctx.TestEntities.AddRange(dalEntities);
        _ctx.SaveChanges();

        // Act
        var result = _testService.GetAll();

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.Equal(dalEntities[0].Id, resultList[0].Id);
        Assert.Equal(dalEntities[0].Value, resultList[0].Value);
        Assert.Equal(dalEntities[1].Id, resultList[1].Id);
        Assert.Equal(dalEntities[1].Value, resultList[1].Value);
    }
}