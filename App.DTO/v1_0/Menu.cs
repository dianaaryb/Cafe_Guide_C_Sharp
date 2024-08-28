namespace App.DTO.v1_0;

public class Menu
{
    public Guid Id { get; set; }
    public string? MenuName { get; set; }
    
    public Guid CafeId { get; set; }
    
    public ICollection<App.DTO.v1_0.MenuItem>? MenuItems { get; set; }
}