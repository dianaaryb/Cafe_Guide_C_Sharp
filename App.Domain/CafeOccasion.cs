using Base.Domain;

namespace App.Domain;

public class CafeOccasion: BaseEntityId
{
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    //FK
    public OccasionOfCafe? OccasionOfCafe { get; set; }
    public Guid OccasionOfCafeId { get; set; }
}