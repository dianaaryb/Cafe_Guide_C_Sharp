using Base.Contracts.Domain;

namespace App.DAL.DTO;

public class Menu: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public string? MenuName { get; set; }
    
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    public ICollection<MenuItem>? MenuItems { get; set; }
}