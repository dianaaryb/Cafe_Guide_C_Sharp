
using App.Domain.Identity;

namespace App.DTO.v1_0;

public class Review
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string? Text { get; set; }
    public DateTime Date { get; set; }
    //FK
    public Guid AppUserId { get; set; }

    //FK
    public Guid CafeId { get; set; }
    
    public ICollection<ReviewPhoto>? ReviewPhotos { get; set; }
}