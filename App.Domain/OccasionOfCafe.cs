using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class OccasionOfCafe: BaseEntityId
{
    [MaxLength(50)]
    public string? OccasionOfCafeName { get; set; }
    [MaxLength(250)]
    public string? OccasionOfCafeDescription { get; set; }
    
    public ICollection<CafeOccasion>? CafeOccasions { get; set; }
}