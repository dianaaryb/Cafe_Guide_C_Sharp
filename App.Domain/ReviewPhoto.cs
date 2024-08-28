using Base.Domain;

namespace App.Domain;

public class ReviewPhoto: BaseEntityId
{
    public string? ReviewPhotoLink { get; set; }
    
    //FK
    public Review? Review { get; set; }
    public Guid ReviewId { get; set; }
}