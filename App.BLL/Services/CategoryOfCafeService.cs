using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class CategoryOfCafeService : 
    BaseEntityService<App.DAL.DTO.CategoryOfCafe, App.BLL.DTO.CategoryOfCafe, ICategoryOfCafeRepository>, ICategoryOfCafeService
{
    public CategoryOfCafeService(IAppUnitOfWork uoW, ICategoryOfCafeRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.CategoryOfCafe, App.BLL.DTO.CategoryOfCafe>(mapper))
    {
    }
    
    public async Task<IEnumerable<App.BLL.DTO.CategoryOfCafe>> GetAllForCafeAsync(Guid id)
    {
        return (await Repository.GetAllForCafeAsync(id)).Select(e => Mapper.Map(e));
    }

    public async Task<bool> DeleteCategoryAsync(Guid categoryId)
    {
        return (await Repository.DeleteCategoryAsync(categoryId));
    }
}