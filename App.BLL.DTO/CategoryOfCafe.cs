using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class CategoryOfCafe: IDomainEntityId
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? CategoryOfCafeName { get; set; }
    [MaxLength(250)]
    public string? CategoryOfCafeDescription{get; set; }
    
    public ICollection<CafeCategory>? CafeCategories { get; set; }
}