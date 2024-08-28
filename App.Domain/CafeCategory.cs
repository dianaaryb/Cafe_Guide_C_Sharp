using Base.Domain;

namespace App.Domain;

public class CafeCategory: BaseEntityId
{
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    //FK
    public CategoryOfCafe? CategoryOfCafe { get; set; }
    public Guid CategoryOfCafeId { get; set; }
}