using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DTO.v1_0;

public class MenuItem
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? MenuItemName { get; set; }
    [MaxLength(250)]
    public string? MenuItemDescription { get; set; }
    [Column(TypeName = "decimal(5, 2)")]
    public decimal MenuItemPrice { get; set; }
    public string? PhotoLink { get; set; }
    
    
    //FK
    public MenuItemCategory? MenuItemCategory { get; set; }
    public Guid MenuItemCategoryId { get; set; }
    
    //FK
    public Menu? Menu { get; set; }
    public Guid MenuId { get; set; }
}