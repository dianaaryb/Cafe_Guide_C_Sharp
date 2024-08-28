using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class City: BaseEntityId
{
    [MaxLength(50)]
    public string? CityName { get; set; }

    public ICollection<Cafe>? Cafes { get; set; }
}