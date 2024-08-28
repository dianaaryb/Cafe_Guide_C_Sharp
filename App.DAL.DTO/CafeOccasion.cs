using Base.Contracts.Domain;

namespace App.DAL.DTO;

public class CafeOccasion: IDomainEntityId
{
    public Guid Id { get; set; }
    
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    //FK
    public OccasionOfCafe? OccasionOfCafe { get; set; }
    public Guid OccasionOfCafeId { get; set; }
}