using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class OccasionOfCafe: IDomainEntityId
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? OccasionOfCafeName { get; set; }
    [MaxLength(250)]
    public string? OccasionOfCafeDescription { get; set; }
    
    public ICollection<CafeOccasion>? CafeOccasions { get; set; }
}