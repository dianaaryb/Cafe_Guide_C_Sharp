using App.Domain;
using App.Domain.Identity;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Review: IDomainEntityId
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string? Text { get; set; }
    public DateTime Date { get; set; }
    
    //FK
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    public ICollection<ReviewPhoto>? ReviewPhotos { get; set; }
}