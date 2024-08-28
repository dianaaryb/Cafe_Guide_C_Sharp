namespace App.DTO.v1_0;

public class CafeOccasion
{
    public Guid Id { get; set; }
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    //FK
    public OccasionOfCafe? OccasionOfCafe { get; set; }
    public Guid OccasionOfCafeId { get; set; }
}