using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1_0;

public class TypeOfCafe
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? TypeOfCafeName { get; set; }
    [MaxLength(250)]
    public string? TypeOfCafeDescription { get; set; }
    
    public ICollection<App.DTO.v1_0.CafeType>? CafeTypes { get; set; }
}