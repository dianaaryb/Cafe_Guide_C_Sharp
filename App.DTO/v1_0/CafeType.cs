namespace App.DTO.v1_0;

public class CafeType
{
    public Guid Id { get; set; }
    
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    //FK
    public TypeOfCafe? TypeOfCafe { get; set; }
    public Guid TypeOfCafeId { get; set; }
}