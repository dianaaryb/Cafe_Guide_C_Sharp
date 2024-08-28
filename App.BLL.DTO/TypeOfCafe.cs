using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class TypeOfCafe: IDomainEntityId
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? TypeOfCafeName { get; set; }
    [MaxLength(250)]
    public string? TypeOfCafeDescription { get; set; }
    
    public ICollection<CafeType>? CafeTypes { get; set; }
}