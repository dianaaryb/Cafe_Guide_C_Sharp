using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1_0;

public class MenuItemCategory
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? MenuItemCategoryName { get; set; }
    
    public ICollection<App.DTO.v1_0.MenuItem>? MenuItems { get; set; }
}