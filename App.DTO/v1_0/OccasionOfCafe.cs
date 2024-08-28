using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1_0;

public class OccasionOfCafe
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? OccasionOfCafeName { get; set; }
    [MaxLength(250)]
    public string? OccasionOfCafeDescription { get; set; }
    
    public ICollection<App.DTO.v1_0.CafeOccasion>? CafeOccasions { get; set; }
}