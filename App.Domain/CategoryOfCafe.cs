using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using Base.Domain;

namespace App.Domain;

public class CategoryOfCafe: BaseEntityId
{
    [MaxLength(50)]
    public string? CategoryOfCafeName { get; set; }
    [MaxLength(250)]
    public string? CategoryOfCafeDescription{get; set; }
    
    public ICollection<CafeCategory>? CafeCategories { get; set; }
}