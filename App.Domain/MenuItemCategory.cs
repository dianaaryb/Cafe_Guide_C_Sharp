using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class MenuItemCategory: BaseEntityId
{
    [MaxLength(50)]
    public string? MenuItemCategoryName { get; set; }
    
    public ICollection<MenuItem>? MenuItems { get; set; }
}