namespace App.DTO.v1_0;

public class CafeCategory
{
    public Guid Id { get; set; }
    
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    //FK
    public CategoryOfCafe? CategoryOfCafe { get; set; }
    public Guid CategoryOfCafeId { get; set; }
}