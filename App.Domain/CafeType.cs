using Base.Domain;

namespace App.Domain;

public class CafeType: BaseEntityId
{
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    //FK
    public TypeOfCafe? TypeOfCafe { get; set; }
    public Guid TypeOfCafeId { get; set; }
}