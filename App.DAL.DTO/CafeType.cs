using Base.Contracts.Domain;

namespace App.DAL.DTO;

public class CafeType: IDomainEntityId
{
    public Guid Id { get; set; }
    
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
    
    //FK
    public TypeOfCafe? TypeOfCafe { get; set; }
    public Guid TypeOfCafeId { get; set; }
}