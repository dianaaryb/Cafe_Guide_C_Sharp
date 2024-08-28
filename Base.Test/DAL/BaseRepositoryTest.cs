using AutoMapper;
using Base.DAL.EF;
using Base.Test.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.Test.DAL;

public class BaseRepositoryTest
{
    private readonly TestDbContext _ctx;
    private readonly TestEntityRepository _testRepo; 
    
    
    public BaseRepositoryTest()
    {
        // set up mock database - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new TestDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        var config = new MapperConfiguration(cfg => cfg.CreateMap<TestEntity, TestEntity>());
        var mapper = config.CreateMapper();
        
        _testRepo = new TestEntityRepository(_ctx, new BaseDalDomainMapper<TestEntity, TestEntity>(mapper));
    }
    
    
    [Fact]
    public async Task TestAddEntity()
    {
        // Arrange
        var entity = _testRepo.Add(new TestEntity { Value = "Foo" });
        _ctx.ChangeTracker.Clear();

        // Act
        var data = await _testRepo.GetAllAsync();

        // Assert
        var datum = data.ToList();
        Assert.Single(datum);
        Assert.Equivalent(entity, datum[0]);
    }
    
    [Fact]
    public void TestUpdateEntity()
    {
        // Arrange
        var entity = _testRepo.Add(new TestEntity { Value = "Foo" });
        _ctx.ChangeTracker.Clear();

        // Act
        entity.Value = "Bar";
        var updated = _testRepo.Update(entity);

        // Assert
        Assert.Equal("Bar", updated.Value);
    }
    
    [Fact]
    public void TestExists()
    {
        // Arrange
        var entity = _testRepo.Add(new TestEntity { Value = "Foo" });
        _ctx.ChangeTracker.Clear();
        
        // Act
        var entityExists = _testRepo.Exists(entity.Id);

        // Assert
        Assert.True(entityExists);
    }
    
    [Fact]
    public async Task TestExistsAsync()
    {
        // Arrange
        var entity = _testRepo.Add(new TestEntity { Value = "Foo" });
        _ctx.ChangeTracker.Clear();
        
        // Act
        var entityExists = await _testRepo.ExistsAsync(entity.Id);

        // Assert
        Assert.True(entityExists);
    }
    
    [Fact]
    public void GetAll_ShouldReturnMappedEntities()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new TestEntity { Id = Guid.NewGuid(), Value = "Entity1" },
            new TestEntity { Id = Guid.NewGuid(), Value = "Entity2" }
        };
        _ctx.TestEntities.AddRange(entities);
        _ctx.SaveChanges();

        // Act
        var result = _testRepo.GetAll().ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Entity1", result[0].Value);
        Assert.Equal("Entity2", result[1].Value);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedEntities()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new TestEntity { Id = Guid.NewGuid(), Value = "Entity1" },
            new TestEntity { Id = Guid.NewGuid(), Value = "Entity2" }
        };
        await _ctx.TestEntities.AddRangeAsync(entities);
        await _ctx.SaveChangesAsync();

        // Act
        var result = await _testRepo.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        var testEntities = result.ToList();
        Assert.Equal(2, testEntities.Count);
        Assert.Equal("Entity1", testEntities[0].Value);
        Assert.Equal("Entity2", testEntities[1].Value);
    }
    
    
    [Fact]
    public void FirstOrDefault_ShouldReturnMappedEntity()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var entity = new TestEntity { Id = entityId, Value = "Entity1" };
        _ctx.TestEntities.Add(entity);
        _ctx.SaveChanges();

        // Act
        var result = _testRepo.FirstOrDefault(entityId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(entityId, result.Id);
        Assert.Equal("Entity1", result.Value);
    }

    [Fact]
    public async Task FirstOrDefaultAsync_ShouldReturnMappedEntity()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var entity = new TestEntity { Id = entityId, Value = "Entity1" };
        await _ctx.TestEntities.AddAsync(entity);
        await _ctx.SaveChangesAsync();

        // Act
        var result = await _testRepo.FirstOrDefaultAsync(entityId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(entityId, result.Id);
        Assert.Equal("Entity1", result.Value);
    }
    
    // [Fact]
    // public async Task TestRemoveAsyncByEntity()
    // {
    //     // Arrange
    //     var entity = _testRepo.Add(new TestEntity { Value = "Foo" });
    //     _ctx.ChangeTracker.Clear();
    //
    //     // Act
    //     var removed = await _testRepo.RemoveAsync(entity);
    //     var data = await _testRepo.GetAllAsync();
    //
    //     // Assert
    //     Assert.StrictEqual(1, removed);
    //     Assert.Empty(data);
    // }
    
    
    
}