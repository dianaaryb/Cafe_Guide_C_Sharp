using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1_0;

public class City
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? CityName { get; set; }

    public ICollection<App.DTO.v1_0.Cafe>? Cafes { get; set; }
}