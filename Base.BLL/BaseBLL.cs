using App.BLL.DTO;
using App.Domain.Identity;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Base.BLL;

public abstract class BaseBLL<TAppDbContext> : IBLL
    where TAppDbContext : DbContext
{
    protected readonly IUnitOfWork UoW;

    protected BaseBLL(IUnitOfWork uow)
    {
        UoW = uow;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await UoW.SaveChangesAsync(); 
    }
}