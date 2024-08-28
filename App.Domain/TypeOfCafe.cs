using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class TypeOfCafe: BaseEntityId
{
    [MaxLength(50)]
    public string? TypeOfCafeName { get; set; }
    [MaxLength(250)]
    public string? TypeOfCafeDescription { get; set; }
    
    public ICollection<CafeType>? CafeTypes { get; set; }
}