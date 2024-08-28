using Base.Domain;

namespace App.Domain;

public class Menu: BaseEntityId
{
    public string? MenuName { get; set; }

    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    public ICollection<MenuItem>? MenuItems { get; set; }
}