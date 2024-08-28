using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1_0;

public class CategoryOfCafe
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? CategoryOfCafeName { get; set; }
    [MaxLength(250)]
    public string? CategoryOfCafeDescription{get; set; }
    
    public ICollection<App.DTO.v1_0.CafeCategory>? CafeCategories { get; set; }
}