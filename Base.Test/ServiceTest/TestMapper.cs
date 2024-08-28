using AutoMapper;
using Base.Contracts.BLL;
using Base.Test.Domain;

namespace Base.Test.ServiceTest;

public class TestMapper : IBLLMapper<TestEntity, TestEntity>
{
    private readonly IMapper _mapper;

    public TestMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TestEntity Map(TestEntity entity)
    {
        return _mapper.Map<TestEntity>(entity);
    }
}