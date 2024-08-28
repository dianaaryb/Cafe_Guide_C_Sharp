using Base.BLL;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Base.Test.Domain;

namespace Base.Test.ServiceTest;

public class TestEntityService : BaseEntityService<TestEntity, TestEntity, IEntityRepository<TestEntity, Guid>>
{
    public TestEntityService(IUnitOfWork uoW, IEntityRepository<TestEntity, Guid> repository, IBLLMapper<TestEntity, TestEntity> mapper)
        : base(uoW, repository, mapper)
    {
    }
}