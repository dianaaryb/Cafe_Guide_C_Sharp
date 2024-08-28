namespace App.DTO.v1_0;

public class ReviewPhoto
{
    public Guid Id { get; set; }
    
    public string? ReviewPhotoLink { get; set; }
    
    //FK
    public Review? Review { get; set; }
    public Guid ReviewId { get; set; }
}