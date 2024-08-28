using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class MenuItemCategory: IDomainEntityId
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? MenuItemCategoryName { get; set; }
    
    public ICollection<MenuItem>? MenuItems { get; set; }
}