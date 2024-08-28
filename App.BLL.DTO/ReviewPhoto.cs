using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class ReviewPhoto: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public string? ReviewPhotoLink { get; set; }
    
    //FK
    public Review? Review { get; set; }
    public Guid ReviewId { get; set; }
}