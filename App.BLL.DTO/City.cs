using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class City: IDomainEntityId
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? CityName { get; set; }

    public ICollection<Cafe>? Cafes { get; set; }
}