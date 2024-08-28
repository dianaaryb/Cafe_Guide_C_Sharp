using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class CafeCategory: IDomainEntityId
{
    public Guid Id { get; set; }
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    //FK
    public CategoryOfCafe? CategoryOfCafe { get; set; }
    public Guid CategoryOfCafeId { get; set; }
}