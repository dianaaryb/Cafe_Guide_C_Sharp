using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DTO.v1_0;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class MenuService : 
    BaseEntityService<App.DAL.DTO.Menu, App.BLL.DTO.Menu, IMenuRepository>, IMenuService
{
    public MenuService(IAppUnitOfWork uoW, IMenuRepository repository, IMapper mapper) : 
        base(uoW, repository, new BllDalMapper<App.DAL.DTO.Menu, App.BLL.DTO.Menu>(mapper))
    {
        Console.WriteLine("MenuService instantiated"); 
    }
    
    public async Task<IEnumerable<App.BLL.DTO.Menu>> GetAllAsync()
    {
        return (await Repository.GetAllAsync()).Select(e=>Mapper.Map(e));
    }
    
    public async Task<IEnumerable<App.BLL.DTO.Menu>> GetAllAsync(Guid cafeId)
    {
        try
        {
            Console.WriteLine($"Service: Received cafeId: {cafeId}");  // Logging
            var menus = await Repository.GetAllAsync(cafeId);
            Console.WriteLine($"Service: Retrieved {menus.Count()} menus for cafeId: {cafeId}");  // Logging
            return menus.Select(e => Mapper.Map(e));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Service: Exception occurred - {ex.Message}");
            throw;
        }
        // Console.WriteLine($"Service: Received cafeId: {cafeId}");  
        // return (await Repository.GetAllAsync(cafeId)).Select(e=>Mapper.Map(e));
    }
    
    public async Task<App.BLL.DTO.Menu?> FirstOrDefaultAsync(Guid id)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id)!);
    }

}